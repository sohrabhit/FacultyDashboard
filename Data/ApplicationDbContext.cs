using Common.Utilities;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<
        User, Role, string, // کلیدهای جداول ما رشته ای هست
        IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>// DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }

        // یا اینجا باید پروایدر رو مشخص کرد یا داخل کلاس Startup
        // پروژه اصلی
        // این متد سازنده بالا رو نمی خواد ولی
        // کلاس داخل Startup سازنده بالا رو می خواد
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MyApiDb;Integrated Security=true");
        //    base.OnConfiguring(optionsBuilder);
        //}

        /// <summary>
        /// با این متد دیگه نیازی به معرفی تک تک جداول با IDbset نیست
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entitiesAssembly = typeof(IEntity).Assembly;
            // معرفی کلاس های جدول
            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
            // معرفی کلاسهای Configuration
            modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
            // تا وقتی بچه ها حذف نشده والد حذف نشه
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            // بجای خط بالا میشه تک تک اینجوری مقار داد
            //modelBuilder.Entity<Post>().Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            modelBuilder.AddSequentialGuidForIdConvention();
            // جداول رو با نام جمع ایجاد کنه
            // تو ورژنهای قبلی خودبخود اینکار رو میکرد
            modelBuilder.AddPluralizingTableNameConvention();
        }

        public override int SaveChanges()
        {
            _cleanString();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _cleanString();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            _cleanString();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _cleanString();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// کار این متد اینه محتوایی که داره درج یا ویرایش میشه رو عربی و فارسیش رو درست کنه
        /// و باید قبل از SaveChanges
        /// صدا زده بشه
        /// </summary>
        private void _cleanString()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var item in changedEntities)
            {
                // Entity : همون کلاس ما هست که قراره اد یا ویرایش بشه
                if (item.Entity == null)
                    continue;

                // در اینجا با Reflection
                // کلیه پراپرتیهای یه کلاس رو استخراج میکنیم
                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string)); // اونایی که رشته ای هستند

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var val = (string)property.GetValue(item.Entity, null);

                    if (val.HasValue()) // از متدهای StringExtention در Utilities
                    {
                        var newVal = val.Fa2En().FixPersianChars();
                        if (newVal == val)
                            continue;
                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }
    }
}
