import { computed, ref } from "vue";

/** Scoped message alerter. */
export function useAlert() {
    /** The message to display within the alert. */
    const alertText = ref("");

    /** The type of aleart. */
    const alertType = ref<"success" | "danger">("success");

    /** Whether the class is success or error. */
    const alertClass = computed(() => ({
        "alert-success": alertType.value === "success",
        "alert-danger": alertType.value === "danger",
    }));

    /** Displays alert within the component. */
    function showAlert(msg: string, type: "success" | "danger" = "success") {
        alertText.value = msg;
        alertType.value = type;
        setTimeout(() => (alertText.value = ""), 2500);
    }

    return { alertText, alertType, alertClass, showAlert };
}
