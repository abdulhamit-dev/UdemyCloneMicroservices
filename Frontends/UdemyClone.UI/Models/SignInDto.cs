using System.ComponentModel;

namespace UdemyClone.UI.Models
{
    public class SignInDto
    {
        [DisplayName("Email Adresiniz")]
        public string Email { get; set; }
        [DisplayName("Şifreniz")]
        public string Password { get; set; }
        [DisplayName("Beni Hatırla")]
        public bool IsRemember { get; set; }
    }
}
