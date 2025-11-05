// Status: Unknowm(Perfect)

import { app } from "@/main";

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
    });
    console.log(content);
    return content;
  });

export const queryParams = (bodyData: any) =>
  Object.values(bodyData).every((value) => value != undefined && value != "")
    ? "?" + new URLSearchParams(bodyData)
    : "";
