<script setup lang="ts">

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
          Doctor Register
        </h1>

        <form
          class="md:mt-8 flex flex-col space-y-4"
        >
          <div>
            <input
              class="outline-none w-full p-4 rounded ring-1 focus:ring"
              placeholder="Phone"
              type="text"
            />
          </div>

          <div>
            <input
              class="outline-none w-full p-4 rounded ring-1 focus:ring"
              placeholder="Specialty"
              type="text"
            />
          </div>
          <div>
            <input
              class="outline-none w-full p-4 rounded ring-1 focus:ring"
              placeholder="Ticket Price"
              type="text"
            />
          </div>

          <div>
            <textarea
              class="w-full outline-none py-4 ring-1 focus:ring rounded px-5"
              placeholder="BIO"
              rows="4"
            ></textarea>
          </div>
          <div class="flex gap-4 items-center  ">
            <p class="text-gray-500 text-lg traking-wider">Your Photo :</p>
            <input class="file-upload-input" type="file" onchange="readURL(this)" accept="Image/*">
          </div>
          <div class="flex gap-4 items-center  ">
            <p class="text-gray-500 text-lg traking-wider">Your Certificate :</p>
            <input class="file-upload-input" type="file" onchange="readURL(this)" accept="Image/*">
          </div>

          <div class="text-center pt-5">
            <button type="submit" class="w-full text-white button-form">
              Register
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>