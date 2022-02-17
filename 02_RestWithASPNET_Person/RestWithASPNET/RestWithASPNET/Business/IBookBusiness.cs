using RestWithASPNET.Data.DTO;
using RestWithASPNET.HyperMedia.Utils;
using System.Collections.Generic;

namespace RestWithASPNET.Business
{
    public interface IBookBusiness
    {
        BookDTO Create(BookDTO book);
        BookDTO FindByID(long id);
        List<BookDTO> FindAll();
        PagedSearchDTO<BookDTO> SearchPaged(string book, string sortDirections, int pageSize, int page);
        BookDTO Update(BookDTO book);
        void Delete(long id);
    }
}