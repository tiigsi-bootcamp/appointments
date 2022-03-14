<script setup lang="ts">

 defineProps<{name:string}>();
</script>

<template>
  <div
    class="
      flex
      md:items-center md:px-28
      flex-col
      md:flex-row
      py-16
      space-y-10
      px-6
    "
  >
    <div class="flex-1">
      <div
        class="
          mx-auto
          md:mx-0
          border-4 border-cyan-500
          shadow-lg
          shadow-cyan-300/40
          overflow-hidden
          rounded-full
          w-96
          h-96
        "
      >
       <slot>
       </slot>
      </div>
      <h1 class="text-center mt-10 text-3xl font-semibold">{{name}}</h1>
    </div>
    <div
      class="
        flex-1
        max-w-xl
        bg-gray-50
        rounded-md
        shadow-lg shadow-cyan-300/50
        max-w-96
        p-8
        border-2 border-cyan-500
      "
    >
      <h1 class="font-semibold text-3xl text-gray-500 py-8">Book Now</h1>

      <div class="flex flex-col space-y-4">
        <div>
          <input
            class="outline-none w-full p-4 rounded ring-1 focus:ring"
            placeholder="Full Name"
            type="email"
          />
        </div>
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
            placeholder="Address"
            type="email"
          />
        </div>
        <div >
          <input
            class="outline-none w-full p-4 rounded ring-1 focus:ring"
            placeholder="Age"
            type="number"
          />
        </div>

        <div class="text-lg text-gray-600 space-x-5 py-3 font-semibold">
          <label><input type="radio" name="money" class="text-cyan-500 focus:ring-cyan-500" /> Zaad</label>
          <label><input type="radio" name="money" class="text-cyan-500 focus:ring-cyan-500" /> Edahab</label>
        </div>

        <div class="text-center">
          <button type="submit" class="w-full text-white button-form">
            Book
          </button>
        </div>
      </div>
    </div>
  </div>
</template>