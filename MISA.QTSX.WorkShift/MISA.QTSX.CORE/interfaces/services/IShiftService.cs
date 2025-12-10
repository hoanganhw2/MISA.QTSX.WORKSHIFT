using MISA.QTSX.CORE.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTSX.CORE.interfaces.services
{
    public interface IShiftService
    {
        List<Shift> GetShifts();
        Shift GetShift(Guid id);

        Shift Insert(Shift shift);

        Shift Update(Guid shiftId, Shift shift);

        void Delete(Guid shiftId);

        void ChangeShiftInactive(List<Shift> shifts, bool inactive);

        void DeleteShifts(List<Shift> shifts);
        
    }
}
