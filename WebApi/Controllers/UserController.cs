using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Dto;
using WebApi.Interfaces.Service;

namespace WebApi.Controllers
{
    /// <summary>
    /// Контроллер для управления пользователями.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Получает пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Информация о пользователе.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);

            if (user == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        /// <summary>
        /// Получает список всех пользователей с ролями.
        /// </summary>
        /// <returns>Список пользователей с ролями.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUsersWithRole()
        {
            var users = _userService.GetUsers();

            if (users == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        /// <summary>
        /// Добавляет роли пользователю по его идентификатору.
        /// </summary>
        /// <param name="roleIds">Массив идентификаторов ролей для добавления.</param>
        /// <param name="userId">Идентификатор пользователя, которому необходимо добавить роли.</param>
        /// <returns>Результат операции.</returns>
        [HttpPost]
        [Route("AddRoles")]
        public async Task<ActionResult> AddRolesToUser(int[] roleIds, int userId)
        {
            if (!_userService.IsUserExist(userId))
                return NotFound();

            if (!_userService.AddRolesToUser(roleIds, userId))
                return StatusCode((int)HttpStatusCode.InternalServerError);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();
        }


        /// <summary>
        /// Обновляет информацию о пользователе по его идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, информацию о котором необходимо обновить.</param>
        /// <param name="userDto">DTO с обновленными данными пользователя.</param>
        /// <returns>Результат операции.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int userId, UserDto userDto)
        {
            if (!_userService.IsUserExist(userId))
                return NotFound();

            if (!_userService.UpdateUser(userDto))
                return StatusCode((int)HttpStatusCode.InternalServerError);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();
        }

        /// <summary>
        /// Создает нового пользователя.
        /// </summary>
        /// <param name="userDto">DTO с данными нового пользователя.</param>
        /// <returns>Результат операции.</returns>
        [HttpPost]
        public async Task<ActionResult> CreateUser(UserDto userDto)
        {
            if (userDto == null)
                return BadRequest(ModelState);

            if (_userService.IsUniqueEmail(userDto))
                return StatusCode((int)HttpStatusCode.Conflict, ModelState);

            if (!_userService.CreateUser(userDto))
                return StatusCode((int)HttpStatusCode.InternalServerError);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();
        }


        /// <summary>
        /// Удаляет пользователя по его идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, которого необходимо удалить.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeletePokemon(int userId)
        {
            if (!_userService.IsUserExist(userId))
                return NotFound();

            if (!_userService.DeleteUser(userId))
                return StatusCode((int)HttpStatusCode.InternalServerError);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();
        }
    }
}
