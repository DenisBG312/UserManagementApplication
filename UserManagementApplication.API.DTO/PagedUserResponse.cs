using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementApplication.API.Dto
{
    public class PagedUserResponse
    {
        public List<UserDto> Users { get; set; } = new List<UserDto>();
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
