using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Constants;
using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;
using BookSmartBackEndDatabase;
using BookSmartBackEndDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSmartBackEnd.BusinessLogic
{
    internal sealed class ServiceBll(BookSmartContext bookSmartContext) : IServiceBll
    {
        public void CreateService(PostServiceModel data)
        {
            User user = bookSmartContext.USERS
                .Include(u => u.USER_ROLES)
                .FirstOrDefault(u => u.USER_ID == data.StaffUserId)
                ?? throw new ArgumentException("User not found.");

            bool isStaff = user.USER_ROLES.Any(r => r.ROLE_ROLETYPEID == RoleTypes.STAFF);
            if (!isStaff)
            {
                throw new ArgumentException("User does not have the Staff role.");
            }

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

            bookSmartContext.SERVICES.Add(service);
            bookSmartContext.SaveChanges();
        }

        public ServiceResponse? GetService(Guid serviceId)
        {
            Service? service = bookSmartContext.SERVICES
                .FirstOrDefault(s => s.SERVICE_ID == serviceId && !s.SERVICE_DELETED);

            if (service == null) return null;

            return MapToResponse(service);
        }

        public List<ServiceResponse> GetServicesByBusiness(Guid businessId)
        {
            return bookSmartContext.SERVICES
                .Where(s => s.SERVICE_BUSINESSID == businessId && !s.SERVICE_DELETED)
                .Select(s => MapToResponse(s))
                .ToList();
        }

        public void UpdateService(Guid serviceId, PostServiceModel data)
        {
            Service service = bookSmartContext.SERVICES
                .FirstOrDefault(s => s.SERVICE_ID == serviceId && !s.SERVICE_DELETED)
                ?? throw new ArgumentException("Service not found.");

            bookSmartContext.SERVICES.Entry(service).State = EntityState.Modified;

            service.SERVICE_NAME = data.Name;
            service.SERVICE_DESCRIPTION = data.Description;
            service.SERVICE_DURATION = data.Duration;
            service.SERVICE_PRICE = data.Price;
            service.SERVICE_CAPACITY = data.Capacity;
            service.SERVICE_UPDATED = DateTime.UtcNow;

            bookSmartContext.SaveChanges();
        }

        public void DeleteService(Guid serviceId)
        {
            Service service = bookSmartContext.SERVICES
                .FirstOrDefault(s => s.SERVICE_ID == serviceId && !s.SERVICE_DELETED)
                ?? throw new ArgumentException("Service not found.");

            bookSmartContext.SERVICES.Entry(service).State = EntityState.Modified;

            service.SERVICE_DELETED = true;
            service.SERVICE_ACTIVE = false;
            service.SERVICE_UPDATED = DateTime.UtcNow;

            bookSmartContext.SaveChanges();
        }

        private static ServiceResponse MapToResponse(Service service)
        {
            return new ServiceResponse
            {
                ServiceId = service.SERVICE_ID,
                Name = service.SERVICE_NAME,
                Description = service.SERVICE_DESCRIPTION,
                Duration = service.SERVICE_DURATION,
                Price = service.SERVICE_PRICE,
                Capacity = service.SERVICE_CAPACITY,
                Active = service.SERVICE_ACTIVE
            };
        }
    }
}