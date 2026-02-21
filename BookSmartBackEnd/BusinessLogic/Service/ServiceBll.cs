using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;
using BookSmartBackEndDatabase.Models;
using BookSmartBackEndDatabase.Repositories;

namespace BookSmartBackEnd.BusinessLogic
{
    internal sealed class ServiceBll(IUserRepository userRepository, IServiceRepository serviceRepository) : IServiceBll
    {
        public void CreateService(PostServiceModel data)
        {
            User user = userRepository.GetStaffUser(data.StaffUserId);

            Service service = new Service
            {
                SERVICE_ID = Guid.NewGuid(),
                SERVICE_NAME = data.Name,
                SERVICE_DESCRIPTION = data.Description,
                SERVICE_DURATION = data.Duration,
                SERVICE_PRICE = data.Price,
                SERVICE_CAPACITY = data.Capacity,
                SERVICE_BUSINESSID = user.BUSINESS_ID,
                SERVICE_ACTIVE = true,
                SERVICE_CREATED = DateTime.UtcNow,
                SERVICE_UPDATED = DateTime.UtcNow,
                SERVICE_DELETED = false
            };

            serviceRepository.Add(service);
        }

        public ServiceResponse? GetService(Guid serviceId, bool excludeUnavailable = false)
        {
            Service? service = serviceRepository.GetById(serviceId);

            if (service == null) return null;

            bool isAvailable = serviceRepository.IsAvailable(serviceId);

            if (excludeUnavailable && !isAvailable) return null;

            return MapToResponse(service, isAvailable);
        }

        public List<ServiceResponse> GetServicesByBusiness(Guid businessId, bool excludeUnavailable = false)
        {
            List<Service> services = serviceRepository.GetByBusiness(businessId);

            List<Guid> serviceIds = services.Select(s => s.SERVICE_ID).ToList();
            HashSet<Guid> availableIds = serviceRepository.GetAvailableIds(serviceIds);

            return services
                .Where(s => !excludeUnavailable || availableIds.Contains(s.SERVICE_ID))
                .Select(s => MapToResponse(s, availableIds.Contains(s.SERVICE_ID)))
                .ToList();
        }

        public void UpdateService(Guid serviceId, PostServiceModel data)
        {
            Service service = serviceRepository.GetById(serviceId)
                ?? throw new ArgumentException("Service not found.");

            service.SERVICE_NAME = data.Name;
            service.SERVICE_DESCRIPTION = data.Description;
            service.SERVICE_DURATION = data.Duration;
            service.SERVICE_PRICE = data.Price;
            service.SERVICE_CAPACITY = data.Capacity;
            service.SERVICE_UPDATED = DateTime.UtcNow;

            serviceRepository.Update(service);
        }

        public void DeleteService(Guid serviceId)
        {
            Service service = serviceRepository.GetById(serviceId)
                ?? throw new ArgumentException("Service not found.");

            service.SERVICE_DELETED = true;
            service.SERVICE_ACTIVE = false;
            service.SERVICE_UPDATED = DateTime.UtcNow;

            serviceRepository.Update(service);
        }

        private static ServiceResponse MapToResponse(Service service, bool isAvailable)
        {
            return new ServiceResponse
            {
                ServiceId = service.SERVICE_ID,
                Name = service.SERVICE_NAME,
                Description = service.SERVICE_DESCRIPTION,
                Duration = service.SERVICE_DURATION,
                Price = service.SERVICE_PRICE,
                Capacity = service.SERVICE_CAPACITY,
                Active = service.SERVICE_ACTIVE,
                IsAvailable = isAvailable
            };
        }
    }
}
