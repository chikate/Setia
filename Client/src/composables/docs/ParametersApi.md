# ParametersApi

All URIs are relative to *https://localhost:5000*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**apiParametersAddPost**](ParametersApi.md#apiparametersaddpost) | **POST** /api/Parameters/Add |  |
| [**apiParametersDeleteDelete**](ParametersApi.md#apiparametersdeletedelete) | **DELETE** /api/Parameters/Delete |  |
| [**apiParametersGetGet**](ParametersApi.md#apiparametersgetget) | **GET** /api/Parameters/Get |  |
| [**apiParametersUpdatePut**](ParametersApi.md#apiparametersupdateput) | **PUT** /api/Parameters/Update |  |



## apiParametersAddPost

> Array&lt;ParameterModel&gt; apiParametersAddPost(parameterModel)



### Example

```ts
import {
  Configuration,
  ParametersApi,
} from '';
import type { ApiParametersAddPostRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new ParametersApi();

  const body = {
    // Array<ParameterModel>
    parameterModel: ...,
  } satisfies ApiParametersAddPostRequest;

  try {
    const data = await api.apiParametersAddPost(body);
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
| **parameterModel** | `Array<ParameterModel>` |  | |

### Return type

[**Array&lt;ParameterModel&gt;**](ParameterModel.md)

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


## apiParametersDeleteDelete

> Array&lt;ParameterModel&gt; apiParametersDeleteDelete(requestBody)



### Example

```ts
import {
  Configuration,
  ParametersApi,
} from '';
import type { ApiParametersDeleteDeleteRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new ParametersApi();

  const body = {
    // Array<string>
    requestBody: ...,
  } satisfies ApiParametersDeleteDeleteRequest;

  try {
    const data = await api.apiParametersDeleteDelete(body);
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

[**Array&lt;ParameterModel&gt;**](ParameterModel.md)

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


## apiParametersGetGet

> Array&lt;ParameterModel&gt; apiParametersGetGet(items, pageSize, pageNumber)



### Example

```ts
import {
  Configuration,
  ParametersApi,
} from '';
import type { ApiParametersGetGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new ParametersApi();

  const body = {
    // Array<ParameterModel> (optional)
    items: ...,
    // ApiUsersGetGetPageSizeParameter (optional)
    pageSize: 56,
    // ApiUsersGetGetPageSizeParameter (optional)
    pageNumber: 56,
  } satisfies ApiParametersGetGetRequest;

  try {
    const data = await api.apiParametersGetGet(body);
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
| **items** | `Array<ParameterModel>` |  | [Optional] |
| **pageSize** | [](.md) |  | [Optional] [Defaults to `undefined`] |
| **pageNumber** | [](.md) |  | [Optional] [Defaults to `undefined`] |

### Return type

[**Array&lt;ParameterModel&gt;**](ParameterModel.md)

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


## apiParametersUpdatePut

> ApiUsersGetGetPageSizeParameter apiParametersUpdatePut(parameterModel)



### Example

```ts
import {
  Configuration,
  ParametersApi,
} from '';
import type { ApiParametersUpdatePutRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new ParametersApi();

  const body = {
    // Array<ParameterModel>
    parameterModel: ...,
  } satisfies ApiParametersUpdatePutRequest;

  try {
    const data = await api.apiParametersUpdatePut(body);
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
| **parameterModel** | `Array<ParameterModel>` |  | |

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

