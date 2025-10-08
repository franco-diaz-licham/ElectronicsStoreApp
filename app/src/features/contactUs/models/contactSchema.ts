import { z } from "zod";

/** AU phone formats. */
export const auPhone = /^(\+?61|0)[2-478](\s?\d){8}$/;

/** Contact form schema. */
export const contactSchema = z.object({
    firstName: z.string().min(1, "First name required."),
    surname: z.string().min(1, "Surname required."),
    phone: z.string().min(1, "Phone required.").regex(auPhone, "Enter a valid Australian phone (e.g., 0412 345 678)."),
    email: z.string().min(1, "Email required.").email("Enter a valid email address."),
    subject: z.string().min(1, "Subject required."),
    message: z.string().min(10, "Message must be at least 10 characters."),
});
export type ContactSchema = z.infer<typeof contactSchema>;

/** Contact form model initial default values. */
export const contactInitialValues: ContactSchema = {
    firstName: "",
    surname: "",
    phone: "",
    email: "",
    subject: "",
    message: "",
};
