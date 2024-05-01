<template>
  <q-page class="row items-center justify-evenly">
    <date-picker/>
    <card-sub title="Sales" :item-list="item"/>
    <card-sub title="Products" :item-list="item"/>
    <card-sub title="Customers" :item-list="item"/>
    <card-sub title="Products" :item-list="item"/>
  </q-page>
</template>

<script setup lang="ts">
import CardSub from 'components/Home/CardSub.vue';
import DatePicker from 'components/Home/DateSelect.vue';
import {ref} from 'vue';
const item = ref([
  {
    name: 'Nikesh',
    total: '20'
  },
  {
    name: 'Nikesh',
    total: '20'
  }
])
</script>
