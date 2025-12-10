<template>
  <div class="main-right flex-1 d-flex flex-col">
    <div class="main-view d-flex flex-col">
      <div class="page-padding layout-list">
        <div class="flex-row d-flex title-box">
          <div class="list-title d-flex">Ca làm việc</div>
          <div class="main-button">
            <ms-button styleButton="primary" @openPopUpAdd="onOpenPopUpAdd">
              <ms-icon class="mi2 icon16 icon-add-white">&nbsp;</ms-icon>
              <div class="button-text">Thêm</div>
            </ms-button>
          </div>
        </div>
        <div class="body-layout-list">
          <div class="body-list">
            <!---->
            <div class="form-list d-flex flex-col">
              <div class="d-flex flex-col">
                <div class="condition-box d-flex flex-row">
                  <div class="d-flex control-gap align-items-center">
                    <ms-input
                      v-model="searchQuery"
                      iconPrefix="mi2 icon16 icon-search left"
                      placeholder="Tìm kiếm"
                      type="text"
                    ></ms-input>
                    <!--Div1 - Feature batch (ẩn)-->
                    <div v-show="selectedItems.length > 0" class="feature-batch">
                      <div class="selected-count">
                        <span class="bold">{{ selectedItems.length }}</span>
                      </div>
                      <div class="unselected" @click="onUnselectAll">Bỏ chọn</div>
                      <ms-button style-button="btn-outline-success" @click="onBatchActivate">
                        <ms-icon class="mi2 icon16 icon-active green"></ms-icon>
                        <div class="button-text green">Sử dụng</div>
                      </ms-button>
                      <ms-button style-button="btn-outline-danger" @click="onBatchDeactivate">
                        <ms-icon class="mi2 icon16 icon-inactive red"></ms-icon>
                        <div class="button-text red">Ngưng sử dụng</div>
                      </ms-button>
                      <ms-button style-button="btn-outline-danger" @click="onBatchDelete">
                        <ms-icon class="mi2 icon16 icon-trash--red red"></ms-icon>
                        <div class="button-text red">Xóa</div>
                      </ms-button>
                    </div>
                    <!--end Div1-->
                  </div>
                  <!--Div2 - Action buttons-->

                  <div v-show="selectedItems.length === 0" class="action flex-row">
                    <ms-button @click="onExport" v-tooltip.top="'Xuất Excel'">
                      <ms-icon class="icon-export icon16 mi2"></ms-icon>
                    </ms-button>
                    <ms-button @click="onRefresh" v-tooltip.top="'Tải lại dữ liệu'">
                      <ms-icon class="icon-reload icon16 mi2"></ms-icon>
                    </ms-button>
                  </div>
                  <!--end Div2-->
                </div>
              </div>
            </div>
            <!---->
            <div class="voucher-body-grid">
              <ms-grid-table
                ref="gridTable"
                :data="filteredShifts"
                :columns="shiftColumns"
                data-key="shiftCode"
                v-model="selectedItems"
                :loading="isLoading"
                @edit="onEditShift"
                @delete="onDeleteShift"
                @duplicate="onDuplicateShift"
                @changeInactive="onChangeInactive"
                @selectionChange="onSelectionChange"
              ></ms-grid-table>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

  <!-- Popup thêm/sửa ca làm việc -->
  <PopupShift
    v-model="showPopup"
    :is-edit="isEdit"
    :shift-data="editingShift"
    @save="onSaveShift"
    @saveAndAdd="onSaveAndAddShift"
  />

  <!-- Show dialog xác nhận xóa nhiều -->
  <MsAlert
    :visible="showDeleteConfirm"
    title="Xác nhận xóa"
    message="Bạn có chắc chắn muốn xóa ca làm việc này không?"
    alertIcon="icon-warning"
    :show-confirm="true"
    @close="showDeleteConfirm = false"
    @confirm="confirmDeleteShift"
  />

  <!-- Show dialog xác nhận xóa 1 -->
  <MsAlert
    :visible="showBatchDeleteConfirm"
    title="Xác nhận xóa nhiều"
    :message="`Bạn có chắc chắn muốn xóa ${selectedItems.length} ca làm việc đã chọn?`"
    alertIcon="icon-warning"
    :show-confirm="true"
    @close="showBatchDeleteConfirm = false"
    @confirm="confirmBatchDelete"
  />
