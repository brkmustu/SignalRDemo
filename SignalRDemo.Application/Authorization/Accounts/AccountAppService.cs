using AutoMapper;
using Microsoft.Extensions.Options;
using SignalRDemo.Application;
using SignalRDemo.Authorization.Roles;
using SignalRDemo.Authorization.Users;
using SignalRDemo.DataAccess;
using SignalRDemo.Extensions;

namespace SignalRDemo.Authorization.Accounts
{
    public class AccountAppService : IAccountAppService
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IGenericRepository<Permission> _permissionRepository;
        private readonly IMapper _mapper;
        private readonly TokenOptions _tokenOptions;

        public AccountAppService(
                IGenericRepository<User> repository, 
                IGenericRepository<Role> roleRepository, 
                IGenericRepository<Permission> permissionRepository,
                IMapper mapper,
                IOptions<TokenOptions> tokenOptions
            )
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _mapper = mapper;
            _tokenOptions = tokenOptions.Value;
        }

        public async Task<Result> RegisterAsync(RegisterRequestDto request)
        {
            var currentAccount = (await _repository.GetAllAsync()).FirstOrDefault(x => x.EmailAddress == request.EmailAddress);

            if (currentAccount == null)
            {
                var user = _mapper.Map<User>(request);

                var encryptedPassword = request.Password.CreatePasswordHash();
                user.PasswordHash = encryptedPassword.PasswordHash;
                user.PasswordSalt = encryptedPassword.PasswordSalt;

                //user.RoleIds

                await _repository.InsertAsync(user);

                return Result.Success("Kullanıcı kayıt işlemi başarıyla tamamlanmıştır.");
            }

            return Result.Failure(new string[] { "Bu e posta adresi ile kayıt mevcut olduğundan işleminiz gerçekleştirilemiyor. Şifrenizi mi unuttunuz?" });
        }

        public async Task<Result<AccessToken>> SignInAsync(SignInRequestDto request)
        {
            var user = (await _repository.GetAllAsync()).FirstOrDefault(x => x.EmailAddress == request.EmailAddress);

            if (user == null)
                return await Task.FromResult(Result.Failure<AccessToken>(new[] { "Kullanıcı bulunamadığından login işlemi gerçekleştirilemedi!" }));

            if (user.RoleIds == null || user.RoleIds.Length == 0)
                return await Task.FromResult(Result.Failure<AccessToken>(new[] { "Kullanıcı herhangi bir yetkiye sahip olmadığından login işlemi gerçekleştirilemedi!" }));

            if (request.Password.VerifyPasswordHash(user.PasswordSalt, user.PasswordHash))
            {
                List<int> permissionIds = new List<int>();
                List<Permission> permissions = new List<Permission>();

                foreach (var roleId in user.RoleIds)
                {
                    var rolePermissionIds = (await _roleRepository.GetAllAsync()).FirstOrDefault(x => x.Id == roleId).PermissionIds;
                    if (rolePermissionIds != null && rolePermissionIds.Length > 0)
                    {
                        permissionIds.AddRange(rolePermissionIds);
                    }
                }
                foreach (var permissionId in permissionIds)
                {
                    var permission = (await _permissionRepository.GetAllAsync()).FirstOrDefault(x => x.Id == permissionId);
                    if (permission != null)
                    {
                        permissions.Add(permission);
                    }
                }

                var token = user.CreateToken(permissions.Select(x => x.Name), _tokenOptions);

                return await Task.FromResult(Result.Success(token));
            }

            return await Task.FromResult(Result.Failure<AccessToken>(new[] { "Şifre eşleştirilemediğinden login işlemi gerçekleştirilemedi!" }));
        }
    }
}
