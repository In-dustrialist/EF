
using Models;

namespace Repositories
{
    public class UserRepository
    {
        private readonly AppContext _context;

        public UserRepository()
        {
            _context = new AppContext();
        }

        // Добавление пользователя
        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        // Получение всех пользователей
        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        // Обновление имени пользователя
        public void UpdateName(int id, string newName)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.Name = newName;
                _context.SaveChanges();
            }
        }
    }
}
