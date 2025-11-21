
# QuestionModel


## Properties

Name | Type
------------ | -------------
`title` | string
`options` | Array&lt;string&gt;
`selection` | [QuestionModelSelection](QuestionModelSelection.md)
`correctAnswers` | [QuestionModelSelection](QuestionModelSelection.md)
`id` | string
`executionDate` | Date
`authorId` | string
`authorData` | [UserModel](UserModel.md)
`tags` | Array&lt;string&gt;

## Example

```typescript
import type { QuestionModel } from ''

// TODO: Update the object below with actual values
const example = {
  "title": null,
  "options": null,
  "selection": null,
  "correctAnswers": null,
  "id": null,
  "executionDate": null,
  "authorId": null,
  "authorData": null,
  "tags": null,
} satisfies QuestionModel

console.log(example)

// Convert the instance to a JSON string
const exampleJSON: string = JSON.stringify(example)
console.log(exampleJSON)

// Parse the JSON string back to an object
const exampleParsed = JSON.parse(exampleJSON) as QuestionModel
console.log(exampleParsed)
```

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


