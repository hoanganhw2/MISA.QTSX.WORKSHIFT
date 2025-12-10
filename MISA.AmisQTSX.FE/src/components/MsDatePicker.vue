<template>
  <div
    data-v-cdb5f071=""
    data-v-348aae1a=""
    class="ms-datepicker ms-editor w-full d-flex align-items-center gap-4 ms-validate w-1/2"
    validate-on-blur="true"
    ref="datepickerRef"
  >
    <slot></slot>
    <div
      class="ms-container--datepicker d-flex flex-row border input-container flex-1"
      :class="{ 'has-error': props.error }"
      v-tooltip.bottom="props.error ? props.errorMessage : null"
    >
      <input
        v-model="displayTime"
        data-v-cdb5f071=""
        class="ms-input--datepicker d-flex"
        placeholder="HH:MM"
        tabindex="0"
        maxlength="5"
        @input="handleInput"
        @keydown="handleKeydown"
        @blur="handleBlur"
      />
      <div class="icon16 ic-time" @click="toggleTimeDropdown"></div>
      <div v-if="showTimeDropdown" class="time-dropdown" ref="dropdownRef">
        <div
          v-for="time in timeOptions"
          :key="time"
          class="time-option"
          :class="{ selected: displayTime === time }"
          @click="selectTime(time)"
        >
          {{ time }}
        </div>
      </div>
    </div>
  </div>
</template>
<script setup>
import { ref, computed, watch, onMounted, onBeforeUnmount, nextTick } from 'vue'

const props = defineProps({
  modelValue: { type: String, default: '' },
  error: { type: Boolean, default: false },
  errorMessage: { type: String, default: 'Không được để trống' },
})

const emit = defineEmits(['update:modelValue', 'blur'])

const displayTime = ref(props.modelValue)
const showTimeDropdown = ref(false)
const datepickerRef = ref(null)
const dropdownRef = ref(null)

// Generate time options from 00:00 to 23:30 with 30-minute intervals
const timeOptions = computed(() => {
  const options = []
  for (let hour = 0; hour <= 23; hour++) {
    for (let minute = 0; minute < 60; minute += 30) {
      const hourStr = String(hour).padStart(2, '0')
      const minuteStr = String(minute).padStart(2, '0')
      options.push(`${hourStr}:${minuteStr}`)
    }
  }
  return options
})

// Toggle time dropdown
const toggleTimeDropdown = () => {
  showTimeDropdown.value = !showTimeDropdown.value
  if (showTimeDropdown.value) {
    nextTick(() => {
      scrollToSelectedTime()
    })
  }
}

// Select time from dropdown
const selectTime = (time) => {
  displayTime.value = time
  emit('update:modelValue', time)
  showTimeDropdown.value = false
}

// Scroll to selected time in dropdown
const scrollToSelectedTime = () => {
  if (!dropdownRef.value) return

  const selectedElement = dropdownRef.value.querySelector('.time-option.selected')
  if (selectedElement) {
    selectedElement.scrollIntoView({ block: 'center', behavior: 'smooth' })
  }
}

// Handle click outside to close dropdown
const handleClickOutside = (event) => {
  if (datepickerRef.value && !datepickerRef.value.contains(event.target)) {
    showTimeDropdown.value = false
  }
}

// Watch for external changes
watch(
  () => props.modelValue,
  (newVal) => {
    displayTime.value = newVal
  },
)

// Add/remove click event listener
onMounted(() => {
  document.addEventListener('click', handleClickOutside)
})

onBeforeUnmount(() => {
  document.removeEventListener('click', handleClickOutside)
})

// Only allow numbers and colon
const handleKeydown = (e) => {
  const allowedKeys = ['Backspace', 'Delete', 'ArrowLeft', 'ArrowRight', 'Tab', 'Home', 'End']

  if (allowedKeys.includes(e.key)) {
    return
  }

  // Only allow digits
  if (!/^\d$/.test(e.key)) {
    e.preventDefault()
  }
}

