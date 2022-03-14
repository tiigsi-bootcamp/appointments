<script setup lang="ts">
import Button from './Button.vue';

	// Props

	defineProps<{ message: string }>();

</script>

<template>
	<h4 class="text-purple-500 text-5xl">{{ message }}</h4>

	<button class="button">Button</button>

	<div class="my-5"></div>

	<Button text="Hello" />
</template>
