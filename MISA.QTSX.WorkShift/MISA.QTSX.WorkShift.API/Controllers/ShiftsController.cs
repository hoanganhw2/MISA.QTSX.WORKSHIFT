using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Dapper;
using MySqlConnector;
using MISA.QTSX.CORE.interfaces.services;
using MISA.QTSX.CORE.entities;
using MISA.QTSX.WorkShift.API.Response;

namespace MISA.QTSX.WorkShift.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        private readonly IShiftService _shiftService;
        public ShiftsController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            try
            {
                MisaReponse<List<Shift>> reponse = new MisaReponse<List<Shift>>();
                var data = _shiftService.GetShifts();
                reponse.Code = 200;
                reponse.data = data;
                reponse.message = "Tải dữ liệu thành công";
                return Ok(reponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                MisaReponse<Shift> reponse = new MisaReponse<Shift>();
                var data = _shiftService.GetShift(id);
                reponse.Code = 200;
                reponse.data = data;
                reponse.message = "Tải dữ liệu thành công";
                reponse.traceId = HttpContext.TraceIdentifier;
                return Ok(reponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new MisaReponse<string>();
                errorResponse.Code = 500;
                errorResponse.message = "Lỗi kết nối đến Server";
                errorResponse.data = ex.Message;
                errorResponse.traceId = HttpContext.TraceIdentifier;
                return  StatusCode(500,errorResponse);
            }
        }
        [HttpPost]
        public IActionResult Insert(Shift shift)
        {
            // 1. Validate dữ liệu trước
            if (!ModelState.IsValid)
            {
                // lấy ra danh sách lỗi 
                var errors = ModelState
                    .Where(ms => ms.Value?.Errors.Count > 0)
                    .ToDictionary(
                        kv => kv.Key,
                        kv => kv.Value!.Errors.Select(e => e.ErrorMessage).ToList()
                    );

                var errorResponse = new MisaReponse<Dictionary<string, List<string>>>
                {
                    Code = 400,
                    message = "Dữ liệu không hợp lệ",
                    data = errors,
                    traceId = HttpContext.TraceIdentifier
                };

                return StatusCode(400,errorResponse); // HTTP 400
            }

            // 2. Check duplicate
            var data = _shiftService.Insert(shift);

            if (data == null)
            {
                var response = new MisaReponse<string>
                {
                    Code = 409,
                    message = "Lỗi tạo ca làm việc",
                    data = "Mã ca đã tồn tại",
                    traceId = HttpContext.TraceIdentifier
                };

                return StatusCode(409, response); // HTTP 409
            }

            // 3. Thành công
            var successRespone = new MisaReponse<Shift>
            {
                Code = 201,
                message = "Thêm mới thành công",
                data = data,
                traceId = HttpContext.TraceIdentifier
            };

            return StatusCode(201, successRespone); // HTTP 201
        }
        [HttpPut]
        public IActionResult Update(Guid shiftId, Shift shift)
        {
            try
            {
                var data = _shiftService.Update(shiftId, shift);
                var successRespone = new MisaReponse<Shift>
                {
                    Code = 200,
                    message = "Cập nhật thành công",
                    data = data,
                    traceId = HttpContext.TraceIdentifier
                };
                return Ok(successRespone);
            }
            catch (Exception ex)
            {
                var errorResponse = new MisaReponse<string>();
                errorResponse.Code = 500;
                errorResponse.message = "Lỗi kết nối đến Server";
                errorResponse.data = ex.Message;
                errorResponse.traceId = HttpContext.TraceIdentifier;
                return  StatusCode(500,errorResponse);
            }
        }
        [HttpDelete]
        public IActionResult Delete(Guid shiftId)
        {
            try
            {
                _shiftService.Delete(shiftId);
                var successRespone = new MisaReponse<string>
                {
                    Code = 200,
                    message = "Xóa thành công",
                    data = "",
                    traceId = HttpContext.TraceIdentifier
                };
                return Ok(successRespone);
            }
            catch (Exception ex)
            {
                var errorResponse = new MisaReponse<string>();
                errorResponse.Code = 500;
                errorResponse.message = "Lỗi kết nối đến Server";
                errorResponse.data = ex.Message;
                errorResponse.traceId = HttpContext.TraceIdentifier;
                return  StatusCode(500,errorResponse);
            }
        }
        [HttpPut("inactive")]
        public IActionResult UpdateShiftsInactive(List<Shift> shifts, bool inactive) {
            try
            {
                _shiftService.ChangeShiftInactive(shifts, inactive);
                var successRespone = new MisaReponse<string>
                {
                    Code = 200,
                    message = "Cập nhật thành công",
                    data = "Cập nhật thành công",
                    traceId = HttpContext.TraceIdentifier
                };
                return Ok(successRespone);
            }
            catch (Exception ex)
            {
                var errorResponse = new MisaReponse<string>();
                errorResponse.Code = 500;
                errorResponse.message = "Lỗi kết nối đến Server";
                errorResponse.data = ex.Message;
                errorResponse.traceId = HttpContext.TraceIdentifier;
                return  StatusCode(500,errorResponse);
            }  
        }
        // xóa nhiều ca làm việc
        [HttpDelete("all")]
        public IActionResult Deletes([FromBody] List<Shift> shifts)
        {
            try
            {
                _shiftService.DeleteShifts(shifts);
                var successRespone = new MisaReponse<string>
                {
                    Code = 200,
                    message = "Xóa thành công",
                    data = "Xóa thành công",
                    traceId = HttpContext.TraceIdentifier
                };
                return Ok(successRespone);
            }
            catch (Exception ex)
            {
                var errorResponse = new MisaReponse<string>();
                errorResponse.Code = 500;
                errorResponse.message = "Lỗi kết nối đến Server";
                errorResponse.data = ex.Message;
                errorResponse.traceId = HttpContext.TraceIdentifier;
                return  StatusCode(500,errorResponse);
            }

        }
    }
}
