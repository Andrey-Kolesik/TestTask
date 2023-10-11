using WebApi.Dto;

namespace WebApi.Interfaces.Service
{
    public interface IUserService
    {
        List<UserDto> GetUsers();
        bool CreateUser(UserDto userDto);
        UserDto GetUserById(int id);
        bool UpdateUser(UserDto userDto);
        public bool IsUniqueEmail(UserDto userDto);
        bool AddRolesToUser(int[] roleIds, int userId);
        bool IsUserExist(int userId);
        bool DeleteUser(int userId);
    }
}
