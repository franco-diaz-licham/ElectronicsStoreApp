<template>
    <transition name="fade">
        <div v-if="show" class="alert position-fixed m-2 d-flex align-items-center gap-2 shadow" :class="[bsPosition, bsVariant]" role="alert" aria-live="polite" style="z-index: 2000">
            <i class="bi" :class="iconClass"></i>
            <span>{{ message }}</span>
        </div>
    </transition>
</template>

<script setup lang="ts">
import { computed, watch, onBeforeUnmount } from "vue";

/** Component props. */
const props = defineProps({
    show: { type: Boolean, default: false },
    message: { type: String, default: "" },
    type: { type: String, default: "success" },
    duration: { type: Number, default: 2500 },
    position: { type: String, default: "bottom-end" },
});

const emit = defineEmits(["update:show"]);

/** Length of time to display the toast component. */
let timer: number = 0;
watch(
    () => props.show,
    (val) => {
        clearTimeout(timer);
        if (val && props.duration > 0) timer = setTimeout(() => emit("update:show", false), props.duration);
    }
);

// Reset timer on initilised.
onBeforeUnmount(() => clearTimeout(timer));

/** Determine the toast variant. */
const bsVariant = computed(() => ({
    "alert-success": props.type === "success",
    "alert-danger": props.type === "danger",
    "alert-info": props.type === "info",
    "alert-warning": props.type === "warning",
}));

/** Determine the type of icon. */
const iconClass = computed(() => {
    return {
        success: "bi-check-circle",
        danger: "bi-exclamation-triangle",
        info: "bi-info-circle",
        warning: "bi-exclamation-circle",
    }[props.type] || "bi-info-circle";
});

/** Determine the toast position. */
const bsPosition = computed(() => {
    return {
        "bottom-end": "bottom-0 end-0",
        "bottom-start": "bottom-0 start-0",
        "top-end": "top-0 end-0",
        "top-start": "top-0 start-0",
    }[props.position] || "bottom-0 end-0";
});
</script>

<style scoped>
.fade-enter-active,
.fade-leave-active {
    transition: opacity 0.3s;
}
.fade-enter-from,
.fade-leave-to {
    opacity: 0;
}
</style>
