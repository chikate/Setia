<template>
  <div v-if="editor" class="control-group">
    <div class="button-group">
      <button
        @click="editor.chain().focus().toggleBold().run()"
        :disabled="!editor.can().chain().focus().toggleBold().run()"
        :class="{ 'is-active': editor.isActive('bold') }"
      >
        Bold
      </button>
      <button
        @click="editor.chain().focus().toggleItalic().run()"
        :disabled="!editor.can().chain().focus().toggleItalic().run()"
        :class="{ 'is-active': editor.isActive('italic') }"
      >
        Italic
      </button>
      <button
        @click="editor.chain().focus().toggleStrike().run()"
        :disabled="!editor.can().chain().focus().toggleStrike().run()"
        :class="{ 'is-active': editor.isActive('strike') }"
      >
        Strike
      </button>
      <button
        @click="editor.chain().focus().toggleCode().run()"
        :disabled="!editor.can().chain().focus().toggleCode().run()"
        :class="{ 'is-active': editor.isActive('code') }"
      >
        Code
      </button>
      <button @click="editor.chain().focus().unsetAllMarks().run()">Clear marks</button>
      <button @click="editor.chain().focus().clearNodes().run()">Clear nodes</button>
      <button
        @click="editor.chain().focus().setParagraph().run()"
        :class="{ 'is-active': editor.isActive('paragraph') }"
      >
        Paragraph
      </button>
      <button
        @click="editor.chain().focus().toggleHeading({ level: 1 }).run()"
        :class="{ 'is-active': editor.isActive('heading', { level: 1 }) }"
      >
        H1
      </button>
      <button
        @click="editor.chain().focus().toggleHeading({ level: 2 }).run()"
        :class="{ 'is-active': editor.isActive('heading', { level: 2 }) }"
      >
        H2
      </button>
      <button
        @click="editor.chain().focus().toggleHeading({ level: 3 }).run()"
        :class="{ 'is-active': editor.isActive('heading', { level: 3 }) }"
      >
        H3
      </button>
      <button
        @click="editor.chain().focus().toggleHeading({ level: 4 }).run()"
        :class="{ 'is-active': editor.isActive('heading', { level: 4 }) }"
      >
        H4
      </button>
      <button
        @click="editor.chain().focus().toggleHeading({ level: 5 }).run()"
        :class="{ 'is-active': editor.isActive('heading', { level: 5 }) }"
      >
        H5
      </button>
      <button
        @click="editor.chain().focus().toggleHeading({ level: 6 }).run()"
        :class="{ 'is-active': editor.isActive('heading', { level: 6 }) }"
      >
        H6
      </button>
      <button
        @click="editor.chain().focus().toggleBulletList().run()"
        :class="{ 'is-active': editor.isActive('bulletList') }"
      >
        Bullet list
      </button>
      <button
        @click="editor.chain().focus().toggleOrderedList().run()"
        :class="{ 'is-active': editor.isActive('orderedList') }"
      >
        Ordered list
      </button>
      <button
        @click="editor.chain().focus().toggleCodeBlock().run()"
        :class="{ 'is-active': editor.isActive('codeBlock') }"
      >
        Code block
      </button>
      <button
        @click="editor.chain().focus().toggleBlockquote().run()"
        :class="{ 'is-active': editor.isActive('blockquote') }"
      >
        Blockquote
      </button>
      <button @click="editor.chain().focus().setHorizontalRule().run()">Horizontal rule</button>
      <button @click="editor.chain().focus().setHardBreak().run()">Hard break</button>
      <button
        @click="editor.chain().focus().undo().run()"
        :disabled="!editor.can().chain().focus().undo().run()"
      >
        Undo
      </button>
      <button
        @click="editor.chain().focus().redo().run()"
        :disabled="!editor.can().chain().focus().redo().run()"
      >
        Redo
      </button>
      <button
        @click="editor.chain().focus().setColor('#958DF1').run()"
        :class="{ 'is-active': editor.isActive('textStyle', { color: '#958DF1' }) }"
      >
        Purple
      </button>
      <button
        @click="editor.chain().focus().setTextAlign('left').run()"
        :class="{ 'is-active': editor.isActive({ textAlign: 'left' }) }"
      >
        Left
      </button>
      <button
        @click="editor.chain().focus().setTextAlign('center').run()"
        :class="{ 'is-active': editor.isActive({ textAlign: 'center' }) }"
      >
        Center
      </button>
      <button
        @click="editor.chain().focus().setTextAlign('right').run()"
        :class="{ 'is-active': editor.isActive({ textAlign: 'right' }) }"
      >
        Right
      </button>
      <button
        @click="editor.chain().focus().setTextAlign('justify').run()"
        :class="{ 'is-active': editor.isActive({ textAlign: 'justify' }) }"
      >
        Justify
      </button>
    </div>
  </div>
  <EditorContent :editor="editor" class="border-1 border-gray-200 border-round custom-shadow-1" />
  <!-- :style="`aspect-ratio: ${isLandscape ? `${paperAspectRatio}/1` : `1/${paperAspectRatio}`}`" -->
