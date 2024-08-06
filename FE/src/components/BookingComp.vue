<script setup lang="ts">
const text = defineModel('text', {
  type: String,
  required: false,
  default: ''
})
const checkIn = defineModel('checkIn', {
  type: Object as PropType<Date>,
  required: false
})
const checkOut = defineModel('checkOut', {
  type: Object as PropType<Date>,
  required: false
})
const adults = defineModel('adults', {
  type: Number,
  required: false,
  default: 3
})
const children = defineModel('children', {
  type: Number,
  required: false,
  default: 0
})

const fieldTextStyle = ref('p-2 mx-1 pb-0 font-semibold text-xs uppercase text-gray-500') // needs to move to css
</script>

<template>
  <div
    class="card shadow-9 p-5 gap-2 flex-column z-2"
    style="width: 450px; min-width: 450px; max-width: 450px; transform: translateX(80px)"
  >
    <div class="text-3xl font-bold">The perfect place</div>
    <div class="font-light mb-4">Make your reservation for the perfect trip.</div>
    <div class="card flex-column">
      <div :class="fieldTextStyle">Details</div>
      <Textarea v-model="text" class="w-full border-0" rows="1" autoResize />
    </div>
    <div class="card flex-row">
      <div class="flex-column w-full">
        <div :class="fieldTextStyle">CHECK IN</div>
        <Calendar
          v-model="checkIn"
          :minDate="new Date()"
          :disabledDates="[]"
          class="remove-calendar-border"
        />
      </div>
      <Divider layout="vertical" class="mx-0 my-2 border-1 border-gray-100" />
      <div class="flex-column w-full">
        <div :class="fieldTextStyle">CHECK OUT</div>
        <Calendar
          v-model="checkOut"
          :minDate="new Date()"
          :disabledDates="[]"
          class="remove-calendar-border"
        />
      </div>
    </div>
    <div class="card flex-row">
      <div class="flex-column w-full">
        <div :class="fieldTextStyle">ADULTS</div>
        <Dropdown
          class="gradient-effect w-full border-0 cursor-pointer"
          v-model="adults"
          :options="[3, 4, 5]"
        />
      </div>
      <Divider layout="vertical" class="mx-0 my-2 border-1 border-gray-100" />
      <div class="flex-column w-full">
        <div :class="fieldTextStyle">CHILDREN</div>
        <Dropdown
          class="gradient-effect w-full border-0 cursor-pointer"
          v-model="children"
          :options="[0, 1, 2, 3]"
        />
      </div>
    </div>
    <Button
      class="gradient-effect p-3 font-bold border-0 text-xl font-bold mt-4"
      label="Book now"
    />
  </div>
</template>

<style scoped>
.remove-calendar-border > * {
  border-style: none;
  cursor: pointer;
}

.p-image > * {
  border-radius: var(--border-radius) !important;
}

.p-inputtext {
  padding-top: 1px;
  min-height: 35px !important;
  max-height: 300px !important;
}

.p-highlight {
  background-size: 300% 100%;
  background-image: linear-gradient(42deg, #4158d0 0%, #c850c0 46%, #ffcc70 100%);
  transition: all 0.4s ease;
}
.p-highlight:hover {
  background-position: 100% 0;
}
</style>
