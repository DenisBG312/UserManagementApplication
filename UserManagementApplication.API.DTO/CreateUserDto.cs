using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementApplication.API.Dto
{
    public class CreateUserDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between {2} and {1} characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between {2} and {1} characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string EmailAddress { get; set; } = string.Empty;
    }
}
