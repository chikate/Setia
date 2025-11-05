// Status: Perfect

const theme = ref(localStorage.getItem("theme") || "light");
const isDarkMode = computed(() => theme.value == "dark");
const isLightMode = computed(() => theme.value == "light");

const applyThemeClass = () =>
  isDarkMode.value
    ? document.documentElement?.classList?.add("dark-mode")
    : document.documentElement?.classList?.remove("dark-mode");

function toggleDarkMode() {
  theme.value = isDarkMode.value ? "light" : "dark";
  localStorage.setItem("theme", theme.value);
  applyThemeClass();
}

// Apply on load
applyThemeClass();

export const darkMode = () => ({
  theme,
  isDarkMode,
  isLightMode,
  toggleDarkMode,

  themeColor,
  applyThemeColor,
});

const themeColor = ref(localStorage.getItem("theme") || "#ffffff");

const applyThemeColor = (hex?: string) => {
  themeColor.value = hex;
  localStorage.setItem("theme", themeColor.value);
};
