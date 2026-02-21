using BookSmartBackEndDatabase.Models;

namespace BookSmartBackEndDatabase.Repositories;

public interface IUserRepository
{
    User? GetByIdWithRoles(Guid userId);
    User? GetByIdWithRolesAndRoleTypes(Guid userId);
    User? GetByEmailAndPasswordWithRoles(string email, string password);
    User GetStaffUser(Guid userId);
    void Create(User user);
}
