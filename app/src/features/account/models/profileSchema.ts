import { z } from "zod";

/** AU phone formats. */
export const state = /^(NSW|VIC|QLD|SA|WA|TAS|NT|ACT)$/;
export const postcode = /^\d{4}$/;

/** Profile form schema. */
export const profileSchema = z.object({
    firstName: z.string().min(1, "First name required."),
    surname: z.string().min(1, "Surname required."),
    street: z.string().min(1, "Street required."),
    suburb: z.string().min(1, "Suburb required."),
    state: z.string().regex(state, "State must be a valid Australian state abbreviation"),
    postcode: z.string().length(4, "Suburb required.").regex(postcode, "Postcode must be 4 digits"),
});
export type ProfileSchema = z.infer<typeof profileSchema>;

/** Profile form model initial default values. */
export const profileInitialValues: ProfileSchema = {
    firstName: "",
    surname: "",
    street: "",
    suburb: "",
    state: "",
    postcode: "",
};
