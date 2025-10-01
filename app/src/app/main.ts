import { createApp } from "vue";
import App from "./App.vue";
import { createRouter, createWebHistory } from "vue-router";
import "../shared/assets/styles/style.css";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";
import "bootstrap-icons/font/bootstrap-icons.css";
import { createPinia } from "pinia";
import { QueryClient, VueQueryPlugin, type VueQueryPluginOptions } from "@tanstack/vue-query";
import routes from "./routes";

/** Configure routing. */
const router = createRouter({
    history: createWebHistory(),
    routes,
});

/** Configre pinia. */
const pinia = createPinia();

/** Configure queries */
const queryClient = new QueryClient({
    defaultOptions: {
        queries: {
            staleTime: 60_000,   // data considered fresh for 1m
            gcTime: 5 * 60_000,  // garbage-collect after 5m (was cacheTime)
            refetchOnWindowFocus: false,
            retry: 2,
        },
        mutations: {
            retry: 0,
        },
    },
});

// Mount the app and add services.
const app = createApp(App);
app.use(pinia);
app.use(VueQueryPlugin, { queryClient } as VueQueryPluginOptions);
app.use(router);
app.mount("#app");
