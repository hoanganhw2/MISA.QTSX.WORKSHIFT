<template>
  <div :class="['sidebar', { collapsed: sidebar }]">
    <div class="container">
      <div class="menu-container">
        <div class="menu-item-wrapper">
          <div class="menu-item-container">
            <div class="w-full flex-col">
              <router-link to="/dashboard">
                <ms-sidebar-item
                  prefix-icon="icon-dashboard"
                  title="Tổng quan"
                  :collapsed="sidebar"
                ></ms-sidebar-item>
              </router-link>
            </div>
            <div class="w-full flex-col">
              <ms-sidebar-item
                prefix-icon="icon-order"
                title="Đơn đặt hàng"
                :collapsed="sidebar"
              ></ms-sidebar-item>
            </div>
            <div class="w-full flex-col">
              <ms-sidebar-item
                prefix-icon="icon-plan"
                suffix-icon="angle-down"
                :collapsed="sidebar"
                title="Kế hoạch sản xuất"
                name="ke-hoach-san-xuat"
                :is-active="activeMenu === 'ke-hoach-san-xuat'"
                @toggle="handleToggle"
                :items="[
                  'Kế hoạch tổng thể',
                  'Kế hoạch chi tiết',
                  'Kế hoạch nguyên vật liệu',
                  'Yêu cầu mua NVL',
                ]"
              ></ms-sidebar-item>
            </div>
            <div class="w-full flex-col">
              <ms-sidebar-item
                prefix-icon="icon-coordinate"
                suffix-icon="angle-down"
                :collapsed="sidebar"
                title="Điều phối và thực thi"
                name="dieu-phoi"
                :is-active="activeMenu === 'dieu-phoi'"
                @toggle="handleToggle"
                :items="[
                  'Lệnh sản xuất',
                  'Lịch sản xuất',
                  'Yêu cầu xuất vật tư',
                  'Thống kê sản xuất',
                  'Yêu cầu nhập thành phẩm,nguyên liệu thừa',
                ]"
              ></ms-sidebar-item>
            </div>
            <div class="w-full flex-col">
              <ms-sidebar-item
                prefix-icon="icon-quality-control"
                suffix-icon="angle-down"
                :collapsed="sidebar"
                title="Kiểm tra chất lượng"
                name="kiem-tra-chat-luong"
                :is-active="activeMenu === 'kiem-tra-chat-luong'"
                @toggle="handleToggle"
                :items="[
                  'Lệnh sản xuất',
                  'Lịch sản xuất',
                  'Yêu cầu xuất vật tư',
                  'Thống kê sản xuất',
                  'Yêu cầu nhập thành phẩm,nguyên liệu thừa',
                ]"
              ></ms-sidebar-item>
            </div>
            <div class="w-full flex-col">
              <ms-sidebar-item
                prefix-icon="icon-material-factory"
                suffix-icon="angle-down"
                :collapsed="sidebar"
                title="Kho vật tư"
                name="kho-vat-tu"
                :is-active="activeMenu === 'kho-vat-tu'"
                @toggle="handleToggle"
                :items="[
                  'Lệnh sản xuất',
                  'Lịch sản xuất',
                  'Yêu cầu xuất vật tư',
                  'Thống kê sản xuất',
                  'Yêu cầu nhập thành phẩm,nguyên liệu thừa',
                ]"
              ></ms-sidebar-item>
            </div>
            <div class="w-full flex-col">
              <ms-sidebar-item
                prefix-icon="icon-work-order"
                suffix-icon="angle-down"
                :collapsed="sidebar"
                title="Giao việc"
                name="giao-viec"
                :is-active="activeMenu === 'giao-viec'"
                @toggle="handleToggle"
                :items="[
                  'Lệnh sản xuất',
                  'Lịch sản xuất',
                  'Yêu cầu xuất vật tư',
                  'Thống kê sản xuất',
                  'Yêu cầu nhập thành phẩm,nguyên liệu thừa',
                ]"
              ></ms-sidebar-item>
            </div>
            <div class="menu-item-line"></div>
            <div class="w-full flex-col">
              <ms-sidebar-item
                prefix-icon="icon-production-report"
                suffix-icon="angle-down"
                :collapsed="sidebar"
                title="Báo cáo"
                name="bao-cao"
                :is-active="activeMenu === 'bao-cao'"
                @toggle="handleToggle"
              ></ms-sidebar-item>
            </div>
            <div class="menu-item-line"></div>
            <div class="w-full flex-col">
              <ms-sidebar-item
                prefix-icon="icon-product-material"
                suffix-icon="angle-down"
                :collapsed="sidebar"
                title="Sản phẩm,NVL"
                name="san-pham-nvl"
                :is-active="activeMenu === 'san-pham-nvl'"
                @toggle="handleToggle"
                :items="[
                  'Lệnh sản xuất',
                  'Lịch sản xuất',
                  'Yêu cầu xuất vật tư',
                  'Thống kê sản xuất',
                  'Yêu cầu nhập thành phẩm,nguyên liệu thừa',
                ]"
              ></ms-sidebar-item>
            </div>
            <div class="w-full flex-col">
              <ms-sidebar-item
                prefix-icon="icon-process"
                suffix-icon="angle-down"
                :collapsed="sidebar"
                title="Quy trình sản xuất"
                name="quy-trinh-san-xuat"
                :is-active="activeMenu === 'quy-trinh-san-xuat'"
                @toggle="handleToggle"
                :items="[
                  'Lệnh sản xuất',
                  'Lịch sản xuất',
                  'Yêu cầu xuất vật tư',
                  'Thống kê sản xuất',
                  'Yêu cầu nhập thành phẩm,nguyên liệu thừa',
                ]"
              ></ms-sidebar-item>
            </div>
            <div class="w-full flex-col">
              <ms-sidebar-item
                prefix-icon="icon-production-capacity"
                suffix-icon="angle-down"
                :collapsed="sidebar"
                title="Năng lực sản xuất"
                name="nang-luc-san-xuat"
                :is-active="activeMenu === 'nang-luc-san-xuat'"
                @toggle="handleToggle"
                :items="[
                  'Lệnh sản xuất',
                  'Lịch sản xuất',
                  'Yêu cầu xuất vật tư',
                  'Thống kê sản xuất',
                  'Yêu cầu nhập thành phẩm,nguyên liệu thừa',
                ]"
              ></ms-sidebar-item>
            </div>
            <div class="w-full flex-col">
              <ms-sidebar-item
                prefix-icon="icon-production-category"
                suffix-icon="icon-arrow-right"
                :collapsed="sidebar"
                title="Danh mục khác"
                :menuGroups="[
                  {
                    title: 'Đối tượng',
                    items: [
                      { name: 'Khách hàng', active: false },
                      { name: 'Nhân viên', active: false },
                      { name: 'Đối tượng tập hợp chi phí', active: false },
                    ],
                  },
                  {
                    title: 'Lịch làm việc',
                    items: [
                      { name: 'Ca làm việc', active: true, route: '/shift' },
                      { name: 'Ngày nghỉ', active: false },
                      { name: 'Lịch làm việc', active: false },
                    ],
                  },
                  {
                    title: 'Khác',
                    items: [
                      { name: 'Cơ cấu tổ chức', active: false },
                      { name: 'Kho', active: false },
                      { name: 'Đơn vị tính', active: false },
                      { name: 'Lý do dừng công việc', active: false },
                    ],
                  },
                ]"
              ></ms-sidebar-item>
            </div>
            <div class="w-full flex-col">
              <ms-sidebar-item
                prefix-icon="icon-production-system"
                suffix-icon="icon-arrow-right"
                :collapsed="sidebar"
                title="Thiết lập"
                :menuGroups="[
                  {
                    title: 'Hệ thống',
                    items: [
                      { name: 'Cấu hình chung', active: false },
                      { name: 'Phân quyền', active: false },
                      { name: 'Quản lý người dùng', active: false },
                    ],
                  },
                  {
                    title: 'Sản xuất',
                    items: [
                      { name: 'Thiết lập quy trình', active: false },
                      { name: 'Thiết lập kho', active: false },
                      { name: 'Thiết lập báo cáo', active: false },
                    ],
                  },
                ]"
              ></ms-sidebar-item>
            </div>
          </div>
        </div>
        <div class="btn-collapse-footer">
          <div class="btn-collapse" @click="sidebar = !sidebar">
            <div class="icon-collaspe icon20 mi2 collapse-icon"></div>
            <div class="icon-text">Thu gọn</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup>
