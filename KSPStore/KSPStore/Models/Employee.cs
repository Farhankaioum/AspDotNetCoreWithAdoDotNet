using System.ComponentModel.DataAnnotations;

namespace KSPStore.Models
{
    public class Employee
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name ="User Name")]
        public string UserName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Email is not valid")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage ="Password and Confirm Password must match!")]
        //public string ConfirmPassword { get; set; }


    }
}
