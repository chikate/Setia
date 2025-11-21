# LinksApi

All URIs are relative to *https://localhost:5000*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**apiLinksAddPost**](LinksApi.md#apilinksaddpost) | **POST** /api/Links/Add |  |
| [**apiLinksDeleteDelete**](LinksApi.md#apilinksdeletedelete) | **DELETE** /api/Links/Delete |  |
| [**apiLinksGetGet**](LinksApi.md#apilinksgetget) | **GET** /api/Links/Get |  |
| [**apiLinksUpdatePut**](LinksApi.md#apilinksupdateput) | **PUT** /api/Links/Update |  |



## apiLinksAddPost

> Array&lt;LinkModel&gt; apiLinksAddPost(linkModel)



### Example

```ts
import {
  Configuration,
  LinksApi,
} from '';
import type { ApiLinksAddPostRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new LinksApi();

  const body = {
    // Array<LinkModel>
    linkModel: ...,
  } satisfies ApiLinksAddPostRequest;

  try {
    const data = await api.apiLinksAddPost(body);
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
| **linkModel** | `Array<LinkModel>` |  | |

### Return type

[**Array&lt;LinkModel&gt;**](LinkModel.md)

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


## apiLinksDeleteDelete

> Array&lt;LinkModel&gt; apiLinksDeleteDelete(requestBody)



### Example

```ts
import {
  Configuration,
  LinksApi,
} from '';
import type { ApiLinksDeleteDeleteRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new LinksApi();

  const body = {
    // Array<string>
    requestBody: ...,
  } satisfies ApiLinksDeleteDeleteRequest;

  try {
    const data = await api.apiLinksDeleteDelete(body);
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

[**Array&lt;LinkModel&gt;**](LinkModel.md)

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


## apiLinksGetGet

> Array&lt;LinkModel&gt; apiLinksGetGet(items, pageSize, pageNumber)



### Example

```ts
import {
  Configuration,
  LinksApi,
} from '';
import type { ApiLinksGetGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new LinksApi();

  const body = {
    // Array<LinkModel> (optional)
    items: ...,
    // ApiUsersGetGetPageSizeParameter (optional)
    pageSize: 56,
    // ApiUsersGetGetPageSizeParameter (optional)
    pageNumber: 56,
  } satisfies ApiLinksGetGetRequest;

  try {
    const data = await api.apiLinksGetGet(body);
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
| **items** | `Array<LinkModel>` |  | [Optional] |
| **pageSize** | [](.md) |  | [Optional] [Defaults to `undefined`] |
| **pageNumber** | [](.md) |  | [Optional] [Defaults to `undefined`] |

### Return type

[**Array&lt;LinkModel&gt;**](LinkModel.md)

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


## apiLinksUpdatePut

> ApiUsersGetGetPageSizeParameter apiLinksUpdatePut(linkModel)



### Example

```ts
import {
  Configuration,
  LinksApi,
} from '';
import type { ApiLinksUpdatePutRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new LinksApi();

  const body = {
    // Array<LinkModel>
    linkModel: ...,
  } satisfies ApiLinksUpdatePutRequest;

  try {
    const data = await api.apiLinksUpdatePut(body);
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
| **linkModel** | `Array<LinkModel>` |  | |

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

