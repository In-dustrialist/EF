using System;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories;

namespace EF
{
    class Program
    {
        static void Main(string[] args)
        {
            // Удалим и пересоздадим базу данных (только для разработки!)
            using (var context = new AppContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            // Репозитории
            var userRepo = new UserRepository();
            var bookRepo = new BookRepository();

            // Добавим пользователя
            var user = new User { Name = "Александр", Email = "alex@mail.com" };
            userRepo.Add(user);

            // Обновим имя
            userRepo.UpdateName(user.Id, "Александр Пушкин");

            // Добавим книгу и выдадим её пользователю
            var book = new Book
            {
                Title = "Евгений Онегин",
                Year = 1833,
                Author = "А.С. Пушкин",
                Genre = "Роман",
                UserId = user.Id // Книга на руках у пользователя
            };
            bookRepo.Add(book);

            // Обновим год выпуска книги
            bookRepo.UpdateYear(book.Id, 1837);

            // Вывод пользователей
            Console.WriteLine("Пользователи:");
            var users = userRepo.GetAll();
            if (users.Count > 0)
            {
                foreach (var u in users)
                {
                    Console.WriteLine($"{u.Id}: {u.Name} — {u.Email}");
                }
            }
            else
            {
                Console.WriteLine("Нет пользователей.");
            }

            // Вывод книг
            Console.WriteLine("\nКниги:");
            var books = bookRepo.GetAll();
            if (books.Count > 0)
            {
                foreach (var b in books)
                {
                    Console.WriteLine($"{b.Id}: {b.Title} — {b.Year}, Автор: {b.Author}, Жанр: {b.Genre}, На руках у пользователя ID: {b.UserId}");
                }
            }
            else
            {
                Console.WriteLine("Нет книг.");
            }

            Console.WriteLine("\nРабота завершена.");
        }
    }
}
