<script setup lang="ts">
import { ref } from 'vue';
import { ApiService } from '../services/api-service';
import { TokenService } from '../services/token-service';

const gapi = (window as any).gapi;

await gapi.load("auth2", async function () {
  await gapi.auth2.init({ client_id: '385352880967-jn9jkdi0a3lheu4oc3q71jhu7bj820eh.apps.googleusercontent.com' });
});

const invalid = ref(false);

const email = ref('');
const password = ref('');

const handleLogin = async function () {
  const result = await ApiService.login({
    email: email.value,
    password: password.value
  });

if (!result) {
  invalid.value = true;
}

// TODO: Navigate.
};

const loginWithGoogle = async () => {
  const authInstance = gapi.auth2.getAuthInstance();

  await authInstance.signIn();

  const googleUser = authInstance.currentUser.get();
  const token = googleUser.getAuthResponse().id_token;

  const result = await ApiService.loginWithProvider({ provider: 'Google', token });

  const user = TokenService.decode();
  console.log('Current user', user?.name);

  // TODO: Navigate to other pages...
}
</script>

<template>
  <div class="flex md:space-x-5 px-4 md:px-24 py-16 items-center">
    <div class="hidden md:block flex-1">
      <img class src="/images/login.svg" />
    </div>

    <div class="flex-1">
      <div
        class="bg-gray-50 rounded-md shadow-lg shadow-cyan-300/50 block max-w-96 p-8 border-2 border-cyan-500"
      >
        <h1 class="font-semibold text-2xl text-gray-500 p-4">Login</h1>

        <h4 v-if="invalid" class="text-red-500">Email or password is incorrect.</h4>

        <form @submit.prevent="handleLogin" class="md:mt-8 flex flex-col space-y-4">
          <div>
            <input
              class="outline-none w-full p-4 rounded ring-1 focus:ring"
              placeholder="Email"
              type="email"
              v-model="email"
            />
          </div>

          <div>
            <input
              class="outline-none w-full p-4 rounded ring-1 focus:ring"
              placeholder="Password"
              type="password"
              v-model="password"
            />
          </div>

          <div class="flow-root">
            <a
              href="#"
              class="float-right text-gray-500 text-sm hover:text-cyan-300 transition"
            >Forgot Password ?</a>
          </div>

          <div class="text-center">
            <button type="submit" class="w-full text-white button-form">Login</button>

            <div>
              <div class="flex flex-col justify-between">
                <button
                  type="button"
                  @click="loginWithGoogle"
                  class="mt-5 bg-red-600 text-white px-4 py-2 hover:bg-red-500 tracking-widest transition font-semibold rounded"
                >
                  <i class="fab fa-google"></i>
                  Login with Google
                </button>
                <button
                  type="button"
                  class="mt-5 bg-blue-700 text-white px-4 py-2 hover:bg-blue-500 tracking-widest transition font-semibold rounded"
                >
                  <i class="fab fa-facebook"></i>
                  Login with Facebook
                </button>
              </div>
              <p class="text-gray-500 mt-5">
                Don't have an account?
                <RouterLink class="text-cyan-500" to="/signup">Register</RouterLink>
              </p>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>
