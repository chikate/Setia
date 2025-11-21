# FriendsApi

All URIs are relative to *https://localhost:5000*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**apiFriendsAcceptRequestIdPost**](FriendsApi.md#apifriendsacceptrequestidpost) | **POST** /api/Friends/Accept/{requestId} |  |
| [**apiFriendsGet**](FriendsApi.md#apifriendsget) | **GET** /api/Friends |  |
| [**apiFriendsRejectRequestIdPost**](FriendsApi.md#apifriendsrejectrequestidpost) | **POST** /api/Friends/Reject/{requestId} |  |
| [**apiFriendsRequestUserIdPost**](FriendsApi.md#apifriendsrequestuseridpost) | **POST** /api/Friends/Request/{userId} |  |
| [**apiFriendsRequestsGet**](FriendsApi.md#apifriendsrequestsget) | **GET** /api/Friends/Requests |  |



## apiFriendsAcceptRequestIdPost

> apiFriendsAcceptRequestIdPost(requestId)



### Example

```ts
import {
  Configuration,
  FriendsApi,
} from '';
import type { ApiFriendsAcceptRequestIdPostRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new FriendsApi();

  const body = {
    // string
    requestId: 38400000-8cf0-11bd-b23e-10b96e4ef00d,
  } satisfies ApiFriendsAcceptRequestIdPostRequest;

  try {
    const data = await api.apiFriendsAcceptRequestIdPost(body);
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
| **requestId** | `string` |  | [Defaults to `undefined`] |

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


## apiFriendsGet

> apiFriendsGet()



### Example

```ts
import {
  Configuration,
  FriendsApi,
} from '';
import type { ApiFriendsGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new FriendsApi();

  try {
    const data = await api.apiFriendsGet();
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


## apiFriendsRejectRequestIdPost

> apiFriendsRejectRequestIdPost(requestId)



### Example

```ts
import {
  Configuration,
  FriendsApi,
} from '';
import type { ApiFriendsRejectRequestIdPostRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new FriendsApi();

  const body = {
    // string
    requestId: 38400000-8cf0-11bd-b23e-10b96e4ef00d,
  } satisfies ApiFriendsRejectRequestIdPostRequest;

  try {
    const data = await api.apiFriendsRejectRequestIdPost(body);
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
| **requestId** | `string` |  | [Defaults to `undefined`] |

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


## apiFriendsRequestUserIdPost

> apiFriendsRequestUserIdPost(userId)



### Example

```ts
import {
  Configuration,
  FriendsApi,
} from '';
import type { ApiFriendsRequestUserIdPostRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new FriendsApi();

  const body = {
    // string
    userId: 38400000-8cf0-11bd-b23e-10b96e4ef00d,
  } satisfies ApiFriendsRequestUserIdPostRequest;

  try {
    const data = await api.apiFriendsRequestUserIdPost(body);
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
| **userId** | `string` |  | [Defaults to `undefined`] |

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


## apiFriendsRequestsGet

> apiFriendsRequestsGet()



### Example

```ts
import {
  Configuration,
  FriendsApi,
} from '';
import type { ApiFriendsRequestsGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new FriendsApi();

  try {
    const data = await api.apiFriendsRequestsGet();
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

