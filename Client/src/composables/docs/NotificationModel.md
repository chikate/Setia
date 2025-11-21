
# NotificationModel


## Properties

Name | Type
------------ | -------------
`title` | string
`link` | string
`to` | string
`message` | string
`seenDate` | Date
`attachments` | Array&lt;string&gt;
`id` | string
`executionDate` | Date
`authorId` | string
`authorData` | [UserModel](UserModel.md)
`tags` | Array&lt;string&gt;

## Example

```typescript
import type { NotificationModel } from ''

// TODO: Update the object below with actual values
const example = {
  "title": null,
  "link": null,
  "to": null,
  "message": null,
  "seenDate": null,
  "attachments": null,
  "id": null,
  "executionDate": null,
  "authorId": null,
  "authorData": null,
  "tags": null,
} satisfies NotificationModel

console.log(example)

// Convert the instance to a JSON string
const exampleJSON: string = JSON.stringify(example)
console.log(exampleJSON)

// Parse the JSON string back to an object
const exampleParsed = JSON.parse(exampleJSON) as NotificationModel
console.log(exampleParsed)
```

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


