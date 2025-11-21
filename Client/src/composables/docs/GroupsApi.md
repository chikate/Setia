# GroupsApi

All URIs are relative to *https://localhost:5000*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**apiGroupsAddPost**](GroupsApi.md#apigroupsaddpost) | **POST** /api/Groups/Add |  |
| [**apiGroupsDeleteDelete**](GroupsApi.md#apigroupsdeletedelete) | **DELETE** /api/Groups/Delete |  |
| [**apiGroupsGetGet**](GroupsApi.md#apigroupsgetget) | **GET** /api/Groups/Get |  |
| [**apiGroupsUpdatePut**](GroupsApi.md#apigroupsupdateput) | **PUT** /api/Groups/Update |  |



## apiGroupsAddPost

> Array&lt;GroupModel&gt; apiGroupsAddPost(groupModel)



### Example

```ts
import {
  Configuration,
  GroupsApi,
} from '';
import type { ApiGroupsAddPostRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new GroupsApi();

  const body = {
    // Array<GroupModel>
    groupModel: ...,
  } satisfies ApiGroupsAddPostRequest;

  try {
    const data = await api.apiGroupsAddPost(body);
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
| **groupModel** | `Array<GroupModel>` |  | |

### Return type

[**Array&lt;GroupModel&gt;**](GroupModel.md)

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


## apiGroupsDeleteDelete

> Array&lt;GroupModel&gt; apiGroupsDeleteDelete(requestBody)



### Example

```ts
import {
  Configuration,
  GroupsApi,
} from '';
import type { ApiGroupsDeleteDeleteRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new GroupsApi();

  const body = {
    // Array<string>
    requestBody: ...,
  } satisfies ApiGroupsDeleteDeleteRequest;

  try {
    const data = await api.apiGroupsDeleteDelete(body);
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

[**Array&lt;GroupModel&gt;**](GroupModel.md)

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


## apiGroupsGetGet

> Array&lt;GroupModel&gt; apiGroupsGetGet(items, pageSize, pageNumber)



### Example

```ts
import {
  Configuration,
  GroupsApi,
} from '';
import type { ApiGroupsGetGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new GroupsApi();

  const body = {
    // Array<GroupModel> (optional)
    items: ...,
    // ApiUsersGetGetPageSizeParameter (optional)
    pageSize: 56,
    // ApiUsersGetGetPageSizeParameter (optional)
    pageNumber: 56,
  } satisfies ApiGroupsGetGetRequest;

  try {
    const data = await api.apiGroupsGetGet(body);
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
| **items** | `Array<GroupModel>` |  | [Optional] |
| **pageSize** | [](.md) |  | [Optional] [Defaults to `undefined`] |
| **pageNumber** | [](.md) |  | [Optional] [Defaults to `undefined`] |

### Return type

[**Array&lt;GroupModel&gt;**](GroupModel.md)

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


## apiGroupsUpdatePut

> ApiUsersGetGetPageSizeParameter apiGroupsUpdatePut(groupModel)



### Example

```ts
import {
  Configuration,
  GroupsApi,
} from '';
import type { ApiGroupsUpdatePutRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new GroupsApi();

  const body = {
    // Array<GroupModel>
    groupModel: ...,
  } satisfies ApiGroupsUpdatePutRequest;

  try {
    const data = await api.apiGroupsUpdatePut(body);
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
| **groupModel** | `Array<GroupModel>` |  | |

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

