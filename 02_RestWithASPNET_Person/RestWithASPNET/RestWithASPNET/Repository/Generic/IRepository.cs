using RestWithASPNET.Model.Base;
using System.Collections.Generic;

namespace RestWithASPNET.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindByID(long id);
        List<T> FindAll();
        T Update(T item);
        void Delete(long id);
        bool Exists(long id);
        List<T> FindPaged(string query);
        int GetCount(string query);
    }
}