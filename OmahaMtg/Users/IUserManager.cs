using System;

namespace OmahaMtg.Users
{
    public interface IUserManager
    {
        string GetUserEmail(Guid userId);
        string GetUserFullname(Guid userId);
    }
}