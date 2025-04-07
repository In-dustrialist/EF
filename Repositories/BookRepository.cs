using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class BookRepository
    {
        public void Add(Book book)
        {
            using var context = new AppContext();
            context.Books.Add(book);
            context.SaveChanges();
        }

        public void Delete(Book book)
        {
            using var context = new AppContext();
            context.Books.Remove(book);
            context.SaveChanges();
        }

        public Book GetById(int id)
        {
            using var context = new AppContext();
            return context.Books.Include(b => b.User).FirstOrDefault(b => b.Id == id);
        }

        public List<Book> GetAll()
        {
            using var context = new AppContext();
            return context.Books.Include(b => b.User).ToList();
        }

        public void UpdateYear(int id, int newYear)
        {
            using var context = new AppContext();
            var book = context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                book.Year = newYear;
                context.SaveChanges();
            }
        }

        // --- 🔽 Новые методы по заданию: ---

        // 1. Книги определённого жанра и в интервале лет
        public List<Book> GetBooksByGenreAndYearRange(string genre, int startYear, int endYear)
        {
            using var context = new AppContext();
            return context.Books
                .Where(b => b.Genre == genre && b.Year >= startYear && b.Year <= endYear)
                .ToList();
        }

        // 2. Количество книг определённого автора
        public int CountBooksByAuthor(string author)
        {
            using var context = new AppContext();
            return context.Books.Count(b => b.Author == author);
        }

        // 3. Количество книг определённого жанра
        public int CountBooksByGenre(string genre)
        {
            using var context = new AppContext();
            return context.Books.Count(b => b.Genre == genre);
        }

        // 4. Есть ли книга с конкретным автором и названием
        public bool ExistsByAuthorAndTitle(string author, string title)
        {
            using var context = new AppContext();
            return context.Books.Any(b => b.Author == author && b.Title == title);
        }

        // 5. Есть ли конкретная книга на руках у пользователя
        public bool IsBookWithUser(int bookId, int userId)
        {
            using var context = new AppContext();
            return context.Books.Any(b => b.Id == bookId && b.UserId == userId);
        }

        // 6. Количество книг на руках у пользователя
        public int CountBooksWithUser(int userId)
        {
            using var context = new AppContext();
            return context.Books.Count(b => b.UserId == userId);
        }

        // 7. Последняя по году выпуска книга
        public Book GetLatestBook()
        {
            using var context = new AppContext();
            return context.Books
                .OrderByDescending(b => b.Year)
                .FirstOrDefault();
        }

        // 8. Все книги в алфавитном порядке
        public List<Book> GetAllSortedByTitle()
        {
            using var context = new AppContext();
            return context.Books.OrderBy(b => b.Title).ToList();
        }

        // 9. Все книги по убыванию года
        public List<Book> GetAllSortedByYearDesc()
        {
            using var context = new AppContext();
            return context.Books.OrderByDescending(b => b.Year).ToList();
        }
    }
}
