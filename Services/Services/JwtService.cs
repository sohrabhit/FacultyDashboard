using Common;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class JwtService : IJwtService, IScopedDependency
    {
        private readonly SiteSettings _siteSetting;
        private readonly SignInManager<User> signInManager;

        // IOptionsSnapshot : برای دریافت مقادیر configuration
        public JwtService(IOptionsSnapshot<SiteSettings> settings, SignInManager<User> signInManager)
        {
            _siteSetting = settings.Value;
            this.signInManager = signInManager;
        }

        public async Task<AccessToken> GenerateAsync(User user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.SecretKey); // longer that 16 character
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            // رمزنگاری از طریق قرار دادن کلید داخل سرور
            var encryptionkey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.EncryptKey); //must be 16 character
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var claims = await _getClaimsAsync(user);

            // Descriptor : یعنی کلاسهای توضیح دهنده
            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _siteSetting.JwtSettings.Issuer, // صادر کننده توکن
                Audience = _siteSetting.JwtSettings.Audience, // مصرف کننده توکن
                IssuedAt = DateTime.Now, // توکن چه زمانی صادر شده
                NotBefore = DateTime.Now.AddMinutes(_siteSetting.JwtSettings.NotBeforeMinutes),//ازچه زمانی به بعد قابل استفاده باشه و قبل اون قابل استفاده نباشه
                Expires = DateTime.Now.AddMinutes(_siteSetting.JwtSettings.ExpirationMinutes),//تاریخ انقضای توکن برای امنیت
                SigningCredentials = signingCredentials, // امضای توکن که مجموع هش شده هدر و payload هست
                EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(claims) // مشخصات کاربر تحت Claim داده میشه
            };

            // برای مپ کردن کلایم معمولی و کلایم jwt
            // تبدیل نوع claim ها اتفاق نمیفته
            // اینکار سلیقه ای هست و مزیتی ندارد
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            //JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);

            string encryptedJwt = tokenHandler.WriteToken(securityToken);

            return new  AccessToken(securityToken);
        }

        private async Task<IEnumerable<Claim>> _getClaimsAsync(User user)
        {
            // 1- Integrate Jwt with SignManager
            var result = await signInManager.ClaimsFactory.CreateAsync(user);
            //add custom claims
            var list = new List<Claim>(result.Claims);
            //list.Add(new Claim(ClaimTypes.MobilePhone, "09123456987"));
            return list;


            // 2- روش jwt خالی
            //JwtRegisteredClaimNames.Sub  // claim های مخصوص jwt
            /*var securityStampClaimType = new ClaimsIdentityOptions().SecurityStampClaimType;//افزودن SecurityStamp

            var list = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.MobilePhone, "09123456987"),
                new Claim(securityStampClaimType, user.SecurityStamp.ToString())
            };

            var roles = new Role[] { new Role { Name = "Admin" } };
            foreach (var role in roles)
                list.Add(new Claim(ClaimTypes.Role, role.Name));

            return list;*/
        }
    }
}
