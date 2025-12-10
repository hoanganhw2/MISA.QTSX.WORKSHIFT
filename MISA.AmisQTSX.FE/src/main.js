import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import '@/assets/css/base.css'

import '@/assets/css/common.css'
import '@/assets/css/icon.css'
import '@/assets/css/main.css'
// theme Aura (nếu dùng)

import PrimeVue from 'primevue/config'
import Aura from '@primeuix/themes/aura'
import { Theme } from '@primeuix/themes'
import Tooltip from 'primevue/tooltip'
import ToastService from 'primevue/toastservice'

const app = createApp(App)
app.directive('tooltip', Tooltip)
router.beforeEach((to) => {
  // router có meta title thì cập nhật
  if (to.meta.title) {
    document.title = to.meta.title
  }
})
app.use(router)
app.use(PrimeVue, {
  theme: {
    preset: Aura,
  },
})
app.use(ToastService)
app.mount('#app')
