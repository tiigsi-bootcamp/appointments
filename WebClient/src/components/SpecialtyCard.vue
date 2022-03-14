<script setup lang="ts">
	defineProps<{ title: string, doctors: number }>();
</script>

<template>
	<div
		class="bg-blue-200 shadow-md rounded-lg p-8 text-center hover:shadow-lg hover:scale-110 transition"
	>
		<div class="flex items-center justify-center mb-2">
			<slot>
				<h1>Default</h1>
			</slot>
		</div>

		<h1 class="text-2xl">{{ title }}</h1>
		<span>{{ doctors }} Doctors</span>

		<slot name="bottom"></slot>
	</div>
</template>