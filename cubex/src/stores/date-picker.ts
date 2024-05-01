import { defineStore } from 'pinia';

export const useDateStore = defineStore('selectedDate', {
  state: () => ({
    selectedDate: '',
  }),
  actions: {
    increment() {
    },
  },
});
