<template>
  <div class="w-full p-20 bg-sky-200">
    <div
      class="
        grid grid-cols-1
        sm:grid-cols-2
        md:grid-cols-3
        lg:grid-cols-4
        gap-16
      "
    >
      <div class="">
        <h1
          class="
            text-center
            font-bold
            bg-gradient-to-r
            from-green-500
            to-blue-500
            text-transparent
            bg-clip-text
            text-4xl
            shadow-lg
            rounded-2xl
            p-4
            hover:from-blue-500 hover:to-green-500
            mb-12
          "
        >
          Doctors
        </h1>
        <p>
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Nesciunt a
          aperiam est perspiciatis reiciendis beatae, saepe et ipsa tempore
          laborum quam placeat soluta.
        </p>

        <h1 class="text-3xl font-semibold font-mono text-cyan-600 my-4">
          Follow Us
        </h1>

        <p class="flex gap-4 mt-2">
          <i class="fab fa-twitter"></i>
          <i class="fab fa-instagram"></i>
          <i class="fab fa-facebook"></i>
          <i class="fab fa-dribbble"></i>
        </p>
      </div>
      <div class="">
        <h1 class="font-bold">For Patients</h1>
        <p
          class="mt-8 text-gray-500 transition duration-150 hover:translate-x-2"
        >
          Search for doctors
        </p>
        <p
          class="mt-8 text-gray-500 transition duration-150 hover:translate-x-2"
        >
          Login
        </p>
        <p
          class="mt-8 text-gray-500 transition duration-150 hover:translate-x-2"
        >
          Register
        </p>
      </div>
      <div>
        <h1 class="font-bold">For Patients</h1>
        <p
          class="mt-8 text-gray-500 transition duration-150 hover:translate-x-2"
        >
          Appointments
        </p>
        <p
          class="mt-8 text-gray-500 transition duration-150 hover:translate-x-2"
        >
          Chat
        </p>
        <p
          class="mt-8 text-gray-500 transition duration-150 hover:translate-x-2"
        >
          Doctor Dashboard
        </p>
      </div>
      <div>
        <h1 class="font-bold">Contact Us</h1>
        <p
          class="mt-8 text-gray-500 transition duration-150 hover:translate-x-2"
        >
          <i class="fas fa-map-marker-alt"></i>
          Hargeisa somaliland
        </p>
        <p
          class="mt-8 text-gray-500 transition duration-150 hover:translate-x-2"
        >
          <i class="fas fa-envelope"></i>
          doctor@example.com
        </p>
        <p
          class="mt-8 text-gray-500 transition duration-150 hover:translate-x-2"
        >
          <i class="fas fa-phone-alt"></i>
          +252 65 9999999
        </p>
      </div>
    </div>

    <div class="my-12 flex">
      <p class="border-b border-gray-400 w-full"></p>
    </div>

    <div class="flex justify-between text-gray-600">
      <p><span>&#169;</span> 2020 Doctor. All rights reserved.</p>

      <p>Terms and Conditions | Policy</p>
    </div>
  </div>
</template>
