using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Возраст обязательное поле.")]
        [Range(0, int.MaxValue, ErrorMessage = "Возраст должен быть положительным числом.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Имя пользователя обязательное поле.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email обязательное поле.")]
        [EmailAddress(ErrorMessage = "Некорректный формат Email.")]
        public string Email { get; set; }
        public List<RoleDto> Roles { get; set; }
    }
}
