<template>
  <div class="q-pa-md date-picker">
    <q-btn icon="event" round color="dark">
      <q-popup-proxy @before-show="updateProxy" cover transition-show="scale" transition-hide="scale">
        <q-date v-model="proxyDate">
          <div class="row items-center justify-end q-gutter-sm">
            <q-btn label="Cancel" color="primary" flat v-close-popup />
            <q-btn label="OK" color="primary" flat @click="save" v-close-popup />
          </div>
        </q-date>
      </q-popup-proxy>
    </q-btn>
  </div>
</template>

<script lang="ts" setup>
import { ref } from 'vue'
const now =new Date()
    const selectDate = ref(now. toLocaleDateString())
    const proxyDate = ref('2019/03/01')
    const updateProxy = () => {
      proxyDate.value = selectDate.value
    }
    const save = () => {
     selectDate.value = proxyDate.value
    }

</script>
