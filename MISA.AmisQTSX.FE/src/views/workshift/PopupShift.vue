<template>
  <div v-if="showPopup" class="popup-overlay" @click.self="onClose">
    <div class="popup modal-content" ref="modalRef" @keydown="handleKeydown">
      <!-- Header -->
      <div class="popup-header">
        <div class="popup-title">{{ isEdit ? 'Sửa ca làm việc' : 'Thêm ca làm việc' }}</div>
        <div class="d-flex">
          <ms-icon class="mi2 icon20 ic-help cursor-pointer" v-tooltip.top="'Trợ giúp'"></ms-icon>
          <ms-icon
            class="mi2 icon20 icon-close cursor-pointer ml-8"
            v-tooltip.top="'Đóng'"
            @click="onClose"
          ></ms-icon>
        </div>
      </div>

      <!-- Body -->
      <div class="popup-body">
        <div class="form-group">
          <ms-input
            ref="shiftCodeInput"
            v-model="form.ShiftCode"
            type="text"
            style="width: 100%"
            :error="touched.ShiftCode && !form.ShiftCode.trim()"
            error-message="Mã ca không được để trống"
            @blur="touched.ShiftCode = true"
          >
            <slot
              ><div class="d-flex align-items-center" style="width: 150px">
                <div class="label">Mã ca</div>
                <div class="ms-textarea-required">&nbsp;*</div>
              </div>
            </slot>
          </ms-input>
        </div>
        <div class="form-group">
          <ms-input
            v-model="form.ShiftName"
            type="text"
            style="width: 100%"
            :error="touched.ShiftName && !form.ShiftName.trim()"
            error-message="Tên ca không được để trống"
            @blur="touched.ShiftName = true"
          >
            <slot
              ><div class="d-flex align-items-center" style="width: 150px">
                <div class="label">Tên ca</div>
                <div class="ms-textarea-required">&nbsp;*</div>
              </div>
            </slot>
          </ms-input>
        </div>
        <div class="form-group date-picker d-flex w-full">
          <ms-date-picker
            v-model="form.BeginShiftTime"
            type="time"
            class="w-1/2"
            :error="touched.BeginShiftTime && !form.BeginShiftTime"
            error-message="Giờ vào ca không được để trống"
            @blur="touched.BeginShiftTime = true"
          >
            <slot>
              <div class="flex-row" style="width: 150px">
                <label class="label">Giờ vào ca</label>
                <div class="ms-datepicker-required">&nbsp;*</div>
              </div>
            </slot>
          </ms-date-picker>
          <ms-date-picker
            v-model="form.EndShiftTime"
            type="time"
            class="w-1/2"
            style="align-self: flex-end"
            :error="touched.EndShiftTime && !form.EndShiftTime"
            error-message="Giờ ra ca không được để trống"
            @blur="touched.EndShiftTime = true"
          >
            <slot>
              <div class="flex-row" style="width: 150px">
                <label class="label">Giờ ra ca</label>
                <div class="ms-datepicker-required">&nbsp;*</div>
              </div>
            </slot>
          </ms-date-picker>
        </div>
        <div class="form-group date-picker d-flex">
          <ms-date-picker v-model="form.BeginBreakTime" type="time">
            <slot>
              <div class="flex-row" style="width: 150px">
                <label class="label">Bắt đầu nghỉ giữa ca</label>
              </div>
            </slot>
          </ms-date-picker>
          <ms-date-picker v-model="form.EndBreakTime" type="time" class="">
            <slot>
              <div class="flex-row" style="width: 150px">
                <label class="label">Kết thúc nghỉ giữa ca</label>
              </div>
            </slot>
          </ms-date-picker>
        </div>
        <div class="form-group d-flex">
          <ms-input
            v-model="form.WorkingTime"
            class="w-1/2"
            type="text"
            disabled="true"
            width="122px"
          >
            <slot>
              <div class="d-flex align-items-center" style="width: 150px">
                <div class="label">Thời gian làm việc(giờ)</div>
              </div>
            </slot>
          </ms-input>
          <ms-input
            v-model="form.BreakingTime"
            type="text"
            class="w-1/2 justify-content-between"
            disabled="true"
            width="122px"
          >
            <slot>
              <div class="d-flex align-items-center" style="width: 150px">
                <div class="label">Thời gian nghỉ giữa ca(giờ)</div>
              </div>
            </slot>
          </ms-input>
        </div>
        <div class="form-group d-flex flex-row gap-4">
          <div style="width: 150px">Mô tả</div>
          <ms-area-input class="flex-1" v-model="form.Description"></ms-area-input>
        </div>
        <div v-if="isEdit" class="form-group d-flex gap-4">
          <div style="width: 150px">Trạng thái</div>
          <ms-radio-button v-model="form.IsActive" :value="true" name="status"
            >Đang sử dụng</ms-radio-button
          >
          <ms-radio-button v-model="form.IsActive" :value="false" name="status"
            >Ngưng sử dụng</ms-radio-button
          >
        </div>
      </div>

      <!-- Footer -->
      <div class="popup-footer">
        <ms-button class="btn" @click="onClose" v-tooltip.top.focus="'Esc'"
          ><div>Hủy</div></ms-button
        >
        <ms-button class="btn" @click="onSaveAndAdd" v-tooltip.top.focus="'Ctr + Shift + S'"
          ><div>Lưu và Thêm</div></ms-button
        >
        <ms-button class="btn primary" @click="onSave" v-tooltip.top.focus="'Ctr + S'"
          ><div>Lưu</div></ms-button
        >
      </div>
    </div>

    <!-- Alert Modal for errors -->
    <MsAlert
      :visible="showError"
      :title="alertTitle"
      :message="alertMessage"
      alertIcon="icon-warning"
      @close="showError = false"
    />

    <!-- Confirm Dialog for close -->
    <MsAlert
      :visible="showConfirmClose"
      title="Thoát và không lưu"
      alertIcon="icon-confirm"
      message="Nếu bạn thoát, các dữ liệu đang nhập liệu sẽ không được lưu lại."
      :show-confirm="true"
      @close="showConfirmClose = false"
      @confirm="confirmClose"
    />
  </div>
