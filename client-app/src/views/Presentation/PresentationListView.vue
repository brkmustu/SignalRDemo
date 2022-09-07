<script setup>
import NavbarDefault from "@/components/NavbarDefault.vue";
import PresentationListNavbar from "./Sections/PresentationListNavbarItems.vue";
import PresentationListTable from "./Sections/PresentationListTable.vue";
import { computed, inject, ref } from "vue";

const appAxios = inject("appAxios");
const getCars = inject("getCars");
const getOrders = inject("getOrders");

const cars = ref([]);
const tableDatas = ref([]);

getCars(function (response) {
  response.data.map((x) => cars.value.push(x));
});

getOrders(function (response) {
  response.data.map((x) => {
    let car = cars.value.find((y) => y.id == x.carId);
    tableDatas.value.push({
      id: x.id,
      orderDate: x.date,
      carCode: car.code,
      carName: car.name,
      carImageUrl: car.imageUrl,
      carDescription: car.description,
    });
  });
});
</script>

<template>
  <NavbarDefault>
    <PresentationListNavbar />
  </NavbarDefault>
  <PresentationListTable :headers="['Araç İkonu', 'Araç Kodu', 'Araç Adı', 'Sipariş Tarihi', 'Açıklama']" :rows="tableDatas" />
</template>
