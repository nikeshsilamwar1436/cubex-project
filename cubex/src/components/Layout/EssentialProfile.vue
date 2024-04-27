<template >
  <q-avatar class="profile-avatar" color="dark" text-color="white" size="30px">
  <img v-if="imageUrl" :src="imageUrl" />
  <template v-else>{{ initials }}</template>
  </q-avatar>
</template>
<script lang="ts" setup>
import { getInitials } from 'src/utils';
import {computed, ref} from 'vue';
const imageUrl = ref('https://cdn.quasar.dev/img/avatar.png')
const initials = computed(() => getInitials('Nikesh Kumar'));
</script>
