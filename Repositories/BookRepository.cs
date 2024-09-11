using LMS.Data;
using LMS.Models;
using LMS.Repositories.Read;
using LMS.Repositories.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Repositories
{
    internal class BookRepository : IBookReadRepository, IBookWriteRepository
    {
        private readonly LibraryContext? _libraryContext;
        public BookRepository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public void Add(Book entity)
        {
            if (_libraryContext != null)
            {
                _libraryContext.Books.Add(entity);
                _libraryContext.SaveChanges();
            }
            throw new ArgumentNullException(nameof(entity));
        }
        public void ChangeBookCount(int Id, int count)
        {
            if (_libraryContext != null && count > 0)
            {
                Book? book = _libraryContext.Books.FirstOrDefault(book => book.BookID == Id);
                if (book != null)
                {
                    book.Count = count;
                    _libraryContext.Books.Update(book);
                    _libraryContext.SaveChanges();
                }
            }
        }
        public void Delete(Book entity)
        {
            if (_libraryContext != null)
            {
                _libraryContext.Books.Remove(entity);
                _libraryContext.SaveChanges();
            }
            throw new ArgumentNullException(nameof(_libraryContext));
        }
        public void DeleteBookById(int Id)
        {
            if (_libraryContext != null)
            {
                Book? book = _libraryContext.Books.FirstOrDefault(book => book.BookID == Id);
                if (book != null)
                {
                    _libraryContext.Books.Remove(book);
                    _libraryContext.SaveChanges();
                }
            }
            throw new ArgumentNullException(nameof(_libraryContext));
        }
        public IEnumerable<Book> GetAll() => _libraryContext.Books.ToList();
        public IEnumerable<Book> GetAllBooksByPrice()
        {
            if (_libraryContext != null)
            {
                return _libraryContext.Books.ToList();
            }
            throw new ArgumentNullException(nameof(_libraryContext));
        }
        public Book GetBookByAuthor(string author)
        {
            if (_libraryContext != null)
            {
                Book? book = _libraryContext.Books.FirstOrDefault(book => book.Author == author);
                if (book != null)
                {
                    _libraryContext.Books.Remove(book);
                    _libraryContext.SaveChanges();
                }
            }
            throw new ArgumentNullException(nameof(_libraryContext));
        }
        public Book GetBookByID(int id)
        {
            if (_libraryContext != null)
            {
                Book? book = _libraryContext.Books.FirstOrDefault(book => book.BookID == id);
                if (book != null)
                {
                    return book;
                }
            }
            throw new ArgumentNullException(nameof(_libraryContext));
        }
        public void UpdatePrice(int Id, decimal Price)
        {
            if (_libraryContext != null && Price > 0)
            {
                Book? book = _libraryContext.Books.FirstOrDefault(book => book.BookID == Id);
                if (book != null)
                {
                    book.Price = Price;
                    _libraryContext.Books.Update(book);
                }
            }
        }
    }
}