using System.ComponentModel.DataAnnotations;

namespace Platform.Model.View
{
    public class LoginFormModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool PersistLogin { get; set; }
    }

    public class ErrorViewModel
    {
    }

    public class ViewModelBase
    {
    }

    public class ViewModelOfT
    {
    }
}