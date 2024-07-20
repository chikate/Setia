<template>
  <div class="card h-full">
    <Chart class="h-full" type="bar" :data="chartData" :options="chartOptions" />
  </div>
</template>

<script setup lang="ts">
const chartData = defineModel('chartData', {
  type: Object,
  required: false,
  default: {
    labels: [
      'January',
      'February',
      'March',
      'April',
      'May',
      'June',
      'July',
      'February',
      'March',
      'April',
      'May',
      'June',
      'July',
      'February',
      'March',
      'April',
      'May',
      'June',
      'July',
      'February',
      'March',
      'April',
      'May',
      'June',
      'July',
      'March',
      'April',
      'May',
      'June',
      'July',
      'February',
      'March',
      'April',
      'May',
      'June',
      'July',
      'February',
      'March',
      'April',
      'May',
      'June',
      'July'
    ],
    datasets: [
      {
        data: [
          0, 59, 80, 81, 56, 55, 40, 59, 80, 81, 56, 55, 40, 48, 40, 19, 0, 27, 90, 48, 40, 19, 0,
          27, 90, 80, 81, 56, 55, 40, 59, 80, 81, 56, 55, 40, 48, 40, 19, 0, 27, 90, 48, 40, 19, 0,
          27, 90
        ]
      },
      {
        data: [
          28, 48, 40, 19, 0, 27, 90, 48, 40, 19, 0, 27, 90, 48, 40, 19, 0, 27, 90, 48, 40, 19, 0,
          27, 190, 80, 81, 56, 55, 40, 59, 80, 81, 56, 55, 40, 48, 40, 19, 0, 27, 90, 48, 40, 19, 0,
          27, 190
        ]
      }
    ]
  }
})
const chartOptions = defineModel('chartOptions', {
  type: Object,
  required: false,
  default: {
    indexAxis: 'y',
    maintainAspectRatio: true,
    plugins: {
      legend: false,
      tooltips: {
        intersect: false
      }
    },
    scales: {
      x: {
        stacked: true,
        ticks: {
          font: {
            weight: 500
          }
        },
        grid: {
          display: false,
          drawBorder: false
        }
      },
      y: {
        stacked: true,
        ticks: {
          font: {
            weight: 500
          }
        },
        grid: {
          display: false,
          drawBorder: false
        }
      }
    }
  }
})
</script>
