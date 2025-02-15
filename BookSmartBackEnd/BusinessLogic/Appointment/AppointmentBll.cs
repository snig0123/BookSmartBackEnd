using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models;
using BookSmartBackEndDatabase;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookSmartBackEnd.BusinessLogic
{
    public class AppointmentBll : IAppointmentBll
    {
        private BookSmartContext? _bookSmartContext;
        public AppointmentBll(IServiceProvider serviceProvider)
        {
            _bookSmartContext = serviceProvider.GetService<BookSmartContext>();
        }
        public void CreateAppointment(PostAppointmentModel data)
        {
            /*var db = new BookSmartContext();
            db.Add(new BookSmartBackEndDatabase.Models.User
            {
                USER_ID = Guid.NewGuid(),
                USER_FORENAME = data.FORENAME,
                USER_SURNAME = data.SURNAME,
                USER_EMAIL = data.EMAIL,
                USER_PASSWORD = data.PASSWORD
            });
            db.SaveChanges();*/
        }
    }
}
