using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NasGrad.DBEngine;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NasGrad.API.Auth
{
    public static class AuthUtil
    {
        public static bool ValidatePassword(string userSalt, string userPasswordHash, string password)
        {
            try
            {
                var hash = CryptoUtil.GenerateHash(userSalt + password);
                return string.Equals(hash, userPasswordHash);
            }
            catch (Exception)
            {
                //TODO: log error here in future...
            }

            return false;
        }

        public static ClaimsIdentity GenerateClaimsIdentity(string username, string userId, NasGradRole role)
        {
            var claimsList = new [] 
            {
                new Claim(Constants.AuthorizationId, userId),
                new Claim(string.Format(Constants.AuthClaimFormat, Constants.AuthorizationRole, role.Type),role.Id)
            };

            return new ClaimsIdentity(new GenericIdentity(username, "Token"), claimsList);
        }


        public static string EncodeJWTToken(ClaimsIdentity ci, JwtOptions options)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, ci.Name),
                new Claim(JwtRegisteredClaimNames.Jti, options.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, options.IssuedAt.ToUnixEpochDate().ToString(),
                    ClaimValueTypes.Integer64),
                ci.FindFirst(Constants.AuthorizationId)
            };
            claims.AddRange(ci.FindAll(c => c.Type.StartsWith(Constants.AuthorizationRole, StringComparison.OrdinalIgnoreCase)));

            var jwt = new JwtSecurityToken(options.Issuer, null, claims, options.NotBefore, options.Expiration,
                options.SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private static long ToUnixEpochDate(this DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() -
                                     new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
        }

        public class JwtOptions
        {
            public JwtOptions(int validityMinutes)
            {
                ValidFor = TimeSpan.FromMinutes(validityMinutes);
            }

            /// <summary>
            /// 4.1.1.  "iss" (Issuer) Claim - The "iss" (issuer) claim identifies the principal that issued the JWT.
            /// </summary>
            public string Issuer { get; set; }

            /// <summary>
            /// 4.1.2.  "sub" (Subject) Claim - The "sub" (subject) claim identifies the principal that is the subject of the JWT.
            /// </summary>
            public string Subject { get; set; }

            /// <summary>
            /// 4.1.4.  "exp" (Expiration Time) Claim - The "exp" (expiration time) claim identifies the expiration time on or after which the JWT MUST NOT be accepted for processing.
            /// </summary>
            public DateTime Expiration => IssuedAt.Add(ValidFor);

            /// <summary>
            /// 4.1.5.  "nbf" (Not Before) Claim - The "nbf" (not before) claim identifies the time before which the JWT MUST NOT be accepted for processing.
            /// </summary>
            public DateTime NotBefore => DateTime.UtcNow;

            /// <summary>
            /// 4.1.6.  "iat" (Issued At) Claim - The "iat" (issued at) claim identifies the time at which the JWT was issued.
            /// </summary>
            public DateTime IssuedAt => DateTime.UtcNow;

            /// <summary>
            /// Set the timespan the token will be valid for (default is 120 min)
            /// </summary>
            public TimeSpan ValidFor { get; }

            /// <summary>
            /// "jti" (JWT ID) Claim (default ID is a GUID)
            /// </summary>
            public Func<string> JtiGenerator =>
                () => Guid.NewGuid().ToString();

            /// <summary>
            /// The signing key to use when generating tokens.
            /// </summary>
            public SigningCredentials SigningCredentials { get; set; }
        }


        
    }
}
