<script setup>
import { RouterLink, RouterView } from 'vue-router'
import TheSideBar from './layouts/TheSideBar.vue'
import TheHeader from './layouts/TheHeader.vue'
import Toast from 'primevue/toast'
</script>

<template>
  <div class="container d-flex flex-col">
    <!-- Header -->
    <the-header></the-header>
    <div class="content flex-1 d-flex flex-row">
      <!-- Sidebar -->
      <the-side-bar></the-side-bar>
      <!-- View -->
      <router-view></router-view>
    </div>
  </div>

  <!-- Global Toast -->
  <Toast />
</template>

<style scoped>
.container {
  width: 100%;
  height: 100vh;
}
</style>
