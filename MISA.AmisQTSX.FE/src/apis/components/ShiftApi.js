import api from '@/apis/config/APIConfig'
import BaseApi from '@/apis/base/BaseAPI'
class ShiftApi extends BaseApi {
  constructor() {
    super()
    this.controller = 'shifts'
  }
  /**
   * Cập nhật thông tin ca làm việc.
   *
   * @param {Guid} shiftId - ID của ca làm việc cần cập nhật.
   * @param {Object} shiftData - Dữ liệu ca làm việc cần cập nhật.
   * @returns Kết quả từ API sau khi thực hiện cập nhật.
   */

  updateShift(shiftId, shiftData) {
    return api.put(`${this.controller}/?shiftId=${shiftId}`, shiftData)
  }
  /**
   * Xóa ca làm việc.
   *
   * @param {Guid} shiftId - ID của ca làm việc cần xóa.
   * @returns Kết quả từ API sau khi thực hiện xóa.
   */
  deleteShift(shiftId) {
    return api.delete(`${this.controller}/?shiftId=${shiftId}`)
  }
  /**
   * Cập nhật trạng thái ca làm việc (batch).
   *
   * @param {Array} shifts - Danh sách shift objects cần cập nhật.
   * @param {boolean} inactive - Trạng thái cần cập nhật (true để ngưng sử dụng, false để sử dụng).
   * @returns Kết quả từ API sau khi thực hiện cập nhật.
   */
  updateShiftInactive(shifts, inactive) {
    return api.put(`${this.controller}/inactive?inactive=${inactive}`, shifts)
  }
  /**
   * Xóa nhiều ca làm việc (batch).
   *
   * @param {Array} shifts - Danh sách các shift objects cần xóa.
   * @returns Kết quả từ API sau khi thực hiện xóa.
   */
  deleteShifts(shifts) {
    return api.delete(`${this.controller}/all`, { data: shifts })
  }
}
export default new ShiftApi()
