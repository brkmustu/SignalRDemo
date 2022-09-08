<script setup>
import { onMounted, ref } from "vue";
import { useStore } from "vuex";

//example components
import NavbarDefault from "@/components/NavbarDefault.vue";
import NavPills from "@/components/NavPills.vue";

//image
import image from "@/assets/img/illustrations/illustration-signin.jpg";

//material components
import MaterialInput from "@/components/MaterialInput.vue";
import MaterialTextArea from "@/components/MaterialTextArea.vue";
import MaterialButton from "@/components/MaterialButton.vue";

// material-input
import setMaterialInput from "@/assets/js/material-input";

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

const currentCar = ref({
  id: 1,
  code: "",
  name: "",
  imageUrl: "",
  description: "",
});

const changeSelectedCar = (car) => {
  
  // currentCar.value = car;
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
          <div class="container">
            <div class="row">
              <div class="col-lg-6 mx-auto mt-1">
                <div class="page-header min-vh-45">
                  <div
                    id="carouselExampleCaptions"
                    class="carousel slide"
                    data-bs-ride="carousel"
                  >
                    <div class="carousel-indicators">
                      <button
                        type="button"
                        data-bs-target="#carouselExampleCaptions"
                        data-bs-slide-to="0"
                        class="active"
                        aria-current="true"
                        aria-label="Slide 1"
                      ></button>
                      <button
                        type="button"
                        data-bs-target="#carouselExampleCaptions"
                        data-bs-slide-to="1"
                        aria-label="Slide 2"
                      ></button>
                      <button
                        type="button"
                        data-bs-target="#carouselExampleCaptions"
                        data-bs-slide-to="2"
                        aria-label="Slide 3"
                      ></button>
                    </div>
                    <div class="carousel-inner">
                      <div class="carousel-item active">
                        <img
                          src="/src/assets/img/bmw-i8.jpg"
                          class="d-block w-100"
                          alt="..."
                        />
                        <div class="carousel-caption d-none d-md-block">
                          <h5>First slide label</h5>
                          <p>
                            Some representative placeholder content for the
                            first slide.
                          </p>
                        </div>
                      </div>
                      <div class="carousel-item">
                        <img
                          src="/src/assets/img/bmw-i8-left-door.jpg"
                          class="d-block w-100"
                          alt="..."
                        />
                        <div class="carousel-caption d-none d-md-block">
                          <h5>Second slide label</h5>
                          <p>
                            Some representative placeholder content for the
                            second slide.
                          </p>
                        </div>
                      </div>
                      <div class="carousel-item">
                        <img
                          src="/src/assets/img/bmw-i8-light.jpg"
                          class="d-block w-100"
                          alt="..."
                        />
                        <div class="carousel-caption d-none d-md-block">
                          <h5>Third slide label</h5>
                          <p>
                            Some representative placeholder content for the
                            third slide.
                          </p>
                        </div>
                      </div>
                    </div>
                    <button
                      class="carousel-control-prev"
                      type="button"
                      data-bs-target="#carouselExampleCaptions"
                      data-bs-slide="prev"
                    >
                      <span
                        class="carousel-control-prev-icon"
                        aria-hidden="true"
                      ></span>
                      <span class="visually-hidden">Previous</span>
                    </button>
                    <button
                      class="carousel-control-next"
                      type="button"
                      data-bs-target="#carouselExampleCaptions"
                      data-bs-slide="next"
                    >
                      <span
                        class="carousel-control-next-icon"
                        aria-hidden="true"
                      ></span>
                      <span class="visually-hidden">Next</span>
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div
          class="tab-pane fade"
          :class="showTab('mercedes')"
          id="pills-mercedes"
          role="tabpanel"
          aria-labelledby="pills-profile-tab"
        >
          <div class="container">
            <div class="row">
              <div class="col-lg-6 mx-auto mt-1">
                <div class="page-header min-vh-45">
                  <div
                    id="carouselExampleCaptions"
                    class="carousel slide"
                    data-bs-ride="carousel"
                  >
                    <div class="carousel-indicators">
                      <button
                        type="button"
                        data-bs-target="#carouselExampleCaptions"
                        data-bs-slide-to="0"
                        class="active"
                        aria-current="true"
                        aria-label="Slide 1"
                      ></button>
                      <button
                        type="button"
                        data-bs-target="#carouselExampleCaptions"
                        data-bs-slide-to="1"
                        aria-label="Slide 2"
                      ></button>
                      <button
                        type="button"
                        data-bs-target="#carouselExampleCaptions"
                        data-bs-slide-to="2"
                        aria-label="Slide 3"
                      ></button>
                    </div>
                    <div class="carousel-inner">
                      <div class="carousel-item active">
                        <img
                          src="/src/assets/img/bmw-i8.jpg"
                          class="d-block w-100"
                          alt="..."
                        />
                        <div class="carousel-caption d-none d-md-block">
                          <h5>First slide label</h5>
                          <p>
                            Some representative placeholder content for the
                            first slide.
                          </p>
                        </div>
                      </div>
                      <div class="carousel-item">
                        <img
                          src="/src/assets/img/bmw-i8-left-door.jpg"
                          class="d-block w-100"
                          alt="..."
                        />
                        <div class="carousel-caption d-none d-md-block">
                          <h5>Second slide label</h5>
                          <p>
                            Some representative placeholder content for the
                            second slide.
                          </p>
                        </div>
                      </div>
                      <div class="carousel-item">
                        <img
                          src="/src/assets/img/bmw-i8-light.jpg"
                          class="d-block w-100"
                          alt="..."
                        />
                        <div class="carousel-caption d-none d-md-block">
                          <h5>Third slide label</h5>
                          <p>
                            Some representative placeholder content for the
                            third slide.
                          </p>
                        </div>
                      </div>
                    </div>
                    <button
                      class="carousel-control-prev"
                      type="button"
                      data-bs-target="#carouselExampleCaptions"
                      data-bs-slide="prev"
                    >
                      <span
                        class="carousel-control-prev-icon"
                        aria-hidden="true"
                      ></span>
                      <span class="visually-hidden">Previous</span>
                    </button>
                    <button
                      class="carousel-control-next"
                      type="button"
                      data-bs-target="#carouselExampleCaptions"
                      data-bs-slide="next"
                    >
                      <span
                        class="carousel-control-next-icon"
                        aria-hidden="true"
                      ></span>
                      <span class="visually-hidden">Next</span>
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </template>
    </NavPills>
  </section>
</template>
