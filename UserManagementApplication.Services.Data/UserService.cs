using Microsoft.EntityFrameworkCore;
using UserManagementApplication.API.Dto;
using UserManagementApplication.Data;
using UserManagementApplication.Data.Models;
using UserManagementApplication.Data.Repository.Interfaces;
using UserManagementApplication.Services.Data.Interfaces;

namespace UserManagementApplication.Services.Data
{
    public class UserService : IUserService
    {
        private readonly IRepository<User, Guid> _userRepository;

        public UserService(IRepository<User, Guid> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<PagedUserResponse> GetAllAsync(int page = 1, int pageSize = 5)
        {
            var users = await _userRepository.GetAllAsync();

            var notDeletedUsers = users.Where(u => !u.IsDeleted).ToList();

            int totalUsers = notDeletedUsers.Count;
            int totalPages = (int)Math.Ceiling((double)totalUsers / pageSize);

            var pagedUsers = notDeletedUsers
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    PhoneNumber = user.PhoneNumber,
                    EmailAddress = user.EmailAddress
                })
                .ToList();

            return new PagedUserResponse
            {
                Users = pagedUsers,
                TotalPages = totalPages,
                CurrentPage = page
            };
        }


        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return null;

            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                EmailAddress = user.EmailAddress
            };

            return userDto;
        }

        public async Task<PagedUserResponse> SearchAsync(string searchTerm, int page = 1, int pageSize = 5)
        {
            var users = await _userRepository.GetAllAsync();

            var filteredUsers = users
                .Where(u => !u.IsDeleted)
                .Where(u => u.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                            u.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                            u.PhoneNumber.Contains(searchTerm) ||
                            u.EmailAddress.ToLower().Contains(searchTerm.ToLower()))
                .ToList();

            int totalUsers = filteredUsers.Count;
            int totalPages = (int)Math.Ceiling((double)totalUsers / pageSize);

            var pagedUsers = filteredUsers
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    PhoneNumber = user.PhoneNumber,
                    EmailAddress = user.EmailAddress
                })
                .ToList();

            return new PagedUserResponse
            {
                Users = pagedUsers,
                TotalPages = totalPages,
                CurrentPage = page
            };
        }

        public async Task<string> CreateAsync(CreateUserDto userDto)
        {
            var existingUser = await _userRepository.GetAllAsync();
            if (existingUser.Any(u => u.EmailAddress == userDto.EmailAddress))
            {
                throw new InvalidOperationException("Email address is already in use.");
            }
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                DateOfBirth = userDto.DateOfBirth,
                PhoneNumber = userDto.PhoneNumber,
                EmailAddress = userDto.EmailAddress
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return $"You, successfully created user: {user.FirstName} {user.LastName}";
        }

        public async Task<bool> UpdateAsync(Guid id, CreateUserDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return false;

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.DateOfBirth = userDto.DateOfBirth;
            user.PhoneNumber = userDto.PhoneNumber;
            user.EmailAddress = userDto.EmailAddress;

            var result = await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            return result;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return false;

            user.IsDeleted = true;

            var result = await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            return result;
        }
    }
}
