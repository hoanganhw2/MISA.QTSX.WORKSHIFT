<template>
  <div class="table-container">
    <!-- Loading Overlay -->
    <div v-if="loading" class="loading-overlay">
      <div class="loading-spinner">
        <div class="spinner"></div>
        <span class="loading-text">Đang tải dữ liệu...</span>
      </div>
    </div>

    <DataTable
      ref="dt"
      :value="paginatedData"
      responsiveLayout="scroll"
      v-model:selection="selectedItem"
      selectionMode="multiple"
      :dataKey="dataKey"
      :rowClass="rowClass"
      @row-click="onRowClick"
      class="p-datatable-sm ms-table"
      scrollable
      scrollHeight="calc(100% - 48px)"
      style="width: 100%"
    >
      <Column selectionMode="multiple" headerStyle="width:40px" frozen="true"> </Column>

      <!-- Dynamic Columns -->
      <Column
        v-for="col in columns"
        :key="col.field"
        :field="col.field"
        :header="col.header"
        :sortable="col.sortable"
        :style="col.style"
      >
        <template v-if="col.type === 'status'" #body="{ data }">
          <span
            :class="['status-tag', data[col.statusField] ? 'status-active' : 'status-inactive']"
          >
            {{ data[col.statusField] ? col.inactiveText : col.activeText }}
          </span>
        </template>
        <template v-else-if="col.type === 'breakTime'" #body="{ data }">
          <div v-if="data[col.beginField]">
            {{ formatTime(data[col.beginField]) }} → {{ formatTime(data[col.endField]) }}
          </div>
          <span v-else class="text-gray-500">{{ col.emptyText || 'Không nghỉ' }}</span>
        </template>
        <template v-else-if="col.type === 'time'" #body="{ data }">
          <span class="font-medium">{{
            data[col.field] ? formatTime(data[col.field]) : col.emptyText || '-'
          }}</span>
        </template>
        <template v-else-if="col.type === 'date'" #body="{ data }">
          <span>{{ data[col.field] ? formatDate(data[col.field]) : '-' }}</span>
        </template>
        <template v-else-if="col.type === 'number'" #body="{ data }">
          <span>{{ formatNumber(data[col.field]) }}</span>
        </template>
      </Column>

      <!-- Action Column -->
      <Column frozen alignFrozen="right" style="width: 60px">
        <template #body="{ data }">
          <div class="ms-td-widget">
            <div class="widget-container">
              <div class="feature-btn text-info v-popper--has-tooltip" @click.stop="onEdit(data)">
                <ms-icon class="mi2 icon16 pencil" v-tooltip.top="'Sửa'"></ms-icon>
              </div>
              <div class="menu-wrapper widget-more-container">
                <div class="menu-button-container">
                  <div class="feature-btn" @click.stop="toggleMenu(data[dataKey])">
                    <ms-icon class="mi2 icon16 feature-more-blue"></ms-icon>
                  </div>
                  <!-- Dropdown Menu -->
                  <div v-if="activeMenu === data[dataKey]" class="dropdown-menu" @click.stop>
                    <div class="dropdown-item" @click="onDuplicate(data)">
                      <ms-icon class="mi2 icon16 icon-duplicate"></ms-icon>
                      <span>Nhân bản</span>
                    </div>
                    <div class="dropdown-item" @click="onToggleStatus(data)">
                      <ms-icon
                        :class="['mi2', 'icon16', data.inactive ? 'icon-active' : 'icon-inactive']"
                      ></ms-icon>
                      <span @click="changeStatus(data, !data.inactive)">
                        {{ data.inactive ? 'Ngưng sử dụng' : 'Sử dụng' }}
                      </span>
                    </div>

                    <div
                      class="dropdown-item dropdown-item--danger"
                      @click="onDelete(data.shiftId)"
                    >
                      <ms-icon
                        class="mi2 icon16 icon-trash--red"
                        style="background-color: red"
                      ></ms-icon>
                      <span>Xóa</span>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </template>
      </Column>
      <template #footer>
        <div class="ms-pagination">
          <div class="total-count">
            <span class="total-label">Tổng số:</span>
            <span class="total">{{ totalRecords }}</span>
          </div>

          <div class="pagination-controls">
            <span class="text">Số dòng/trang</span>

            <Dropdown
              v-model="pageSize"
              :options="pageSizeOptions"
              class="page-size-dropdown"
              @change="onPageSizeChange"
            >
            </Dropdown>

            <span class="page-info">{{ firstRecord }} - {{ lastRecord }}</span>

            <div class="pagination-buttons">
              <Button
                icon="mi2 icon16 step-backward"
                text
                :disabled="currentPage === 1"
                @click="goToFirstPage"
                class="p-button-sm"
              ></Button>
              <Button
                icon="mi2 icon16 angle-left"
                text
                :disabled="currentPage === 1"
                @click="goToPreviousPage"
                class="p-button-sm"
              ></Button>
              <Button
                icon="mi2 icon16 angle-right"
                text
                :disabled="currentPage === totalPages"
                @click="goToNextPage"
                class="p-button-sm"
              ></Button>
              <Button
                icon="mi2 icon16 step-forward"
                text
                :disabled="currentPage === totalPages"
                @click="goToLastPage"
                class="p-button-sm"
              ></Button>
            </div>
          </div>
        </div>
      </template>
    </DataTable>

    <!-- Hidden DataTable for CSV export (exports all data) -->
    <DataTable ref="dtExport" :value="data" style="display: none">
      <Column
        v-for="col in columns.filter((c) => c.field !== 'ShiftId')"
        :key="col.field"
        :field="col.field"
        :header="col.header"
      ></Column>
    </DataTable>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import MsIcon from './MsIcon.vue'
