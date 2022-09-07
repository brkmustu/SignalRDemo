<script setup>
import { onMounted, ref, reactive, inject } from "vue";
import { useToast } from "vue-toastification";
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
const toast = useToast();
const appAxios = inject("appAxios");

const user = ref({
  email: "",
  password: "",
  repassword: "",
  permissions: ["user"],
});

const onSubmit = () => {
  if (!user.value.email || !user.value.password || !user.value.repassword) {
    toast.warning("Lütfen ilgili alanları doldurun!");
    return;
  }
  if (user.value.password != user.value.repassword) {
    toast.warning("Şifre değerleri eşleşmiyor!");
    return;
  }

  appAxios
    .post("/accounts/register", user.value)
    .then(function (response) {
      toast.success("Kaydınız başarıyla oluşturulmuştur.");
      setTimeout(() => {
        router.push({ path: "/signin" });
      }, 1500);
    })
    .catch(function (error) {
      console.log(error);
      toast.error("Kayıt oluşturma esnasında bir hata oluştu!");
    });
};

const updateEmail = (event) => {
  user.value.email = event;
};
const updatePassword = (event) => {
  user.value.password = event;
};
const updateRePassword = (event) => {
  user.value.repassword = event;
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
                  <h4 class="text-white font-weight-bolder text-center mt-2 mb-0">Kayıt Ol</h4>
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
                  <MaterialInput id="rePassword" class="input-group-outline mb-3" @updateValue="updateRePassword" :label="{ text: 'Şifre Tekrar', class: 'form-label' }" type="password" />
                  <div class="text-center">
                    <button class="my-4 mb-2 btn bg-gradient-success btn-md w-100 false my-4 mb-2">Kayıt Ol</button>
                  </div>
                  <p class="mt-4 text-sm text-center">
                    Hesabınız var mı?
                    <router-link to="/signin" class="text-success text-gradient font-weight-bold"> Oturum Aç </router-link>
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
