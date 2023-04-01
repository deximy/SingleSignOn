import {createApp} from "vue";
import {createRouter, createWebHistory} from "vue-router";
import {IsAuthenticated} from "./apis/Authentication";

import App from "./App.vue";
import Index from "./views/Index.vue";
import Login from "./views/Login.vue";
import Callback from "./views/Callback.vue";
import Authentication from "./views/Authentication.vue";

const routes = [
    {
        name: "Index",
        path: "/",
        component: Index
    },
    {
        name: "Login",
        path: "/login",
        component: Login
    },
    {
        name: "Callback",
        path: "/callback/:external_id_provider",
        component: Callback
    },
    {
        name: "Authentication",
        path: "/authentication",
        component: Authentication
    },
];

const router = createRouter(
    {
        history: createWebHistory(),
        routes
    }
);
router.beforeEach(
    async (to, from) => {
        if (to.name === "Login" || to.name === "Callback")
        {
            if (await IsAuthenticated())
            {
                return {name: "Index"};
            }
        }
    }
);

const app = createApp(App);
app.use(router);
app.mount("#app");
