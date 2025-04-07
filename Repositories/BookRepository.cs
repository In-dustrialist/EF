
using Models;


namespace Repositories
{
    public class BookRepository
    {
        private readonly AppContext _context;

        public BookRepository()
        {
            _context = new AppContext();
        }

        // Добавление книги
        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        // Получение всех книг
        public List<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        // Обновление года выпуска книги
        public void UpdateYear(int id, int newYear)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                book.Year = newYear;
                _context.SaveChanges();
            }
        }

        // Получение списка книг по жанру и году
        public List<Book> GetBooksByGenreAndYearRange(string genre, int startYear, int endYear)
        {
            return _context.Books
                .Where(b => b.Genre == genre && b.Year >= startYear && b.Year <= endYear)
                .ToList();
        }

        // Получение количества книг определённого автора
        public int GetBookCountByAuthor(string author)
        {
            return _context.Books.Count(b => b.Author == author);
        }

        // Получение количества книг определённого жанра
        public int GetBookCountByGenre(string genre)
        {
            return _context.Books.Count(b => b.Genre == genre);
        }

        // Проверка, есть ли книга определённого автора с конкретным названием
        public bool BookExists(string author, string title)
        {
            return _context.Books.Any(b => b.Author == author && b.Title == title);
        }

        // Проверка, есть ли книга на руках у пользователя
        public bool IsBookOnHand(int bookId)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
            return book != null && book.UserId.HasValue;
        }

        // Получение количества книг на руках у пользователя
        public int GetBooksOnHandCount(int userId)
        {
            return _context.Books.Count(b => b.UserId == userId);
        }

        // Получение последней вышедшей книги
        public Book GetLastReleasedBook()
        {
            return _context.Books.OrderByDescending(b => b.Year).FirstOrDefault();
        }

        // Получение списка всех книг, отсортированных по названию
        public List<Book> GetBooksSortedByTitle()
        {
            return _context.Books.OrderBy(b => b.Title).ToList();
        }

        // Получение списка всех книг, отсортированных по году выпуска
        public List<Book> GetBooksSortedByYear()
        {
            return _context.Books.OrderByDescending(b => b.Year).ToList();
        }
    }
}
