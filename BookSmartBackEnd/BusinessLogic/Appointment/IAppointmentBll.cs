using BookSmartBackEnd.Models.GET;
using BookSmartBackEnd.Models.POST;

namespace BookSmartBackEnd.BusinessLogic.Interfaces
{
    public interface IAppointmentBll
    {
        void CreateAppointment(Guid clientUserId, PostAppointmentModel data);
        void CreateAppointmentForClient(PostAppointmentForClientModel data);
        List<AppointmentResponse> GetMyAppointments(Guid clientUserId);
        List<AppointmentResponse> GetAppointmentsByStaff(Guid staffUserId);
        void CancelAppointment(Guid clientUserId, Guid appointmentId);
        void UpdateAppointmentStatus(Guid appointmentId, string status);
    }
}
