const theme = ref(localStorage.getItem("theme") || "light");
const isDarkMode = computed(() => theme.value === "dark");

const initializeTheme = () => {
  if (theme.value === "dark") {
    document.documentElement.classList.add("dark-mode");
  } else {
    document.documentElement.classList.remove("dark-mode");
  }
};

const toggleDarkMode = () => {
  if (isDarkMode.value) {
    theme.value = "light";
    document.documentElement.classList.remove("dark-mode");
  } else {
    theme.value = "dark";
    document.documentElement.classList.add("dark-mode");
  }
  localStorage.setItem("theme", theme.value);
};

initializeTheme();

export const darkMode = () => {
  return {
    theme,
    isDarkMode,
    toggleDarkMode,
  };
};
