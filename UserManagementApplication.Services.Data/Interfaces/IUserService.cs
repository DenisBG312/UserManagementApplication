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
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<UserDto>> SearchAsync(string searchTerm);
        Task<string> CreateAsync(CreateUserDto userDto);
        Task<bool> UpdateAsync(Guid id, CreateUserDto userDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
