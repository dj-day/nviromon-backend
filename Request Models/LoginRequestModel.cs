using System.ComponentModel.DataAnnotations;

namespace Nviromon.Request_Models
{
    public class LoginRequestModel
    {
        [EmailAddress]
        public string username { get; set; }

        public string password { get; set; }
    }
}