using WebApi.Model;

namespace WebApi.Interfaces.Repository
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        bool CreateUser(List<Role> roles, User user);
        User GetUserById(int id);
        Dictionary<int, List<Role>> GetUsersByRoles();
        bool UpdateUser(User user);
        bool AddRolesToUser(int[] roleIds, int userId);
        bool IsUserExist(int userId);
        bool DeleteUser(User user);
        bool Save();
    }
}
