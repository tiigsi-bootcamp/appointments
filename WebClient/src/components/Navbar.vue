<script setup lang="ts">
import { ref } from '@vue/reactivity';
import { RouterLink } from 'vue-router';

let isMenuHidden = ref(true);
function hide(){
	isMenuHidden.value = true
}
</script>

<template>
  <div
    v-if="isMenuHidden === false"
    @click="isMenuHidden = !isMenuHidden"
    class="bg-gray-500 h-screen w-screen fixed md:hidden bg-opacity-25"
  ></div>

  <div class="fixed w-full md:px-20">
    <nav class="max-w-6xl mx-auto bg-gray-100 shadow-lg">
      <div class="flex justify-between items-center px-4">
        <div class="flex space-x-8">
          <div>
            <RouterLink to="/">
              <span
                class="
                  block
                  py-4
                  text-transparent
                  bg-clip-text
                  text-lg
                  font-bold
                  bg-gradient-to-r
                  from-green-500
                  to-blue-500
                "
                >Doctors App</span
              >
            </RouterLink>
          </div>

          <ul class="hidden md:flex space-x-2 items-center">
            <li>
              <RouterLink to="/" class="menu nav-item">Home</RouterLink>
            </li>
            <li>
              <RouterLink to="/doctors" class="menu nav-item"
                >Doctors</RouterLink
              >
            </li>
            <li>
              <RouterLink to="/contact" class="menu nav-item"
                >Contact</RouterLink
              >
            </li>
          </ul>
        </div>

        <div class="hidden md:flex space-x-2">
          <RouterLink
            to="/login"
            class="singin button hover:bg-pink-500 hover:text-white"
            >Login</RouterLink
          >
          <RouterLink to="/signup" class="singup button button-pink"
            >Signup</RouterLink
          >
        </div>

        <button
          class="md:hidden text-gray-600"
          @click="isMenuHidden = !isMenuHidden"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            class="h-6 w-6"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              stroke-width="2"
              d="M4 6h16M4 12h16M4 18h16"
            />
          </svg>
        </button>
      </div>

      <div
        class="
          bg-blue-100
          w-64
          transform
          fixed
          md:hidden
          left-0
          top-0
          h-screen
          transition
          flex flex-col
          space-y-10
          items-center
          justify-center
        "
        :class="{ '-translate-x-64': isMenuHidden }"
      >
        <h4
          class="
            text-4xl
            font-bold
            bg-clip-text
            text-transparent
            bg-gradient-to-r
            from-green-500
            to-blue-500
          "
        >
          Doctors App
        </h4>

				<ul class="w-64 text-center">
					<li>
						<RouterLink @click="hide"
							to="/"
							class="menu block border-b border-blue-200 py-5 px-3 font-semibold tracking-widest transition hover:bg-blue-200 rounded-2xl shadow-lg"
						>Home</RouterLink>
					</li>
					<li>
						<RouterLink @click="hide"
							to="/doctors"
							class="menu block border-b border-blue-200 py-5 px-3 font-semibold tracking-widest transition hover:bg-blue-200 rounded-2xl shadow-lg"
						>Doctor</RouterLink>
					</li>
					<li>
						<RouterLink @click="hide"
							to="/contact"
							class="menu block border-b border-blue-200 py-5 px-3 font-semibold tracking-widest transition hover:bg-blue-200 rounded-2xl shadow-lg"
						>Contact</RouterLink>
					</li>
				</ul>

				<div class="flex space-x-2 pt-10">
					<RouterLink @click="hide" to="/login" class="singin button hover:bg-pink-500 hover:text-white">Login</RouterLink>
					<RouterLink @click="hide" to="/signup" class="singup button button-pink">Signup</RouterLink>
				</div>
			</div>
		</nav>
	</div>
</template>