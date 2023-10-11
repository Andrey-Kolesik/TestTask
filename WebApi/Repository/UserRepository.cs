using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces.Repository;
using WebApi.Model;

namespace WebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateUser(List<Role> roles, User user)
        {
            if (roles != null)
            {
                List<string> rolesName = roles.Select(r => r.Name).ToList();
                var existRoles = _context.Roles.Where(r => rolesName.Contains(r.Name));

                foreach (var role in existRoles)
                {
                    var userRole = new UserRole()
                    {
                        Role = role,
                        User = user,
                    };

                    _context.Add(userRole);
                }
            }

            _context.Add(user);

            return Save();
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public Dictionary<int, List<Role>> GetUsersByRoles()
        {
            return _context.UserRoles.Include(ur => ur.Role).GroupBy(x => x.UserId).ToDictionary(x => x.Key, x => x.Select(u => u.Role).ToList());
        }

        public bool AddRolesToUser(int[] roleIds, int userId)
        {
            var user = _context.Users.Find(userId);
            var rolesToAdd = _context.Roles.Where(r => roleIds.Contains(r.Id)).ToList();

            foreach (var role in rolesToAdd)
            {
                user.UserRoles.Add(new UserRole() { Role = role, User = user });
            }

            return Save();
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);

            return Save();
        }

        public bool IsUserExist(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            _context.RemoveRange(GetUserRolesByUserId(user.Id));

            return Save();
        }

        public List<UserRole> GetUserRolesByUserId(int userId)
        {
            return _context.UserRoles.Where(ur => ur.UserId == userId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
