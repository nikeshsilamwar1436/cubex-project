<template>
<q-list>
  <q-item v-for="(menuItem, index) in menuList" v-bind:key="index"
          clickable
          v-ripple
          exact-active-class="item-exact-active"
          :to="menuItem.link">
    <q-item-section avatar>
      <q-icon :name="menuItem.icon" />
    </q-item-section>
    <q-item-section class="item-section-label">
      {{ menuItem.label }}
    </q-item-section>
  </q-item>
</q-list>
</template>
<script setup lang="ts">
import {ref} from 'vue';
import { menuInter } from 'components/models'
import { menu } from 'src/test/menu';
const menuList = ref<menuInter[]>(menu)
</script>
