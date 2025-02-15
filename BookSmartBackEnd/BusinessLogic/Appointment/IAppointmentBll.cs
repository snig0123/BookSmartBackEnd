using BookSmartBackEnd.Models;

namespace BookSmartBackEnd.BusinessLogic.Interfaces
{
    public interface IAppointmentBll
    {
        void CreateAppointment(PostAppointmentModel data);
    }
}
