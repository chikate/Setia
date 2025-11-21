//#region Cookies Extensions
export const getCookie = (name: string) =>
  `; ${document.cookie}`.split(`; ${name}=`).pop()?.split(";").shift() || null;

export const setCookie = (name, value, days = 7, path = "/") => {
  const expires = `expires=${new Date(
    Date.now() + days * 864e5
  ).toUTCString()}`;
  document.cookie = `${name}=${value}; ${expires}; path=${path}; SameSite=Strict; Secure`;
};

export const clearCookies = () =>
  document.cookie.split(";")?.forEach((cookie) => {
    const cookieName = cookie.split("=")[0].trim();
    document.cookie = `${cookieName}=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/; domain=${window.location.hostname}; SameSite=Strict; Secure`;
  });

//#region String Extensions
export const capitalizeString = (input: string) =>
  input.charAt(0).toUpperCase() + input.slice(1);

export const capitalizeWords = (input: string) =>
  capitalizeString(
    input.replace(/\b\w+/g, (word) =>
      ["if", "of", "to", "at", "a", "with", "my"].includes(word.toLowerCase())
        ? word
        : capitalizeString(word)
    )
  );

export function stringToColor(str: string, alpha?: number): string {
  let hash = 0;

  for (let i = 0; i < str.length; i++)
    hash = str.charCodeAt(i) + ((hash << 5) - hash);

  return `rgba(${(hash >> 16) & 0xff}, ${(hash >> 8) & 0xff}, ${hash & 0xff}, ${alpha ?? 0.6
    })`;
}

export const generateGuid = (): string =>
  "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, function (c) {
    const r = (Math.random() * 16) | 0,
      v = c == "x" ? r : (r & 0x3) | 0x8;
    return v.toString(16);
  });

//#region Date Extensions
export function isValidISODate(dateString: string): boolean {
  const isoDatePattern = /^\d{4}-\d{2}-\d{2}$/;
  if (!isoDatePattern.test(dateString)) return false;
  const date = new Date(dateString);
  return (
    !isNaN(date.getTime()) && dateString == date.toISOString().split("T")[0]
  );
}

export function downloadInBrowser(url: any, name?: string) {
  const a: HTMLAnchorElement = document.createElement("a");
  a.href = url;
  a.download = String(name ?? url?.split("/")?.pop() ?? "download").replace(
    /[^0-9A-Z]+/gi,
    ""
  );
  document.body.appendChild(a).click();
  document.body.removeChild(a);
}

//
export const isAdmin = () => false;
export const isAuthenticated = () => getCookie("access_token");
export const canUserAccessRoute = (to, from, next) =>
  next(!isAuthenticated() && to.path != "/" ? { path: "/" } : undefined);