import Dropdown from 'primevue/dropdown'
import Button from 'primevue/button'
// Props
const props = defineProps({
  data: {
    type: Array,
    default: () => [],
  },
  columns: {
    type: Array,
    default: () => [],
  },
  dataKey: {
    type: String,
    default: 'id',
  },
  modelValue: {
    type: Array,
    default: () => [],
  },
  loading: {
    type: Boolean,
    default: false,
  },
})

// Format functions
// Format time: HH:MM:SS -> HH:MM
const formatTime = (timeStr) => {
  if (!timeStr) return '-'
  // Nếu là string dạng "08:00:00", lấy HH:MM
  if (typeof timeStr === 'string' && timeStr.includes(':')) {
    const parts = timeStr.split(':')
    return `${parts[0]}:${parts[1]}`
  }
  return timeStr
}

// Format date: ISO string -> dd/MM/yyyy
const formatDate = (dateStr) => {
  if (!dateStr) return '-'
  try {
    const date = new Date(dateStr)
    const day = String(date.getDate()).padStart(2, '0')
    const month = String(date.getMonth() + 1).padStart(2, '0')
    const year = date.getFullYear()
    return `${day}/${month}/${year}`
  } catch {
    return dateStr
  }
}

// Format number: làm tròn đến 0.5, số nguyên thì hiện nguyên
const formatNumber = (num) => {
  if (num === null || num === undefined) return '-'
  const value = Number(num)
  // Làm tròn đến 0.5: nhân 2, làm tròn, chia 2
  const rounded = Math.round(value * 2) / 2
  // Nếu là số nguyên thì hiện nguyên, không thì 1 chữ số thập phân
  return rounded % 1 === 0 ? rounded.toString() : rounded.toFixed(1)
}

// Emit events
const emit = defineEmits([
  'edit',
  'toggleStatus',
  'duplicate',
  'changeInactive',
  'delete',
  'update:modelValue',
  'selectionChange',
])

// Multiple checkbox selection
const selectedItem = ref([])

// Track clicked row (separate from checkbox selection)
const clickedRowKey = ref(null)

// Active dropdown menu
const activeMenu = ref(null)

// Watch selectedItem và emit lên parent
import { watch } from 'vue'
watch(
  selectedItem,
  (newVal) => {
    emit('update:modelValue', newVal)
    emit('selectionChange', newVal)
  },
  { deep: true },
)

