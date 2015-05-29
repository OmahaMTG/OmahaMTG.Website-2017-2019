using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using OmahaMtg.Data;

namespace OmahaMtg
{
    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<User, Guid>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }

        public override async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            var user = await UserManager.FindByNameAsync(userName);
            if (user == null)
                user = await UserManager.FindByEmailAsync(userName);

            if (user == null)
                return SignInStatus.Failure;

            var result = await base.PasswordSignInAsync(user.UserName, password, isPersistent, shouldLockout);
            
            if (result != SignInStatus.Failure)
                return result;

            //if they did not succede in logging in, try them using the membership way - if they succede at that way, go ahead and convert thier password to the new way.
            Guid securityStamp;
            if (!Guid.TryParse(user.SecurityStamp, out securityStamp) && user.PasswordHash == EncodePassword(password, user.SecurityStamp))
            {
                await UserManager.RemovePasswordAsync(user.Id);
                var token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var identResult = await UserManager.ResetPasswordAsync(user.Id, token, password);
                await UserManager.UpdateSecurityStampAsync(user.Id);

                var result2 = await base.PasswordSignInAsync(user.UserName, password, isPersistent, shouldLockout);
                return result2;
            }
            return SignInStatus.Failure;
        }

        public string EncodePassword(string pass, string salt)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(pass);
            //byte[] src = Encoding.Unicode.GetBytes(salt); Corrected 5/15/2013
            byte[] src = Convert.FromBase64String(salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        } 
    }
}
