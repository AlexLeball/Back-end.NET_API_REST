using Microsoft.AspNetCore.Identity;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;

namespace P7CreateRestApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // List all users
        public IEnumerable<User> FindAll()
        {
            return _userManager.Users.ToList();
        }

        // Find user by id 
        public async Task<User> FindByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        // Add a new user and assign role via Identity role system
        public async Task<IdentityResult> AddAsync(User user, string password)
        {
            // Défauts côté serveur
            var roleToAssign = string.IsNullOrWhiteSpace(user.Role) ? "User" : user.Role;
            user.Role = roleToAssign;
            user.Fullname = string.IsNullOrWhiteSpace(user.Fullname) ? "New User" : user.Fullname;

            var createResult = await _userManager.CreateAsync(user, password);
            if (!createResult.Succeeded)
                return createResult;

            // make sure role exists
            if (!await _roleManager.RoleExistsAsync(roleToAssign))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleToAssign));
            }

            var addRoleResult = await _userManager.AddToRoleAsync(user, roleToAssign);
            if (!addRoleResult.Succeeded)
            {
                // clean if role assignment fails
                await _userManager.DeleteAsync(user);
                return IdentityResult.Failed(addRoleResult.Errors.ToArray());
            }

            return IdentityResult.Success;
        }

        // Update an existing user
        public async Task<IdentityResult> UpdateAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }
    }
}