// Watch modelValue từ parent để sync selection
watch(
  () => props.modelValue,
  (newVal) => {
    selectedItem.value = newVal || []
  },
  { deep: true },
)

// Watch data để reset currentPage khi data thay đổi
watch(
  () => props.data,
  () => {
    // Reset về trang 1 khi data thay đổi (refresh, filter, etc.)
    currentPage.value = 1
  },
  { deep: true },
)

// Row click handler
const onRowClick = (event) => {
  clickedRowKey.value = event.data[props.dataKey]
  activeMenu.value = null
}

// Edit handler
const onEdit = (data) => {
  activeMenu.value = null
  emit('edit', data)
}

// Toggle dropdown menu
const toggleMenu = (key) => {
  activeMenu.value = activeMenu.value === key ? null : key
}

// Toggle status handler
const onToggleStatus = (data) => {
  activeMenu.value = null
}

// Duplicate handler
const onDuplicate = (data) => {
  activeMenu.value = null
  emit('duplicate', data)
}

// Delete handler
const onDelete = (data) => {
  activeMenu.value = null
  emit('delete', data)
}

// Row class để highlight hàng được click
const rowClass = (data) => {
  return clickedRowKey.value === data[props.dataKey] ? 'row-selected' : ''
}
// change status handler
const changeStatus = (data, inactive) => {
  activeMenu.value = null
  emit('changeInactive', data, inactive)
}

// Reference to DataTable
const dt = ref(null)
const dtExport = ref(null)

// Export to CSV function - exports ALL data using hidden table
const exportCSV = () => {
  if (dtExport.value) {
    dtExport.value.exportCSV()
  }
}

// Expose exportCSV method to parent component
defineExpose({
  exportCSV,
})

// Pagination state
const pageSize = ref(10)
const currentPage = ref(1)
const pageSizeOptions = ref([10, 20, 50, 100])

// Computed properties for pagination
const totalRecords = computed(() => props.data.length)
const totalPages = computed(() => Math.max(1, Math.ceil(totalRecords.value / pageSize.value)))
const firstRecord = computed(() => {
  if (totalRecords.value === 0) return 0
  return (currentPage.value - 1) * pageSize.value + 1
})
const lastRecord = computed(() => {
  if (totalRecords.value === 0) return 0
  const last = currentPage.value * pageSize.value
  return last > totalRecords.value ? totalRecords.value : last
})

// Computed data for current page - chỉ hiển thị dữ liệu của trang hiện tại
const paginatedData = computed(() => {
  const start = (currentPage.value - 1) * pageSize.value
  const end = start + pageSize.value
  return props.data.slice(start, end)
})

// Pagination methods
const onPageSizeChange = () => {
  currentPage.value = 1
}

const goToFirstPage = () => {
  currentPage.value = 1
}

const goToPreviousPage = () => {
  if (currentPage.value > 1) {
    currentPage.value--
  }
}

const goToNextPage = () => {
  if (currentPage.value < totalPages.value) {
    currentPage.value++
  }
}

const goToLastPage = () => {
  currentPage.value = totalPages.value
}
</script>

<style>
.table-container {
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  position: relative;
}
.p-datatable-table-container {
  height: calc(100% - 48px);
}

.p-datatable .p-datatable-column-header-content .p-checkbox-box {
  width: 16px;
  height: 16px;
  border-radius: 0.5px;
}

.p-datatable .p-datatable-tbody .p-checkbox-box {
  width: 16px;
  height: 16px;
  border-radius: 0.5px;
}

/* Hoặc cho tất cả checkbox trong table */
.p-datatable input[type='checkbox'] {
  width: 16px;
  height: 16px;
  cursor: pointer;
}

/* Sticky header với màu nền */
.p-datatable .p-datatable-thead > tr > th {
  background-color: #f3f4f6;
  z-index: 2;
  white-space: nowrap;
  font-weight: 600;
}

