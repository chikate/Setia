
# UserModel


## Properties

Name | Type
------------ | -------------
`username` | string
`password` | string
`email` | string
`signiture` | string
`birthDay` | Date
`friends` | Array&lt;string&gt;
`saves` | Array&lt;string&gt;
`name` | string
`emailVerifiedDate` | Date
`avatar` | string
`id` | string
`executionDate` | Date
`authorId` | string
`authorData` | [UserModel](UserModel.md)
`tags` | Array&lt;string&gt;

## Example

```typescript
import type { UserModel } from ''

// TODO: Update the object below with actual values
const example = {
  "username": null,
  "password": null,
  "email": null,
  "signiture": null,
  "birthDay": null,
  "friends": null,
  "saves": null,
  "name": null,
  "emailVerifiedDate": null,
  "avatar": null,
  "id": null,
  "executionDate": null,
  "authorId": null,
  "authorData": null,
  "tags": null,
} satisfies UserModel

console.log(example)

// Convert the instance to a JSON string
const exampleJSON: string = JSON.stringify(example)
console.log(exampleJSON)

// Parse the JSON string back to an object
const exampleParsed = JSON.parse(exampleJSON) as UserModel
console.log(exampleParsed)
```

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