import { RouterLink } from 'vue-router'
import MsIcon from '@/components/MsIcon.vue'
import MsSidebarItem from '@/components/MsSidebarItem.vue'
import { ref } from 'vue'

const sidebar = ref(false)
const activeMenu = ref(null)

// Handler để toggle menu - chỉ cho phép 1 menu mở tại 1 thời điểm
const handleToggle = (menuName) => {
  if (activeMenu.value === menuName) {
    activeMenu.value = null // Đóng nếu đang mở
  } else {
    activeMenu.value = menuName // Mở menu mới
  }
}
</script>
<style scoped>
.sidebar {
  width: 234px;
  max-width: 234px;
  height: calc(100vh - 48px);
  background-color: #111827;
  transition: all 0.2s ease;
}

.sidebar .container {
  display: block;
  float: left;
  height: 100%;

  background: #111827;
  width: 100%;
}
.menu-container {
  height: 100%;
  position: relative;
  color: #fff;
  visibility: visible;
  overflow: hidden;
  white-space: nowrap;
  background: #111827;
  display: flex;

  flex-direction: column;
  justify-content: space-between;
}
.menu-item-wrapper {
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  overflow: auto;
}
.menu-item-wrapper .menu-item-container {
  display: flex;
  flex-direction: column;
  padding: 12px 12px 0;
  row-gap: 4px;

  flex: 1;
  min-height: 0;
}
.btn-collapse-footer {
  box-sizing: border-box;
  max-width: 234px;
  height: 56px;
  padding: 10px;
  display: flex;
  align-items: center;
  background: #111827;
  border-top: 1px solid rgba(209, 213, 219, 0.3);
}
.btn-collapse {
  width: 100%;
  height: 36px;
  border-radius: 4px;
  min-height: 36px;
  background: #111827;
  color: #9ca3af;
  z-index: 2;
  display: flex;
  justify-content: center;
  align-items: center;

  padding-right: 5px;
  white-space: nowrap;
}
.btn-collapse .icon-text {
  margin-left: 8px;
  text-overflow: ellipsis;
}
.btn-collapse:hover {
  background: rgba(156, 163, 175, 0.15);
  cursor: pointer;
}
.sidebar.collapsed {
  width: 60px !important;
}
.collapse-icon {
  transition: transform 0.2s;
}

.sidebar.collapsed .icon-collaspe {
  transform: rotate(180deg);
}
.sidebar.collapsed .btn-collapse-footer {
  width: 60px;
}
.sidebar.collapsed .btn-collapse-footer .icon-text {
  display: none;
}

.menu-item-line {
  margin: 8px auto;
  width: 100%;
  border-bottom: 1px solid rgba(209, 213, 219, 0.3);
}
.menu-item-admin.active {
  background: #009b71;
}
.menu-item-admin.active .icon-menu {
  background-color: #fff;
}
.menu-item-admin.active .menu-item-icon .icon {
  background-color: #fff;
}
.menu-item-admin.active .menu-item-title {
  color: #fff;
}
.menu-item-wrapper::-webkit-scrollbar {
  width: 4px;
  height: 4px;
}
</style>