</template>

<script setup>
import { ref, reactive, watch, nextTick } from 'vue'
import MsIcon from '@/components/MsIcon.vue'
import MsButton from '@/components/MsButton.vue'
import MsInput from '@/components/MsInput.vue'
import MsDatePicker from '@/components/MsDatePicker.vue'
import MsRadioButton from '@/components/MsRadioButton.vue'
import MsAlert from '@/components/MsAlert.vue'
import MsAreaInput from '@/components/MsAreaInput.vue'

// Props
const props = defineProps({
  modelValue: { type: Boolean, default: false },
  shiftData: { type: Object, default: null },
  isEdit: { type: Boolean, default: false },
})

// Emits
const emit = defineEmits(['update:modelValue', 'save', 'saveAndAdd'])
// Alert state
const showError = ref(false)
const alertMessage = ref('')
const alertTitle = ref('')
// Confirm close state
const showConfirmClose = ref(false)
// Computed show popup
const showPopup = ref(props.modelValue)

// Ref for auto-focus
const shiftCodeInput = ref(null)

// Ref for modal (focus trap)
const modalRef = ref(null)

// Focus trap - giữ tab navigation trong popup
const handleKeydown = (e) => {
  if (e.key === 'Tab' && modalRef.value) {
    const focusableElements = modalRef.value.querySelectorAll(
      '.btn, [href], input:not([disabled]), select:not([disabled]), textarea:not([disabled]), [tabindex]:not([tabindex="-1"])',
    )
    const firstElement = focusableElements[0]
    const lastElement = focusableElements[focusableElements.length - 1]

    if (e.shiftKey) {
      // Shift + Tab: nếu đang ở phần tử đầu tiên, chuyển đến phần tử cuối
      if (document.activeElement === firstElement) {
        e.preventDefault()
        lastElement.focus()
      }
    } else {
      // Tab: nếu đang ở phần tử cuối, chuyển về phần tử đầu
      if (document.activeElement === lastElement) {
        e.preventDefault()
        firstElement.focus()
      }
    }
  }

  // Đóng popup khi nhấn Escape
  if (e.key === 'Escape') {
    onClose()
  }
}

// Form data
const form = reactive({
  ShiftId: null,
  ShiftCode: '',
  ShiftName: '',
  BeginShiftTime: '',
  EndShiftTime: '',
  BeginBreakTime: '',
  EndBreakTime: '',
  Description: '',
  WorkingTime: '0',
  BreakingTime: '0',
  CreatedBy: 'Phí Hoàng Anh',
  CreatedDate: new Date().toISOString(),
  ModifiedBy: 'Phí Hoàng Anh',
  ModifiedDate: new Date().toISOString(),
  IsActive: true,
})

