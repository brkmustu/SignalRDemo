<script setup>
import { onMounted } from "vue";

// nav-pill
import setNavPills from "@/assets/js/nav-pills.js";

onMounted(() => {
  setNavPills();
});
</script>
<template>
  <section class="py-1">
    <div class="container">
      <div class="row">
        <div class="col-lg-4 mx-auto">
          <div class="nav-wrapper position-relative end-0">
            <ul class="nav nav-pills nav-fill p-1" role="tablist">
              <slot name="tabList" />
            </ul>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-lg-12 mx-auto mt-1">
          <div class="tab-content" id="pills-tabContent">
            <slot name="tabBody" />
          </div>
        </div>
      </div>
    </div>
  </section>
</template>
