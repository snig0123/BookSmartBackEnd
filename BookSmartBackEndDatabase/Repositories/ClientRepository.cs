using BookSmartBackEndDatabase.Constants;
using BookSmartBackEndDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSmartBackEndDatabase.Repositories;

public class ClientRepository(BookSmartContext context) : IClientRepository
{
    public List<User> GetAll()
    {
        return context.USERS
            .Include(u => u.USER_ROLES)
            .Where(u => u.USER_ROLES.Any(r => r.ROLE_ROLETYPEID == RoleTypes.CLIENT) && !u.USER_DELETED)
            .ToList();
    }

    public User GetById(Guid clientId)
    {
        User user = context.USERS
            .Include(u => u.USER_ROLES)
            .FirstOrDefault(u => u.USER_ID == clientId && !u.USER_DELETED)
            ?? throw new ArgumentException("Client not found.");

        bool isClient = user.USER_ROLES.Any(r => r.ROLE_ROLETYPEID == RoleTypes.CLIENT);
        if (!isClient)
            throw new ArgumentException("User is not a client.");

        return user;
    }

    public void Update(User user)
    {
        context.Entry(user).State = EntityState.Modified;
        context.SaveChanges();
    }

    public void SoftDelete(User user)
    {
        user.USER_DELETED = true;
        user.USER_UPDATED = DateTime.UtcNow;
        context.Entry(user).State = EntityState.Modified;
        context.SaveChanges();
    }
}
