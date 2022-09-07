<script setup>
defineProps({
  headers: {
    type: Array,
    default: () => ["Author", "Function", "Status", "Employed", "Action"],
  },
  rows: {
    type: Array,
    required: true,
    id: String,
    orderDate: String,
    carCode: String,
    carName: String,
    carImageUrl: String,
    carDescription: String,
  },
});
</script>
<template>
  <section class="pt-5 mt-5">
    <div class="container">
      <div class="row justify-content-center">
        <div class="col-lg-10">
          <div class="card">
            <div class="table-responsive">
              <table class="table align-items-center mb-0">
                <thead>
                  <tr>
                    <th v-for="(header, index) in headers" :key="header" class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7" :class="{ 'ps-2': index == 1, 'text-center': index > 1 }">
                      {{ header }}
                    </th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="({ id, orderDate, carCode, carName, carImageUrl, carDescription }, index) of rows" :key="index">
                    <td>
                      <div class="d-flex px-2 py-1">
                        <div>
                          <img :src="carImageUrl" class="avatar avatar-sm me-3" />
                        </div>
                      </div>
                    </td>
                    <td>{{ carCode }}</td>
                    <td>{{ carName }}</td>
                    <td>{{ orderDate }}</td>
                    <td>{{ carDescription }}</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>
