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
        private readonly IMapper _mapper;
        private readonly TokenOptions _tokenOptions;

        public AccountAppService(
                IGenericRepository<User> repository,
                IGenericRepository<Role> roleRepository,
                IMapper mapper,
                IOptions<TokenOptions> tokenOptions
            )
        {
            _repository = repository;
            _roleRepository = roleRepository;
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

        public async Task<Result<TokenResult>> SignInAsync(SignInRequestDto request)
        {
            var user = (await _repository.GetAllAsync()).FirstOrDefault(x => x.EmailAddress == request.EmailAddress);

            if (user == null)
                return await Task.FromResult(Result.Failure<TokenResult>(new[] { "Kullanıcı bulunamadığından login işlemi gerçekleştirilemedi!" }));

            if (user.RoleIds == null || user.RoleIds.Length == 0)
                return await Task.FromResult(Result.Failure<TokenResult>(new[] { "Kullanıcı herhangi bir yetkiye sahip olmadığından login işlemi gerçekleştirilemedi!" }));

            if (request.Password.VerifyPasswordHash(user.PasswordSalt, user.PasswordHash))
            {
                List<string> roles = new List<string>();

                foreach (var roleId in user.RoleIds)
                {
                    var role = (await _roleRepository.GetAllAsync()).FirstOrDefault(x => x.Id == roleId);
                    if (role != null)
                    {
                        roles.Add(role.Name);
                    }
                }

                var token = user.CreateToken(roles, _tokenOptions);

                token.Roles = roles.ToArray();

                return await Task.FromResult(Result.Success(token));
            }

            return await Task.FromResult(Result.Failure<TokenResult>(new[] { "Şifre eşleştirilemediğinden login işlemi gerçekleştirilemedi!" }));
        }
    }
}
