using Common.Utilities;
using Data.Repositories;
using Entities;
using System.Linq;

namespace Services.DataInitializer
{
    public class UserDataInitializer : IDataInitializer
    {
        private readonly IUserRepository userRepository;

        public UserDataInitializer(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void InitializeData()
        {
            if (!userRepository.TableNoTracking.Any(p => p.UserName == "Admin"))
            {
                var passwordHash = SecurityHelper.GetSha256Hash("123456");
                userRepository.Add(new User
                {
                    UserName = "admin",
                    NormalizedUserName = "admin", // اگر این نباشه نمیشناسه
                    Email = "admin@site.com",
                    NormalizedEmail = "admin@site.com",
                    PhoneNumberConfirmed = false,
                    PasswordHash = passwordHash, // jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    EmailConfirmed = false,
                    IsActive = true,
                    TwoFactorEnabled = false, 
                    //Age = 2,
                    FirstName = "مهدی",
                    LastName = "کرمی",
                    //City = 1,
                    //Gender = GenderType.Male
                });
            }
        }
    }
}
