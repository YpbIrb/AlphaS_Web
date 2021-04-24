
namespace AlphaS_Web.Services.ApiAuthentication
{
    using Microsoft.IdentityModel.Tokens;
    public interface IJwtSigningDecodingKey
    {
        SecurityKey GetKey();
    }
}
