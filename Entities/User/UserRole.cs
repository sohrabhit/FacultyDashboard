using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class UserRole : IdentityUserRole<string>, IEntity
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
