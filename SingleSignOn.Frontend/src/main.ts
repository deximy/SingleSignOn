import {createApp} from "vue";
import {createRouter, createWebHistory} from "vue-router";

import App from "./App.vue";
import Login from "./views/Login.vue";
import Callback from "./views/Callback.vue";

const routes = [
    {path: "/login", component: Login},
    {path: "/callback/:external_id_provider", component: Callback},
];

const router = createRouter(
    {
        history: createWebHistory(),
        routes
    }
);

const app = createApp(App);
app.use(router);
app.mount("#app");
