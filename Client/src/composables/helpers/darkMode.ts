// Status: Perfect

const theme = ref<string>(localStorage.getItem("theme") || "light");
const hue = ref<number>(Number(localStorage.getItem("themeHue")) || 0);
const chroma = ref<number>(Number(localStorage.getItem("themeChroma")) || 180);

const hueSecondary = computed(() => (hue.value + 180) % 360);
const isDarkMode = computed(() => theme.value == "dark");

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
  toggleDarkMode,

  themeColor,
  applyThemeColor,
});

const themeColor = ref(localStorage.getItem("theme") || "#ffffff");

const applyThemeColor = (hex?: string) => {
  themeColor.value = hex;
  localStorage.setItem("theme", themeColor.value);
};
