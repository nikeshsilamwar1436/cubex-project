<template>
  <q-input dense bottom-slots v-model="keywords" hide-bottom-space borderless clearable clear-icon="close"
           class="search_input" :class="{ 'search_input-closed': sfClose }"
           @blur="sfClose = true; keywords = ''" @focus="sfClose = false">
    <template #prepend>
      <q-icon name="search" class="cursor-pointer q-mx-sm"></q-icon>
    </template>
  </q-input>
</template>
<script setup lang="ts">
import { ref } from 'vue'

const keywords = ref('')
const sfClose = ref(true)
</script>
