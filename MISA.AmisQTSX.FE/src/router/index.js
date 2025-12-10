import App from '@/App.vue'
import { createRouter, createWebHistory } from 'vue-router'
import Dashboard from '@/views/dashboard/DashBoard.vue'
import WorkShift from '@/views/workshift/WorkShift.vue'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'Home',
      redirect: '/shift',
    },
    {
      path: '/shift',
      name: 'Shift',
      component: WorkShift,
      meta: {
        title:
          'Ca làm việc | Danh mục | (SMECloud) VĂN PHÒNG ĐẠI DIỆN CÔNG TY CỔ PHẦN MISA TẠI HÀ NỘI | AMIS Sản Xuất',
      },
    },
    {
      path: '/dashboard',
      name: 'Dashboard',
      component: Dashboard,
      meta: {
        title:
          'Dashboard | (SMECloud) VĂN PHÒNG ĐẠI DIỆN CÔNG TY CỔ PHẦN MISA TẠI HÀ NỘI | AMIS Sản Xuất',
      },
    },
  ],
})

export default router
