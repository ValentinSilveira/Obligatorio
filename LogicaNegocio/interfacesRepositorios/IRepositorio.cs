using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.interfacesRepositorios
{
    public interface IRepositorio<T>
    {
        void Add(T item);
        void Delete(T item);
        void Update(T item);
        T FindById(int id);
        IEnumerable<T> FindAll();
    }
}
