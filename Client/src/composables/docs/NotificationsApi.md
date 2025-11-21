# NotificationsApi

All URIs are relative to *https://localhost:5000*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**apiNotificationsAddPost**](NotificationsApi.md#apinotificationsaddpost) | **POST** /api/Notifications/Add |  |
| [**apiNotificationsDeleteDelete**](NotificationsApi.md#apinotificationsdeletedelete) | **DELETE** /api/Notifications/Delete |  |
| [**apiNotificationsGetGet**](NotificationsApi.md#apinotificationsgetget) | **GET** /api/Notifications/Get |  |
| [**apiNotificationsGetUnreadGet**](NotificationsApi.md#apinotificationsgetunreadget) | **GET** /api/Notifications/GetUnread |  |
| [**apiNotificationsMarkAsReadPost**](NotificationsApi.md#apinotificationsmarkasreadpost) | **POST** /api/Notifications/MarkAsRead |  |
| [**apiNotificationsSendGet**](NotificationsApi.md#apinotificationssendget) | **GET** /api/Notifications/Send |  |
| [**apiNotificationsUpdatePut**](NotificationsApi.md#apinotificationsupdateput) | **PUT** /api/Notifications/Update |  |



## apiNotificationsAddPost

> Array&lt;NotificationModel&gt; apiNotificationsAddPost(notificationModel)



### Example

```ts
import {
  Configuration,
  NotificationsApi,
} from '';
import type { ApiNotificationsAddPostRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new NotificationsApi();

  const body = {
    // Array<NotificationModel>
    notificationModel: ...,
  } satisfies ApiNotificationsAddPostRequest;

  try {
    const data = await api.apiNotificationsAddPost(body);
    console.log(data);
  } catch (error) {
    console.error(error);
  }
}

// Run the test
example().catch(console.error);
```

### Parameters


| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **notificationModel** | `Array<NotificationModel>` |  | |

### Return type

[**Array&lt;NotificationModel&gt;**](NotificationModel.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: `application/json`, `text/json`, `application/*+json`
- **Accept**: `text/plain`, `application/json`, `text/json`


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


## apiNotificationsDeleteDelete

> Array&lt;NotificationModel&gt; apiNotificationsDeleteDelete(requestBody)



### Example

```ts
import {
  Configuration,
  NotificationsApi,
} from '';
import type { ApiNotificationsDeleteDeleteRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new NotificationsApi();

  const body = {
    // Array<string>
    requestBody: ...,
  } satisfies ApiNotificationsDeleteDeleteRequest;

  try {
    const data = await api.apiNotificationsDeleteDelete(body);
    console.log(data);
  } catch (error) {
    console.error(error);
  }
}

// Run the test
example().catch(console.error);
```

### Parameters


| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **requestBody** | `Array<string>` |  | |

### Return type

[**Array&lt;NotificationModel&gt;**](NotificationModel.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: `application/json`, `text/json`, `application/*+json`
- **Accept**: `text/plain`, `application/json`, `text/json`


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


## apiNotificationsGetGet

> Array&lt;NotificationModel&gt; apiNotificationsGetGet(items, pageSize, pageNumber)



### Example

```ts
import {
  Configuration,
  NotificationsApi,
} from '';
import type { ApiNotificationsGetGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new NotificationsApi();

  const body = {
    // Array<NotificationModel> (optional)
    items: ...,
    // ApiUsersGetGetPageSizeParameter (optional)
    pageSize: 56,
    // ApiUsersGetGetPageSizeParameter (optional)
    pageNumber: 56,
  } satisfies ApiNotificationsGetGetRequest;

  try {
    const data = await api.apiNotificationsGetGet(body);
    console.log(data);
  } catch (error) {
    console.error(error);
  }
}

// Run the test
example().catch(console.error);
```

### Parameters


| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **items** | `Array<NotificationModel>` |  | [Optional] |
| **pageSize** | [](.md) |  | [Optional] [Defaults to `undefined`] |
| **pageNumber** | [](.md) |  | [Optional] [Defaults to `undefined`] |

### Return type

[**Array&lt;NotificationModel&gt;**](NotificationModel.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: `text/plain`, `application/json`, `text/json`


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


## apiNotificationsGetUnreadGet

> Array&lt;NotificationModel&gt; apiNotificationsGetUnreadGet()



### Example

```ts
import {
  Configuration,
  NotificationsApi,
} from '';
import type { ApiNotificationsGetUnreadGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new NotificationsApi();

  try {
    const data = await api.apiNotificationsGetUnreadGet();
    console.log(data);
  } catch (error) {
    console.error(error);
  }
}

// Run the test
example().catch(console.error);
```

### Parameters

This endpoint does not need any parameter.

### Return type

[**Array&lt;NotificationModel&gt;**](NotificationModel.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: `text/plain`, `application/json`, `text/json`


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


## apiNotificationsMarkAsReadPost

> apiNotificationsMarkAsReadPost(id)



### Example

```ts
import {
  Configuration,
  NotificationsApi,
} from '';
import type { ApiNotificationsMarkAsReadPostRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new NotificationsApi();

  const body = {
    // string (optional)
    id: 38400000-8cf0-11bd-b23e-10b96e4ef00d,
  } satisfies ApiNotificationsMarkAsReadPostRequest;

  try {
    const data = await api.apiNotificationsMarkAsReadPost(body);
    console.log(data);
  } catch (error) {
    console.error(error);
  }
}

// Run the test
example().catch(console.error);
```

### Parameters


| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **id** | `string` |  | [Optional] [Defaults to `undefined`] |

### Return type

`void` (Empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


## apiNotificationsSendGet

> apiNotificationsSendGet(notificationModel)



### Example

```ts
import {
  Configuration,
  NotificationsApi,
} from '';
import type { ApiNotificationsSendGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new NotificationsApi();

  const body = {
    // NotificationModel
    notificationModel: ...,
  } satisfies ApiNotificationsSendGetRequest;

  try {
    const data = await api.apiNotificationsSendGet(body);
    console.log(data);
  } catch (error) {
    console.error(error);
  }
}

// Run the test
example().catch(console.error);
```

### Parameters


| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **notificationModel** | [NotificationModel](NotificationModel.md) |  | |

### Return type

`void` (Empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: `application/json`, `text/json`, `application/*+json`
- **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


## apiNotificationsUpdatePut

> ApiUsersGetGetPageSizeParameter apiNotificationsUpdatePut(notificationModel)



### Example

```ts
import {
  Configuration,
  NotificationsApi,
} from '';
import type { ApiNotificationsUpdatePutRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new NotificationsApi();

  const body = {
    // Array<NotificationModel>
    notificationModel: ...,
  } satisfies ApiNotificationsUpdatePutRequest;

  try {
    const data = await api.apiNotificationsUpdatePut(body);
    console.log(data);
  } catch (error) {
    console.error(error);
  }
}

// Run the test
example().catch(console.error);
```

### Parameters


| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **notificationModel** | `Array<NotificationModel>` |  | |

### Return type

[**ApiUsersGetGetPageSizeParameter**](ApiUsersGetGetPageSizeParameter.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: `application/json`, `text/json`, `application/*+json`
- **Accept**: `text/plain`, `application/json`, `text/json`


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)