/* Header frozen columns cần z-index cao hơn body frozen columns */
.p-datatable .p-datatable-thead > tr > th.p-datatable-frozen-column {
  z-index: 3 !important;
}

/* Style cho body cells */
.p-datatable .p-datatable-tbody > tr > td {
  white-space: nowrap;
  padding: 4px 8px !important;
  height: 32px !important;
  max-height: 32px !important;
  line-height: 24px !important;
  overflow: hidden;
  color: #111827;
}

/* Allow overflow for action column */
.p-datatable .p-datatable-tbody > tr > td:has(.ms-td-widget) {
  overflow: visible !important;
}

/* Chiều cao cố định cho các hàng */
.p-datatable .p-datatable-tbody > tr {
  height: 32px !important;
  max-height: 32px !important;
}

/* Wrapper scroll */
.p-datatable-wrapper {
  overflow-x: auto;
  overflow-y: visible;
}
.p-datatable-table-container {
  scrollbar-width: thin;
  scrollbar-color: #cfcbcb;
}
/* Đảm bảo table có chiều rộng tối thiểu để hiển thị thanh cuộn */
.p-datatable .p-datatable-table {
  min-width: 100%;
  width: max-content;
}

/* Pagination Container */
.ms-pagination {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 6px 16px;
  background-color: #f3f4f6;
  border-top: 1px solid #e5e7eb;
  height: 48px;
}

/* Total Count Section */
.ms-pagination .total-count {
  display: flex;
  align-items: center;
  gap: 4px;
  font-size: 13px;
}

.ms-pagination .total-label {
  color: #111827;
  font-weight: 400;
}

.ms-pagination .total {
  color: #111827;
  font-weight: 700;
}

/* Pagination Controls Section */
.ms-pagination .pagination-controls {
  display: flex;
  align-items: center;
  gap: 16px;
}

.ms-pagination .page-size-label {
  color: #6b7280;
  font-size: 13px;
  font-weight: 500;
}

/* Dropdown Styling */
.ms-pagination .page-size-dropdown {
  width: 70px;
  height: 28px;
  display: flex;
  align-items: center;
}

.ms-pagination .page-size-dropdown .p-dropdown {
  height: 28px;
  font-size: 13px;
}

/* Page Info */
.ms-pagination .page-info {
  color: #111827;
  font-size: 13px;
  font-weight: 600;
  min-width: 50px;
  text-align: center;
}

/* Pagination Buttons */
.ms-pagination .pagination-buttons {
  display: flex;
  gap: 4px;
}
.ms-table {
  flex: 1;
  display: flex;
  flex-direction: column;
  overflow: hidden;
}

.ms-table .p-datatable-wrapper {
  flex: 1;
  overflow-x: auto;
  overflow-y: auto;
}
.ms-pagination .pagination-buttons .p-button {
  width: 24px;
  height: 24px;
  padding: 0;
  color: #6b7280;
}

.ms-pagination .pagination-buttons .p-button:not(:disabled):hover {
  background-color: #e5e7eb;
  color: #111827;
}

.ms-pagination .pagination-buttons .p-button:disabled {
  opacity: 0.4;
  cursor: not-allowed;
}
.p-datatable.p-component.p-datatable-scrollable.p-datatable-sm .ms-table {
  display: flex;
  flex-direction: column;
  width: 100%;
  height: 100%;
}
.p-datatable-footer {
  padding: 0 !important;

  height: 45px;
}
.text {
  font-weight: 400 !important;
  color: black;
  font-family: Inter;
}
.page-size-dropdown .p-select-label {
  overflow: visible;
}
/* Tất cả rows */
.p-datatable .p-datatable-tbody tr:hover {
  background-color: #e5e7eb !important;
  cursor: pointer;
}

/* Row được chọn khi click - màu xanh green */
.p-datatable .p-datatable-tbody tr.row-selected {
  background-color: #a4f6d3;
}
.p-datatable .p-datatable-tbody tr.row-selected:hover {
  background-color: #a4f6d3 !important;
}

