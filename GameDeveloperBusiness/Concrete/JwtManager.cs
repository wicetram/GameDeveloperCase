using GameDeveloperBusiness.Abstract;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameDeveloperBusiness.Concrete
{
    public class JwtManager<T> : IJwtService<T>
    {
        private readonly string secretKey;
        private readonly string issuer;
        private readonly string audience;
        private readonly double expirationMinutes;

        public JwtManager()
        {
            // app.config dosyasından ayarları çekecek şekilde güncelle
            secretKey = "JwtSecretKeyGameDeveloper";
            issuer = "JwtIssuerGameDeveloper";
            audience = "JwtAudienceGameDeveloper";
            expirationMinutes = 10;
        }

        public string GenerateToken(T payload)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var payloadJson = JsonConvert.SerializeObject(payload);

            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, payloadJson),
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ValidateToken(string token, out T payload)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                SecurityToken validatedToken;
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                var payloadJson = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                if (!string.IsNullOrEmpty(payloadJson))
                {
                    payload = JsonConvert.DeserializeObject<T>(payloadJson);
                    return true;
                }

                payload = default(T);
                return false;
            }
            catch
            {
                payload = default(T);
                return false;
            }
        }
    }
}