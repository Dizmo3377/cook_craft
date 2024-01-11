using Microsoft.AspNetCore.Identity;

namespace Cook_Craft.Data;

public class UserRoles : IdentityRole
{
    public const string Admin = "admin";
    public const string User = "user";
}