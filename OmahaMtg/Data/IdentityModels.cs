using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OmahaMtg.Data
{
    public class UserRole : IdentityUserRole<Guid>
    {
    }

    public class UserClaim : IdentityUserClaim<Guid>
    {
    }

    public class UserLogin : IdentityUserLogin<Guid>
    {
    }

    public class Role : IdentityRole<Guid, UserRole>
    {
        public Role() { }
        public Role(string name) { Name = name; }
    }

    class UserStoreIntPk : UserStore<User, Role, Guid,
        UserLogin, UserRole, UserClaim>
    {
        public UserStoreIntPk(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    class RoleStoreIntPk : RoleStore<Role, Guid, UserRole>
    {
        public RoleStoreIntPk(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}