</template>
<script setup>
import MsIcon from '@/components/MsIcon.vue'
import MsButton from '@/components/MsButton.vue'
import MsInput from '@/components/MsInput.vue'
import MsGridTable from '@/components/MsGridTable.vue'
import PopupShift from './PopupShift.vue'
import ShiftApi from '@/apis/components/ShiftApi'
import { useToast } from 'primevue/usetoast'
import MsAlert from '@/components/MsAlert.vue'
import { ref, computed, onMounted } from 'vue'

// Popup state
const showPopup = ref(false)
const isEdit = ref(false)
const editingShift = ref(null)

// Selected items for batch actions
const selectedItems = ref([])

// Loading state
const isLoading = ref(false)

// Delete confirm dialog state
const showDeleteConfirm = ref(false)
const showBatchDeleteConfirm = ref(false)
const deletingShiftId = ref(null)

// Reference to grid table
const gridTable = ref(null)

// Danh sách ca làm việc
const shifts = ref([])

// Search query
const searchQuery = ref('')

// Filtered shifts - lọc theo mã ca và tên ca
const filteredShifts = computed(() => {
  if (!searchQuery.value.trim()) {
    return shifts.value
  }
  const query = searchQuery.value.toLowerCase().trim()
  return shifts.value.filter((shift) => {
    const shiftCode = (shift.shiftCode || '').toLowerCase()
    const shiftName = (shift.shiftName || '').toLowerCase()
    return shiftCode.includes(query) || shiftName.includes(query)
  })
})

// Refresh data - load lại dữ liệu
const onRefresh = async () => {
  isLoading.value = true
  try {
    const res = await ShiftApi.getAll()
    shifts.value = res.data.data
  } catch (error) {
    console.error('Load data failed:', error)
  } finally {
    isLoading.value = false
  }
}

// Export to CSV
const onExport = () => {
  if (gridTable.value) {
    gridTable.value.exportCSV()
  }
}

// Columns config cho table
const shiftColumns = ref([
  { field: 'ShiftId', header: 'ID', style: 'display: none' },
  { field: 'shiftCode', header: 'Mã ca', sortable: true, style: 'min-width: 100px' },
  { field: 'shiftName', header: 'Tên ca', sortable: true, style: 'width: 250px' },
  {
    field: 'beginShiftTime',
    header: 'Giờ vào ca',
    sortable: true,
    type: 'time',
    style: 'min-width: 130px',
  },
  {
    field: 'endShiftTime',
    header: 'Giờ hết ca',
    sortable: true,
    type: 'time',
    style: 'min-width: 130px',
  },
  {
    field: 'beginBreakTime',
    header: 'Bắt đầu nghỉ giữa ca',
    type: 'time',
    emptyText: '-',
    style: 'min-width: 200px',
  },
  {
    field: 'endBreakTime',
    header: 'Kết thúc nghỉ giữa ca',
    sortable: true,
    type: 'time',
    emptyText: '-',
    style: 'min-width: 200px',
  },
  {
    field: 'workingTime',
    header: 'Thời gian làm việc(giờ)',
    sortable: true,
    type: 'number',
    style: 'width: 200px;text-align: right',
  },
  {
    field: 'breakingTime',
    header: 'Thời gian nghỉ giữa ca(giờ)',
    sortable: true,
    type: 'number',
    style: 'width: 210px;text-align: right',
  },
  {
    field: 'inactive',
    header: 'Trạng thái',
    type: 'status',
    statusField: 'inactive',
    activeText: 'Ngừng sử dụng',
    inactiveText: 'Đang sử dụng',
    style: 'width: 210px',
  },
  {
    field: 'createdBy',
    header: 'Người tạo',
    style: 'min-width: 160px;white-space:nowrap;text-overflow:ellipsis;overflow:hidden',
  },
  {
    field: 'createdDate',
    header: 'Ngày tạo',
    sortable: true,
    type: 'date',
    style: 'min-width: 160px;',
  },
  {
    field: 'modifiedBy',
    header: 'Người sửa',
    style: 'min-width: 160px;white-space:nowrap;text-overflow:ellipsis;overflow:hidden',
  },
  {
    field: 'modifiedDate',
    header: 'Ngày sửa',
    sortable: true,
    type: 'date',
    style: 'min-width: 160px;',
  },
])
/*
goi api danh sach ca lam viec

*/
onMounted(async () => {
  isLoading.value = true
  try {
    const res = await ShiftApi.getAll()
    shifts.value = res.data.data
    console.log('load dữ liệu thành công')
  } catch (error) {
    console.error('Load data failed:', error)
  } finally {
    isLoading.value = false
  }
})

