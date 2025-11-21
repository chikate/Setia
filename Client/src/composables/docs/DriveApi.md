# DriveApi

All URIs are relative to *https://localhost:5000*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**apiDriveDeleteDelete**](DriveApi.md#apidrivedeletedelete) | **DELETE** /api/Drive/Delete |  |
| [**apiDriveDownloadGet**](DriveApi.md#apidrivedownloadget) | **GET** /api/Drive/Download |  |
| [**apiDriveGetAllPartitionsGet**](DriveApi.md#apidrivegetallpartitionsget) | **GET** /api/Drive/GetAllPartitions |  |
| [**apiDriveGetBasePathGet**](DriveApi.md#apidrivegetbasepathget) | **GET** /api/Drive/GetBasePath |  |
| [**apiDriveGetFolderContentGet**](DriveApi.md#apidrivegetfoldercontentget) | **GET** /api/Drive/GetFolderContent |  |
| [**apiDriveSearchGet**](DriveApi.md#apidrivesearchget) | **GET** /api/Drive/Search |  |
| [**apiDriveUploadPost**](DriveApi.md#apidriveuploadpost) | **POST** /api/Drive/Upload |  |



## apiDriveDeleteDelete

> boolean apiDriveDeleteDelete(filePath)



### Example

```ts
import {
  Configuration,
  DriveApi,
} from '';
import type { ApiDriveDeleteDeleteRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new DriveApi();

  const body = {
    // string (optional)
    filePath: filePath_example,
  } satisfies ApiDriveDeleteDeleteRequest;

  try {
    const data = await api.apiDriveDeleteDelete(body);
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
| **filePath** | `string` |  | [Optional] [Defaults to `undefined`] |

### Return type

**boolean**

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


## apiDriveDownloadGet

> apiDriveDownloadGet(filePath)



### Example

```ts
import {
  Configuration,
  DriveApi,
} from '';
import type { ApiDriveDownloadGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new DriveApi();

  const body = {
    // string (optional)
    filePath: filePath_example,
  } satisfies ApiDriveDownloadGetRequest;

  try {
    const data = await api.apiDriveDownloadGet(body);
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
| **filePath** | `string` |  | [Optional] [Defaults to `undefined`] |

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


## apiDriveGetAllPartitionsGet

> Array&lt;DriveInfoDTO&gt; apiDriveGetAllPartitionsGet()



### Example

```ts
import {
  Configuration,
  DriveApi,
} from '';
import type { ApiDriveGetAllPartitionsGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new DriveApi();

  try {
    const data = await api.apiDriveGetAllPartitionsGet();
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

[**Array&lt;DriveInfoDTO&gt;**](DriveInfoDTO.md)

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


## apiDriveGetBasePathGet

> string apiDriveGetBasePathGet()



### Example

```ts
import {
  Configuration,
  DriveApi,
} from '';
import type { ApiDriveGetBasePathGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new DriveApi();

  try {
    const data = await api.apiDriveGetBasePathGet();
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

**string**

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


## apiDriveGetFolderContentGet

> Array&lt;string&gt; apiDriveGetFolderContentGet(filePath)



### Example

```ts
import {
  Configuration,
  DriveApi,
} from '';
import type { ApiDriveGetFolderContentGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new DriveApi();

  const body = {
    // string (optional)
    filePath: filePath_example,
  } satisfies ApiDriveGetFolderContentGetRequest;

  try {
    const data = await api.apiDriveGetFolderContentGet(body);
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
| **filePath** | `string` |  | [Optional] [Defaults to `&#39;&#39;`] |

### Return type

**Array<string>**

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


## apiDriveSearchGet

> string apiDriveSearchGet(input)



### Example

```ts
import {
  Configuration,
  DriveApi,
} from '';
import type { ApiDriveSearchGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new DriveApi();

  const body = {
    // string (optional)
    input: input_example,
  } satisfies ApiDriveSearchGetRequest;

  try {
    const data = await api.apiDriveSearchGet(body);
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
| **input** | `string` |  | [Optional] [Defaults to `undefined`] |

### Return type

**string**

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


## apiDriveUploadPost

> string apiDriveUploadPost(saveToPath, contentType, contentDisposition, headers, length, name, fileName)



### Example

```ts
import {
  Configuration,
  DriveApi,
} from '';
import type { ApiDriveUploadPostRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new DriveApi();

  const body = {
    // string (optional)
    saveToPath: saveToPath_example,
    // string (optional)
    contentType: contentType_example,
    // string (optional)
    contentDisposition: contentDisposition_example,
    // { [key: string]: Array<string>; } (optional)
    headers: Object,
    // ApiDriveUploadPostRequestLength (optional)
    length: ...,
    // string (optional)
    name: name_example,
    // string (optional)
    fileName: fileName_example,
  } satisfies ApiDriveUploadPostRequest;

  try {
    const data = await api.apiDriveUploadPost(body);
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
| **saveToPath** | `string` |  | [Optional] [Defaults to `&#39;&#39;`] |
| **contentType** | `string` |  | [Optional] [Defaults to `undefined`] |
| **contentDisposition** | `string` |  | [Optional] [Defaults to `undefined`] |
| **headers** | `{ [key: string]: Array<string>; }` |  | [Optional] |
| **length** | [ApiDriveUploadPostRequestLength](ApiDriveUploadPostRequestLength.md) |  | [Optional] [Defaults to `undefined`] |
| **name** | `string` |  | [Optional] [Defaults to `undefined`] |
| **fileName** | `string` |  | [Optional] [Defaults to `undefined`] |

### Return type

**string**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: `application/x-www-form-urlencoded`
- **Accept**: `text/plain`, `application/json`, `text/json`


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)

