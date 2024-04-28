<template>
  <q-card class="sub-card">
    <q-card-section>
      <div class="text-h6 q-mb-xs">{{title}}</div>
    </q-card-section>

  </q-card>
</template>
<script lang="ts" setup>
defineProps({
  title: String,
})
</script>