// Mở popup thêm mới
const onOpenPopUpAdd = () => {
  isEdit.value = false
  editingShift.value = null
  showPopup.value = true
}

// Toast
const toast = useToast()

// Lưu ca làm việc (thêm mới hoặc cập nhật)
const onSaveShift = async (shiftData) => {
  try {
    if (isEdit.value) {
      // Cập nhật ca làm việc - lấy shiftId từ editingShift ban đầu
      const shiftId = editingShift.value.shiftId
      console.log('Updating shift with ID:', shiftId, 'Data:', shiftData)
      await ShiftApi.updateShift(shiftId, shiftData)
      toast.add({
        position: 'top-center',
        severity: 'success',
        summary: 'Thành công',
        detail: 'Cập nhật ca làm việc thành công',
        life: 3000,
      })
    } else {
      // Thêm mới ca làm việc
      const response = await ShiftApi.add(shiftData)
      if (response.data.code === 201) {
        toast.add({
          position: 'top-center',
          severity: 'success',
          summary: 'Thành công',
          detail: 'Thêm ca làm việc thành công',
          life: 3000,
        })
      }
    }
    // Refresh data
    onRefresh()
  } catch (error) {
    console.log('Error:', error)
    // Kiểm tra nếu là lỗi 409 (Conflict - trùng mã ca)
    if (error.response && error.response.status === 409) {
      toast.add({
        position: 'top-center',
        severity: 'error',
        summary: 'Lỗi',
        detail: error.response.data?.data || 'Mã ca đã tồn tại',
        life: 3000,
      })
    } else {
      // Các lỗi khác
      toast.add({
        position: 'top-center',
        severity: 'error',
        summary: 'Lỗi',
        detail: error.response?.data?.data || 'Có lỗi xảy ra khi lưu ca làm việc',
        life: 3000,
      })
    }
  }
}

// Lưu và thêm ca làm việc - giữ popup mở để tiếp tục nhập
const onSaveAndAddShift = async (shiftData) => {
  try {
    // Thêm mới ca làm việc
    const response = await ShiftApi.add(shiftData)
    console.log(response)
    toast.add({
      position: 'top-center',
      severity: 'success',
      summary: 'Thành công',
      detail: 'Thêm ca làm việc thành công',
      life: 3000,
    })
    // Refresh data nhưng không đóng popup
    onRefresh()
  } catch (error) {
    console.log('Error:', error)
    // Kiểm tra nếu là lỗi 409 (Conflict - trùng mã ca)
    if (error.response && error.response.status === 409) {
      toast.add({
        position: 'top-center',
        severity: 'error',
        summary: 'Lỗi',
        detail: error.response.data?.data || 'Mã ca đã tồn tại',
        life: 3000,
      })
    } else {
      // Các lỗi khác
      toast.add({
        position: 'top-center',
        severity: 'error',
        summary: 'Lỗi',
        detail: error.response?.data?.data || 'Có lỗi xảy ra khi lưu ca làm việc',
        life: 3000,
      })
    }
  }
}

// Sửa ca làm việc - mở popup với dữ liệu để sửa
const onEditShift = (shiftData) => {
  console.log('Edit shift data from table:', shiftData)
  console.log('shiftId field:', shiftData.shiftId)
  isEdit.value = true
  editingShift.value = shiftData
  showPopup.value = true
}

