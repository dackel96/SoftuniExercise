namespace BookShop
{
    using BookShop.Models;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);
            Console.WriteLine(RemoveBooks(db)); //////////////////////////////////////////////////
        }
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var ageRestriction = Enum.Parse<AgeRestriction>(command, true);

            var books = context.Books
                .Where(books => books.AgeRestriction == ageRestriction)
                .Select(book => book.Title)
                .OrderBy(x => x)
                .ToList();
            var result = string.Join(Environment.NewLine, books);

            return result;
        }
        public static string GetGoldenBooks(BookShopContext context)
        {
            //var editionType = Enum.Parse<EditionType>("Gold", true);

            var books = context.Books
                .Where(book => (book.EditionType == EditionType.Gold) && (book.Copies < 5000))
                .Select(x => new
                {
                    x.BookId,
                    x.Title
                })
                .OrderBy(x => x.BookId)
                .ToList();

            var result = string.Join(Environment.NewLine, books.Select(x => x.Title));
            return result;
            //StringBuilder result = new StringBuilder();
            //foreach (var book in books)
            //{
            //    result.AppendLine($"{book.Title}");
            //}
            //
            //return result.ToString().TrimEnd();
        }
        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.Price > 40)
                .Select(x => new
                {
                    x.Title,
                    x.Price
                })
                .OrderByDescending(x => x.Price)
                .ToList();
            var result = string.Join(Environment.NewLine, books.Select(x => $"{x.Title} - ${x.Price:f2}"));
            //StringBuilder result = new StringBuilder();
            //foreach (var book in books)
            //{
            //    result.AppendLine($"{book.Title} - ${book.Price:f2}");
            //}
            //
            //return result.ToString().TrimEnd();
            return result;
        }
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(x => x.ReleaseDate.Value.Year != year)
                .Select(x => new
                {
                    x.BookId,
                    x.Title
                })
                .OrderBy(x => x.BookId)
                .ToList();

            var result = string.Join(Environment.NewLine, books.Select(x => x.Title));
            return result;
        }
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categories = input
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToLower())
                .ToArray();

            var books = context.Books
                .Where(x => x.BookCategories
                .Any(x => categories.Contains(x.Category.Name.ToLower())))
                .Select(x => x.Title)
                .OrderBy(x => x)
                .ToList();
            var result = string.Join(Environment.NewLine, books);
            return result;

        }
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var getDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books
                .Where(x => x.ReleaseDate < getDate)
                .Select(x => new
                {
                    x.Title,
                    x.EditionType,
                    x.Price,
                    x.ReleaseDate
                })
                .OrderByDescending(x => x.ReleaseDate)
                .ToList();

            var result = string.Join(Environment.NewLine, books.Select(x => $"{x.Title} - {x.EditionType} - ${x.Price:f2}"));
            return result;
        }
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            //Return the full names of authors, whose first name ends with a given string.
            //Return all names in a single string, each on a new row ordered alphabetically
            var authors = context.Authors
                .Where(x => EF.Functions.Like(x.FirstName, $"%{input}"))
                .Select(x => new
                {
                    FullName = x.FirstName + " " + x.LastName
                })
                .OrderBy(x => x.FullName)
                .ToList();
            var result = string.Join(Environment.NewLine, authors.Select(x => x.FullName));
            return result;
        }
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(x => EF.Functions.Like(x.Title.ToLower(), $"%{input.ToLower()}%"))
                .Select(x => new
                {
                    x.Title
                })
                .OrderBy(x => x.Title)
                .ToList();
            var result = string.Join(Environment.NewLine, books.Select(x => x.Title));
            return result;
            // Return the titles of the book, which contain a given string.Ignore casing.
            // Return all titles in a single string, each on a new row ordered alphabetically.
            //var books = context.Books
            //     .Select(x => new
            //     {
            //         Title = x.Title
            //     })
            //     .OrderBy(x => x.Title)
            //     .ToList();
            //char[] chars = input.ToLower().ToCharArray();
            //StringBuilder result = new StringBuilder();
            //foreach (var symb in chars)
            //{
            //    foreach (var book in books)
            //    {
            //        if (book.Title.ToLower().Contains(symb))
            //        {
            //            result.AppendLine(book.Title);
            //        }
            //    }
            //}
            ////var result = string.Join(Environment.NewLine, books.Select(x => x.Title));
            //return result.ToString().TrimEnd();
        }
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            //Return all titles of books and their authors' names for books, which are written by authors whose last names start with the given string.
            //Return a single string with each title on a new row.Ignore casing.Order by book id ascending.
            var books = context.Books
                .Where(x => EF.Functions.Like(x.Author.LastName.ToLower(), $"{input.ToLower()}%"))
                .Select(x => new
                {
                    BookId = x.BookId,
                    Title = x.Title,
                    AuthorFullName = x.Author.FirstName + " " + x.Author.LastName,
                })
                .OrderBy(x => x.BookId)
                .ToList();
            var result = string.Join(Environment.NewLine, books.Select(x => $"{x.Title} ({x.AuthorFullName})"));
            return result;
        }
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var books = context.Books
                .Select(x => new
                {
                    XCount = x.Title
                })
                .ToList();
            int counter = 0;
            foreach (var book in books)
            {
                if (book.XCount.Length > lengthCheck)
                {
                    counter++;
                }
            }
            return counter;
        }
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors
                .Select(x => new
                {
                    FullName = x.FirstName + " " + x.LastName,
                    TotalBooks = x.Books.Sum(x => x.Copies)
                })
                .OrderByDescending(x => x.TotalBooks)
                .ToList();
            var result = string.Join(Environment.NewLine, authors.Select(x => $"{x.FullName} - {x.TotalBooks}"));
            return result;
        }
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            //Return the total profit of all books by category.Profit for a book can be calculated
            //by multiplying its number of copies by the price per single book.Order the results
            //by descending by total profit for a category and ascending by category name.


            var books = context.Categories
                  .Select(x => new
                  {
                      Category = x.Name,
                      Profit = x.CategoryBooks.Sum(x => x.Book.Copies * x.Book.Price)
                  })
                  .OrderByDescending(x => x.Profit)
                  .ThenBy(x => x.Category);
            var result = string.Join(Environment.NewLine, books.Select(x => $"{x.Category} ${x.Profit:f2}"));
            return result;
        }
        public static string GetMostRecentBooks(BookShopContext context)
        {
            //Get the most recent books by categories.
            //The categories should be ordered by name alphabetically.
            //Only take the top 3 most recent books from each category -ordered by release date(descending.
            //Select and print the category name and for each book – its title and release year.
            var categories = context.Categories
                .Select(x => new
                {
                    CategoryName = x.Name,
                    Books = x.CategoryBooks.Select(x => new
                    {
                        BookName = x.Book.Title,
                        Date = x.Book.ReleaseDate
                    })
                    .OrderByDescending(x => x.Date.Value)
                    .Take(3)
                    .ToList()
                })
                .OrderBy(x => x.CategoryName);
            var result = new StringBuilder();
            foreach (var category in categories)
            {
                result.AppendLine($"--{category.CategoryName}");
                foreach (var book in category.Books)
                {
                    result.AppendLine($"{book.BookName} ({book.Date.Value.Year})");
                }
            }
            return result.ToString().TrimEnd();
        }
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.ReleaseDate.Value.Year < 2010)
                .ToList();
            foreach (var book in books)
            {
                book.Price += 5;
            }
            context.SaveChanges();
        }
        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.Copies < 4200)
                .ToList();
            context.Books.RemoveRange(books);
            context.SaveChanges();
            return books.Count;
        }
    }
}
