using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementApplication.API.Dto;

namespace UserManagementApplication.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<PagedUserResponse> GetAllAsync(int page = 1, int pageSize = 5);
        Task<UserDto?> GetByIdAsync(Guid id);
        Task<PagedUserResponse> SearchAsync(string searchTerm, int page = 1, int pageSize = 5);
        Task<string> CreateAsync(CreateUserDto userDto);
        Task<bool> UpdateAsync(Guid id, CreateUserDto userDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
