namespace BookSmartBackEnd.Models.POST
{
    public class PostRegisterModel(string forename, string surname, string email, string password)
    {
        public string FORENAME { get; set; } = forename;
        public string SURNAME { get; set; } = surname;
        public string EMAIL { get; set; } = email;
        public string PASSWORD { get; set; } = password;
    }
}
