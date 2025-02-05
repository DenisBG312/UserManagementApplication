using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementApplication.API.Dto
{
    public class EditUserDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between {2} and {1} characters.")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name must be between {2} and {1} characters.")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string EmailAddress { get; set; } = string.Empty;
    }
}
