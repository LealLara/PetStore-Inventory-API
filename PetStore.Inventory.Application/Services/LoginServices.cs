using PetStore.Inventory.Application.BusinessDTOs.Requests;
using PetStore.Inventory.Application.Interfaces.Services;

namespace PetStore.Inventory.Application.Services
{
    public class LoginServices : ILoginServices
    {
        public Task<string> Login(LoginRegisterRequest data)
        {


            /* User? user = await _userRepository.GetByNicknameAsync(nick);
             if (user == null)
             {
                 return new Success(
                 successFlag: false,
                 message: Messages.UserNotFound,
                 logId: 0,
                 data: new List<object>()
             );
             }
             string hash = await _userRepository.GetHash(nick);

             if (hash == null || !BCrypt.Net.BCrypt.Verify(password, hash))
                 throw new Exception(Messages.InvalidCredentials);

             string token = await _tokenService.GenerateToken(hash);

             if (string.IsNullOrEmpty(token))
                 throw new Exception(Messages.TokenGenerationError);

             LogEntity logBody = new(logMessage: $"{Messages.UserLoggedIn} {nick}",
                          logTypeId: (int)ELogType.Login,
                          userId: user.UserId
             );

             Log? log = await _logRepository.AddLog(logBody);

             return new Success(true, $"{Messages.UserLoggedIn} {nick}", log.LogId, new List<object> { token });
         }*/

        }

        public Task<bool> Logoff(int userId)
        {
            throw new NotImplementedException();
        }
    }
}