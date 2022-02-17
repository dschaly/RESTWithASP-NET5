using RestWithASPNET.Data.Converter.Implementations;
using RestWithASPNET.Data.DTO;
using RestWithASPNET.HyperMedia.Utils;
using RestWithASPNET.Model;
using RestWithASPNET.Repository;
using System.Collections.Generic;

namespace RestWithASPNET.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {

        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        // Method responsible for returning all people,
        public List<BookDTO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }
        public PagedSearchDTO<BookDTO> SearchPaged(string book, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc")) ? "asc" : "desc";
            var size = pageSize < 1 ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            var query = @"SELECT * FROM books B WHERE 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(book)) query += $"AND B.Title LIKE '%{book}%' ";
            query += $"ORDER BY B.Title {sort} LIMIT {size} OFFSET {offset}";

            string countQuery = @"SELECT COUNT(*) FROM Books B WHERE 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(book)) query += $"AND B.Title LIKE '%{book}%'";

            var books = _repository.FindPaged(query);
            var booksCount = _repository.GetCount(countQuery);

            return new PagedSearchDTO<BookDTO> {
                CurrentPage = page,
                List = _converter.Parse(books),
                PageSize = size,
                TotalResults = booksCount
            };
        }

        // Method responsible for returning one book by ID
        public BookDTO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }

        // Method responsible to crete one new book
        public BookDTO Create(BookDTO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Create(bookEntity);
            return _converter.Parse(bookEntity);
        }

        // Method responsible for updating one book
        public BookDTO Update(BookDTO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Update(bookEntity);
            return _converter.Parse(bookEntity);
        }

        // Method responsible for deleting a book from an ID
        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}