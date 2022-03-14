<script setup lang="ts">
import Footer from '../components/Footer.vue';
const handleLogin = function () {};
</script>

<template>
  <div class="flex md:space-x-5  items-center px-4 md:px-24 py-16">
    <div class="hidden md:block flex-1">
      <img class="" src="/images/login.svg" alt="" />
    </div>

    <div class="flex-1">
      <div
        class="
          bg-gray-50
          rounded-md
          shadow-2xl shadow-cyan-500/50
          block
          max-w-96
          p-8
          border-2 border-cyan-500
        "
      >
        <h1 class="font-semibold text-2xl text-gray-500 p-4">
          Patient Register
        </h1>

        <form
          @submit.prevent="handleLogin"
          class="md:mt-8 flex flex-col space-y-4"
        >
          <div>
            <input
              class="outline-none w-full p-4 rounded ring-1 focus:ring"
              placeholder="Name"
              type="text"
            />
          </div>

          <div>
            <input
              class="outline-none w-full p-4 rounded ring-1 focus:ring"
              placeholder="Mobile Number"
              type="text"
            />
          </div>

          <div>
            <input
              class="outline-none w-full p-4 rounded ring-1 focus:ring"
              placeholder="Create Password"
              type="password"
            />
          </div>

          <div class="flow-root">
            <a href="#"></a>

            <RouterLink
              to="/login"
              class="
                float-right
                text-gray-500 text-sm
                hover:text-cyan-300
                transition
              "
              >Already have an account?</RouterLink
            >
          </div>

          <div class="text-center">
            <button type="submit" class="w-full text-white button-form">
              Signup
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>
