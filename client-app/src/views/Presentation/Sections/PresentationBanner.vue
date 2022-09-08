<script setup>
import { computed } from "vue";
import { useStore } from "vuex";

import NavPills from "@/components/NavPills.vue";

const store = useStore();

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

const getImageUrl = (carCode) => {
  if (store.getters.cars && store.getters.cars.length > 0) {
    return store.getters.cars.find((x) => x.code === carCode).imageUrl;
  } else {
    setTimeout(() => {
      return store.getters.cars.find((x) => x.code === carCode).imageUrl;
    }, 1000);
  }
};

const bgBmw = computed(() => getImageUrl("bmw"));
const bgMercedes = computed(() => getImageUrl("mercedes"));

const changeSelectedCar = (car) => {
  store.commit("setSelectedCar", car);
};
</script>
<template>
  <NavPills>
    <template #tabList>
      <li class="nav-item">
        <a
          class="nav-link mb-0 px-0 py-1 active"
          data-bs-toggle="tab"
          data-bs-target="#pills-bmw"
          @click="changeSelectedCar('bmw')"
          role="tab"
          aria-controls="profile"
          aria-selected="true"
        >
          BMW
        </a>
      </li>
      <li class="nav-item">
        <a
          class="nav-link mb-0 px-0 py-1"
          data-bs-toggle="tab"
          data-bs-target="#pills-mercedes"
          @click="changeSelectedCar('mercedes')"
          role="tab"
          aria-controls="dashboard"
          aria-selected="false"
        >
          Mercedes
        </a>
      </li>
    </template>
    <template #tabBody>
      <div
        class="tab-pane fade"
        :class="showTab('bmw')"
        id="pills-bmw"
        role="tabpanel"
        aria-labelledby="pills-home-tab"
      >
        <div
          class="page-header min-vh-75"
          :style="{ backgroundImage: `url(${bgBmw})` }"
        ></div>
        <br />
        <h3>BMW i8 modelimizi keşfedin.</h3>
      </div>
      <div
        class="tab-pane fade"
        :class="showTab('mercedes')"
        id="pills-mercedes"
        role="tabpanel"
        aria-labelledby="pills-profile-tab"
      >
        <div
          class="page-header min-vh-75"
          :style="{ backgroundImage: `url(${bgMercedes})` }"
        ></div>
        <br />
        <h3>Mercedes EQS modelimizi keşfedin.</h3>
      </div>
    </template>
  </NavPills>
</template>
