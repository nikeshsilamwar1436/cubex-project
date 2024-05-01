<template>
  <div class="q-pa-md">
    <q-table
      class="my-sticky-header-column-table"
      :title="title"
      :rows="rows"
      :columns="columns"
      row-key="name"
      :pagination="pagination"
    />
  </div>
</template>
<script lang="ts" setup>
import {tableColumns} from 'components/models';
import {ref} from 'vue';

defineProps({
  rows: {
    type: Array,
    default: null
  },
  columns: {
    type: Array<tableColumns>,
  },
  title: String
});
const pagination = ref({
  sortBy: 'desc',
  descending: false,
  page: 1,
  rowsPerPage: 10
})
</script>
