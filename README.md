# SignalRDemo

SignalR ile client ve server side iletişimi realtime olarak sağlayacak bir uygulama örneğidir. 

- Client uygulaması için vuejs,
- Back-end için asp.net 6 web api,
- Genel uygulama mimarisi için DDD ve clean architect,
- Orm aracı olarak Dapper,
- Loglama için Serilog (burada şimdilik sadece debug modda output verecek şekilde bir düzenleme yapıldı),
- Back-end uygulamada DI container için stashbox,
- Kimlik doğrulama ve yetki kontrolü için Jwt token,

baz alınarak ele alınmaya çalışılmıştır.

## Back-end uygulama için;

- Kimlik doğrulama ve yetki kontrolü
- Aspect orianted programming için örnek olması adına hata yönetimi için "ExceptionHandlingInterceptor" adında bir "interceptor" örneği yapıldı.
