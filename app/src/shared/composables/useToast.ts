import { reactive } from "vue";

/** toaster state. */
const state = reactive({
    show: false,
    message: "",
    type: "success",
    duration: 2500,
    position: "bottom-end",
});

/** Custom hook for toast state. */
export function useToast() {
    function notify(message: string, type = "success", duration = 2500, position = "bottom-end") {
        state.message = message;
        state.type = type;
        state.duration = duration;
        state.position = position;
        state.show = true;
    }
    return { state, notify };
}
