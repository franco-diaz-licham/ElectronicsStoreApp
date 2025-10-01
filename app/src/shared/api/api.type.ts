/** Base axio response model wrapping api data. */
export type axioResponse<T> = { data: T[] } | { data: { data: T[] } };