// Nhân bản ca làm việc - mở popup thêm mới với dữ liệu copy, để trống mã ca
const onDuplicateShift = (shiftData) => {
  console.log('Duplicate shift data:', shiftData)
  isEdit.value = false // Chế độ thêm mới, không phải sửa
  // Copy dữ liệu nhưng để trống mã ca để nhập mới
  editingShift.value = {
    ...shiftData,
    shiftCode: '', // Để trống mã ca
    shiftId: null, // Xóa ID để tạo mới
  }
  showPopup.value = true
}
// sửa trạng thái của ca làm việc trong 1 ròng được chọn
const onChangeInactive = async (shiftData, inactive) => {
  console.log(inactive)
  console.log(shiftData)
  // goi api cap nhat
  //gán lại active
  shiftData.inactive = inactive
  try {
    let response = await ShiftApi.updateShift(shiftData.shiftId, shiftData)
    if (response.status === 200) {
      toast.add({
        position: 'top-center',
        severity: 'success',
        summary: 'Thành công',
        detail: 'Cập nhật trạng thái ca làm việc thành công',
        life: 3000,
      })
      onRefresh()
    }
  } catch (error) {
    console.log('Error:', error)
    // Kiểm tra nếu là lỗi 409 (Conflict - trùng mã ca)
    if (error.response && error.response.status === 409) {
      toast.add({
        position: 'top-center',
        severity: 'error',
        summary: 'Lỗi',
        detail: error.response.data?.data || 'Mã ca đã tồn tại',
        life: 3000,
      })
    } else {
      // Các lỗi khác
      toast.add({
        position: 'top-center',
        severity: 'error',
        summary: 'Lỗi',
        detail: error.response?.data?.data || 'Có lỗi xảy ra khi lưu ca làm việc',
        life: 3000,
      })
    }
  }
}
// Selection change handler
const onSelectionChange = (items) => {
  selectedItems.value = items || []
}

// Bỏ chọn tất cả
const onUnselectAll = () => {
  selectedItems.value = []
}

// Batch activate - Sử dụng các items đã chọn
const onBatchActivate = async () => {
  if (selectedItems.value.length === 0) {
    toast.add({
      position: 'top-center',
      severity: 'warn',
      summary: 'Cảnh báo',
      detail: 'Vui lòng chọn ít nhất một ca làm việc',
      life: 3000,
    })
    return
  }

  try {
    // Gửi toàn bộ danh sách shift objects
    const shifts = selectedItems.value

    // Gọi API cập nhật trạng thái inactive = false (đang sử dụng)
    const response = await ShiftApi.updateShiftInactive(shifts, true)

    if (response.status === 200) {
      toast.add({
        position: 'top-center',
        severity: 'success',
        summary: 'Thành công',
        detail: 'Đã cập nhật trạng thái thành công',
        life: 3000,
      })
      // Reset selection và refresh data
      selectedItems.value = []
      onRefresh()
    }
  } catch (error) {
    console.log('Error:', error)
    toast.add({
      position: 'top-center',
      severity: 'error',
      summary: 'Lỗi',
      detail: error.response?.data?.data || 'Có lỗi xảy ra khi cập nhật trạng thái',
      life: 3000,
    })
  }
}

// Batch deactivate - Ngưng sử dụng các items đã chọn
const onBatchDeactivate = async () => {
  if (selectedItems.value.length === 0) {
    toast.add({
      position: 'top-center',
      severity: 'warn',
      summary: 'Cảnh báo',
      detail: 'Vui lòng chọn ít nhất một ca làm việc',
      life: 3000,
    })
    return
  }

  try {
    // Gửi toàn bộ danh sách shift objects
    const shifts = selectedItems.value

    // Gọi API cập nhật trạng thái inactive = true (ngưng sử dụng)
    const response = await ShiftApi.updateShiftInactive(shifts, false)

    if (response.status === 200) {
      toast.add({
        position: 'top-center',
        severity: 'success',
        summary: 'Thành công',
        detail: 'Đã ngưng sử dụng thành công',
        life: 3000,
      })
      // Reset selection và refresh data
      selectedItems.value = []
      onRefresh()
    }
  } catch (error) {
    console.log('Error:', error)
    toast.add({
      position: 'top-center',
      severity: 'error',
      summary: 'Lỗi',
      detail: error.response?.data?.data || 'Có lỗi xảy ra khi cập nhật trạng thái',
      life: 3000,
    })
  }
}

