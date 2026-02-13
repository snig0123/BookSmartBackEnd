using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;

namespace BookSmartBackEnd.BusinessLogic.Interfaces
{
    public interface IServiceBll
    {
        void CreateService(PostServiceModel data);
        ServiceResponse? GetService(Guid serviceId);
        List<ServiceResponse> GetServicesByBusiness(Guid businessId);
        void UpdateService(Guid serviceId, PostServiceModel data);
        void DeleteService(Guid serviceId);
    }
}