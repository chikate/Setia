
# LinkModel


## Properties

Name | Type
------------ | -------------
`sourceType` | string
`sourceId` | string
`targetType` | string
`targetId` | string
`metadata` | string
`id` | string
`executionDate` | Date
`authorId` | string
`authorData` | [UserModel](UserModel.md)
`tags` | Array&lt;string&gt;

## Example

```typescript
import type { LinkModel } from ''

// TODO: Update the object below with actual values
const example = {
  "sourceType": null,
  "sourceId": null,
  "targetType": null,
  "targetId": null,
  "metadata": null,
  "id": null,
  "executionDate": null,
  "authorId": null,
  "authorData": null,
  "tags": null,
} satisfies LinkModel

console.log(example)

// Convert the instance to a JSON string
const exampleJSON: string = JSON.stringify(example)
console.log(exampleJSON)

// Parse the JSON string back to an object
const exampleParsed = JSON.parse(exampleJSON) as LinkModel
console.log(exampleParsed)
```

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


