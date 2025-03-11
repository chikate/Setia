<template>
  <div class="h-full flex-column p-2">
    <div class="h-full flex-row align-items-center justify-content-center w-full">
      <img
        src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT-hQ3atw-HFLYiFyDBkdZlUvwWu0OOJMl8fQx2j6lE0PQKvW4iRHjeZhY-JAcmjGLs1j8&usqp=CAU"
      />
      <Accordion style="width: 300px">
        <AccordionTab header="Language">
          <div class="flex-column gap-3 mt-2">
            <div
              v-for="language in languages"
              :key="language"
              :style="{
                borderColor: stringToColor(language),
                color: stringToColor(language),
                animation: 'electricBorder 1.5s infinite alternate'
              }"
              class="flex-row border-3 border-round align-items-center text-start"
            >
              <label class="text-start w-full p-3">{{ language }}</label>
              <div v-tooltip="'Natives'" class="flex-row gap-2 px-2">
                <i class="pi pi-user" /> 2000
              </div>
            </div>
          </div>
        </AccordionTab>
      </Accordion>
    </div>
    <div class="timeline-scroll" ref="timelineScroll">
      <div
        v-for="year in sortedTimelineEvents"
        :key="year"
        class="timeline-item"
        :class="{ active: year == selectedYear }"
        @click="selectedYear = year"
      >
        <label>{{ year }}</label>
      </div>
    </div>
    <div class="timeline-scroll bg-gray-200">
      <div
        v-for="event in timelineEvents?.filter((e) => e.year == selectedYear)"
        :key="event.year"
        class="timeline-item flex gap-2 px-2"
        style="min-width: 200px"
        :class="{ active: event == selectedEvent }"
        @click="selectedEvent = event"
      >
        <i class="pi pi-image" />
        <span>{{ event.title }}</span>
      </div>
    </div>

    <div v-if="selectedEvent">
      <h2>{{ selectedEvent.title }}</h2>
      <p>{{ selectedEvent.description }}</p>
    </div>
  </div>
</template>

<script setup lang="ts">
interface TimelineEvent {
  year: string
  title: string
  description: string
}

const selectedYear = ref<string>()
const selectedEvent = ref<TimelineEvent>()
const languages = ref(['English', 'Romana'])
const timelineEvents = ref<TimelineEvent[]>([
  {
    year: '1000 BC',
    title: 'Ancient Civilization',
    description: 'Formation of early human societies.'
  },
  {
    year: '1000 BC',
    title: 'Ancient Civilization',
    description: 'Formation of early human societies.'
  },
  {
    year: '1000 BC',
    title: 'Ancient Civilization',
    description: 'Formation of early human societies.'
  },
  {
    year: '1000 BC',
    title: 'Ancient Civilization',
    description: 'Formation of early human societies.'
  },
  {
    year: '1000 BC',
    title: 'Ancient Civilization',
    description: 'Formation of early human societies.'
  },
  {
    year: '1000 BC',
    title: 'Ancient Civilization',
    description: 'Formation of early human societies.'
  },
  {
    year: '1000 BC',
    title: 'Ancient Civilization',
    description: 'Formation of early human societies.'
  },
  {
    year: '1000 BC',
    title: 'Ancient Civilization',
    description: 'Formation of early human societies.'
  },
  {
    year: '1000 BC',
    title: 'Ancient Civilization',
    description: 'Formation of early human societies.'
  },
  {
    year: '1000 BC',
    title: 'Ancient Civilization',
    description: 'Formation of early human societies.'
  },
  {
    year: '1000 BC',
    title: 'Ancient Civilization',
    description: 'Formation of early human societies.'
  },
  {
    year: '1000 BC',
    title: 'Ancient Civilization',
    description: 'Formation of early human societies.'
  },
  {
    year: '1000 BC',
    title: 'Ancient Civilization',
    description: 'Formation of early human societies.'
  },
  {
    year: '1000 BC',
    title: 'Ancient Civilization',
    description: 'Formation of early human societies.'
  },
  {
    year: '1000 BC',
    title: 'Ancient Civilization',
    description: 'Formation of early human societies.'
  },
  {
    year: '1000 BC',
    title: 'Ancient Civilization',
    description: 'Formation of early human societies.'
  },
  {
    year: '1000 BC',
    title: 'Ancient Civilization',
    description: 'Formation of early human societies.'
  },
  {
    year: '1000 BC',
    title: 'Ancient Civilization',
    description: 'Formation of early human societies.'
  },
  {
    year: '1000 BC',
    title: 'Ancient Civilization',
    description: 'Formation of early human societies.'
  },
  {
    year: '1000 BC',
    title: 'Ancient Civilization',
    description: 'Formation of early human societies.'
  },
  {
    year: '500 BC',
    title: 'Classical Philosophy',
    description: 'The rise of Greek philosophers.'
  },
  {
    year: '0',
    title: 'Start of the Common Era',
    description: 'Marking the year 0 in history.'
  },
  {
    year: '476',
    title: 'Fall of the Roman Empire',
    description: 'End of the Western Roman Empire.'
  },
  {
    year: '1492',
    title: 'Discovery of the Americas',
    description: 'Columbus reaches the New World.'
  },
  {
    year: '1776',
    title: 'American Independence',
    description: 'Declaration of Independence signed.'
  },
  {
    year: '1930',
    title: 'Advanced Psychiatric Fellowships Awarded',
    description: 'Fellowships for advanced psychiatric study...'
  },
  {
    year: '1940',
    title: 'World War II Contributions',
    description: 'Significant advancements in medicine...'
  },
  {
    year: '1950',
    title: 'Another Milestone',
    description: 'A significant event in the timeline...'
  },
  {
    year: '1960',
    title: 'Space Exploration Era',
    description: 'Start of the space race...'
  },
  {
    year: '1980',
    title: 'Modern Computing Revolution',
    description: 'The rise of personal computers...'
  },
  {
    year: '2000',
    title: 'Modern Advancements',
    description: 'The modern era begins...'
  },
  {
    year: '2020',
    title: 'Pandemic and Recovery',
    description: 'Global response to challenges...'
  }
])

const timelineScroll = ref<HTMLDivElement | null>(null)

// Sorting logic: Convert year to number, treating BC years as negative
const sortedTimelineEvents = computed(() => {
  const seenYears = new Set()
  return timelineEvents.value
    .map((e) => e.year)
    ?.sort((a, b) => parseYear(a) - parseYear(b))
    .filter((y) => {
      if (seenYears.has(y)) return false
      seenYears.add(y)
      return true
    })
})

// Parse a year string and handle BC dates as negative
const parseYear = (year: string): number =>
  year.replaceAll('.', '').includes('BC')
    ? -parseInt(year.replace('BC', '').trim())
    : parseInt(year.trim())

// Enable horizontal scrolling with the mouse wheel
onMounted(() => {
  if (timelineScroll.value)
    timelineScroll.value.addEventListener('wheel', (event) => {
      event.preventDefault() // Prevent default vertical scrolling
      timelineScroll.value!.scrollLeft += event.deltaY // Scroll horizontally
    })
})
</script>

<style scoped>
/* Scrollable area for the timeline */
.timeline-scroll {
  overflow-x: auto;
  display: flex;
  flex-direction: row;
  background-color: lightgray;
}

.timeline-scroll > * {
  min-width: 100px;
}

.timeline-item {
  padding: 0.5rem 0;
  text-align: center;
  cursor: pointer;
  transition: background-color 0.3s;
}

.timeline-item.active {
  background-color: #ffd700;
}
</style>
