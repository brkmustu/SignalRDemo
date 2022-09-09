<script setup>
import { onMounted, ref, computed, inject } from "vue";
import { useStore } from "vuex";
import { useToast } from "vue-toastification";

//example components
import NavbarDefault from "@/components/NavbarDefault.vue";
import NavPills from "@/components/NavPills.vue";

//image
import image from "@/assets/img/illustrations/illustration-signin.jpg";

//material components
import MaterialInput from "@/components/MaterialInput.vue";
import MaterialTextArea from "@/components/MaterialTextArea.vue";
import MaterialButton from "@/components/MaterialButton.vue";
import CenteredBlogCard from "@/components/CenteredBlogCard.vue";

// material-input
import setMaterialInput from "@/assets/js/material-input";

const appAxios = inject("appAxios");
const store = useStore();
const toast = useToast();
const cars = computed(() => store.getters.cars);
const bmwImages = computed(() => store.getters.carImages(1));
const mercedesImages = computed(() => store.getters.carImages(2));

const getTabClass = (carCode) => {
  return store.getters.selectedCar.code === carCode ? "show active" : "";
};

const showTab = (carCode) => {
  if (!store.getters.selectedCar) {
    setTimeout(() => {
      return getTabClass(carCode);
    }, 1000);
  } else {
    return getTabClass(carCode);
  }
};

const setBackgroundUrl = (carImage) => {
  appAxios
    .put("/cars/updateImage", { id: carImage.carId, url: carImage.url })
    .then(function (response) {
      toast.success("Arka plan resmi başarıyla güncellendi!");
    })
    .catch(function (error) {
      console.log(error)
      toast.error("Arka plan resmini güncelleme işlemi esnasında hata alındı.");
    });
};

onMounted(() => {
  setMaterialInput();
});
</script>
<template>
  <div class="container position-sticky z-index-sticky top-0">
    <div class="row">
      <div class="col-12">
        <NavbarDefault
          :sticky="true"
          :action="{
            route: 'https://www.creative-tim.com/product/vue-material-kit-pro',
            color: 'bg-gradient-success',
            label: 'Buy Now',
          }"
        />
      </div>
    </div>
  </div>
  <section class="py-7">
    <NavPills>
      <template #tabList>
        <li class="nav-item">
          <a class="nav-link mb-0 px-0 py-1 active" data-bs-toggle="tab" data-bs-target="#pills-bmw" role="tab" aria-controls="profile" aria-selected="true"> BMW </a>
        </li>
        <li class="nav-item">
          <a class="nav-link mb-0 px-0 py-1" data-bs-toggle="tab" data-bs-target="#pills-mercedes" role="tab" aria-controls="dashboard" aria-selected="false"> Mercedes </a>
        </li>
      </template>

      <template #tabBody>
        <div class="tab-pane fade" :class="showTab('bmw')" id="pills-bmw" role="tabpanel" aria-labelledby="pills-home-tab">
          <div class="container">
            <div class="row">
              <div class="col-lg-3 mx-auto mt-1" v-for="item in bmwImages">
                <div class="page-header min-vh-45">
                  <CenteredBlogCard :image="item.url" :title="item.title" :description="item.description">
                    <template #buttonSection>
                      <button type="button" class="btn btn-sm mb-0 mt-3 bg-gradient-info" @click="setBackgroundUrl(item)">Arka Planı Ayarla</button>
                    </template>
                  </CenteredBlogCard>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="tab-pane fade" :class="showTab('mercedes')" id="pills-mercedes" role="tabpanel" aria-labelledby="pills-profile-tab">
          <div class="container">
            <div class="row">
              <div class="col-lg-3 mx-auto mt-1" v-for="item in mercedesImages">
                <div class="page-header min-vh-45">
                  <CenteredBlogCard :image="item.url" :title="item.title" :description="item.description">
                    <template #buttonSection>
                      <button type="button" class="btn btn-sm mb-0 mt-3 bg-gradient-info" @click="setBackgroundUrl(item)">Arka Planı Ayarla</button>
                    </template>
                  </CenteredBlogCard>
                </div>
              </div>
            </div>
          </div>
        </div>
      </template>
    </NavPills>
  </section>
</template>