// Format input as HH:MM
const handleInput = (e) => {
  let value = e.target.value
  const isDeleting = e.inputType && e.inputType.includes('delete')

  // Remove all non-digit characters
  let digits = value.replace(/\D/g, '')

  // Limit to 4 digits
  digits = digits.slice(0, 4)

  // Validate hours (00-23)
  if (digits.length >= 2) {
    let hours = parseInt(digits.slice(0, 2), 10)
    if (hours > 23) {
      digits = '23' + digits.slice(2)
    }
  }

  // Validate minutes (00-59)
  if (digits.length >= 4) {
    let minutes = parseInt(digits.slice(2, 4), 10)
    if (minutes > 59) {
      digits = digits.slice(0, 2) + '59'
    }
  }

  // Format with colon - only add colon automatically when typing, not when deleting
  let formatted = ''
  if (digits.length > 2) {
    // More than 2 digits, always show colon
    formatted = digits.slice(0, 2) + ':' + digits.slice(2)
  } else if (digits.length === 2 && !isDeleting) {
    // Exactly 2 digits and typing forward, add colon
    formatted = digits + ':'
  } else {
    // Just digits, no colon
    formatted = digits
  }

  displayTime.value = formatted
  emit('update:modelValue', formatted)
}

// Validate on blur
const handleBlur = () => {
  const value = displayTime.value

  // If incomplete, clear or keep as is
  if (value && value.length < 5) {
    // Pad with zeros if possible
    let digits = value.replace(/\D/g, '')
    if (digits.length === 1) {
      digits = '0' + digits + '00'
    } else if (digits.length === 2) {
      digits = digits + '00'
    } else if (digits.length === 3) {
      digits = digits + '0'
    }

    if (digits.length === 4) {
      let hours = parseInt(digits.slice(0, 2), 10)
      let minutes = parseInt(digits.slice(2, 4), 10)

      if (hours > 23) hours = 23
      if (minutes > 59) minutes = 59

      const formatted = String(hours).padStart(2, '0') + ':' + String(minutes).padStart(2, '0')
      displayTime.value = formatted
      emit('update:modelValue', formatted)
    }
  }
  emit('blur')
}
</script>
<style scoped>
.icon16.ic-time {
  background-image: url(../assets/icons/ic-time.svg);
  background-repeat: no-repeat;
  background-size: contain;
}
.ms-datepicker {
  background: transparent;
  border: none;
  box-shadow: none;
  border-radius: 2px;
  box-sizing: content-box;
  font-size: 13px;
  color: #212121;
  position: relative;
  left: unset;
  opacity: 100;
  transition:
    opacity 0.3s ease,
    transform 0.3s ease,
    left 0s 0.3s;
}
.gap-4 {
  gap: 1rem;
}
.w-1\/2 {
  width: 50%;
}
.mr-3 {
  margin-right: 0.75rem;
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
.ms-container--datepicker {
  max-width: 122px !important;
  padding-right: 0 !important;
}
.ms-editor .border {
  border: 1px solid #d1d5db;
  border-radius: 4px;
}

.ms-editor .input-container {
  background-color: #fff;
  padding: 5px 12px;
  -moz-column-gap: 4px;
  column-gap: 4px;
}
.ms-input--datepicker {
  width: 78px !important;
}
.ms-datepicker .ms-input--datepicker {
  height: 16px;
  border: none;
  width: 100%;
  outline: none;
  box-shadow: none;
}
.ms-datepicker .border:focus-within {
  border-color: #009b71;
}
.ms-datepicker .border:hover {
  border-color: #9ca3af;
}
.ms-datepicker .border.has-error {
  border-color: #e54848;
}
.ms-datepicker .border.has-error:focus-within {
  border-color: #e54848;
}

/* Time dropdown styles */
.icon16.ic-time {
  cursor: pointer;
  transition: opacity 0.2s;
}

.icon16.ic-time:hover {
  opacity: 0.7;
}

.time-dropdown {
  position: absolute;
  top: calc(100% + 4px);
  right: 0;
  background: white;
  border: 1px solid #d1d5db;
  border-radius: 4px;
  box-shadow:
    0 4px 6px -1px rgba(0, 0, 0, 0.1),
    0 2px 4px -1px rgba(0, 0, 0, 0.06);
  max-height: 200px;
  overflow-y: auto;
  z-index: 1000;
  width: 120px;
}

.time-option {
  padding: 6px 12px;
  cursor: pointer;
  font-size: 13px;
  color: #212121;
  transition: background-color 0.15s;
}

.time-option:hover {
  background-color: #f3f4f6;
}

.time-option.selected {
  background-color: #009b71;
  color: white;
}

.time-option.selected:hover {
  background-color: #008060;
}

/* Custom scrollbar for dropdown */
.time-dropdown::-webkit-scrollbar {
  width: 6px;
}

.time-dropdown::-webkit-scrollbar-track {
  background: #f1f1f1;
  border-radius: 4px;
}

.time-dropdown::-webkit-scrollbar-thumb {
  background: #888;
  border-radius: 4px;
}

.time-dropdown::-webkit-scrollbar-thumb:hover {
  background: #555;
}
</style>
