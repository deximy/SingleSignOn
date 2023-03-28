<script setup lang="ts">
import Login from "./views/Login.vue";

import {NMessageProvider} from "naive-ui";
</script>

<template>
    <n-message-provider>
        <Login/>
    </n-message-provider>
</template>

<style>
* {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
}

body {
    height: 100vh;
    display: flex;
    justify-content: center;
    align-items: center;
}
</style>
