<script setup lang="ts">
import DoctorsCard from "../components/DoctorsCard.vue";
import { RouterLink } from "vue-router";
import { Doctor } from "../models/doctor";
import { ref } from "vue";
import { ApiService } from "../services/api-service";

let doctors = ref<Doctor[]>([]);

// fetch('https://localhost:7028/Doctors?page=0&size=10')
//   .then(response => response.json())
//   .then(json => doctors.value = json);

// const response = await fetch('https://localhost:7028/Doctors?page=0&size=10');

// const result = await response.json();
// doctors.value = result;

doctors.value = await ApiService.getDoctors(0,10);

console.log('list', doctors);

</script>

<template>
  <div class="flex justify-center">
    <h1
      class="text-center font-bold bg-gradient-to-r from-green-500 to-blue-500 text-transparent bg-clip-text text-6xl shadow-lg rounded-2xl p-4 hover:from-blue-500 hover:to-green-500 transition-all"
    >Doctors</h1>
  </div>
  <div class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-y-4">
    <DoctorsCard v-for="item in doctors" :name="item.user!.fullName" :specialty="item.specialty">
      <template v-slot:button>
        <RouterLink
          to="one"
          class="rounded-lg text-white bg-cyan-500 transition hover:bg-cyan-400 my-5 px-10 py-1"
        >Book Now</RouterLink>
      </template>
    </DoctorsCard>

    <DoctorsCard name="Xawa Abdi" specialty="Midwifery">
      <img
        class="shadow-md rounded-lg object-cover transition duration-300 hover:scale-90 h-full w-full"
        src="images/istockphoto.jpg"
        alt="Doctor image not found"
      />
      <template v-slot:button>
        <RouterLink
          to="two"
          class="rounded-lg text-white bg-cyan-500 transition hover:bg-cyan-400 my-10 px-10 md:px-24 py-1"
        >Book Now</RouterLink>
      </template>
    </DoctorsCard>

    <DoctorsCard name="Samir Nasri" specialty="Nerves">
      <template v-slot:button>
        <RouterLink
          to="#"
          class="rounded-lg text-white bg-cyan-500 transition hover:bg-cyan-400 my-10 px-10 py-1"
        >Book Now</RouterLink>
      </template>
    </DoctorsCard>

    <DoctorsCard name="Test Person" specialty="PhD in testing & stuff">
      <template v-slot:button>
        <RouterLink
          to="#"
          class="rounded-lg text-white bg-cyan-500 transition hover:bg-cyan-400 my-10 px-10 md:px-24 py-1"
        >Book Now</RouterLink>
      </template>
    </DoctorsCard>
  </div>
</template>
