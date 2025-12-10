<template>
  <div class="ms-textarea ms-editor d-flex align-items-start gap-4" style="height: auto">
    <slot></slot>
    <div class="flex-1 d-flex textarea-container border">
      <textarea
        style="resize: none"
        ref="textareaRef"
        v-model="inputValue"
        class="ms-textarea-item w-full"
        :placeholder="props.placeholder"
        :rows="props.rows"
        :maxlength="props.maxlength"
        :disabled="props.disabled"
      ></textarea>
    </div>
  </div>
</template>

<script setup>
import { computed, ref } from 'vue'

const props = defineProps({
  modelValue: { type: [String, Number], default: '' },
  placeholder: { type: String, default: '' },
  rows: { type: [String, Number], default: 2 },
  maxlength: { type: [String, Number], default: null },
  disabled: { type: Boolean, default: false },
})

const emit = defineEmits(['update:modelValue'])

const textareaRef = ref(null)

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
  textareaRef.value?.focus()
}

defineExpose({ focus })
</script>

<style scoped>
.ms-textarea {
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
.textarea-container {
  background-color: #fff;
  padding: 8px 12px;
}
.ms-textarea-item {
  border: none;
  background: transparent;
  resize: vertical;
  font-family: Inter, sans-serif;
  font-size: 13px;
  color: #212121;
  line-height: 1.5;
  min-height: 60px;
}
.ms-textarea-item:focus {
  outline: none;
}
.ms-textarea-item:disabled {
  background-color: #f3f4f6;
  cursor: not-allowed;
}
.ms-textarea-item::placeholder {
  color: #9ca3af;
}
</style>
