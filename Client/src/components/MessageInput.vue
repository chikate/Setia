<template>
  <div class="message-input bg-surface-100 dark:bg-surface-800 p-2 rounded-lg flex flex-col gap-2">
    <editor-content :editor="editor" class="prose dark:prose-invert max-w-none p-2 min-h-[40px] max-h-[200px] overflow-y-auto outline-none" />
    <div class="flex justify-between items-center px-2">
        <div class="flex gap-2">
            <Button icon="pi pi-paperclip" text rounded size="small" />
            <Button icon="pi pi-image" text rounded size="small" />
        </div>
        <div class="text-xs text-surface-500">
            Enter to send, Shift+Enter for new line
        </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useEditor, EditorContent } from '@tiptap/vue-3';
import StarterKit from '@tiptap/starter-kit';
import Placeholder from '@tiptap/extension-placeholder';
import Button from 'primevue/button';

const props = defineProps<{
    placeholder?: string
}>();

const emit = defineEmits(['send']);

const editor = useEditor({
    content: '',
    extensions: [
        StarterKit,
        Placeholder.configure({
            placeholder: props.placeholder || 'Message...',
        }),
    ],
    editorProps: {
        attributes: {
            class: 'outline-none focus:outline-none',
        },
        handleKeyDown: (view, event) => {
            if (event.key === 'Enter' && !event.shiftKey) {
                event.preventDefault();
                const content = editor.value?.getHTML();
                if (content && editor.value?.getText().trim()) {
                    emit('send', content);
                    editor.value?.commands.clearContent();
                }
                return true;
            }
            return false;
        }
    },
});
</script>

<style>
.ProseMirror p.is-editor-empty:first-child::before {
  content: attr(data-placeholder);
  float: left;
  color: #adb5bd;
  pointer-events: none;
  height: 0;
}
</style>
