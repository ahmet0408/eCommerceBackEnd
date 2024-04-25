using System.ComponentModel.DataAnnotations;

namespace eCommerce.bll.ViewModel
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
