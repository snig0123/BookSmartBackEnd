using BookSmartBackEndDatabase.Models;

namespace BookSmartBackEndDatabase.Repositories;

public interface IClientRepository
{
    List<User> GetAll();
    User GetById(Guid clientId);
    void Update(User user);
    void SoftDelete(User user);
}
