using LayuiAdminNetCore.AuthorizationModels;

namespace LayuiAdminNetGate.IServices
{
    public interface IAuthenticateService
    {
        /// <summary>
        /// Admin 后台 生成 AccessToken 接口
        /// </summary>
        /// <param name="request"></param>
        /// <param name="assessToken"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        bool IsAuthenticated(Authenticate request, out string assessToken);
    }
}