import { createApp } from 'vue'
import { createPinia } from "pinia";
import App from './App.vue'
import router from "./router";
import store from "./store";
import Toast, { POSITION }  from "vue-toastification";
import { appAxios, getCars, getOrders, setJwtTokenHeader } from "./utils/appAxios.js"
import { getCarHub } from './utils/carHub.js'

import "vue-toastification/dist/index.css";

import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";

// Nucleo Icons
import "./assets/css/nucleo-icons.css";
import "./assets/css/nucleo-svg.css";

import materialKit from "@/material-kit.js";

const app = createApp(App);

app.use(createPinia());
app.use(store);
app.use(router);
app.use(Toast, {
    // Setting the global default position
    position: POSITION.BOTTOM_RIGHT
});
app.provide("appAxios", appAxios);
app.provide("getCars", getCars);
app.provide("getOrders", getOrders);
app.provide("getCarHub", getCarHub);
app.provide("setJwtTokenHeader", setJwtTokenHeader);
app.use(materialKit);
app.mount("#app");
