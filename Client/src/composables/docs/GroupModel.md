
# GroupModel


## Properties

Name | Type
------------ | -------------
`name` | string
`description` | string
`id` | string
`executionDate` | Date
`authorId` | string
`authorData` | [UserModel](UserModel.md)
`tags` | Array&lt;string&gt;

## Example

```typescript
import type { GroupModel } from ''

// TODO: Update the object below with actual values
const example = {
  "name": null,
  "description": null,
  "id": null,
  "executionDate": null,
  "authorId": null,
  "authorData": null,
  "tags": null,
} satisfies GroupModel

console.log(example)

// Convert the instance to a JSON string
const exampleJSON: string = JSON.stringify(example)
console.log(exampleJSON)

// Parse the JSON string back to an object
const exampleParsed = JSON.parse(exampleJSON) as GroupModel
console.log(exampleParsed)
```

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


