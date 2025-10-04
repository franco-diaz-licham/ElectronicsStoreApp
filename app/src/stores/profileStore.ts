import { defineStore } from "pinia";
import { ref } from "vue";
import type { ProfileModel } from "../features/account/models/profile.type";
import { profileInitialValues } from "../features/account/models/profileSchema";

/** Local storage key for user profile. */
const STORAGE_KEY = "profile";

export const useProfileStore = defineStore("profile", () => {
    /** Get save profile if present. Otherwise, return a default. */
    const initial = (() => {
        try {
            return JSON.parse(localStorage.getItem(STORAGE_KEY) ?? "") as ProfileModel;
        } catch {
            return profileInitialValues;
        }
    })();

    /** Current user profile information. */
    const profile = ref<ProfileModel>(initial);

    /** Method which updates user profile. */
    function updateProfile(p: ProfileModel) {
        profile.value = p;
        localStorage.setItem(STORAGE_KEY, JSON.stringify(profile.value));
    }

    return { profile, updateProfile };
});
