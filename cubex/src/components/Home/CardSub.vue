<template>
  <q-card class="sub-card">
    <q-card-section>
      <div class="text-h6 q-mb-xs">{{title}}</div>
    </q-card-section>
    <q-card-section>
      <q-list>
        <q-item v-for="(item, index) in itemList" v-bind:key="index">
          <q-item-section>{{item.name}}</q-item-section>
          <q-item-section side>{{item.total}}</q-item-section>
        </q-item>
      </q-list>
    </q-card-section>
  </q-card>
</template>
<script lang="ts" setup>
import { itemData } from 'components/models'
defineProps({
  title: String,
  itemList: Array<itemData>
})
</script>
