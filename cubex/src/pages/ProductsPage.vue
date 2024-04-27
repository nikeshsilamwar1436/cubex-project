<template>
  <q-page>
  <TableView :rows="rows" :columns="columns" title="Products"/>
  </q-page>
</template>
<script setup lang="ts">
import TableView from 'components/Table/TableView.vue';
import { columnsData, rowData } from 'src/test/tableData'
import { tableColumns } from 'components/models'
import {ref} from 'vue';
const rows = ref(rowData)
const columns = ref<tableColumns[]>(columnsData)
</script>
