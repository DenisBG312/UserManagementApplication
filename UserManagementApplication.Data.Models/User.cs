using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementApplication.Data.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [MaxLength(50)]
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [Column("date_of_birth")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        [Column("email_address")]
        public string EmailAddress { get; set; } = string.Empty;
        [Required]
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
