using MISA.QTSX.CORE.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTSX.CORE.interfaces.repository
{
    public interface IShiftRepository:IBaseRepository<Shift>
    {
        bool CheckDuplicate(Shift shift);

      
          
    }
}