// Errors
const errors = reactive({
  ShiftCode: '',
  ShiftName: '',
  BeginShiftTime: '',
  EndShiftTime: '',
})

// Touched state - theo dõi các trường đã được focus và blur
const touched = reactive({
  ShiftCode: false,
  ShiftName: false,
  BeginShiftTime: false,
  EndShiftTime: false,
})

// Watch props
watch(
  () => props.modelValue,
  (val) => {
    showPopup.value = val
    if (val && props.isEdit && props.shiftData) {
      // Chế độ sửa: Map API data to form fields
      form.ShiftId = props.shiftData.ShiftId
      form.ShiftCode = props.shiftData.shiftCode || ''
      form.ShiftName = props.shiftData.shiftName || ''
      form.BeginShiftTime = formatTimeToHHMM(props.shiftData.beginShiftTime || '')
      form.EndShiftTime = formatTimeToHHMM(props.shiftData.endShiftTime || '')
      form.BeginBreakTime = formatTimeToHHMM(props.shiftData.beginBreakTime || '')
      form.EndBreakTime = formatTimeToHHMM(props.shiftData.endBreakTime || '')
      form.WorkingTime = props.shiftData.workingTime || '0'
      form.BreakingTime = props.shiftData.breakingTime || '0'
      form.Description = props.shiftData.description || ''
      form.CreatedBy = props.shiftData.createdBy || 'Phí Hoàng Anh'
      form.CreatedDate = props.shiftData.createdDate || new Date().toISOString()
      form.ModifiedBy = props.shiftData.modifiedBy || 'Phí Hoàng Anh'
      form.ModifiedDate = props.shiftData.modifiedDate || new Date().toISOString()
      form.IsActive = props.shiftData.inactive
    } else if (val && !props.isEdit && props.shiftData) {
      // Chế độ nhân bản: Copy dữ liệu nhưng để trống mã ca
      form.ShiftId = null // Không có ID vì là tạo mới
      form.ShiftCode = '' // Để trống để nhập mã ca mới
      form.ShiftName = props.shiftData.shiftName || ''
      form.BeginShiftTime = formatTimeToHHMM(props.shiftData.beginShiftTime || '')
      form.EndShiftTime = formatTimeToHHMM(props.shiftData.endShiftTime || '')
      form.BeginBreakTime = formatTimeToHHMM(props.shiftData.beginBreakTime || '')
      form.EndBreakTime = formatTimeToHHMM(props.shiftData.endBreakTime || '')
      form.WorkingTime = props.shiftData.workingTime || '0'
      form.BreakingTime = props.shiftData.breakingTime || '0'
      form.Description = props.shiftData.description || ''
      form.CreatedBy = 'Phí Hoàng Anh'
      form.CreatedDate = new Date().toISOString()
      form.ModifiedBy = 'Phí Hoàng Anh'
      form.ModifiedDate = new Date().toISOString()
      form.IsActive = props.shiftData.inactive
    } else if (val) {
      // Chế độ thêm mới thông thường
      resetForm()
    }
    // Auto focus on ShiftCode input when popup opens
    if (val) {
      nextTick(() => {
        shiftCodeInput.value?.focus()
      })
    }
  },
)

watch(showPopup, (val) => {
  emit('update:modelValue', val)
})

// Helper function: tính số giờ từ số phút (làm tròn đến 0.5, số nguyên hiện nguyên)
const minutesToHours = (minutes) => {
  const hours = minutes / 60
  // Làm tròn đến 0.5: nhân 2, làm tròn, chia 2
  const rounded = Math.round(hours * 2) / 2
  // Nếu là số nguyên thì hiện nguyên, không thì 1 chữ số thập phân
  return rounded % 1 === 0 ? rounded.toString() : rounded.toFixed(1)
}

// Watch BeginShiftTime và EndShiftTime để tính WorkingTime
watch(
  [
    () => form.BeginShiftTime,
    () => form.EndShiftTime,
    () => form.BeginBreakTime,
    () => form.EndBreakTime,
  ],
  () => {
    // Tính thời gian làm việc
    if (form.BeginShiftTime && form.EndShiftTime) {
      const beginShift = timeToMinutes(form.BeginShiftTime)
      const endShift = timeToMinutes(form.EndShiftTime)

      let totalMinutes = endShift - beginShift

      // Nếu giờ ra ca nhỏ hơn giờ vào ca (qua ngày mới)
      if (totalMinutes < 0) {
        totalMinutes = 24 * 60 + totalMinutes
      }

      // Trừ đi thời gian nghỉ nếu có
      if (form.BeginBreakTime && form.EndBreakTime) {
        const beginBreak = timeToMinutes(form.BeginBreakTime)
        const endBreak = timeToMinutes(form.EndBreakTime)
        let breakMinutes = endBreak - beginBreak

        if (breakMinutes < 0) {
          breakMinutes = 24 * 60 + breakMinutes
        }

        totalMinutes -= breakMinutes
      }

      form.WorkingTime = minutesToHours(totalMinutes)
    } else {
      form.WorkingTime = '0'
    }
  },
)

