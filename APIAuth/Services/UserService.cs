using APIAuth.Models;

namespace APIAuth.Services;

public class UserService
{
    public bool ValidateUser(UserViewModel userViewModel)
    {
        return userViewModel is { UserName: "admin", Password: "1010" };
    }
}