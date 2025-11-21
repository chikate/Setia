
# DriveInfoDTO


## Properties

Name | Type
------------ | -------------
`name` | string
`volumeLabel` | string
`driveType` | string
`availableFreeSpace` | [ApiDriveUploadPostRequestLength](ApiDriveUploadPostRequestLength.md)
`totalSize` | [ApiDriveUploadPostRequestLength](ApiDriveUploadPostRequestLength.md)

## Example

```typescript
import type { DriveInfoDTO } from ''

// TODO: Update the object below with actual values
const example = {
  "name": null,
  "volumeLabel": null,
  "driveType": null,
  "availableFreeSpace": null,
  "totalSize": null,
} satisfies DriveInfoDTO

console.log(example)

// Convert the instance to a JSON string
const exampleJSON: string = JSON.stringify(example)
console.log(exampleJSON)

// Parse the JSON string back to an object
const exampleParsed = JSON.parse(exampleJSON) as DriveInfoDTO
console.log(exampleParsed)
```

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


