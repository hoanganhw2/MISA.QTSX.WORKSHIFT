using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTSX.CORE.dtos
{
    public class ShiftDto
    {
        // id ca làm việc
        public Guid? ShiftId { get; set; }
        //Mã ca làm việc
        public string ShiftCode { get; set; }
        //Tên ca làm việc
        public string ShiftName { get; set; }
        // Giờ bắt đầu làm
        public DateTime BeginShiftTime { get; set; }
        //Giờ kết thúc làm
        public DateTime EndShiftTime { get; set; }
        //Giờ bắt đầu nghỉ
        public DateTime BeginBreakTime { get; set; }
        //Giờ kết thúc nghỉ
        public DateTime EndBreakTime { get; set; }
        //Số giờ làm việc
        public double WorkingTime { get; set; }
        // Số giờ nghỉ
        public double BreakingTime { get; set; }
        //Trạng thái sử dụng : true là sử dụng , false là không sử dụng
        public bool Inactive { get; set; }
        // Tên Người tạo
        public string CreateBy { get; set; }
        // Thời gian tạo
        public DateTime CreateDate { get; set; }
        // Tên Người sửa
        public string ModifiedBy { get; set; }
        // Thời gian sửa
        public DateTime ModifiedDate { get; set; }
    }
}
