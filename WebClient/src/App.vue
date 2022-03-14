<script setup lang="ts">
import { RouterView } from 'vue-router';
import Navbar from './components/Navbar.vue';
import Footer from './components/Footer.vue';

// Refactoring!

</script>

<template class="bg-gray-200">
  <Navbar />

  <div class="py-10"></div>

  <RouterView />
  <Footer />

  <!-- Footer will go here -->
</template>
