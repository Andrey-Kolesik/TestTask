using AutoMapper;
using WebApi.Dto;
using WebApi.Interfaces.Repository;
using WebApi.Interfaces.Service;
using WebApi.Model;

namespace WebApi.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public bool AddRolesToUser(int[] roleIds, int userId)
        {
            return _userRepository.AddRolesToUser(roleIds, userId);
        }

        public bool CreateUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var roles = _mapper.Map<List<RoleDto>, List<Role>>(userDto.Roles);

            return _userRepository.CreateUser(roles, user);
        }

        public bool DeleteUser(int userId)
        {
            var user = _userRepository.GetUserById(userId);

            return _userRepository.DeleteUser(user);
        }

        public UserDto GetUserById(int id)
        {
            var user = _mapper.Map<UserDto>(_userRepository.GetUserById(id));
            var usersByRoles = _userRepository.GetUsersByRoles();

            List<Role> roles;
            if (usersByRoles.TryGetValue(user.Id, out roles))
            {
                user.Roles = _mapper.Map<List<Role>, List<RoleDto>>(roles);
            }

            return user;
        }

        public List<UserDto> GetUsers()
        {
            var users = _mapper.Map<List<User>, List<UserDto>>(_userRepository.GetUsers());
            var usersByRoles = _userRepository.GetUsersByRoles();

            foreach (var user in users)
            {
                List<Role> roles;
                if (usersByRoles.TryGetValue(user.Id, out roles))
                {
                    user.Roles = _mapper.Map<List<Role>, List<RoleDto>>(roles);
                }
            }

            return users;
        }

        public bool IsUserExist(int userId)
        {
            return _userRepository.IsUserExist(userId);
        }

        public bool IsUniqueEmail(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            return _userRepository.GetUsers().Exists(u => u.Email == user.Email);
        }

        public bool UpdateUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            return _userRepository.UpdateUser(user);
        }
    }
}
