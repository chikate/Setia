# PostsApi

All URIs are relative to *https://localhost:5000*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**apiPostsAddPost**](PostsApi.md#apipostsaddpost) | **POST** /api/Posts/Add |  |
| [**apiPostsDeleteDelete**](PostsApi.md#apipostsdeletedelete) | **DELETE** /api/Posts/Delete |  |
| [**apiPostsGetGet**](PostsApi.md#apipostsgetget) | **GET** /api/Posts/Get |  |
| [**apiPostsUpdatePut**](PostsApi.md#apipostsupdateput) | **PUT** /api/Posts/Update |  |



## apiPostsAddPost

> Array&lt;PostModel&gt; apiPostsAddPost(postModel)



### Example

```ts
import {
  Configuration,
  PostsApi,
} from '';
import type { ApiPostsAddPostRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new PostsApi();

  const body = {
    // Array<PostModel>
    postModel: ...,
  } satisfies ApiPostsAddPostRequest;

  try {
    const data = await api.apiPostsAddPost(body);
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
| **postModel** | `Array<PostModel>` |  | |

### Return type

[**Array&lt;PostModel&gt;**](PostModel.md)

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


## apiPostsDeleteDelete

> Array&lt;PostModel&gt; apiPostsDeleteDelete(requestBody)



### Example

```ts
import {
  Configuration,
  PostsApi,
} from '';
import type { ApiPostsDeleteDeleteRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new PostsApi();

  const body = {
    // Array<string>
    requestBody: ...,
  } satisfies ApiPostsDeleteDeleteRequest;

  try {
    const data = await api.apiPostsDeleteDelete(body);
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

[**Array&lt;PostModel&gt;**](PostModel.md)

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


## apiPostsGetGet

> Array&lt;PostModel&gt; apiPostsGetGet(items, pageSize, pageNumber)



### Example

```ts
import {
  Configuration,
  PostsApi,
} from '';
import type { ApiPostsGetGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new PostsApi();

  const body = {
    // Array<PostModel> (optional)
    items: ...,
    // ApiUsersGetGetPageSizeParameter (optional)
    pageSize: 56,
    // ApiUsersGetGetPageSizeParameter (optional)
    pageNumber: 56,
  } satisfies ApiPostsGetGetRequest;

  try {
    const data = await api.apiPostsGetGet(body);
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
| **items** | `Array<PostModel>` |  | [Optional] |
| **pageSize** | [](.md) |  | [Optional] [Defaults to `undefined`] |
| **pageNumber** | [](.md) |  | [Optional] [Defaults to `undefined`] |

### Return type

[**Array&lt;PostModel&gt;**](PostModel.md)

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


## apiPostsUpdatePut

> ApiUsersGetGetPageSizeParameter apiPostsUpdatePut(postModel)



### Example

```ts
import {
  Configuration,
  PostsApi,
} from '';
import type { ApiPostsUpdatePutRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new PostsApi();

  const body = {
    // Array<PostModel>
    postModel: ...,
  } satisfies ApiPostsUpdatePutRequest;

  try {
    const data = await api.apiPostsUpdatePut(body);
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
| **postModel** | `Array<PostModel>` |  | |

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

