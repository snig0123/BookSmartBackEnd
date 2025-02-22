using BookSmartBackEnd.Models;
using BookSmartBackEnd.Models.POST;

namespace BookSmartBackEnd.BusinessLogic.Interfaces
{
    public interface IAppointmentBll
    {
        void CreateAppointment(PostAppointmentModel data);
    }
}
