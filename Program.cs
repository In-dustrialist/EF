using Models;
using Repositories;


class Program
{
    static void Main()
    {
        using (var context = new AppContext())
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        var userRepo = new UserRepository();
        var bookRepo = new BookRepository();

        // Добавим пользователей
        var users = new List<User>
        {
            new User { Name = "Александр Пушкин", Email = "pushkin@mail.com" },
            new User { Name = "Лев Толстой", Email = "tolstoy@mail.com" },
            new User { Name = "Фёдор Достоевский", Email = "dostoevsky@mail.com" },
            new User { Name = "Антон Чехов", Email = "chekhov@mail.com" }
        };

        foreach (var user in users)
            userRepo.Add(user);

        // Добавим книги
        var books = new List<Book>
        {
            new Book { Title = "Евгений Онегин", Year = 1833, Author = "Александр Пушкин", Genre = "Роман", UserId = users[0].Id },
            new Book { Title = "Капитанская дочка", Year = 1836, Author = "Александр Пушкин", Genre = "Исторический роман", UserId = users[0].Id },
            new Book { Title = "Война и мир", Year = 1869, Author = "Лев Толстой", Genre = "Роман", UserId = users[1].Id },
            new Book { Title = "Анна Каренина", Year = 1877, Author = "Лев Толстой", Genre = "Роман", UserId = users[1].Id },
            new Book { Title = "Преступление и наказание", Year = 1866, Author = "Фёдор Достоевский", Genre = "Роман", UserId = users[2].Id },
            new Book { Title = "Идиот", Year = 1869, Author = "Фёдор Достоевский", Genre = "Роман", UserId = users[2].Id },
            new Book { Title = "Вишнёвый сад", Year = 1904, Author = "Антон Чехов", Genre = "Пьеса", UserId = users[3].Id },
            new Book { Title = "Палата №6", Year = 1892, Author = "Антон Чехов", Genre = "Повесть", UserId = null } // не на руках
        };

        foreach (var book in books)
            bookRepo.Add(book);

        // Получаем список книг определённого жанра и года
        var booksByGenre = bookRepo.GetBooksByGenreAndYearRange("Роман", 1800, 1900);
        Console.WriteLine("\nКниги, жанр 'Роман', года 1800-1900:");
        foreach (var book in booksByGenre)
            Console.WriteLine($"{book.Title} ({book.Year})");

        // Проверим, есть ли книга определённого автора с названием
        bool exists = bookRepo.BookExists("Александр Пушкин", "Евгений Онегин");
        Console.WriteLine($"\nЕсть ли книга 'Евгений Онегин' автором 'Александр Пушкин'? {exists}");

        // Получим последнюю вышедшую книгу
        var lastBook = bookRepo.GetLastReleasedBook();
        Console.WriteLine($"\nПоследняя вышедшая книга: {lastBook.Title} ({lastBook.Year})");

        // Выведем все книги, отсортированные по названию
        var sortedBooks = bookRepo.GetBooksSortedByTitle();
        Console.WriteLine("\nКниги, отсортированные по названию:");
        foreach (var book in sortedBooks)
            Console.WriteLine($"{book.Title} ({book.Year})");

        // Выведем всех пользователей
        Console.WriteLine("\nПользователи:");
        foreach (var u in userRepo.GetAll())
            Console.WriteLine($"{u.Name} — {u.Email}");
    }
}
