using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolTemplate.Models
{
    public class PersonModel
    {
        [Required(ErrorMessage = "First name is mandatory")] 
        public string firstname { get; set; }

        [Required(ErrorMessage ="Last name is mandatory")]
        public string lastname { get; set; }

        [Required(ErrorMessage ="E-mail address is mandatory")]
        [EmailAddress(ErrorMessage ="Not a valid e-mail address")]
        public string email { get; set; }

        public DateTime birthdate { get; set; }
    }

}