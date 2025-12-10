using MISA.QTSX.CORE.misaatribiute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QTSX.CORE.entities
{
    /* 
    * Ca làm việc
      Create by Phi Hoang Anh 12/4/2025
   */
    [MisaTable("shift")]
    public class Shift
    {
        // id ca làm việc
        [MisaColumnName("shift_id")]
        public Guid ShiftId { get; set; }
        //Mã ca làm việc
        [MisaColumnName("shift_code")]
        [Required(ErrorMessage ="Mã ca làm việc không được trống")]
        [MaxLength(20,ErrorMessage ="Mã ca tối đa là 20 ký tự")]
        public string ShiftCode { get; set; }
        //Tên ca làm việc
        [MisaColumnName("shift_name")]
        [Required(ErrorMessage = "Tên ca làm việc không được trống")]
        [MaxLength(50, ErrorMessage = "Mã ca tối đa là 50 ký tự")]
        public string ShiftName { get; set; }
        // Giờ bắt đầu làm
        [MisaColumnName("begin_shift_time")]
        [Required(ErrorMessage ="Giời vào ca không được trống")]
        public TimeSpan BeginShiftTime { get; set; }
        //Giờ kết thúc làm
        [MisaColumnName("end_shift_time")]
        [Required(ErrorMessage ="Giờ tan ca không được trống")]
        public TimeSpan EndShiftTime { get; set; }

        //Giờ bắt đầu nghỉ
        [MisaColumnName("begin_break_time")]
        public TimeSpan? BeginBreakTime { get; set; }
        //Giờ kết thúc nghỉ
        [MisaColumnName("end_break_time")]
        public TimeSpan? EndBreakTime { get; set; }
        //Số giờ làm việc
        [MisaColumnName("working_time")]
        public double WorkingTime { get; set; }
        // Số giờ nghỉ
        [MisaColumnName("breaking_time")]
        public double BreakingTime { get; set; }
        //Trạng thái sử dụng : true là sử dụng , false là không sử dụng
        [MisaColumnName("inactive")]
        public bool Inactive { get; set; }
        // Tên Người tạo
        [MisaColumnName("created_by")]
        public string CreatedBy { get; set; }
        // Thời gian tạo
        [MisaColumnName("created_date")]
        public DateTime CreatedDate { get; set; }
        // Tên Người sửa
        [MisaColumnName("modified_by")]
        public string ModifiedBy { get; set; }
        // Thời gian sửa
        [MisaColumnName("modified_date")]
        public DateTime ModifiedDate { get; set; }
    }
}
