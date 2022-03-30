<script setup lang="ts">
import { ApiService } from '../services/api-service';

type NewDoctor = {
  phone?: string;
  specialty?: string;
  bio?: string;
  ticketPrice?: number;
  picture?: File;
  certificate?: File;
};

const doctor: NewDoctor = {};

const setFile = (e: Event, property: 'picture' | 'certificate') => {
  const inputElement = e.target as HTMLInputElement;
  const files = inputElement.files;
  if (!files || files.length < 1) {
    return;
  }
  
  doctor[property] = files[0];
}

const saveDoctor = async () => {
  const data = new FormData();
  data.append('phone', doctor.phone!);
  data.append('specialty', doctor.specialty!);
  data.append('bio', doctor.bio!);
  data.append('ticketPrice', doctor.ticketPrice!.toString());
  data.append('picture', doctor.picture!, doctor.picture!.name);
  data.append('certificate', doctor.certificate!, doctor.certificate!.name);

  const result = await ApiService.upload('doctors', data);
  console.log('Result', result);
}

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
          @submit.prevent="saveDoctor"
        >
          <div>
            <input
              class="outline-none w-full p-4 rounded ring-1 focus:ring"
              placeholder="Phone"
              v-model="doctor.phone"
              type="text"
            />
          </div>

          <div>
            <input
              class="outline-none w-full p-4 rounded ring-1 focus:ring"
              placeholder="Specialty"
              type="text"
              v-model="doctor.specialty"
            />
          </div>
          <div>
            <input
              class="outline-none w-full p-4 rounded ring-1 focus:ring"
              placeholder="Ticket Price"
              type="text"
              v-model="doctor.ticketPrice"
            />
          </div>

          <div>
            <textarea
              class="w-full outline-none py-4 ring-1 focus:ring rounded px-5"
              placeholder="BIO"
              rows="4"
              v-model="doctor.bio"
            ></textarea>
          </div>
          <div class="flex gap-4 items-center  ">
            <p class="text-gray-500 text-lg traking-wider">Photo</p>
            <input class="file-upload-input" type="file" @change="(e) => setFile(e, 'picture')" accept="Image/*">
          </div>
          <div class="flex gap-4 items-center ">
            <p class="text-gray-500 text-lg traking-wider">Certificate</p>
            <input class="file-upload-input" type="file" @change="(e) => setFile(e, 'certificate')" accept="Image/*">
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