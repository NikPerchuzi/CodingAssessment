<!--
An example of creating a reusable grid component and using it with external data.
-->

<script>
import DemoGrid from './components/CustomGrid.vue'

export default {
  components: {
    DemoGrid
  },
  data: () => ({
    gridColumns: ['agentId', 'agentName', 'timestampUtc', 'state', 'queueIds'],
    sortableColumns: ['timestampUtc', 'state'],
    gridData: [
      {
        agentId: "6B29FC40-CA47-1067-B31D-00DD010662DA",
        agentName: "John Smith",
        timestampUtc: new Date("2023-04-23 18:25:43.511"),
        state: "ON_LUNCH",
        queueIds: [
          "3a8cc33a-3f09-4ce5-9c53-e94585a410c8",
          "3d887de3-8351-4391-b155-e174f472456a"
        ]
      },
      {
        agentId: "6B29FC40-CA47-1067-B31D-00DD010634DA",
        agentName: "John Dhoe",
        timestampUtc: new Date("2023-04-23 18:15:43.511"),
        state: "ON_CALL",
        queueIds: [
          "3a8cc33a-3f09-4ce5-9c53-e94585fg10c8",
          "3d887de3-8351-4391-b155-e654f472456a"
        ]
      },
      {
        agentId: "962e8338-a74a-489c-909e-ebd59fec2263",
        agentName: "Kamel Back",
        timestampUtc: new Date("2023-04-23 18:20:43.511"),
        state: "ON_CALL",
        queueIds: [
          "1532e206-6114-4bff-bd09-c4d1beb304b1",
          "8d648bda-f8ec-4d33-acba-0737614af039"
        ]
      }
    ]
  })
}
</script>

<template>
  <DemoGrid :data="gridData" :columns="gridColumns" :sortable="sortableColumns" />
</template>