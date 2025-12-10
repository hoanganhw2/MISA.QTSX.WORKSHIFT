<template>
  <div class="flex-row d-flex flex-1 combo-box-input input-container border">
    <input class="input d-flex flex-1"></input>
    <div :class="['editor-icon-container', 'icon-combo-dropdown-arrow', { expand: angleDown }]">
      <div :class="['mi2', 'icon16', 'angle-down']" @click="angleDown = !angleDown"></div>
    </div>
  </div>
</template>
<script setup>
  import { ref } from 'vue';
  const angleDown = ref(false)
</script>
<style scoped>
.combo-box-input {
  justify-content: center;
}

.input-container {
  padding: 5px 8px 5px 12px;
}

.input-container.border {
  border: 1px solid #D1D5DB;
  border-radius: 4px;
  ;
}

.input-container.border:hover {
  border: 1px solid #9ca3af;
  ;
}

.input-container.border:focus-within {
  border: 1px solid #009b71;
}

.editor-icon-container {
  cursor: pointer;
  position: relative;
  width: 16px;
  height: 16px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.input {
  width: 100%;
  border: 0;
  height: 16px;
  outline: none;
  text-overflow: ellipsis;
}

.icon-combo-dropdown-arrow {
  transition: all .2s ease;
}

.icon-combo-dropdown-arrow.expand {
  transform: rotate(180deg);
  -webkit-transform: rotate(-180deg);

}
</style>
