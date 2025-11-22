import { app } from "@/main";
import type { App } from "vue";

export const apiRequest = (
  actionPath: string,
  bodyData?: any,
  method: "GET" | "POST" | "PUT" | "DELETE" = "GET",
  headers: HeadersInit = {
    Authorization: `Bearer ${getCookie("access_token")}`,
    "Content-Type":
      bodyData instanceof FormData
        ? ""
        : method == "GET"
        ? "text/plain"
        : "application/json",
  }
) =>
  fetch(`/api/${actionPath}${queryParams(bodyData)}`, {
    method,
    headers,
    body:
      method != "GET"
        ? bodyData instanceof FormData
          ? bodyData
          : JSON.stringify(bodyData)
        : undefined,
  }).then(async (response: Response) => {
    const contentType = response.headers.get("Content-Type");
    if (!contentType) return false;
    const content = contentType?.includes("application/json")
      ? await response.json()
      : contentType?.includes("text/plain")
      ? await response.text()
      : await response.blob();

    app.config.globalProperties.$toast?.add({
      summary: "Server",
      detail: content,
      severity: response.ok
        ? "success"
        : response.status >= 400 && response.status < 600
        ? "error"
        : "info",
      life: 3000,
    });

    return content;
  });

export const queryParams = (bodyData: any) =>
  Object.values(bodyData).every((value) => value != undefined && value != "")
    ? "?" + new URLSearchParams(bodyData)
    : "";

export function setupGlobalFetchInterceptor(appInstance: App) {
  const originalFetch = window.fetch;

  window.fetch = async function (
    input: RequestInfo | URL,
    init: RequestInit = {}
  ): Promise<Response> {
    const forcedHeaders: HeadersInit = {
      Authorization: `Bearer ${getCookie("access_token")}`,
      "X-Requested-With": "XMLHttpRequest",
    };

    const headers: HeadersInit = {
      ...(init.headers || {}),
      ...forcedHeaders,
    };

    const response = await originalFetch(input, { ...init, headers });

    const clone = response.clone();
    let message = "";

    try {
      const text = await clone.text();
      message = text?.length > 200 ? text.slice(0, 200) + "..." : text;
    } catch {
      message = "[Unable to read response body]";
    }

    const severity = response.ok
      ? "success"
      : response.status >= 400 && response.status < 600
      ? "error"
      : "info";

    if (message && appInstance.config.globalProperties.$toast) {
      appInstance.config.globalProperties.$toast.add({
        summary: capitalizeWords(severity),
        detail: message || "No response body",
        severity,
        life: 3000,
      });
    }

    return response;
  };
}
