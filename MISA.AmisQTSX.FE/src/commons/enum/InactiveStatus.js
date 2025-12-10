/**
 * Enum cho trạng thái Inactive
 * Trong database: true = Ngừng sử dụng, false = Đang sử dụng
 */
export const InactiveStatus = {
  // Đang hoạt động (Inactive = false trong DB)
  ACTIVE: {
    value: false,
    label: 'Đang sử dụng',
    severity: 'success',
  },
  // Ngừng hoạt động (Inactive = true trong DB)
  INACTIVE: {
    value: true,
    label: 'Ngừng sử dụng',
    severity: 'danger',
  },
}

/**
 * Lấy thông tin status từ giá trị Inactive
 * @param {boolean} inactive - Giá trị Inactive từ database
 * @returns {Object} - Object chứa label và severity
 */
export const getInactiveStatus = (inactive) => {
  return inactive ? InactiveStatus.INACTIVE : InactiveStatus.ACTIVE
}

/**
 * Lấy label hiển thị từ giá trị Inactive
 * @param {boolean} inactive - Giá trị Inactive từ database
 * @returns {string} - Label để hiển thị
 */
export const getInactiveLabel = (inactive) => {
  return inactive ? InactiveStatus.INACTIVE.label : InactiveStatus.ACTIVE.label
}

/**
 * Danh sách tất cả status để sử dụng trong dropdown filter
 */
export const InactiveStatusList = [InactiveStatus.ACTIVE, InactiveStatus.INACTIVE]

export default InactiveStatus