// Watch BeginBreakTime và EndBreakTime để tính BreakTime
watch([() => form.BeginBreakTime, () => form.EndBreakTime], () => {
  if (form.BeginBreakTime && form.EndBreakTime) {
    const beginBreak = timeToMinutes(form.BeginBreakTime)
    const endBreak = timeToMinutes(form.EndBreakTime)

    let breakMinutes = endBreak - beginBreak

    // Nếu giờ kết thúc nghỉ nhỏ hơn giờ bắt đầu nghỉ (qua ngày mới)
    if (breakMinutes < 0) {
      breakMinutes = 24 * 60 + breakMinutes
    }

    form.BreakingTime = minutesToHours(breakMinutes)
  } else {
    form.BreakingTime = '0'
  }
})

const resetForm = () => {
  form.ShiftCode = ''
  form.ShiftName = ''
  form.BeginShiftTime = ''
  form.EndShiftTime = ''
  form.BeginBreakTime = ''
  form.EndBreakTime = ''
  form.WorkingTime = '0'
  form.BreakingTime = '0'
  form.IsActive = true
  Object.keys(errors).forEach((key) => (errors[key] = ''))
  // Reset touched state
  Object.keys(touched).forEach((key) => (touched[key] = false))
}

// Helper function: chuyển đổi time string "HH:MM" thành số phút để so sánh
const timeToMinutes = (timeStr) => {
  if (!timeStr) return null
  const [hours, minutes] = timeStr.split(':').map(Number)
  return hours * 60 + minutes
}

const validate = () => {
  // Validate required fields
  if (!form.ShiftCode.trim()) {
    return 'Mã ca không được để trống'
  }
  if (!form.ShiftName.trim()) {
    return 'Tên ca không được để trống'
  }
  if (!form.BeginShiftTime) {
    return 'Giờ vào ca không được để trống'
  }
  if (!form.EndShiftTime) {
    return 'Giờ ra ca không được để trống'
  }

  // Validate: Giờ ra ca phải lớn hơn giờ vào ca
  const beginShift = timeToMinutes(form.BeginShiftTime)
  const endShift = timeToMinutes(form.EndShiftTime)

  if (endShift <= beginShift) {
    return 'Giờ ra ca không được bằng hoặc nhỏ hơn giờ vào ca'
  }

  // Validate thời gian nghỉ giữa ca (nếu có)
  if (form.BeginBreakTime || form.EndBreakTime) {
    // Nếu có một trong hai thì phải có cả hai
    if (!form.BeginBreakTime) {
      return 'Vui lòng nhập thời gian bắt đầu nghỉ giữa ca'
    }
    if (!form.EndBreakTime) {
      return 'Vui lòng nhập thời gian kết thúc nghỉ giữa ca'
    }

    const beginBreak = timeToMinutes(form.BeginBreakTime)
    const endBreak = timeToMinutes(form.EndBreakTime)

    // Thời gian kết thúc nghỉ phải lớn hơn thời gian bắt đầu nghỉ
    if (endBreak <= beginBreak) {
      return 'Thời gian kết thúc nghỉ giữa ca phải lớn hơn thời gian bắt đầu nghỉ'
    }

    // Thời gian bắt đầu nghỉ phải nằm trong thời gian làm việc
    if (beginBreak < beginShift || beginBreak > endShift) {
      return 'Thời gian bắt đầu nghỉ giữa ca phải nằm trong thời gian làm việc'
    }

    // Thời gian kết thúc nghỉ phải nằm trong thời gian làm việc
    if (endBreak <= beginShift || endBreak > endShift) {
      return 'Thời gian kết thúc nghỉ giữa ca phải nằm trong thời gian làm việc'
    }
  }

  return ''
}

const onClose = () => {
  // Hiển thị confirm dialog thay vì đóng trực tiếp
  showConfirmClose.value = true
}

