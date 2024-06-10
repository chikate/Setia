/* eslint-disable */
/* prettier-ignore */
// @ts-nocheck
// noinspection JSUnusedGlobalSymbols
// Generated by unplugin-auto-import
export {}
declare global {
  const EffectScope: typeof import('vue')['EffectScope']
  const computed: typeof import('vue')['computed']
  const createApp: typeof import('vue')['createApp']
  const customRef: typeof import('vue')['customRef']
  const defineAsyncComponent: typeof import('vue')['defineAsyncComponent']
  const defineComponent: typeof import('vue')['defineComponent']
  const definePage: typeof import('unplugin-vue-router/runtime')['definePage']
  const effectScope: typeof import('vue')['effectScope']
  const getCurrentInstance: typeof import('vue')['getCurrentInstance']
  const getCurrentScope: typeof import('vue')['getCurrentScope']
  const h: typeof import('vue')['h']
  const inject: typeof import('vue')['inject']
  const isProxy: typeof import('vue')['isProxy']
  const isReactive: typeof import('vue')['isReactive']
  const isReadonly: typeof import('vue')['isReadonly']
  const isRef: typeof import('vue')['isRef']
  const markRaw: typeof import('vue')['markRaw']
  const nextTick: typeof import('vue')['nextTick']
  const onActivated: typeof import('vue')['onActivated']
  const onBeforeMount: typeof import('vue')['onBeforeMount']
  const onBeforeRouteLeave: typeof import('vue-router/auto')['onBeforeRouteLeave']
  const onBeforeRouteUpdate: typeof import('vue-router/auto')['onBeforeRouteUpdate']
  const onBeforeUnmount: typeof import('vue')['onBeforeUnmount']
  const onBeforeUpdate: typeof import('vue')['onBeforeUpdate']
  const onDeactivated: typeof import('vue')['onDeactivated']
  const onErrorCaptured: typeof import('vue')['onErrorCaptured']
  const onMounted: typeof import('vue')['onMounted']
  const onRenderTracked: typeof import('vue')['onRenderTracked']
  const onRenderTriggered: typeof import('vue')['onRenderTriggered']
  const onScopeDispose: typeof import('vue')['onScopeDispose']
  const onServerPrefetch: typeof import('vue')['onServerPrefetch']
  const onUnmounted: typeof import('vue')['onUnmounted']
  const onUpdated: typeof import('vue')['onUpdated']
  const provide: typeof import('vue')['provide']
  const reactive: typeof import('vue')['reactive']
  const readonly: typeof import('vue')['readonly']
  const ref: typeof import('vue')['ref']
  const resolveComponent: typeof import('vue')['resolveComponent']
  const shallowReactive: typeof import('vue')['shallowReactive']
  const shallowReadonly: typeof import('vue')['shallowReadonly']
  const shallowRef: typeof import('vue')['shallowRef']
  const toRaw: typeof import('vue')['toRaw']
  const toRef: typeof import('vue')['toRef']
  const toRefs: typeof import('vue')['toRefs']
  const toValue: typeof import('vue')['toValue']
  const triggerRef: typeof import('vue')['triggerRef']
  const unref: typeof import('vue')['unref']
  const useAttrs: typeof import('vue')['useAttrs']
  const useAuthStore: typeof import('./src/stores/AuthStore')['useAuthStore']
  const useCRUDStore: typeof import('./src/stores/CRUDStore')['useCRUDStore']
  const useCssModule: typeof import('vue')['useCssModule']
  const useCssVars: typeof import('vue')['useCssVars']
  const useHelperStore: typeof import('./src/stores/HelperStore')['useHelperStore']
  const useI18n: typeof import('vue-i18n')['useI18n']
  const useLink: typeof import('vue-router/auto')['useLink']
  const useNotificationsCRUDStore: typeof import('./src/stores/cruds/NotificationsCRUDStore')['useNotificationsCRUDStore']
  const usePontajCRUDStore: typeof import('./src/stores/cruds/PontajCRUDStore')['usePontajCRUDStore']
  const usePostsCRUDStore: typeof import('./src/stores/cruds/PostsCRUDStore')['usePostsCRUDStore']
  const useQuestionAnswersCRUDStore: typeof import('./src/stores/cruds/QuestionAnswersCRUDStore')['useQuestionAnswersCRUDStore']
  const useQuestionsCRUDStore: typeof import('./src/stores/cruds/QuestionsCRUDStore')['useQuestionsCRUDStore']
  const useRoute: typeof import('vue-router/auto')['useRoute']
  const useRouter: typeof import('vue-router/auto')['useRouter']
  const useSettingsStore: typeof import('./src/stores/SettingsStore')['useSettingsStore']
  const useSlots: typeof import('vue')['useSlots']
  const useTagsCRUDStore: typeof import('./src/stores/cruds/TagsCRUDStore')['useTagsCRUDStore']
  const useUserCollectionCRUDStore: typeof import('./src/stores/cruds/UserCollectionCRUDStore')['useUserCollectionCRUDStore']
  const useUserTagsCRUDStore: typeof import('./src/stores/cruds/UserTagsCRUDStore')['useUserTagsCRUDStore']
  const useUsersCRUDStore: typeof import('./src/stores/cruds/UsersCRUDStore')['useUsersCRUDStore']
  const watch: typeof import('vue')['watch']
  const watchEffect: typeof import('vue')['watchEffect']
  const watchPostEffect: typeof import('vue')['watchPostEffect']
  const watchSyncEffect: typeof import('vue')['watchSyncEffect']
}
// for type re-export
declare global {
  // @ts-ignore
  export type { Component, ComponentPublicInstance, ComputedRef, ExtractDefaultPropTypes, ExtractPropTypes, ExtractPublicPropTypes, InjectionKey, PropType, Ref, VNode, WritableComputedRef } from 'vue'
  import('vue')
}
// for vue template auto import
import { UnwrapRef } from 'vue'
declare module 'vue' {
  interface GlobalComponents {}
  interface ComponentCustomProperties {
    readonly EffectScope: UnwrapRef<typeof import('vue')['EffectScope']>
    readonly computed: UnwrapRef<typeof import('vue')['computed']>
    readonly createApp: UnwrapRef<typeof import('vue')['createApp']>
    readonly customRef: UnwrapRef<typeof import('vue')['customRef']>
    readonly defineAsyncComponent: UnwrapRef<typeof import('vue')['defineAsyncComponent']>
    readonly defineComponent: UnwrapRef<typeof import('vue')['defineComponent']>
    readonly definePage: UnwrapRef<typeof import('unplugin-vue-router/runtime')['definePage']>
    readonly effectScope: UnwrapRef<typeof import('vue')['effectScope']>
    readonly getCurrentInstance: UnwrapRef<typeof import('vue')['getCurrentInstance']>
    readonly getCurrentScope: UnwrapRef<typeof import('vue')['getCurrentScope']>
    readonly h: UnwrapRef<typeof import('vue')['h']>
    readonly inject: UnwrapRef<typeof import('vue')['inject']>
    readonly isProxy: UnwrapRef<typeof import('vue')['isProxy']>
    readonly isReactive: UnwrapRef<typeof import('vue')['isReactive']>
    readonly isReadonly: UnwrapRef<typeof import('vue')['isReadonly']>
    readonly isRef: UnwrapRef<typeof import('vue')['isRef']>
    readonly markRaw: UnwrapRef<typeof import('vue')['markRaw']>
    readonly nextTick: UnwrapRef<typeof import('vue')['nextTick']>
    readonly onActivated: UnwrapRef<typeof import('vue')['onActivated']>
    readonly onBeforeMount: UnwrapRef<typeof import('vue')['onBeforeMount']>
    readonly onBeforeRouteLeave: UnwrapRef<typeof import('vue-router/auto')['onBeforeRouteLeave']>
    readonly onBeforeRouteUpdate: UnwrapRef<typeof import('vue-router/auto')['onBeforeRouteUpdate']>
    readonly onBeforeUnmount: UnwrapRef<typeof import('vue')['onBeforeUnmount']>
    readonly onBeforeUpdate: UnwrapRef<typeof import('vue')['onBeforeUpdate']>
    readonly onDeactivated: UnwrapRef<typeof import('vue')['onDeactivated']>
    readonly onErrorCaptured: UnwrapRef<typeof import('vue')['onErrorCaptured']>
    readonly onMounted: UnwrapRef<typeof import('vue')['onMounted']>
    readonly onRenderTracked: UnwrapRef<typeof import('vue')['onRenderTracked']>
    readonly onRenderTriggered: UnwrapRef<typeof import('vue')['onRenderTriggered']>
    readonly onScopeDispose: UnwrapRef<typeof import('vue')['onScopeDispose']>
    readonly onServerPrefetch: UnwrapRef<typeof import('vue')['onServerPrefetch']>
    readonly onUnmounted: UnwrapRef<typeof import('vue')['onUnmounted']>
    readonly onUpdated: UnwrapRef<typeof import('vue')['onUpdated']>
    readonly provide: UnwrapRef<typeof import('vue')['provide']>
    readonly reactive: UnwrapRef<typeof import('vue')['reactive']>
    readonly readonly: UnwrapRef<typeof import('vue')['readonly']>
    readonly ref: UnwrapRef<typeof import('vue')['ref']>
    readonly resolveComponent: UnwrapRef<typeof import('vue')['resolveComponent']>
    readonly shallowReactive: UnwrapRef<typeof import('vue')['shallowReactive']>
    readonly shallowReadonly: UnwrapRef<typeof import('vue')['shallowReadonly']>
    readonly shallowRef: UnwrapRef<typeof import('vue')['shallowRef']>
    readonly toRaw: UnwrapRef<typeof import('vue')['toRaw']>
    readonly toRef: UnwrapRef<typeof import('vue')['toRef']>
    readonly toRefs: UnwrapRef<typeof import('vue')['toRefs']>
    readonly toValue: UnwrapRef<typeof import('vue')['toValue']>
    readonly triggerRef: UnwrapRef<typeof import('vue')['triggerRef']>
    readonly unref: UnwrapRef<typeof import('vue')['unref']>
    readonly useAttrs: UnwrapRef<typeof import('vue')['useAttrs']>
    readonly useAuthStore: UnwrapRef<typeof import('./src/stores/AuthStore')['useAuthStore']>
    readonly useCRUDStore: UnwrapRef<typeof import('./src/stores/CRUDStore')['useCRUDStore']>
    readonly useCssModule: UnwrapRef<typeof import('vue')['useCssModule']>
    readonly useCssVars: UnwrapRef<typeof import('vue')['useCssVars']>
    readonly useHelperStore: UnwrapRef<typeof import('./src/stores/HelperStore')['useHelperStore']>
    readonly useLink: UnwrapRef<typeof import('vue-router/auto')['useLink']>
    readonly useNotificationsCRUDStore: UnwrapRef<typeof import('./src/stores/cruds/NotificationsCRUDStore')['useNotificationsCRUDStore']>
    readonly usePontajCRUDStore: UnwrapRef<typeof import('./src/stores/cruds/PontajCRUDStore')['usePontajCRUDStore']>
    readonly usePostsCRUDStore: UnwrapRef<typeof import('./src/stores/cruds/PostsCRUDStore')['usePostsCRUDStore']>
    readonly useQuestionAnswersCRUDStore: UnwrapRef<typeof import('./src/stores/cruds/QuestionAnswersCRUDStore')['useQuestionAnswersCRUDStore']>
    readonly useQuestionsCRUDStore: UnwrapRef<typeof import('./src/stores/cruds/QuestionsCRUDStore')['useQuestionsCRUDStore']>
    readonly useRoute: UnwrapRef<typeof import('vue-router/auto')['useRoute']>
    readonly useRouter: UnwrapRef<typeof import('vue-router/auto')['useRouter']>
    readonly useSettingsStore: UnwrapRef<typeof import('./src/stores/SettingsStore')['useSettingsStore']>
    readonly useSlots: UnwrapRef<typeof import('vue')['useSlots']>
    readonly useTagsCRUDStore: UnwrapRef<typeof import('./src/stores/cruds/TagsCRUDStore')['useTagsCRUDStore']>
    readonly useUserCollectionCRUDStore: UnwrapRef<typeof import('./src/stores/cruds/UserCollectionCRUDStore')['useUserCollectionCRUDStore']>
    readonly useUsersCRUDStore: UnwrapRef<typeof import('./src/stores/cruds/UsersCRUDStore')['useUsersCRUDStore']>
    readonly watch: UnwrapRef<typeof import('vue')['watch']>
    readonly watchEffect: UnwrapRef<typeof import('vue')['watchEffect']>
    readonly watchPostEffect: UnwrapRef<typeof import('vue')['watchPostEffect']>
    readonly watchSyncEffect: UnwrapRef<typeof import('vue')['watchSyncEffect']>
  }
}
declare module '@vue/runtime-core' {
  interface GlobalComponents {}
  interface ComponentCustomProperties {
    readonly EffectScope: UnwrapRef<typeof import('vue')['EffectScope']>
    readonly computed: UnwrapRef<typeof import('vue')['computed']>
    readonly createApp: UnwrapRef<typeof import('vue')['createApp']>
    readonly customRef: UnwrapRef<typeof import('vue')['customRef']>
    readonly defineAsyncComponent: UnwrapRef<typeof import('vue')['defineAsyncComponent']>
    readonly defineComponent: UnwrapRef<typeof import('vue')['defineComponent']>
    readonly definePage: UnwrapRef<typeof import('unplugin-vue-router/runtime')['definePage']>
    readonly effectScope: UnwrapRef<typeof import('vue')['effectScope']>
    readonly getCurrentInstance: UnwrapRef<typeof import('vue')['getCurrentInstance']>
    readonly getCurrentScope: UnwrapRef<typeof import('vue')['getCurrentScope']>
    readonly h: UnwrapRef<typeof import('vue')['h']>
    readonly inject: UnwrapRef<typeof import('vue')['inject']>
    readonly isProxy: UnwrapRef<typeof import('vue')['isProxy']>
    readonly isReactive: UnwrapRef<typeof import('vue')['isReactive']>
    readonly isReadonly: UnwrapRef<typeof import('vue')['isReadonly']>
    readonly isRef: UnwrapRef<typeof import('vue')['isRef']>
    readonly markRaw: UnwrapRef<typeof import('vue')['markRaw']>
    readonly nextTick: UnwrapRef<typeof import('vue')['nextTick']>
    readonly onActivated: UnwrapRef<typeof import('vue')['onActivated']>
    readonly onBeforeMount: UnwrapRef<typeof import('vue')['onBeforeMount']>
    readonly onBeforeRouteLeave: UnwrapRef<typeof import('vue-router/auto')['onBeforeRouteLeave']>
    readonly onBeforeRouteUpdate: UnwrapRef<typeof import('vue-router/auto')['onBeforeRouteUpdate']>
    readonly onBeforeUnmount: UnwrapRef<typeof import('vue')['onBeforeUnmount']>
    readonly onBeforeUpdate: UnwrapRef<typeof import('vue')['onBeforeUpdate']>
    readonly onDeactivated: UnwrapRef<typeof import('vue')['onDeactivated']>
    readonly onErrorCaptured: UnwrapRef<typeof import('vue')['onErrorCaptured']>
    readonly onMounted: UnwrapRef<typeof import('vue')['onMounted']>
    readonly onRenderTracked: UnwrapRef<typeof import('vue')['onRenderTracked']>
    readonly onRenderTriggered: UnwrapRef<typeof import('vue')['onRenderTriggered']>
    readonly onScopeDispose: UnwrapRef<typeof import('vue')['onScopeDispose']>
    readonly onServerPrefetch: UnwrapRef<typeof import('vue')['onServerPrefetch']>
    readonly onUnmounted: UnwrapRef<typeof import('vue')['onUnmounted']>
    readonly onUpdated: UnwrapRef<typeof import('vue')['onUpdated']>
    readonly provide: UnwrapRef<typeof import('vue')['provide']>
    readonly reactive: UnwrapRef<typeof import('vue')['reactive']>
    readonly readonly: UnwrapRef<typeof import('vue')['readonly']>
    readonly ref: UnwrapRef<typeof import('vue')['ref']>
    readonly resolveComponent: UnwrapRef<typeof import('vue')['resolveComponent']>
    readonly shallowReactive: UnwrapRef<typeof import('vue')['shallowReactive']>
    readonly shallowReadonly: UnwrapRef<typeof import('vue')['shallowReadonly']>
    readonly shallowRef: UnwrapRef<typeof import('vue')['shallowRef']>
    readonly toRaw: UnwrapRef<typeof import('vue')['toRaw']>
    readonly toRef: UnwrapRef<typeof import('vue')['toRef']>
    readonly toRefs: UnwrapRef<typeof import('vue')['toRefs']>
    readonly toValue: UnwrapRef<typeof import('vue')['toValue']>
    readonly triggerRef: UnwrapRef<typeof import('vue')['triggerRef']>
    readonly unref: UnwrapRef<typeof import('vue')['unref']>
    readonly useAttrs: UnwrapRef<typeof import('vue')['useAttrs']>
    readonly useAuthStore: UnwrapRef<typeof import('./src/stores/AuthStore')['useAuthStore']>
    readonly useCRUDStore: UnwrapRef<typeof import('./src/stores/CRUDStore')['useCRUDStore']>
    readonly useCssModule: UnwrapRef<typeof import('vue')['useCssModule']>
    readonly useCssVars: UnwrapRef<typeof import('vue')['useCssVars']>
    readonly useHelperStore: UnwrapRef<typeof import('./src/stores/HelperStore')['useHelperStore']>
    readonly useLink: UnwrapRef<typeof import('vue-router/auto')['useLink']>
    readonly useNotificationsCRUDStore: UnwrapRef<typeof import('./src/stores/cruds/NotificationsCRUDStore')['useNotificationsCRUDStore']>
    readonly usePontajCRUDStore: UnwrapRef<typeof import('./src/stores/cruds/PontajCRUDStore')['usePontajCRUDStore']>
    readonly usePostsCRUDStore: UnwrapRef<typeof import('./src/stores/cruds/PostsCRUDStore')['usePostsCRUDStore']>
    readonly useQuestionAnswersCRUDStore: UnwrapRef<typeof import('./src/stores/cruds/QuestionAnswersCRUDStore')['useQuestionAnswersCRUDStore']>
    readonly useQuestionsCRUDStore: UnwrapRef<typeof import('./src/stores/cruds/QuestionsCRUDStore')['useQuestionsCRUDStore']>
    readonly useRoute: UnwrapRef<typeof import('vue-router/auto')['useRoute']>
    readonly useRouter: UnwrapRef<typeof import('vue-router/auto')['useRouter']>
    readonly useSettingsStore: UnwrapRef<typeof import('./src/stores/SettingsStore')['useSettingsStore']>
    readonly useSlots: UnwrapRef<typeof import('vue')['useSlots']>
    readonly useTagsCRUDStore: UnwrapRef<typeof import('./src/stores/cruds/TagsCRUDStore')['useTagsCRUDStore']>
    readonly useUserCollectionCRUDStore: UnwrapRef<typeof import('./src/stores/cruds/UserCollectionCRUDStore')['useUserCollectionCRUDStore']>
    readonly useUsersCRUDStore: UnwrapRef<typeof import('./src/stores/cruds/UsersCRUDStore')['useUsersCRUDStore']>
    readonly watch: UnwrapRef<typeof import('vue')['watch']>
    readonly watchEffect: UnwrapRef<typeof import('vue')['watchEffect']>
    readonly watchPostEffect: UnwrapRef<typeof import('vue')['watchPostEffect']>
    readonly watchSyncEffect: UnwrapRef<typeof import('vue')['watchSyncEffect']>
  }
}
