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

        public ServiceResponse? GetService(Guid serviceId, bool excludeUnavailable = false)
        {
            Service? service = bookSmartContext.SERVICES
                .FirstOrDefault(s => s.SERVICE_ID == serviceId && !s.SERVICE_DELETED);

            if (service == null) return null;

            bool isAvailable = IsServiceAvailable(serviceId);

            if (excludeUnavailable && !isAvailable) return null;

            return MapToResponse(service, isAvailable);
        }

        public List<ServiceResponse> GetServicesByBusiness(Guid businessId, bool excludeUnavailable = false)
        {
            List<Service> services = bookSmartContext.SERVICES
                .Where(s => s.SERVICE_BUSINESSID == businessId && !s.SERVICE_DELETED)
                .ToList();

            List<Guid> serviceIds = services.Select(s => s.SERVICE_ID).ToList();
            HashSet<Guid> availableIds = GetAvailableServiceIds(serviceIds);

            return services
                .Where(s => !excludeUnavailable || availableIds.Contains(s.SERVICE_ID))
                .Select(s => MapToResponse(s, availableIds.Contains(s.SERVICE_ID)))
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

        private bool IsServiceAvailable(Guid serviceId)
        {
            return bookSmartContext.SERVICESCHEDULES
                .Any(ss => ss.SERVICESCHEDULE_SERVICEID == serviceId
                        && !ss.SERVICESCHEDULE_DELETED
                        && !ss.SERVICESCHEDULE_SCHEDULE.SCHEDULE_DELETED
                        && ss.SERVICESCHEDULE_SCHEDULE.SCHEDULE_ACTIVE)
                ||
                bookSmartContext.SERVICESCHEDULEOVERRIDES
                .Any(sso => sso.SERVICESCHEDULEOVERRIDE_SERVICEID == serviceId
                         && !sso.SERVICESCHEDULEOVERRIDE_DELETED
                         && !sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDE.SCHEDULEOVERRIDE_DELETED
                         && sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDE.SCHEDULEOVERRIDE_ISAVAILABLE);
        }

        private HashSet<Guid> GetAvailableServiceIds(List<Guid> serviceIds)
        {
            HashSet<Guid> result = bookSmartContext.SERVICESCHEDULES
                .Where(ss => serviceIds.Contains(ss.SERVICESCHEDULE_SERVICEID)
                          && !ss.SERVICESCHEDULE_DELETED
                          && !ss.SERVICESCHEDULE_SCHEDULE.SCHEDULE_DELETED
                          && ss.SERVICESCHEDULE_SCHEDULE.SCHEDULE_ACTIVE)
                .Select(ss => ss.SERVICESCHEDULE_SERVICEID)
                .ToHashSet();

            result.UnionWith(bookSmartContext.SERVICESCHEDULEOVERRIDES
                .Where(sso => serviceIds.Contains(sso.SERVICESCHEDULEOVERRIDE_SERVICEID)
                           && !sso.SERVICESCHEDULEOVERRIDE_DELETED
                           && !sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDE.SCHEDULEOVERRIDE_DELETED
                           && sso.SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDE.SCHEDULEOVERRIDE_ISAVAILABLE)
                .Select(sso => sso.SERVICESCHEDULEOVERRIDE_SERVICEID)
                .ToHashSet());

            return result;
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