<script setup>
import { onMounted, ref, inject } from "vue";
import { useToast } from "vue-toastification";
import { useStore } from "vuex";
import { useRouter } from "vue-router";
// example components
import Header from "@/views/LandingPages/Header.vue";

//Vue Material Kit 2 components
import MaterialInput from "@/components/MaterialInput.vue";
import MaterialSwitch from "@/components/MaterialSwitch.vue";
import MaterialButton from "@/components/MaterialButton.vue";

// material-input
import setMaterialInput from "@/assets/js/material-input";

const router = useRouter();
const store = useStore();
const toast = useToast();
const appAxios = inject("appAxios");

const user = ref({
  email: "",
  password: "",
});

const updateEmail = (event) => {
  user.value.email = event;
};
const updatePassword = (event) => {
  user.value.password = event;
};

const onSubmit = () => {
  if (!user.value.email || !user.value.password) {
    toast.warning("Lütfen ilgili alanları doldurun!");
    return;
  }

  appAxios
    .get("/accounts/signin", { params: { email: user.value.email } })
    .then(function (response) {
      if (response && response.data && response.data.length > 0) {
        toast.success("Oturum açma işlemi başarılı.");
        store.commit("setSignInInfo", { user: response.data[0], jwtToken: "txaxTHkm3GBGWckwUa2h" });
        router.push({ path: "/" });
      } else {
        toast.error("Girdiğiniz e posta adresiyle herhangi bir kayıt bulunamamıştır!");
      }
    })
    .catch(function (error) {
      console.log(error);
      toast.error("Oturum açma işlemi esnasında bir hata oluştu!");
    });
};

onMounted(() => {
  setMaterialInput();
});
</script>
<template>
  <Header>
    <div
      class="page-header align-items-start min-vh-100"
      :style="{
        backgroundImage: 'url(https://images.unsplash.com/photo-1497294815431-9365093b7331?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1950&q=80)',
      }"
      loading="lazy"
    >
      <span class="mask bg-gradient-dark opacity-6"></span>
      <div class="container my-auto">
        <div class="row">
          <div class="col-lg-4 col-md-8 col-12 mx-auto">
            <div class="card z-index-0 fadeIn3 fadeInBottom">
              <div class="card-header p-0 position-relative mt-n4 mx-3 z-index-2">
                <div class="bg-gradient-success shadow-success border-radius-lg py-3 pe-1">
                  <h4 class="text-white font-weight-bolder text-center mt-2 mb-0">Giriş</h4>
                  <div class="row mt-3">
                    <div class="col-2 text-center ms-auto">
                      <a class="btn btn-link px-3" href="javascript:;">
                        <i class="fa fa-facebook text-white text-lg"></i>
                      </a>
                    </div>
                    <div class="col-2 text-center px-1">
                      <a class="btn btn-link px-3" href="javascript:;">
                        <i class="fa fa-github text-white text-lg"></i>
                      </a>
                    </div>
                    <div class="col-2 text-center me-auto">
                      <a class="btn btn-link px-3" href="javascript:;">
                        <i class="fa fa-google text-white text-lg"></i>
                      </a>
                    </div>
                  </div>
                </div>
              </div>
              <div class="card-body">
                <form role="form" @submit.prevent="onSubmit" class="text-start">
                  <MaterialInput id="email" class="input-group-outline my-3" @updateValue="updateEmail" :label="{ text: 'E-posta', class: 'form-label' }" type="email" />
                  <MaterialInput id="password" class="input-group-outline mb-3" @updateValue="updatePassword" :label="{ text: 'Şifre', class: 'form-label' }" type="password" />
                  <MaterialSwitch class="d-flex align-items-center mb-3" id="rememberMe" labelClass="mb-0 ms-3" checked>Beni hatırla</MaterialSwitch>

                  <div class="text-center">
                    <MaterialButton class="my-4 mb-2" variant="gradient" color="success" fullWidth>Oturum aç</MaterialButton>
                  </div>
                  <p class="mt-4 text-sm text-center">
                    Hesabınız yok mu?
                    <router-link to="/register" class="text-success text-gradient font-weight-bold"> Kayıt Ol </router-link>
                  </p>
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </Header>
</template>
