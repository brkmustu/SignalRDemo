<template>
  <li class="nav-item dropdown dropdown-hover mx-2">
    <a role="button" class="nav-link ps-2 bg-gradient-success text-white rounded d-flex cursor-pointer align-items-center" data-bs-toggle="modal" data-bs-target="#buy-modal" aria-expanded="false"> Satın Al </a>
  </li>
  <li class="nav-item dropdown dropdown-hover mx-2">
    <router-link to="/list" class="nav-link ps-2 bg-gradient-success text-white rounded d-flex cursor-pointer align-items-center"> Satın Alınanlar </router-link>
  </li>
  <li class="nav-item dropdown dropdown-hover mx-2">
    <router-link to="/product-settings" class="nav-link ps-2 bg-gradient-success text-white rounded d-flex cursor-pointer align-items-center"> Ürün Ayarları </router-link>
  </li>
</template>
