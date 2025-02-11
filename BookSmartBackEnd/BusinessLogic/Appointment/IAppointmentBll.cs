using BookSmartBackEnd.Models;

namespace BookSmartBackEnd.BusinessLogic.User.Interfaces
{
    public interface IAppointmentBll
    {
        void CreateAppointment(PostAppointmentModel data);
    }
}
