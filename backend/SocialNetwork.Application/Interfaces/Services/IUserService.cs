using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace FilmMatch.Application.Interfaces.Services ;

    /// <summary>
    /// Сервис по работе с пользователем
    /// </summary>
    public interface IUserService
    {
       /// <summary>
        /// Зарегистрировать пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        public Task<IdentityResult> RegisterUserAsync(IdentityUser<Guid> user, string password);

        /// <summary>
        /// Найти пользователя по Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<IdentityUser<Guid>?> FindUserByEmailAsync(string email);

        /// <summary>
        /// Добавить роль пользователю
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="role">Роль</param>
        /// <returns></returns>
        public Task<IdentityResult> AddRoleAsync(IdentityUser<Guid> user, string role);

        /// <summary>
        /// Добавить клеймы
        /// </summary>
        /// <param name="user">пользователь</param>
        /// <param name="claims">Клеймы</param>
        /// <returns></returns>
        public Task<IdentityResult> AddClaimsAsync(IdentityUser<Guid> user, IEnumerable<Claim> claims);

        /// <summary>
        /// Проверить пароль
        /// </summary>
        /// <param name="user">пользователь</param>
        /// <param name="password">пароль</param>
        /// <returns></returns>
        public Task<bool> CheckPasswordAsync(IdentityUser<Guid> user, string password);

        /// <summary>
        /// Получить роли пользователя
        /// </summary>
        /// <param name="user">пользователь</param>
        /// <returns></returns>
        public Task<IList<string>> GetRolesAsync(IdentityUser<Guid> user);

        /// <summary>
        /// Получить роль пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string?> GetRoleAsync(IdentityUser<Guid> user);

        /// <summary>
        /// Сменить пароль пользователю
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="token">Токен смены пароля</param>
        /// <param name="newPassword">Новый пароль</param>
        /// <returns></returns>
        public Task<IdentityResult> ResetPasswordAsync(IdentityUser<Guid> user, string token, string newPassword);

        /// <summary>
        /// Сгенерировать токен смены пароля
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        public Task<string> GeneratePasswordResetTokenAsync(IdentityUser<Guid> user);

        /// <summary>
        /// Получить пользователя по идентификатору
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <returns>-</returns>
        public Task<IdentityUser<Guid>?> GetUserById(Guid id);

        /// <summary>
        /// Проверить, является ли пользователь богом
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        public Task<bool> IsGodAsync(IdentityUser<Guid> user);

        /// <summary>
        /// Проверить, является ли пользователь администратором
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        public Task<bool> IsAdminAsync(IdentityUser<Guid> user);

        /// <summary>
        /// Проверить, является ли пользователь обычным пользователем
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        public Task<bool> IsUserAsync(IdentityUser<Guid> user);

        /// <summary>
        /// Создать нового администратора
        /// </summary>
        /// <param name="email">Email администратора</param>
        /// <param name="password">Пароль администратора</param>
        /// <returns></returns>
        public Task<IdentityResult> CreateAdminAsync(string email, string password);

        /// <summary>
        /// Назначить пользователю роль администратора по email
        /// </summary>
        /// <param name="email">Email пользователя</param>
        /// <returns></returns>
        public Task<IdentityResult> MakeAdminAsync(string email);
    }