
import { defineStore } from "pinia";
import { ref } from "vue";

export type Profile = { id: string; displayName: string; email: string };
export const useProfileStore = defineStore("profile", () => {
    const profile = ref<Profile | null>(null);
    function setProfile(p: Profile | null) {
        profile.value = p;
    }
    return { profile, setProfile };
});
