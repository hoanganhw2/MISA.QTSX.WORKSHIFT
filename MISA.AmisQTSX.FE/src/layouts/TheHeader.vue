<template>
  <div class="header align-items-center d-flex justify-content-between flex-row">
    <div class="header-left d-flex align-items-center">
      <ms-icon class="logo-menu mi2 icon24"></ms-icon>
      <router-link to="#" class="home">
        <ms-icon class="logo-icon" icon="icon24"></ms-icon>
        <div class="logo-text">Sản xuất</div>
      </router-link>
    </div>
    <div class="header-right">
      <div class="items d-flex align-items-center justify-content-between w-full h-full">
        <div class="item-left">
          <div class="name-company-title pos-relative">
            <div class="cursor-pointer d-flex align-items-center">
              <div class="name-company-title-text name-company-truncate">
                (SMECloud) VĂN PHÒNG ĐẠI DIỆN CÔNG TY CỔ PHẦN MISA TẠI HÀ NỘI
              </div>
              <ms-icon class="mi2 angle-down-smal icon16"></ms-icon>
            </div>
            <div class="company-popup" style="display: none">
              <div class="company-popup__title">Chọn nhà máy làm việc</div>
              <div class="company-popup__row">
                <div class="label" style="width: 140px">Nhà máy làm việc</div>
                <ms-combobox>
                  <ms-icon></ms-icon>
                </ms-combobox>
              </div>
            </div>
          </div>
        </div>
        <div class="item-right">
          <div class="icon-function-nav icon-nav-hover mr-12">
            <ms-icon class="icon24 mi2 download-status" v-tooltip.bottom="'Xuất Excel'"></ms-icon>
          </div>
          <div class="nav-separator"></div>
          <div class="icon-function-nav">
            <ms-icon class="icon24 icon-package-2" v-tooltip.bottom="'Tra cứu tồn kho'"></ms-icon>
          </div>
          <div class="icon-function-nav">
            <ms-icon class="icon24 user-control-panel" v-tooltip.bottom="'Mời tham gia'"></ms-icon>
          </div>
          <div class="icon-function-nav">
            <ms-icon class="icon24 mi2 bell" v-tooltip.bottom="'Thông báo'"></ms-icon>
          </div>
          <div class="icon-function-nav">
            <ms-icon class="icon24 question-cirrcle mi2" v-tooltip.bottom="'Trợ giúp'"></ms-icon>
          </div>
          <div class="icon-function-nav">
            <ms-icon
              class="icon24 dots-circle-horizontal mi"
              v-tooltip.bottom="'Tính năng khác'"
            ></ms-icon>
          </div>
          <div class="icon-function-nav">
            <ms-icon class="icon24 knowledge" v-tooltip.bottom="'Kiến thức hữu ích'"></ms-icon>
          </div>
          <div class="icon-function-nav avatar-account">
            <ms-icon class="icon32 avatar-account w-full h-full"></ms-icon>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup>
import MsIcon from '@/components/MsIcon.vue'
import MsCombobox from '@/components/MsCombobox.vue'
import { InputText } from 'primevue'
</script>
<style scoped>
.header {
  height: 48px;
  max-height: 48px;
  width: 100%;
  background-color: #ffffff;
  border-bottom: 1px solid #d1d5db;
}

.header .header-left {
  padding-left: 16px;
  margin-right: 16px;
  padding-right: 16px;
}

.header .header-right {
  flex: 1;
  padding: 0 16px 0 20px;
  align-items: center;
  display: flex;
  height: 100%;
}

.home {
  margin-left: 16px;
  display: flex;
  align-items: center;
  text-decoration: none;
  width: 100%;
  height: 100%;
}

.header .header-left .logo-icon {
  background-image: url(/logo.ico);
  width: 30px;
  height: 30px;
  background-size: contain;
  background-repeat: no-repeat;
}

.header .header-left .logo-text {
  margin-left: 12px;
  font-size: 18px;
  font-weight: 700;
  color: #262626;
  white-space: nowrap;
}

.header-right .items .item-left {
  display: flex;
  align-items: center;
}

.header-right .items .item-left .name-company-title {
  font-size: 13px;
  font-weight: 700;
  margin-top: 2px;
  display: flex;
  align-items: center;
}

.header-right .items .item-left .name-company-title .name-company-title-text {
  margin-right: 0.25rem;
}

.name-company-truncate {
  max-width: 400px;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.mi2.angle-down-smal.icon16 {
  margin-left: 0.25rem;
}

.company-popup {
  position: absolute;
  left: 0;
  top: 36px;
  z-index: 99;
  background-color: #fff;
  border: 1px solid #eee;
  min-width: 460px;
  box-shadow: 0 2px 4px #0000001a;
  border-radius: 4px;
}

.company-popup__title {
  padding: 16px 20px;
  font-weight: 700;
  font-size: 20px;
}

.company-popup__row {
  padding: 0 20px 20px;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.company-popup__row .label {
  font-size: 13px;
  font-weight: 500;
  color: #262626;
  display: block;
}

.item-right {
  height: 100%;
  display: flex;

  align-items: center;
}
.icon-function-nav {
  cursor: pointer;
  position: relative;
  margin-left: 12px;
}
.nav-separator {
  width: 1px;
  height: 16px;
  background: #d1d5db;
  align-self: center;
  border-radius: 1px;
}
.icon-function-nav.icon-nav-hover:hover {
  background-color: #f2f2f2;
  border-radius: 50%;
}
.icon-function-nav.avatar-account {
  border-radius: 50%;
  width: 32px;
  height: 32px;
  cursor: pointer;
  overflow: hidden;
  border: none;
  margin-left: 24px;
}
</style>