// Batch delete - Xóa các items đã chọn
const onBatchDelete = () => {
  if (selectedItems.value.length === 0) {
    toast.add({
      position: 'top-center',
      severity: 'warn',
      summary: 'Cảnh báo',
      detail: 'Vui lòng chọn ít nhất một ca làm việc',
      life: 3000,
    })
    return
  }

  // Hiển thị MsAlert confirm dialog
  showBatchDeleteConfirm.value = true
}

// Xác nhận xóa nhiều ca làm việc
const confirmBatchDelete = async () => {
  showBatchDeleteConfirm.value = false

  try {
    // Gửi toàn bộ danh sách shift objects
    const shifts = selectedItems.value

    // Gọi API xóa nhiều ca làm việc
    const response = await ShiftApi.deleteShifts(shifts)

    if (response.status === 200 || response.data.code === 200) {
      toast.add({
        position: 'top-center',
        severity: 'success',
        summary: 'Thành công',
        detail: `Đã xóa thành công các ca làm việc`,
        life: 3000,
      })
      // Reset selection và refresh data
      selectedItems.value = []
      onRefresh()
    }
  } catch (error) {
    console.log('Error:', error)
    toast.add({
      position: 'top-center',
      severity: 'error',
      summary: 'Lỗi',
      detail: error.response?.data?.data || 'Có lỗi xảy ra khi xóa ca làm việc',
      life: 3000,
    })
  }
}
// ham xoa ca lam viec - show confirm dialog truoc
const onDeleteShift = (shiftId) => {
  console.log('Delete shift with ID:', shiftId)
  deletingShiftId.value = shiftId
  showDeleteConfirm.value = true
}

// Xac nhan xoa ca lam viec
const confirmDeleteShift = async () => {
  showDeleteConfirm.value = false

  try {
    let response = await ShiftApi.deleteShift(deletingShiftId.value)
    if (response.data.code === 200) {
      toast.add({
        position: 'top-center',
        severity: 'success',
        summary: 'Thành công',
        detail: 'Xóa ca làm việc thành công',
        life: 3000,
      })
      onRefresh()
    }
  } catch (error) {
    console.log('Error:', error)
    toast.add({
      position: 'top-center',
      severity: 'error',
      summary: 'Lỗi',
      detail: error.response?.data?.data || 'Có lỗi xảy ra khi xóa ca làm việc',
      life: 3000,
    })
  } finally {
    deletingShiftId.value = null
  }
}
</script>

<style scoped>
.main-right {
  background-color: #fff;
  width: calc(100% - 234px);
  height: 100%;
}
.main-right .main-view {
  background-color: #e5e7eb;
  height: 100%;
}
.page-padding {
  padding: 16px 20px 20px;
}
.layout-list {
  display: flex;
  flex-direction: column;
  height: 100%;
}
.title-box {
  justify-content: space-between;
  align-items: center;
  margin-bottom: 16px;
}
.title-box .list-title {
  font-size: 24px;
  font-weight: 700;
  font-family: Inter;
  color: #111827;
}
.main-button .text {
  font-weight: 500;
}
.layout-list .body-layout-list {
  flex: 1;
  height: 0;
}
.body-list {
  background-color: #fff;
  border-radius: 4px;
  overflow: hidden;
  height: 100%;
  display: flex;
  flex-direction: column;
}

.body-list .voucher-body-grid {
  flex: 1;
  height: 0;
}
.form-list .condition-box {
  padding: 8px 16px;
  background-color: #fff;
  border-top-left-radius: 4px;
  border-top-right-radius: 4px;
}
.control-gap {
  gap: 8px;
}
.form-list .action {
  margin: auto 0 auto auto;
  z-index: 1;
  column-gap: 8px;
}
.feature-batch {
  background-color: #fff;
  display: flex;
  align-items: center;
  gap: 8px;
  margin-left: 8px;
  height: 28px;
}
.selected-count::before {
  content: 'Đã chọn';
  margin-right: 0.25rem;
}
.bold {
  font-weight: 700;
}
.feature-batch .unselected {
  color: #009b71;
  cursor: pointer;
  margin: 0 8px;
}
.feature-batch .unselected:hover {
  text-decoration: underline;
}
.button-text {
  font-family: Inter;
  margin-left: 0.25rem;
}
.icon-active.green {
  background-color: #009b71;
}
.icon-inactive.red,
.icon-trash--red.red {
  background-color: #dc2626;
}
</style>