</template>

<script>
// import StarterKit from '@tiptap/starter-kit'
import { Editor, EditorContent } from '@tiptap/vue-3'

// import { Color } from '@tiptap/extension-color'
import ListItem from '@tiptap/extension-list-item'
import TextStyle from '@tiptap/extension-text-style'
import StarterKit from '@tiptap/starter-kit'
import TextAlign from '@tiptap/extension-text'

// import { Editor } from '@tiptap/core'
import Document from '@tiptap/extension-document'
import Paragraph from '@tiptap/extension-paragraph'
import Text from '@tiptap/extension-text'
import Heading from '@tiptap/extension-heading'
import Strike from '@tiptap/extension-strike'

export default {
  components: {
    EditorContent
  },

  props: {
    modelValue: {
      type: String,
      default: ''
    }
  },

  emits: ['update:modelValue'],

  data() {
    return {
      editor: null
    }
  },

  watch: {
    modelValue(value) {
      // HTML
      const isSame = this.editor.getHTML() === value

      // JSON
      // const isSame = JSON.stringify(this.editor.getJSON()) === JSON.stringify(value)

      if (isSame) {
        return
      }

      this.editor.commands.setContent(value, false)
    }
  },

  mounted() {
    this.editor = new Editor({
      // element: document.querySelector('.element'),
      editorProps: {
        attributes: {
          class: 'bg-white border-round p-2',
          style: 'height: 80vh; aspect-ratio: 1/1.414',
          id: 'paper'
        }
        // transformPastedText(text) {
        //   return text.toUpperCase()
        // }
      },
      extensions: [
        Document,
        Strike,
        Paragraph,
        Text,
        Heading.configure({
          levels: [1, 2, 3]
        }),
        TextAlign,
        // Color.configure({ types: [TextStyle.name, ListItem.name] }),
        TextStyle.configure({ types: [ListItem.name] }),
        StarterKit
      ],
      content: this.modelValue,
      onUpdate: () => {
        // HTML
        this.$emit('update:modelValue', this.editor.getHTML())

        // JSON
        // this.$emit('update:modelValue', this.editor.getJSON())
      }
    })
  },

  beforeUnmount() {
    this.editor.destroy()
  }
}
</script>

<style>
/* Basic editor styles */
.tiptap {
  :first-child {
    margin-top: 0;
  }

  /* List styles */
  ul,
  ol {
    padding: 0 1rem;
    margin: 1.25rem 1rem 1.25rem 0.4rem;

    li p {
      margin-top: 0.25em;
      margin-bottom: 0.25em;
    }
  }

  /* Heading styles */
  h1,
  h2,
  h3,
  h4,
  h5,
  h6 {
    margin-top: 2.5rem;
    text-wrap: pretty;
  }

  h1,
  h2 {
    margin-top: 3.5rem;
    margin-bottom: 1.5rem;
  }

  h1 {
    font-size: 1.4rem;
  }

  h2 {
    font-size: 1.2rem;
  }

  h3 {
    font-size: 1.1rem;
  }

  p {
    margin: 0;
  }

  h4,
  h5,
  h6 {
    font-size: 1rem;
  }

  /* Code and preformatted text styles */
  code {
    background-color: var(--purple-light);
    border-radius: 0.4rem;
    color: var(--black);
    font-size: 0.85rem;
    padding: 0.25em 0.3em;
  }

  pre {
    background: var(--black);
    border-radius: 0.5rem;
    color: var(--white);
    font-family: 'JetBrainsMono', monospace;
    margin: 1.5rem 0;
    padding: 0.75rem 1rem;

    code {
      background: none;
      color: inherit;
      font-size: 0.8rem;
      padding: 0;
    }
  }

  blockquote {
    border-left: 3px solid var(--gray-3);
    margin: 1.5rem 0;
    padding-left: 1rem;
  }

  hr {
    border: none;
    border-top: 1px solid var(--gray-2);
    margin: 2rem 0;
  }
}
</style>
