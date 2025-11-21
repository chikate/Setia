export const osStore = defineStore(
  "osStore",
  () => {
    const favorites = ref([]);
    const theme = ref({ hue: 0, saturation: 50, brightness: 50 });

    function addToFavorites(object) {
      favorites.value.push(object);
    }

    return { favorites, addToFavorites };
  },
  {
    persist: {
      key: "osStoreFavorites",
      pick: ["favorites"],
    },
  }
);
