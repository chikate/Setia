// Global theme management with dynamic color generation using OKLCH color space

const theme = ref<string>(localStorage.getItem("theme") || "light");
const primaryColor = ref<string>(
  localStorage.getItem("primaryColor") || "#6366f1"
);

const isDarkMode = computed(() => theme.value === "dark");

// Consolidated theme application
const applyTheme = () => {
  const root = document.documentElement;
  const body = document.body;

  // Apply dark mode class
  root.classList.toggle("dark-mode", isDarkMode.value);

  // Set body background based on theme
  body.style.backgroundColor = isDarkMode.value
    ? "var(--p-surface-50)"
    : "var(--p-surface-0)";
};

// Dynamic color palette generation
const updateColorPalette = (color: string) => {
  const root = document.documentElement;
  const normalizedColor = color.startsWith("#") ? color : `#${color}`;

  // Set base color
  root.style.setProperty("--p-primary-500", normalizedColor);

  // Generate shades using OKLCH color-mix for perceptually uniform results
  const shades = {
    50: "color-mix(in oklch, var(--p-primary-500), white 95%)",
    100: "color-mix(in oklch, var(--p-primary-500), white 90%)",
    200: "color-mix(in oklch, var(--p-primary-500), white 80%)",
    300: "color-mix(in oklch, var(--p-primary-500), white 70%)",
    400: "color-mix(in oklch, var(--p-primary-500), white 50%)",
    600: "color-mix(in oklch, var(--p-primary-500), black 10%)",
    700: "color-mix(in oklch, var(--p-primary-500), black 20%)",
    800: "color-mix(in oklch, var(--p-primary-500), black 30%)",
    900: "color-mix(in oklch, var(--p-primary-500), black 40%)",
    950: "color-mix(in oklch, var(--p-primary-500), black 50%)",
  };

  Object.entries(shades).forEach(([key, value]) => {
    root.style.setProperty(`--p-primary-${key}`, value);
  });
};

// Public API
const toggleDarkMode = () => {
  theme.value = isDarkMode.value ? "light" : "dark";
  localStorage.setItem("theme", theme.value);
  applyTheme();
};

const setPrimaryColor = (color: string) => {
  const normalizedColor = `#${color.replace("#", "")}`;
  primaryColor.value = normalizedColor;
  localStorage.setItem("primaryColor", normalizedColor);
  updateColorPalette(normalizedColor);
};

// Initialize theme on load
applyTheme();
updateColorPalette(primaryColor.value);

// Reactive watchers
watch(theme, applyTheme);
watch(primaryColor, updateColorPalette);

// Export composable
export const themeManager = () => ({
  theme,
  isDarkMode,
  toggleDarkMode,
  primaryColor,
  setPrimaryColor,
});
