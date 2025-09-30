import { createApp } from "vue";
import App from "./App.vue";
import routes from "./routes";
import { createRouter, createWebHistory } from "vue-router";
import "../shared/assets/styles/style.css";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";
import "bootstrap-icons/font/bootstrap-icons.css";

const router = createRouter({
    history: createWebHistory(),
    routes,
});

createApp(App).use(router).mount("#app");
