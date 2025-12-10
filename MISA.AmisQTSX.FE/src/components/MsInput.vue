<template>
  <div
    class="ms-input ms-editor search-input-list gap-4 d-flex align-items-center"
    style="height: auto"
  >
    <slot></slot>
    <div
      class="flex-1 d-flex align-items-center flex-row input-container border"
      :class="{ disabled: props.disabled, 'has-error': props.error }"
      :style="{ maxWidth: props.width }"
      v-tooltip.bottom="props.error ? props.errorMessage : null"
    >
      <ms-icon v-if="props.iconPrefix != null" :class="props.iconPrefix"></ms-icon>
      <input
        ref="inputRef"
        v-model="inputValue"
        class="ms-input-item d-flex w-full"
        :type="props.type"
        :placeholder="props.placeholder"
        :disabled="props.disabled"
        @blur="$emit('blur')"
      />
      <ms-icon v-if="props.iconSubfix != null" :class="props.iconSubfix"></ms-icon>
    </div>
  </div>
</template>
<script setup>
import { computed, ref } from 'vue'
import MsIcon from './MsIcon.vue'

const props = defineProps({
  modelValue: { type: [String, Number], default: '' },
  placeholder: { type: String, default: '' },
  type: { type: String, default: 'text' },
  width: { type: String, default: '' },
  iconPrefix: { type: String, default: null },
  iconSubfix: { type: String, default: null },
  disabled: { type: Boolean, default: false },
  error: { type: Boolean, default: false },
  errorMessage: { type: String, default: 'Không được để trống' },
})

const emit = defineEmits(['update:modelValue', 'blur'])

const inputRef = ref(null)

const inputValue = computed({
  get() {
    return props.modelValue
  },
  set(value) {
    emit('update:modelValue', value)
  },
})

// Expose focus method to parent
const focus = () => {
  inputRef.value?.focus()
}

defineExpose({ focus })
</script>
<style scoped>
.search-input-list {
  width: 200px;
  transition: all 0.2s cubic-bezier(0.4, 0, 0.2, 1);
}
.ms-input {
  padding: 0;
}
.gap-4 {
  gap: 1rem;
}
.ms-editor .border {
  border: 1px solid #d1d5db;
  border-radius: 4px;
}
.ms-editor .border:hover {
  border-color: #9ca3af;
}
.ms-editor .border:focus-within {
  border-color: #009b71;
}
.ms-editor .border.has-error {
  border-color: #e54848;
}
.ms-editor .border.has-error:focus-within {
  border-color: #e54848;
}
.ms-editor .input-container {
  background-color: #fff;
  padding: 5px 12px;
  column-gap: 4px;
}
.pointer {
  cursor: pointer;
}
.icon-search.left {
  margin: 0 4px 0 0;
}
.ms-input input {
  background-color: #fff;
  font-family: Inter;
  padding: 0;
}
.ms-input-item {
  border: none;
  background: transparent;
  white-space: nowrap;
  overflow: hidden;
}
.ms-input-item:focus {
  outline: none;
}
.ms-input-item:disabled {
  background-color: #f3f4f6;
  cursor: not-allowed;
  color: #6b7280;
}
.input-container.disabled {
  background-color: #f3f4f6;
  cursor: not-allowed;
}
</style>
