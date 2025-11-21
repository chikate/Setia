# QuestionAnswersApi

All URIs are relative to *https://localhost:5000*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**apiQuestionAnswersAddPost**](QuestionAnswersApi.md#apiquestionanswersaddpost) | **POST** /api/QuestionAnswers/Add |  |
| [**apiQuestionAnswersDeleteDelete**](QuestionAnswersApi.md#apiquestionanswersdeletedelete) | **DELETE** /api/QuestionAnswers/Delete |  |
| [**apiQuestionAnswersGetGet**](QuestionAnswersApi.md#apiquestionanswersgetget) | **GET** /api/QuestionAnswers/Get |  |
| [**apiQuestionAnswersUpdatePut**](QuestionAnswersApi.md#apiquestionanswersupdateput) | **PUT** /api/QuestionAnswers/Update |  |



## apiQuestionAnswersAddPost

> Array&lt;QuestionModel&gt; apiQuestionAnswersAddPost(questionModel)



### Example

```ts
import {
  Configuration,
  QuestionAnswersApi,
} from '';
import type { ApiQuestionAnswersAddPostRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new QuestionAnswersApi();

  const body = {
    // Array<QuestionModel>
    questionModel: ...,
  } satisfies ApiQuestionAnswersAddPostRequest;

  try {
    const data = await api.apiQuestionAnswersAddPost(body);
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
| **questionModel** | `Array<QuestionModel>` |  | |

### Return type

[**Array&lt;QuestionModel&gt;**](QuestionModel.md)

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


## apiQuestionAnswersDeleteDelete

> Array&lt;QuestionModel&gt; apiQuestionAnswersDeleteDelete(requestBody)



### Example

```ts
import {
  Configuration,
  QuestionAnswersApi,
} from '';
import type { ApiQuestionAnswersDeleteDeleteRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new QuestionAnswersApi();

  const body = {
    // Array<string>
    requestBody: ...,
  } satisfies ApiQuestionAnswersDeleteDeleteRequest;

  try {
    const data = await api.apiQuestionAnswersDeleteDelete(body);
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

[**Array&lt;QuestionModel&gt;**](QuestionModel.md)

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


## apiQuestionAnswersGetGet

> Array&lt;QuestionModel&gt; apiQuestionAnswersGetGet(items, pageSize, pageNumber)



### Example

```ts
import {
  Configuration,
  QuestionAnswersApi,
} from '';
import type { ApiQuestionAnswersGetGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new QuestionAnswersApi();

  const body = {
    // Array<QuestionModel> (optional)
    items: ...,
    // ApiUsersGetGetPageSizeParameter (optional)
    pageSize: 56,
    // ApiUsersGetGetPageSizeParameter (optional)
    pageNumber: 56,
  } satisfies ApiQuestionAnswersGetGetRequest;

  try {
    const data = await api.apiQuestionAnswersGetGet(body);
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
| **items** | `Array<QuestionModel>` |  | [Optional] |
| **pageSize** | [](.md) |  | [Optional] [Defaults to `undefined`] |
| **pageNumber** | [](.md) |  | [Optional] [Defaults to `undefined`] |

### Return type

[**Array&lt;QuestionModel&gt;**](QuestionModel.md)

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


## apiQuestionAnswersUpdatePut

> ApiUsersGetGetPageSizeParameter apiQuestionAnswersUpdatePut(questionModel)



### Example

```ts
import {
  Configuration,
  QuestionAnswersApi,
} from '';
import type { ApiQuestionAnswersUpdatePutRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new QuestionAnswersApi();

  const body = {
    // Array<QuestionModel>
    questionModel: ...,
  } satisfies ApiQuestionAnswersUpdatePutRequest;

  try {
    const data = await api.apiQuestionAnswersUpdatePut(body);
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
| **questionModel** | `Array<QuestionModel>` |  | |

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

