using System.ComponentModel;
using System.Text.Json.Serialization;

namespace UserManagementApplication.Web.Dtos
{
    public class UserDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("emailAddress")]
        public string EmailAddress { get; set; }
    }
}
