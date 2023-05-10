using LayuiAdminNetCore.AuthorizationModels;
using LayuiAdminNetGate.IServices;

namespace LayuiAdminNetGate.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        public bool IsAuthenticated(Authenticate request, out string assessToken)
        {
            throw new NotImplementedException();
        }
    }
}