/* Row được chọn bằng checkbox - cùng màu với row-selected */
.p-datatable .p-datatable-tbody tr[aria-selected='true'] {
  background-color: #a4f6d3 !important;
}
.p-datatable .p-datatable-tbody tr.p-row-selected:hover {
  background-color: #a4f6d3 !important;
}
.p-datatable .p-datatable-tbody tr.p-row-selected td {
  background-color: #a4f6d3 !important;
}
/* Firefox scrollbar */
.p-datatable-wrapper {
  scrollbar-width: thin;
  scrollbar-color: #c1c1c1 #f1f1f1;
}

/* Status Tag Styles */
.status-tag {
  display: inline-block;
  padding: 4px 12px;
  border-radius: 999px;
  font-size: 12px;
  font-weight: 500;
  text-align: center;
  white-space: nowrap;
}

.status-active {
  background-color: #d1fae5;
  color: #065f46;
}

.status-inactive {
  background-color: #fee2e2;
  color: #991b1b;
}

.widget-item {
  padding: 0 16px;
  right: 0;
}
.widget-container {
  transition: visibility 0s ease;
}
.widget-container {
  display: flex;
  justify-content: flex-start;
  padding-left: 8px;
  align-items: center;
  gap: 8px;
}
.feature-btn {
  cursor: pointer;
  transition: all ease 0.1s;
  border-radius: 50%;
  width: 24px;
  height: 24px;
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: #fff;
}
.text-info {
  color: #2563eb !important;
  font-weight: 500;
}
.mi2.icon16.pencil:hover {
  background-color: #009b71 !important;
}

/* Dropdown Menu Styles */
.menu-button-container {
  position: relative;
}

.ms-td-widget {
  overflow: visible !important;
}

.widget-container {
  overflow: visible !important;
}

.menu-wrapper {
  position: relative;
  overflow: visible !important;
}

.dropdown-menu {
  position: absolute;
  top: 100%; /* Hiển thị ngay bên dưới nút */
  right: 0; /* Lệch sang bên trái (căn theo cạnh phải của nút) */
  left: auto; /* Bỏ left: 0 */
  min-width: 160px;
  background-color: #fff;
  border: 1px solid #e5e7eb;
  border-radius: 4px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
  z-index: 999;
  padding: 4px 0;
}
/* Override frozen column để dropdown hiển thị được */
.p-datatable .p-datatable-tbody > tr > td.p-datatable-frozen-column {
  overflow: visible !important;
}

.p-datatable .p-datatable-tbody > tr > td {
  padding-right: 12px !important;
}
/* Khi row có dropdown đang mở, tăng z-index của frozen column đó */
.p-datatable .p-datatable-tbody > tr > td.p-datatable-frozen-column:has(.dropdown-menu) {
  z-index: 100 !important;
}
.dropdown-item {
  display: flex;
  align-items: center;
  gap: 8px;
  padding: 8px 12px;
  cursor: pointer;
  font-size: 13px;
  color: #111827;
  transition: background-color 0.15s ease;
}

.dropdown-item:hover {
  background-color: #f3f4f6;
}

.dropdown-item .mi2 {
  flex-shrink: 0;
}

.icon-duplicate {
  mask-position: -144px 0;
  background-color: #4b5563;
}

/* Loading Overlay Styles */
.loading-overlay {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(255, 255, 255, 0.8);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 100;
}

.loading-spinner {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 12px;
}

.spinner {
  width: 40px;
  height: 40px;
  border: 3px solid #e5e7eb;
  border-top-color: #009b71;
  border-radius: 50%;
  animation: spin 0.8s linear infinite;
}

@keyframes spin {
  to {
    transform: rotate(360deg);
  }
}

.loading-text {
  color: #6b7280;
  font-size: 13px;
  font-weight: 500;
}

/* PrimeVue DataTable loading indicator override */
.p-datatable-loading-overlay {
  background-color: rgba(255, 255, 255, 0.7) !important;
}

.p-datatable-loading-icon {
  color: #009b71 !important;
  font-size: 24px !important;
}
</style>
