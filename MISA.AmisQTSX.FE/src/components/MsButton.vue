<template>
  <button tabindex="0" :class="['ms-button', styleButton]" @click="$emit('openPopUpAdd')">
    <slot></slot>
  </button>
</template>
<script setup>
const props = defineProps({
  styleButton: { type: String, default: '' },
})
defineEmits(['openPopUpAdd'])
</script>
<style scoped>
.ms-button {
  transition: all 0.2s ease;
  cursor: pointer;
  display: flex;
  align-items: center;
  outline: none;
  padding: 6px 12px;
  border-radius: 4px;
  position: relative;
  font-size: 13px;
  height: 28px;
  background-color: #fff;
  border: 1px solid #d1d5db;
  font-weight: 500 !important;
  font-family: Inter;
}

.primary {
  background-color: #009b71;
  border: none;
  color: #fff;
}
.ms-button.primary:hover {
  background-color: #007b5d;
}
.ms-button:hover {
  background-color: #f3f4f6;
}
.ms-button.btn-outline-success {
  border: 1px solid #009b71;
  color: #009b71;
  background-color: #fff;
}
.ms-button.btn-outline-success:hover {
  background-color: #dcfce7;
}
.ms-button.btn-outline-danger {
  border: 1px solid #dc2626;
  color: #dc2626;
  background-color: #fff;
}
.ms-button.btn-outline-danger:hover {
  background-color: #fee2e2;
}

/* Focus styles - hiển thị border khi tab vào */
.ms-button:focus {
  outline: none;
  border-color: #009b71;
  box-shadow: 0 0 0 2px rgba(0, 155, 113, 0.3);
}

.ms-button.primary:focus {
  outline: none;
  box-shadow: 0 0 0 2px rgba(0, 155, 113, 0.5);
}
</style>
