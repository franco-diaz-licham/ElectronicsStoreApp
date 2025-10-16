import z from "zod";

/** Login form schema. */
export const loginSchema = z.object({
    username: z.string().min(1, "Username required."),
    password: z.string().min(1, "Password required."),
    rememberMe: z.boolean(),
});
export type LoginSchema = z.infer<typeof loginSchema>;

/** Signup form schema. */
export const signupSchema = z.object({
    firstName: z.string().min(1, "First name required."),
    surname: z.string().min(1, "Surname required."),
    email: z.string().min(1, "Email required.").email("Enter a valid email address."),
    username: z.string().min(1, "Username required."),
    password: z.string().min(5, "Message must be at least 10 characters."),
});
export type SignupSchema = z.infer<typeof signupSchema>;

/** Login form model initial default values. */
export const loginInitialValues: LoginSchema = {
    username: "",
    password: "",
    rememberMe: false,
};

/** Signup form model initial default values. */
export const signupInitialValues: SignupSchema = {
    firstName: "",
    surname: "",
    email: "",
    username: "",
    password: "",
};
