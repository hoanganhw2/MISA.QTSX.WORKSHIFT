<template>
  <div
    class="ms-sidebar-item"
    v-tooltip.top="{ value: props.title, disabled: !collapsed || hasSubmenu }"
    @mouseenter="onMouseEnter"
    @mouseleave="onMouseLeave"
  >
    <div class="menu-item-admin" :class="{ active: isActive || showFloatingMenu }" @click="toggle">
      <div class="menu-item-icon">
        <ms-icon :class="['icon icon20 mi', prefixIcon]"></ms-icon>
      </div>
      <div class="menu-item-title">{{ title }}</div>
      <div class="d-flex justify-content-end flex-1 align-items-center" style="padding-right: 8px">
        <ms-icon
          :class="['icon-menu icon20 mi2', suffixIcon, { open: isActive }]"
          v-if="(items.length > 0 || menuGroups.length > 0) && !collapsed"
        ></ms-icon>
      </div>
    </div>

    <!-- Dropdown submenu (khi có angle-down và sidebar mở) -->
    <div class="sub-menu-inside" v-show="isActive && !collapsed && !isMegaMenu">
      <div
        v-for="(item, index) in flatItems"
        :key="index"
        class="sub-menu-inside-item w-full d-flex align-items-center"
      >
        <ms-icon class="item-icon icon20 mi icon-next ml-8"></ms-icon>
        <button type="button" class="menu-item-admin child pointer-events-auto w-full text-left">
          <div class="menu-item-title" style="padding-left: 12px">{{ item }}</div>
        </button>
      </div>
    </div>

    <!-- Floating Submenu (khi sidebar thu gọn và có angle-down) -->
    <Transition name="float-fade">
      <div
        v-if="showFloatingMenu && collapsed && hasSubmenu && !isMegaMenu"
        class="floating-submenu"
        @mouseenter="onFloatingMenuEnter"
        @mouseleave="onFloatingMenuLeave"
      >
        <div v-for="(item, index) in flatItems" :key="index" class="floating-submenu-item">
          <ms-icon class="item-icon icon20 mi icon-next"></ms-icon>
          <span>{{ item }}</span>
        </div>
      </div>
    </Transition>

    <!-- Mega Menu (khi hover và có icon-arrow-right) -->
    <Transition name="mega-fade">
      <div
        v-if="showMegaMenu && isMegaMenu"
        class="mega-menu"
        :class="{ 'mega-menu--collapsed': collapsed }"
        @mouseenter="onMegaMenuEnter"
        @mouseleave="onMegaMenuLeave"
      >
        <div class="mega-menu-content">
          <div v-for="(group, groupIndex) in menuGroups" :key="groupIndex" class="mega-menu-column">
            <div class="mega-menu-header">{{ group.title }}</div>
            <div
              v-for="(item, itemIndex) in group.items"
              :key="itemIndex"
              class="mega-menu-item"
              :class="{ active: item.active }"
              @click="onMenuItemClick(item)"
            >
              <ms-icon class="item-icon icon20 mi icon-next ml-8"></ms-icon>
              <span>{{ item.name }}</span>
            </div>
          </div>
        </div>
      </div>
    </Transition>
  </div>
</template>

<script setup>
import MsIcon from './MsIcon.vue'
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const showMegaMenu = ref(false)
const showFloatingMenu = ref(false)
let megaMenuTimeout = null
let floatingMenuTimeout = null

const props = defineProps({
  title: { type: String, required: true },
  prefixIcon: { type: String, default: '' },
  suffixIcon: { type: String, default: '' },
  items: { type: Array, default: () => [] },
  menuGroups: {
    type: Array,
    default: () => [],
  },
  name: { type: String, default: '' },
  collapsed: { type: Boolean },
  isActive: { type: Boolean, default: false },
})

const emit = defineEmits(['toggle'])

// Check if this is a mega menu (has icon-arrow-right suffix)
const isMegaMenu = computed(() => props.suffixIcon === 'icon-arrow-right')

// Check if has submenu items
const hasSubmenu = computed(() => props.items.length > 0 || props.menuGroups.length > 0)

// Flat items for normal dropdown
const flatItems = computed(() => {
  if (Array.isArray(props.items) && props.items.length > 0) {
    if (typeof props.items[0] === 'string') {
      return props.items
    }
  }
  return []
})

const toggle = () => {
  if (isMegaMenu.value) {
    return // Don't toggle for mega menu items
  }
  if (!props.collapsed) {
    emit('toggle', props.name || props.title)
  }
}

const onMouseEnter = () => {
  if (isMegaMenu.value) {
    clearTimeout(megaMenuTimeout)
    showMegaMenu.value = true
  } else if (props.collapsed && hasSubmenu.value) {
    // Show floating submenu when sidebar is collapsed
    clearTimeout(floatingMenuTimeout)
    showFloatingMenu.value = true
  }
}

const onMouseLeave = () => {
  if (isMegaMenu.value) {
    megaMenuTimeout = setTimeout(() => {
      showMegaMenu.value = false
    }, 100)
  } else if (props.collapsed) {
    floatingMenuTimeout = setTimeout(() => {
      showFloatingMenu.value = false
    }, 100)
  }
}

const onMegaMenuEnter = () => {
  clearTimeout(megaMenuTimeout)
  showMegaMenu.value = true
}

const onMegaMenuLeave = () => {
  showMegaMenu.value = false
}

const onFloatingMenuEnter = () => {
  clearTimeout(floatingMenuTimeout)
  showFloatingMenu.value = true
}

const onFloatingMenuLeave = () => {
  showFloatingMenu.value = false
}

