using BookSmartBackEndDatabase.Constants;
using BookSmartBackEndDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSmartBackEndDatabase.Repositories;

public class UserRepository(BookSmartContext context) : IUserRepository
{
    public User? GetByIdWithRoles(Guid userId)
    {
        return context.USERS
            .Include(u => u.USER_ROLES)
            .FirstOrDefault(u => u.USER_ID == userId);
    }

    public User? GetByIdWithRolesAndRoleTypes(Guid userId)
    {
        return context.USERS
            .Include(u => u.USER_ROLES)
            .ThenInclude(r => r.ROLE_ROLETYPE)
            .FirstOrDefault(u => u.USER_ID == userId);
    }

    public User? GetByEmailAndPasswordWithRoles(string email, string password)
    {
        return context.USERS
            .Include(u => u.USER_ROLES)
            .ThenInclude(r => r.ROLE_ROLETYPE)
            .FirstOrDefault(u => u.USER_EMAIL == email && u.USER_PASSWORD == password);
    }

    public User GetStaffUser(Guid userId)
    {
        User user = context.USERS
            .Include(u => u.USER_ROLES)
            .FirstOrDefault(u => u.USER_ID == userId)
            ?? throw new ArgumentException("User not found.");

        bool isStaff = user.USER_ROLES.Any(r => r.ROLE_ROLETYPEID == RoleTypes.STAFF);
        if (!isStaff)
        {
            throw new ArgumentException("User does not have the Staff role.");
        }

        return user;
    }

    public void Create(User user)
    {
        context.Add(user);
        context.SaveChanges();
    }
}
