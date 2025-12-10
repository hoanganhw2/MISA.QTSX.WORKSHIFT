using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTSX.CORE.interfaces.repository
{
    public interface IBaseRepository<T>
    {
        List<T> GetAll();
        T Get(Guid id);
        T Insert(T entity);
        T Update(Guid id, T entity);
        bool Delete(Guid id);
    }
}
