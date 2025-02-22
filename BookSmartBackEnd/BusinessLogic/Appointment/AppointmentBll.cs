using BookSmartBackEnd.BusinessLogic.Interfaces;
using BookSmartBackEnd.Models;
using BookSmartBackEndDatabase;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookSmartBackEnd.Models.POST;
using BookSmartBackEnd.Utilities;
using BookSmartBackEndDatabase.Models;

namespace BookSmartBackEnd.BusinessLogic
{
    internal sealed class AppointmentBll(BookSmartContext bookSmartContext) : IAppointmentBll
    {
        public void CreateAppointment(PostAppointmentModel data)
        {
            throw new NotImplementedException();
        }
    }
}
