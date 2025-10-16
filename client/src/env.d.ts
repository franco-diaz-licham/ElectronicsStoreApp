/// <reference types="vite/client" />

// Tell TS how to type .vue files
declare module "*.vue" {
    import type { DefineComponent } from "vue";
    const component: DefineComponent<{}, {}, any>;
    export default component;
}

// (optional) static assets used via import
declare module "*.png" {
    const src: string;
    export default src;
}
