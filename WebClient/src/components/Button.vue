<script setup lang="ts">
import { defineComponent } from '@vue/runtime-core';

const props = defineProps<{ text: string; color?: string }>();

let defaultColor = props.color;
if (!props.color) {
	defaultColor = 'bg-green-800';
}
</script>

<template>
  <button @click="$emit('sayHi', 'Fatima')"
    v-bind:class="
      'px-3 py-2 text-white uppercase rounded hover:bg-blue-400 transition ' +
      defaultColor
    "
  >
    {{ text }}
  </button>
</template>