// Handle menu item click - navigate if route exists
const onMenuItemClick = (item) => {
  if (item.route) {
    router.push(item.route)
    showMegaMenu.value = false
  }
}
</script>

<style scoped>
.menu-item-admin {
  display: block;
  position: relative;
  padding: 9px 0;
  height: 36px;
  font-size: 13px;
  display: flex;
  align-items: center;
  column-gap: 8px;
  width: 100%;
  border-radius: 4px;
  cursor: pointer;
}
.menu-item-icon {
  display: flex;
  align-items: center;
  width: 20px;
  height: 20px;
  justify-content: center;
  margin-left: 8px;
}
.menu-item-title {
  color: #9ca3af;
  white-space: nowrap;
  overflow-x: hidden;
  text-overflow: ellipsis;
  max-width: 170px;
  pointer-events: none;
}
.menu-item-admin:hover {
  background: rgba(156, 163, 175, 0.15);
}
.menu-item-admin:hover .menu-item-title {
  color: #fff;
}
.menu-item-admin:hover .menu-item-icon .icon {
  background-color: #fff;
}

.menu-item-admin:hover .icon-menu {
  background-color: #fff;
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
.item-icon {
  background-color: #111827;
}
.sub-menu-inside-item:hover {
  background-color: rgba(156, 163, 175, 0.15);
}
.sub-menu-inside-item:hover .item-icon {
  background-color: #fff;
}

.icon-menu {
  background-color: #9ca3af;
  flex-shrink: 0;
  transition: transform 0.2s ease;
}

.icon-menu.open {
  transform: rotate(180deg);
}

.sub-menu-inside .sub-menu-content {
  overflow-x: visible;
}
.sub-menu-inside-item {
  margin-top: 4px;
  cursor: default;
  border-radius: 4px;
}
.menu-container .sub-menu-inside .sub-menu-inside-item button.menu-item-admin {
  border: none;
  background: transparent;
  outline: none;
  text-align: left;
  width: 100%;
  padding: 9px 0;
  display: flex;
  align-items: center;
  column-gap: 8px;
  cursor: pointer;
}

.menu-container
  .sub-menu-inside
  .sub-menu-inside-item
  button.menu-item-admin:hover
  .menu-item-title {
  color: #fff;
}

/* Floating submenu when sidebar is collapsed */
.ms-sidebar-item {
  position: relative;
}
.sidebar.collapsed .icon-menu {
  display: none;
}

/* Floating Submenu Styles (khi sidebar thu gọn) */
.floating-submenu {
  position: fixed;
  left: 60px;
  top: auto;
  background-color: #1f2937;
  border-radius: 6px;
  box-shadow: 0 10px 40px rgba(0, 0, 0, 0.4);
  z-index: 9999;
  min-width: 200px;
  padding: 8px 0;
}

.floating-submenu-item {
  display: flex;
  align-items: center;
  gap: 8px;
  padding: 10px 16px;
  color: #9ca3af;
  cursor: pointer;
  font-size: 13px;
  transition: all 0.15s ease;
}

.floating-submenu-item:hover {
  background-color: rgba(156, 163, 175, 0.15);
  color: #fff;
}

.floating-submenu-item:hover .item-icon {
  background-color: #fff;
}

.floating-submenu-item .item-icon {
  background-color: #1f2937;
  flex-shrink: 0;
}

/* Mega Menu Styles */
.mega-menu {
  position: fixed;
  left: 234px;
  bottom: 60px; /* Position from bottom instead of top to avoid going off screen */
  background-color: #1f2937;
  border-radius: 6px;
  box-shadow: 0 10px 40px rgba(0, 0, 0, 0.4);
  z-index: 9999;
  min-width: 500px;
  max-height: calc(100vh - 150px); /* Limit height for laptop screens */
  overflow-y: auto;
  padding: 16px;
}

.mega-menu.mega-menu--collapsed {
  left: 60px;
}

.mega-menu-content {
  display: flex;
  gap: 24px;
}

.mega-menu-column {
  min-width: 140px;
}

.mega-menu-header {
  color: #6b7280;
  font-size: 12px;
  font-weight: 500;
  text-transform: uppercase;
  letter-spacing: 0.5px;
  padding: 8px 12px;

  margin-bottom: 8px;
}

.mega-menu-item {
  display: flex;
  align-items: center;
  gap: 8px;
  padding: 10px 12px;
  color: #9ca3af;
  cursor: pointer;
  border-radius: 4px;
  font-size: 13px;
  transition: all 0.15s ease;
}

.mega-menu-item:hover {
  background-color: rgba(156, 163, 175, 0.15);
  color: #fff;
}
.mega-menu-item:hover .icon-next {
  background-color: #fff;
}

.mega-menu-item:hover .mi2 {
  background-color: #fff;
}

.mega-menu-item.active {
  background-color: #009b71;
  color: #fff;
}
.mega-menu-item .icon-next {
  background-color: #1f2937;
}
.mega-menu-item.active .icon-next {
  background-color: #fff;
}
.mega-menu-item.active .mi2 {
  background-color: #fff;
}

.mega-menu-item .mi2 {
  background-color: #6b7280;
  flex-shrink: 0;
}

/* Mega Menu Transition */
.mega-fade-enter-active,
.mega-fade-leave-active {
  transition: all 0.2s ease;
}

.mega-fade-enter-from,
.mega-fade-leave-to {
  opacity: 0;
  transform: translateX(-10px);
}

/* Floating Submenu Transition */
.float-fade-enter-active,
.float-fade-leave-active {
  transition: all 0.2s ease;
}

.float-fade-enter-from,
.float-fade-leave-to {
  opacity: 0;
  transform: translateX(-10px);
}
</style>
