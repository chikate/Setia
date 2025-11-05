export const osStore = defineStore(
  "osStore",
  () => {
    const favorites = ref([]);

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
