using MISA.QTSX.CORE.entities;
using MISA.QTSX.CORE.interfaces.repository;
using MISA.QTSX.CORE.interfaces.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTSX.CORE.services
{
    public class ShiftService :IShiftService
    {
        private readonly IShiftRepository _shiftRepository;
        public ShiftService(IShiftRepository shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }
        public List<Shift> GetShifts()
        {
           var data = _shiftRepository.GetAll();
           return data;
        }
        



        public Shift Insert(Shift shift)
        {
            if (!_shiftRepository.CheckDuplicate(shift))
            {
                var data = _shiftRepository.Insert(shift);
                return data;
            }
            return null;
        }

        public Shift Update(Guid shiftId, Shift shift)
        {
            var data = _shiftRepository.Update(shiftId, shift);
            return data;
        }
        public void Delete(Guid shiftId)
        {
           _shiftRepository.Delete(shiftId); 
        }

        public Shift GetShift(Guid id)
        {
            return _shiftRepository.Get(id);
        }
        /// <summary>
        ///Cập nhật  inactive : trạng thái sử dụng của một danh sách ca làm việc
        /// </summary>
        /// <param name="shifts">danh sách ca làm việc cần cập nhật lại trạng thái</param>
        /// <param name="inactive">trạng thái sửa cho toàn bộ ca làm việc (true : đang sử dụng , false : ngưng sử dụng)</param>
        public void ChangeShiftInactive(List<Shift> shifts,bool intactive)
        {
            foreach(var shift in shifts)
            {

                // cập nhật lại inactive
                shift.Inactive = intactive;
                _shiftRepository.Update(shift.ShiftId, shift);

            }
        }
        /// <summary>
        /// Xóa một nhiều ca làm việc
        /// </summary>
        /// <param name="shifts">Các ca làm việc muốn xóa</param>
        public void DeleteShifts(List<Shift> shifts)
        {
            foreach (var shift in shifts)
            {
               var kq = _shiftRepository.Delete(shift.ShiftId);
            }
        }
    }
}
