using BookSmartBackEndDatabase.Models;

namespace BookSmartBackEndDatabase.Repositories;

public interface IServiceRepository
{
    Service? GetById(Guid serviceId);
    List<Service> GetByBusiness(Guid businessId);
    void Add(Service service);
    void Update(Service service);
    bool IsAvailable(Guid serviceId);
    HashSet<Guid> GetAvailableIds(List<Guid> serviceIds);
}
