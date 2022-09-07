<script setup>
//Vue Material Kit 2 components
import MaterialButton from "@/components/MaterialButton.vue";
import { defineProps, computed } from "vue";

const props = defineProps({
  title: {
    type: String,
    default: "",
  },
  id:{
    type: String,
    default: "", 
  }
});

const modalLabelId = computed(() => props.id + "-modal-label")
</script>
<template>
  <div class="container py-7">
    <div class="row mt-2 flex justify-content-center">
      <div class="col-sm-4 col-6 ms-8">
        <!-- Modal -->
        <div class="modal fade" :id="id" tabindex="-1" :aria-labelledby="modalLabelId" aria-hidden="true">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" :id="modalLabelId">{{ title }}</h5>
                <MaterialButton color="none" class="btn-close" data-bs-dismiss="modal" aria-label="Close"> </MaterialButton>
              </div>
              <div class="modal-body">
                <slot />
              </div>
              <div class="modal-footer justify-content-between">
                <slot name="buttonSection"></slot>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
