import {defineConfig} from "vite";
import vue from "@vitejs/plugin-vue";

// https://vitejs.dev/config/
export default defineConfig(
    {
        build: {
            outDir: "../SingleSignOn.Backend/wwwroot",
        },
        plugins: [
            vue()
        ],
        server: {
            proxy: {
                "/jwts": {
                    target: "http://localhost:5000",
                    changeOrigin: true,
                },
            },
        },
    },
);
