using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    public class User : IdentityUser/*<int>*/, IEntity
    {
        public User()
        {
            // مقداردهی اولیه دیتابیس
            // بطور پیش فرض کاربر ما فعال است
            IsActive = true;
            //SecurityStamp = Guid.NewGuid().ToString(); // خود Identity مقدار میده
        }

        //[Required]
        //[StringLength(100)]
        //public string UserName { get; set; }
        //[Required]
        //[StringLength(500)]
        //public string PasswordHash { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }
        //public int? City { get; set; }
        //public int? Age { get; set; }
        //public EducationType? Education { get; set; }
        //public GenderType? Gender { get; set; }
        //public MarialType? Marial { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }

        //public Guid SecurityStamp { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }


        public virtual ICollection<Education_1> Education_1s { get; set; }
        public virtual ICollection<Education_2> Education_2s { get; set; }
        public virtual ICollection<Education_3> Education_3s { get; set; }
        public virtual ICollection<Education_4> Education_4s { get; set; }
        public virtual ICollection<Education_5> Education_5s { get; set; }
        public virtual ICollection<Education_6> Education_6s { get; set; }
        public virtual ICollection<Education_7> Education_7s { get; set; }
        public virtual ICollection<Education_8> Education_8s { get; set; }
        public virtual ICollection<Education_9> Education_9s { get; set; }
        public virtual ICollection<Education_10> Education_10s { get; set; }
        public virtual ICollection<Education_11> Education_11s { get; set; }
        public virtual ICollection<Education_12> Education_12s { get; set; }
        //***************************************
        public virtual ICollection<IT_1> IT_1s { get; set; }
        public virtual ICollection<IT_2> IT_2s { get; set; }
        public virtual ICollection<IT_3> IT_3s { get; set; }
        public virtual ICollection<IT_4> IT_4s { get; set; }
        public virtual ICollection<IT_5> IT_5s { get; set; }
        public virtual ICollection<IT_6> IT_6s { get; set; }
        public virtual ICollection<IT_7> IT_7s { get; set; }
        //********************************
        public virtual ICollection<Research_1> Research_1s { get; set; }
        public virtual ICollection<Research_2> Research_2s { get; set; }
        public virtual ICollection<Research_3> Research_3s { get; set; }
        public virtual ICollection<Research_4> Research_4s { get; set; }
        public virtual ICollection<Research_5> Research_5s { get; set; }
        public virtual ICollection<Research_6> Research_6s { get; set; }
        //***************************************
        public virtual ICollection<Resource_1> Resource_1s { get; set; }
        public virtual ICollection<Resource_2> Resource_2s { get; set; }
        public virtual ICollection<Resource_3> Resource_3s { get; set; }
        public virtual ICollection<Resource_4> Resource_4s { get; set; }
        public virtual ICollection<Resource_5> Resource_5s { get; set; }
        public virtual ICollection<Resource_6> Resource_6s { get; set; }
        public virtual ICollection<Resource_7> Resource_7s { get; set; }
        //***************************************
        public virtual ICollection<Student_1> Student_1s { get; set; }
        public virtual ICollection<Student_2> Student_2s { get; set; }
        public virtual ICollection<Student_3> Student_3s { get; set; }
        public virtual ICollection<Student_4> Student_4s { get; set; }
        public virtual ICollection<Student_5> Student_5s { get; set; }
        public virtual ICollection<Student_6> Student_6s { get; set; }
        public virtual ICollection<Student_7> Student_7s { get; set; }
        public virtual ICollection<Student_8> Student_8s { get; set; }

        public virtual ICollection<Reminder> Reminders { get; set; }
        public virtual ICollection<UserReport> UserReports { get; set; }
    }
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(100);

            // builder.HasMany(e => e.Quality_21s)
            //.WithOne()
            //.HasForeignKey(uc => uc.RegistereUser)
            //.IsRequired();
       


            // Each User can have many UserClaims
            builder.HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

            // Each User can have many UserLogins
            builder.HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(ul => ul.UserId)
                .IsRequired();

            // Each User can have many UserTokens
            builder.HasMany(e => e.Tokens)
                .WithOne()
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }

}