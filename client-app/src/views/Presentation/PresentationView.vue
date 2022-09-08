<script setup>
import NavbarDefault from "@/components/NavbarDefault.vue";
import SimpleModal from "@/components/SimpleModal.vue";
import PresentationBanner from "@/views/Presentation/Sections/PresentationBanner.vue";
import PresentationNavbar from "./Sections/PresentationNavbarItems.vue";
import HorizonalProductCard from "@/components/HorizonalProductCard.vue";
import { computed, inject, onBeforeMount } from "vue";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
import { useToast } from "vue-toastification";

const appAxios = inject("appAxios");
const router = useRouter();
const store = useStore();
const toast = useToast();
const selectedCar = computed(() => store.getters.selectedCar);

const getToday = () => {
  var today = new Date();
  var dd = String(today.getDate()).padStart(2, "0");
  var mm = String(today.getMonth() + 1).padStart(2, "0"); //January is 0!
  var yyyy = today.getFullYear();

  return yyyy + "-" + mm + "-" + dd;
};

const saveOrder = () => {
  appAxios
    .post("/orders/insert", { carId: store.getters.selectedCar.id })
    .then(function (response) {
      toast.success("Sipariş başarıyla kaydedildi.");
    })
    .catch(function (error) {
      console.log(error)
      toast.error("Siparişi kaydederken bir hata oluştu!");
    });
};

onBeforeMount(() => {
  appAxios.get("/cars/getAll").then(function (response) {
    store.commit("setCars", response.data);
  });
});
</script>

<template>
  <NavbarDefault>
    <PresentationNavbar />
  </NavbarDefault>
  <PresentationBanner />
  <SimpleModal id="buy-modal">
    <template #default>
      <HorizonalProductCard class="mt-4" :image="selectedCar.imageUrl" :profile="{ name: selectedCar.name, link: 'javascript:;' }" :position="{ label: selectedCar.code, color: 'success' }" :description="selectedCar.description" />
    </template>
    <template #buttonSection>
      <MaterialButton variant="gradient" color="dark" data-bs-dismiss="modal"> Kapat </MaterialButton>
      <button class="bg-gradient-success text-white rounded btn-md w-20" @click="saveOrder">Kaydet</button>
    </template>
  </SimpleModal>
</template>
