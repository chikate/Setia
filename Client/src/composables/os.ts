export const installedApps = Object.values(
  import.meta.glob("@/pages/*.vue", { eager: true })
)
  .map((m: any) => markRaw(m.default))
  ?.filter((elem) => elem.name);

export const windows = [];
