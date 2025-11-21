# AuthApi

All URIs are relative to *https://localhost:5000*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**apiAuthLoginPost**](AuthApi.md#apiauthloginpost) | **POST** /api/Auth/Login |  |
| [**apiAuthRecoverAccountGet**](AuthApi.md#apiauthrecoveraccountget) | **GET** /api/Auth/RecoverAccount |  |
| [**apiAuthRegisterGet**](AuthApi.md#apiauthregisterget) | **GET** /api/Auth/Register |  |



## apiAuthLoginPost

> string apiAuthLoginPost(authenticationDTO)



### Example

```ts
import {
  Configuration,
  AuthApi,
} from '';
import type { ApiAuthLoginPostRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new AuthApi();

  const body = {
    // AuthenticationDTO
    authenticationDTO: ...,
  } satisfies ApiAuthLoginPostRequest;

  try {
    const data = await api.apiAuthLoginPost(body);
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
| **authenticationDTO** | [AuthenticationDTO](AuthenticationDTO.md) |  | |

### Return type

**string**

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


## apiAuthRecoverAccountGet

> apiAuthRecoverAccountGet(email)



### Example

```ts
import {
  Configuration,
  AuthApi,
} from '';
import type { ApiAuthRecoverAccountGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new AuthApi();

  const body = {
    // string (optional)
    email: email_example,
  } satisfies ApiAuthRecoverAccountGetRequest;

  try {
    const data = await api.apiAuthRecoverAccountGet(body);
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
| **email** | `string` |  | [Optional] [Defaults to `undefined`] |

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


## apiAuthRegisterGet

> UserModel apiAuthRegisterGet(username, password, email, signiture, birthDay, name, avatar)



### Example

```ts
import {
  Configuration,
  AuthApi,
} from '';
import type { ApiAuthRegisterGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new AuthApi();

  const body = {
    // string (optional)
    username: username_example,
    // string (optional)
    password: password_example,
    // string (optional)
    email: email_example,
    // string (optional)
    signiture: signiture_example,
    // Date (optional)
    birthDay: 2013-10-20T19:20:30+01:00,
    // string (optional)
    name: name_example,
    // string (optional)
    avatar: avatar_example,
  } satisfies ApiAuthRegisterGetRequest;

  try {
    const data = await api.apiAuthRegisterGet(body);
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
| **username** | `string` |  | [Optional] [Defaults to `undefined`] |
| **password** | `string` |  | [Optional] [Defaults to `undefined`] |
| **email** | `string` |  | [Optional] [Defaults to `undefined`] |
| **signiture** | `string` |  | [Optional] [Defaults to `undefined`] |
| **birthDay** | `Date` |  | [Optional] [Defaults to `undefined`] |
| **name** | `string` |  | [Optional] [Defaults to `undefined`] |
| **avatar** | `string` |  | [Optional] [Defaults to `undefined`] |

### Return type

[**UserModel**](UserModel.md)

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