// Xử lý khi người dùng xác nhận đóng
const confirmClose = () => {
  showConfirmClose.value = false
  showPopup.value = false
  resetForm()
}
// Helper function: chuyển đổi thời gian về định dạng HH:mm (loại bỏ giây)
function formatTimeToHHMM(time) {
  if (!time) return ''
  // Nếu có dạng HH:mm:ss -> lấy HH:mm
  if (time.length >= 5) return time.substring(0, 5)
  return time
}
function ensureSeconds(time) {
  if (!time) return '00:00:00' // nếu rỗng -> trả về chuẩn
  if (time.length === 5) return time + ':00' // dạng HH:mm -> thêm :00
  return time // dạng HH:mm:ss -> giữ nguyên
}
const onSave = () => {
  // Đánh dấu tất cả các trường là đã touched
  Object.keys(touched).forEach((key) => (touched[key] = true))

  const errorMessage = validate()
  if (errorMessage) {
    alertMessage.value = errorMessage
    alertTitle.value = 'Cảnh báo!'
    showError.value = true
    return
  }

  emit('save', {
    ShiftCode: form.ShiftCode,
    ShiftName: form.ShiftName,
    BeginShiftTime: ensureSeconds(form.BeginShiftTime),
    EndShiftTime: ensureSeconds(form.EndShiftTime),
    BeginBreakTime: ensureSeconds(form.BeginBreakTime),
    EndBreakTime: ensureSeconds(form.EndBreakTime),
    WorkingTime: form.WorkingTime,
    BreakingTime: form.BreakingTime,
    Inactive: form.IsActive,
    CreatedBy: form.CreatedBy,
    ModifiedBy: form.ModifiedBy,
    CreatedDate: form.CreatedDate,
    ModifiedDate: new Date().toISOString(),
  })
  showPopup.value = false
}

const onSaveAndAdd = () => {
  // Đánh dấu tất cả các trường là đã touched
  Object.keys(touched).forEach((key) => (touched[key] = true))

  const errorMessage = validate()
  if (errorMessage) {
    alertMessage.value = errorMessage
    alertTitle.value = 'Cảnh báo!'
    showError.value = true
    return
  }

  emit('saveAndAdd', {
    ShiftId: form.ShiftId,
    ShiftCode: form.ShiftCode,
    ShiftName: form.ShiftName,
    BeginShiftTime: ensureSeconds(form.BeginShiftTime),
    EndShiftTime: ensureSeconds(form.EndShiftTime),
    BeginBreakTime: ensureSeconds(form.BeginBreakTime),
    EndBreakTime: ensureSeconds(form.EndBreakTime),
    WorkingTime: form.WorkingTime,
    BreakingTime: form.BreakingTime,
    Inactive: form.IsActive,
    CreatedBy: form.CreatedBy,
    ModifiedBy: form.ModifiedBy,
    CreatedDate: form.CreatedDate,
    ModifiedDate: new Date().toISOString(),
  })
  // Reset form nhưng không đóng popup
  resetForm()
}
</script>

<style scoped>
.popup-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
}

.popup {
  width: 680px;
  touch-action: none;
}

.modal-content {
  display: flex;
  flex-direction: column;
  border-radius: 4px;
  background-color: #fff;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.15);
}

.popup-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 16px 20px;
}

.popup-title {
  font-size: 24px;
  font-family: Inter;
  color: #111827;
  margin-right: 32px;
  white-space: nowrap;
  cursor: text;
  font-weight: 700;
}

.popup-body {
  padding: 20px;
}
.form-group:not(.top) {
  margin-bottom: 16px;
}
.ms-input {
  padding: 0;
}
.ms-editor .label {
  color: #262626;
  display: block;
  font-weight: 500;
  white-space: nowrap;
}
.ms-textarea-required {
  color: #e54848;
}
.gap-4 {
  gap: 1rem;
}
.mb-16 {
  margin-bottom: 16px;
}
.popup-footer {
  display: flex;
  justify-content: flex-end;
  gap: 12px;
  padding: 16px 20px;
  border-top: 1px solid #e5e7eb;
}
.ms-editor .label {
  color: #262626;
  display: block;
  font-weight: 500;
}
.label {
  font-size: 13px;
  font-weight: 600;
  color: #111827;
}
.ms-datepicker-required {
  color: #e54848;
}
.w-1\/2 {
  width: 50%;
}
.date-picker {
  align-items: center;
  justify-content: space-between;
  gap: 60px;
}
</style>
