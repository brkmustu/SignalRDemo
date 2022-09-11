# SignalRDemo

SignalR ile client ve server side iletişimi realtime olarak sağlayacak bir uygulama örneğidir. 

- Client uygulaması için vuejs,
- Back-end için asp.net 6 web api,
- Genel uygulama mimarisi için DDD ve clean architect,
- Api Gateway olarak Ocelot,
- Orm aracı olarak Dapper (EntityFrameworkCore da kullanıldı ancak bu code first ile uygulama ayağa kalktığında tabloları veritabanında otomatik oluşturmak amacıyla kullanıldı),
- Loglama için Serilog (burada şimdilik sadece debug modda output verecek şekilde bir düzenleme yapıldı),
- Back-end uygulamada DI container için stashbox,
- Kimlik doğrulama ve yetki kontrolü için Jwt token,
- Veritabanı olarak Postgresql,

baz alınarak ele alınmaya çalışılmıştır.

## Back-end uygulama için;

- Kimlik doğrulama ve yetki kontrolü
- Aspect orianted programming için örnek olması adına hata yönetimi için "ExceptionHandlingInterceptor" adında bir "interceptor" örneği yapıldı.

## Uygulamayı kendi yerel bilgisayarınızda ayağa kaldırmak için;

- Uygulama ilk ayağa kalktığında entityframeworkcore code first yardımıyla tabloları oluşturuyor, sonrasında da varsayılan demo datalarını bu tablolara otomatik olarak ekliyor, bu kısım için herhangi bir işlem yapılmasına gerek yok,
- Uygulama bir apigateway kullandığı için "Solution Explorer"'dan solution'a sağ tıkladıktan sonra "Set Startup Projects..." seçeneğini tıklayarak "SignalRDemo.ApiGateway" ve "SignalRDemo.WebAPI" projelerini seçmelisiniz, yani bu iki uygulama birlikte ayağa kalkmalı,
- Back-end tarafı ayağa kaldırdıktan sonra "client" uygulaması için ilk olarak "yarn" komutuyla paketleri yüklemeslisiniz, sonrasında "yarn dev" ile "client" uygulamasını ayağa kaldırmalısınız ve hepsi bu kadar :) http://localhost:3000 üzerinden uygulamaya erişebilir hale geleceksiniz.

## Uygulama kullanımı için;

- Varsayılan olarak admin kullanıcısı email adresi "admin@admin.com" olarak tanımlı. şifresi de yine admin. Bu bilgilerle giriş yapabilirsiniz.
- Giriş yaptıktan sonra uygulama ana ekranı sizi karşılayacak, burada iki adet araç var. Bunlardan hangisi üzerinde iken sağ üstteki "Satın Al" butonuna tıklarsanız onu "Orders" tablosuna ekleyecektir. Bu eklenen kayıtları da "Satın Alınanlar" butonu ile yönlendirilen ekranda görüntüleyebilirsiniz.
- Gerçek zamanlı iletişimi ekran üzerinden deneyimleyebilmek için ana ekran bir sekmede açık iken başka bir sekmede "ürün ayarları" butonu yardımıyla açılan ekrana geçiş yapmalısınız, sonrasında burada "Arka Planı Ayarla" butonuna tıkladığınızda diğer ekranda seçili olan aracın arka plan resminin değiştiğini gözlemliyor olabilmesiniz.

Önemli Not: Uygulamayı yerel bilgisayarınızda çalıştırıp postgresql üzerinde tabloların oluştuğunu gözlemledikten sonra (ki burada WebApi projesindeki connection string'e göz atmanızda fayda var) pgAdmin yardımıyla veritabanına bağlanıp [şuradaki blogda](https://www.graymatterdeveloper.com/2019/12/02/listening-events-postgresql/) bulunan sql script'lerini veritabanınızda çalıştırmanız gerekiyor. Aksi takdirde realtime süreçleri çalışmayacaktır.

Eğer link'e erişemiyosanız çalıştırmanız gereken script'i aşağıya ekliyorum;

```
CREATE FUNCTION public."NotifyOnDataChange"()
  RETURNS trigger
  LANGUAGE 'plpgsql'
AS $BODY$ 
DECLARE 
  data JSON;
  notification JSON;
BEGIN
  -- if we delete, then pass the old data
  -- if we insert or update, pass the new data
  IF (TG_OP = 'DELETE') THEN
    data = row_to_json(OLD);
  ELSE
    data = row_to_json(NEW);
  END IF;

  -- create json payload
  -- note that here can be done projection 
  notification = json_build_object(
            'table',TG_TABLE_NAME,
            'action', TG_OP, -- can have value of INSERT, UPDATE, DELETE
            'data', data);  
            
    -- note that channel name MUST be lowercase, otherwise pg_notify() won't work
    PERFORM pg_notify('datachange', notification::TEXT);
  RETURN NEW;
END
$BODY$;
```

sonrasında da bu notification'ın "cars" tablomuzdan trigger ile çağırılması için;

```
CREATE TRIGGER "OnDataChange"
  AFTER INSERT OR DELETE OR UPDATE 
  ON public.Cars
  FOR EACH ROW
  EXECUTE PROCEDURE public."NotifyOnDataChange"();
```