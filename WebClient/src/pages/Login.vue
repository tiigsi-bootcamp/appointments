<script setup lang="ts">
import Footer from '../components/Footer.vue';
const handleLogin = function () {};
</script>

<template>
  <div class="flex md:space-x-5 px-4 md:px-24 py-16 items-center">
    <div class="hidden md:block flex-1">
      <img class="" src="/images/login.svg" alt="" />
    </div>

    <div class="flex-1">
      <div
        class="
          bg-gray-50
          rounded-md
          shadow-lg shadow-cyan-300/50
          block
          max-w-96
          p-8
          border-2 border-cyan-500
        "
      >
        <h1 class="font-semibold text-2xl text-gray-500 p-4">Login</h1>

        <form
          @submit.prevent="handleLogin"
          class="md:mt-8 flex flex-col space-y-4"
        >
          <div>
            <input
              class="outline-none w-full p-4 rounded ring-1 focus:ring"
              placeholder="Email"
              type="email"
            />
          </div>

          <div>
            <input
              class="outline-none w-full p-4 rounded ring-1 focus:ring"
              placeholder="Password"
              type="password"
            />
          </div>

          <div class="flow-root">
            <a
              href="#"
              class="
                float-right
                text-gray-500 text-sm
                hover:text-cyan-300
                transition
              "
              >Forgot Password ?</a
            >
          </div>

          <div class="text-center">
            <button type="submit" class="w-full text-white button-form">
              Login
            </button>

            <div>
              <p class="text-gray-500 mt-5">
                Don't have an account?
                <RouterLink class="text-cyan-500" to="/signup"
                  >Register</RouterLink
                >
              </p>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>

</template>
