using Microsoft.AspNetCore.Mvc;
using UserManagementApplication.API.Dto;
using UserManagementApplication.Data.Models;
using UserManagementApplication.Web.Dtos;
using UserDto = UserManagementApplication.Web.Dtos.UserDto;

namespace UserManagementApplication.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(
            string searchTerm,
            string sortBy = "LastName",
            string sortOrder = "asc")
        {
            var client = _httpClientFactory.CreateClient("UserApi");
            HttpResponseMessage response;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                response = await client.GetAsync($"/api/User/SearchUser?searchTerm={searchTerm}");
            }
            else
            {
                response = await client.GetAsync("/api/User/GetAllUsers");
            }

            if (response.IsSuccessStatusCode)
            {
                var users = await response.Content.ReadFromJsonAsync<List<User>>();

                users = sortBy switch
                {
                    "LastName" => sortOrder == "asc"
                        ? users.OrderBy(u => u.LastName).ToList()
                        : users.OrderByDescending(u => u.LastName).ToList(),
                    "DateOfBirth" => sortOrder == "asc"
                        ? users.OrderBy(u => u.DateOfBirth).ToList()
                        : users.OrderByDescending(u => u.DateOfBirth).ToList(),
                    _ => users
                };

                ViewBag.SortBy = sortBy;
                ViewBag.SortOrder = sortOrder;
                ViewBag.SearchTerm = searchTerm;

                return View(users);
            }

            return View("Error");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            user.DateOfBirth = user.DateOfBirth.ToUniversalTime();

            var client = _httpClientFactory.CreateClient("UserApi");
            var response = await client.PostAsJsonAsync("/api/User/CreateUser", user);

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"API Response: {response.StatusCode} - {responseContent}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Error creating user");
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = _httpClientFactory.CreateClient("UserApi");
            var response = await client.GetAsync($"/api/User/GetSpecificUser/{id}");

            if (response.IsSuccessStatusCode)
            {
                var userDto = await response.Content.ReadFromJsonAsync<EditUserDto>();

                return View(userDto);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditUserDto user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            user.DateOfBirth = user.DateOfBirth.ToUniversalTime();

            var client = _httpClientFactory.CreateClient("UserApi");
            var response = await client.PutAsJsonAsync($"/api/User/UpdateUser/{id}", user);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Error updating user");
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("UserApi");
                var response = await client.GetAsync($"/api/User/GetSpecificUser/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<UserDto>();
                    return View(user);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("UserApi");
                var response = await client.DeleteAsync($"/api/User/DeleteUser/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "User deleted successfully";
                    return RedirectToAction(nameof(Index));
                }

                TempData["ErrorMessage"] = "Error deleting user";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the user";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
