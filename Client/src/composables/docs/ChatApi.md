# ChatApi

All URIs are relative to *https://localhost:5000*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**apiChatHistoryOtherUserIdGet**](ChatApi.md#apichathistoryotheruseridget) | **GET** /api/Chat/History/{otherUserId} |  |



## apiChatHistoryOtherUserIdGet

> apiChatHistoryOtherUserIdGet(otherUserId)



### Example

```ts
import {
  Configuration,
  ChatApi,
} from '';
import type { ApiChatHistoryOtherUserIdGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new ChatApi();

  const body = {
    // string
    otherUserId: 38400000-8cf0-11bd-b23e-10b96e4ef00d,
  } satisfies ApiChatHistoryOtherUserIdGetRequest;

  try {
    const data = await api.apiChatHistoryOtherUserIdGet(body);
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
| **otherUserId** | `string` |  | [Defaults to `undefined`] |

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

