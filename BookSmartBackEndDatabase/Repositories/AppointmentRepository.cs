using BookSmartBackEndDatabase.Constants;
using BookSmartBackEndDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSmartBackEndDatabase.Repositories;

public class AppointmentRepository(BookSmartContext context) : IAppointmentRepository
{
    public Appointment? GetById(Guid appointmentId)
    {
        return context.APPOINTMENTS
            .Include(a => a.APPOINTMENT_SERVICE)
            .FirstOrDefault(a => a.APPOINTMENT_ID == appointmentId && !a.APPOINTMENT_DELETED);
    }

    public List<Appointment> GetByClient(Guid clientUserId)
    {
        return context.APPOINTMENTS
            .Include(a => a.APPOINTMENT_SERVICE)
            .Where(a => a.APPOINTMENT_CLIENTUSERID == clientUserId && !a.APPOINTMENT_DELETED)
            .ToList();
    }

    public List<Appointment> GetByStaff(Guid staffUserId)
    {
        return context.APPOINTMENTS
            .Include(a => a.APPOINTMENT_CLIENTUSER)
            .Include(a => a.APPOINTMENT_SERVICE)
            .Where(a => a.APPOINTMENT_STAFFUSERID == staffUserId && !a.APPOINTMENT_DELETED)
            .ToList();
    }

    public int CountActiveForSlot(Guid serviceId, Guid? scheduleId, Guid? scheduleOverrideId, DateTime requestedStartTime)
    {
        return context.APPOINTMENTS
            .Count(a => a.APPOINTMENT_SERVICEID == serviceId
                     && a.APPOINTMENT_SCHEDULEID == scheduleId
                     && a.APPOINTMENT_SCHEDULEOVERRIDEID == scheduleOverrideId
                     && a.APPOINTMENT_STARTDATETIME == requestedStartTime
                     && a.APPOINTMENT_STATUS != AppointmentStatuses.Cancelled
                     && !a.APPOINTMENT_DELETED);
    }

    public void Add(Appointment appointment)
    {
        context.APPOINTMENTS.Add(appointment);
        context.SaveChanges();
    }

    public void Update(Appointment appointment)
    {
        context.Entry(appointment).State = EntityState.Modified;
        context.SaveChanges();
    }
}
