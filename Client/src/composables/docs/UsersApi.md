# UsersApi

All URIs are relative to *https://localhost:5000*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**apiUsersAddPost**](UsersApi.md#apiusersaddpost) | **POST** /api/Users/Add |  |
| [**apiUsersDeleteDelete**](UsersApi.md#apiusersdeletedelete) | **DELETE** /api/Users/Delete |  |
| [**apiUsersGetGet**](UsersApi.md#apiusersgetget) | **GET** /api/Users/Get |  |
| [**apiUsersUpdatePut**](UsersApi.md#apiusersupdateput) | **PUT** /api/Users/Update |  |



## apiUsersAddPost

> Array&lt;UserModel&gt; apiUsersAddPost(userModel)



### Example

```ts
import {
  Configuration,
  UsersApi,
} from '';
import type { ApiUsersAddPostRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new UsersApi();

  const body = {
    // Array<UserModel>
    userModel: ...,
  } satisfies ApiUsersAddPostRequest;

  try {
    const data = await api.apiUsersAddPost(body);
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
| **userModel** | `Array<UserModel>` |  | |

### Return type

[**Array&lt;UserModel&gt;**](UserModel.md)

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


## apiUsersDeleteDelete

> Array&lt;UserModel&gt; apiUsersDeleteDelete(requestBody)



### Example

```ts
import {
  Configuration,
  UsersApi,
} from '';
import type { ApiUsersDeleteDeleteRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new UsersApi();

  const body = {
    // Array<string>
    requestBody: ...,
  } satisfies ApiUsersDeleteDeleteRequest;

  try {
    const data = await api.apiUsersDeleteDelete(body);
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

[**Array&lt;UserModel&gt;**](UserModel.md)

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


## apiUsersGetGet

> Array&lt;UserModel&gt; apiUsersGetGet(items, pageSize, pageNumber)



### Example

```ts
import {
  Configuration,
  UsersApi,
} from '';
import type { ApiUsersGetGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new UsersApi();

  const body = {
    // Array<UserModel> (optional)
    items: ...,
    // ApiUsersGetGetPageSizeParameter (optional)
    pageSize: 56,
    // ApiUsersGetGetPageSizeParameter (optional)
    pageNumber: 56,
  } satisfies ApiUsersGetGetRequest;

  try {
    const data = await api.apiUsersGetGet(body);
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
| **items** | `Array<UserModel>` |  | [Optional] |
| **pageSize** | [](.md) |  | [Optional] [Defaults to `undefined`] |
| **pageNumber** | [](.md) |  | [Optional] [Defaults to `undefined`] |

### Return type

[**Array&lt;UserModel&gt;**](UserModel.md)

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


## apiUsersUpdatePut

> ApiUsersGetGetPageSizeParameter apiUsersUpdatePut(userModel)



### Example

```ts
import {
  Configuration,
  UsersApi,
} from '';
import type { ApiUsersUpdatePutRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new UsersApi();

  const body = {
    // Array<UserModel>
    userModel: ...,
  } satisfies ApiUsersUpdatePutRequest;

  try {
    const data = await api.apiUsersUpdatePut(body);
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
| **userModel** | `Array<UserModel>` |  | |

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

