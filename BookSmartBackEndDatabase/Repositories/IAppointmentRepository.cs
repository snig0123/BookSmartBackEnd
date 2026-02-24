using BookSmartBackEndDatabase.Models;

namespace BookSmartBackEndDatabase.Repositories;

public interface IAppointmentRepository
{
    Appointment? GetById(Guid appointmentId);
    List<Appointment> GetByClient(Guid clientUserId);
    List<Appointment> GetByStaff(Guid staffUserId);
    int CountActiveForSlot(Guid serviceId, Guid? scheduleId, Guid? scheduleOverrideId, DateTime requestedStartTime);
    void Add(Appointment appointment);
    void Update(Appointment appointment);
}
