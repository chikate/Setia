<template>
  <div class="custom-shadow-1 p-5 flex flex-column w-3 m-5">
    <div class="text-3xl font-bold">The perfect place</div>
    <div class="font-light mb-4">
      Make your reservation for the perfect trip.
    </div>
    <div class="flex flex-column">
      <div :class="fieldTextStyle">Details</div>
      <Textarea
        v-model="text"
        class="border-0 bg-transparent"
        rows="1"
        autoResize
      />
    </div>
    <div class="flex flex-row">
      <div class="flex flex-column">
        <div :class="fieldTextStyle">CHECK IN</div>
        <Calendar
          v-model="checkIn"
          :minDate="new Date()"
          :disabledDates="[]"
          class="bg-transparent"
        />
      </div>
      <Divider layout="vertical" class="mx-0 my-2 border-1 border-gray-300" />
      <div class="flex flex-column">
        <div :class="fieldTextStyle">CHECK OUT</div>
        <Calendar
          v-model="checkOut"
          :minDate="new Date()"
          :disabledDates="[]"
          class="bg-transparent"
        />
      </div>
    </div>
    <div class="flex flex-row">
      <div class="flex flex-column">
        <div :class="fieldTextStyle">ADULTS</div>
        <Dropdown
          class="button-gradient-effect cursor-pointer bg-transparent"
          v-model="adults"
          placeholder="Select number of adults"
          :options="[3, 4, 5]"
        />
      </div>
      <Divider layout="vertical" class="mx-0 my-2 border-1 border-gray-300" />
      <div class="flex flex-column">
        <div :class="fieldTextStyle">CHILDREN</div>
        <Dropdown
          class="button-gradient-effect cursor-pointer bg-transparent"
          v-model="children"
          placeholder="Select number of childrens"
          :options="[0, 1, 2, 3]"
        />
      </div>
    </div>
    <Button
      class="button-gradient-effect p-3 font-bold text-xl font-bold mt-4 border-5 border-gray-200"
      label="Book now"
    />
  </div>
</template>

<script setup lang="ts">
const text = defineModel("text", {
  type: String,
  required: false,
  default: "",
});
const checkIn = defineModel("checkIn", {
  type: Object as PropType<Date>,
  required: false,
});
const checkOut = defineModel("checkOut", {
  type: Object as PropType<Date>,
  required: false,
});
const adults = defineModel("adults", {
  type: Number,
  required: false,
  default: 3,
});
const children = defineModel("children", {
  type: Number,
  required: false,
  default: 0,
});

const fieldTextStyle = ref(
  "p-2 mx-1 pb-0 font-semibold text-xs uppercase text-gray-500"
);
</script>

<style scoped>
:deep().p-inputtext,
.p-textarea,
.p-select {
  background-color: transparent;
  box-shadow: none;
}
</style>
