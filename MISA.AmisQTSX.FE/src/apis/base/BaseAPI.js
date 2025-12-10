import api from '../config/APIConfig'

export default class BaseAPI {
  constructor() {
    this.controller = null
  }

  /* Phương thức lấy tất cả dữ liệu */
  getAll() {
    return api.get(this.controller)
  }

  /**
   *Hàm lấy dữ liệu phân trang
   *@param {*} playlaod
   **/
  /**
   * Hàm lấy bản ghi theo id
   * @param {*} id
   */
  get(id) {
    return api.get(`${this.controller}/${id}`)
  }

  /** Hàm thêm dữ liệu
   * @param {*} payload
   */
  add(payload) {
    return api.post(`${this.controller}`, payload)
  }
  paging(payload) {
    return api.post(`${this.controller}/paging`, payload)
  }
  /**
   * Hàm cập nhật dữ liệu
   * @param {*} id
   * @param {*} body
   */
  update(body) {
    return api.put(`${this.controller}`, body)
  }
  /**
   * Hàm xóa bản ghi
   * @param {*} id
   */
  delete(id) {
    return api.delete(`${this.controller}/${id}`)
  }
}
