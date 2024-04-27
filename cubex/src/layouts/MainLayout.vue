<template>
  <q-layout view="lHh lpR lFf">

    <q-header elevated class="header bg-primary text-white">
      <q-toolbar>
        <q-btn class="header-button" dense flat round icon="menu" @click="toggleLeftDrawer" />
        <q-space />
        <div class="row">
          <essential-search/>
          <q-separator class="q-ml-md" vertical style="height:40px"></q-separator>
          <essential-profile/>
        </div>
      </q-toolbar>
    </q-header>

    <q-drawer
      class="side-drawer"
      show-if-above
      v-model="leftDrawerOpen"
      side="left"
      :width="200"
      :breakpoint="700"
      bordered>
      <q-img class="side-image" src="images/cubexLogo.png"/>
      <q-separator></q-separator>
      <essential-link/>
    </q-drawer>

    <q-page-container>
      <router-view />
    </q-page-container>

  </q-layout>
</template>

<script lang="ts" setup>
import { ref } from 'vue';
import EssentialLink from 'components/Layout/EssentialLink.vue';
import EssentialSearch from 'components/Layout/EssentialSearch.vue';
import EssentialProfile from 'components/Layout/EssentialProfile.vue';

const leftDrawerOpen = ref(false)
const toggleLeftDrawer = () => {
  leftDrawerOpen.value = !leftDrawerOpen.value
}
</script>
