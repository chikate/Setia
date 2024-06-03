<template>
  <main>
    <div
      class="bg-cover bg-center"
      style="background-image: url('https://localhost:44381/1/path36.png')"
    >
      <main style="background: linear-gradient(rgba(23, 23, 23, 0) 20%, rgba(23, 23, 23, 1))">
        <a class="text-7xl font-bold">PROFILE</a>
        <FileUpload
          mode="basic"
          name="demo[]"
          accept="image/*"
          customUpload
          @uploader="useHelperStore().uploadFiles($event.files)"
          :maxFileSize="1000000"
        />
      </main>
    </div>
  </main>
</template>
