<script setup lang="ts">
import InternalLogin from "../components/InternalLogin.vue";
import ExternalLogin from "../components/ExternalLogin.vue";


</script>

<template>
    <div class="login-view">
        <h1 class="welcome-slogan">Welcome</h1>
        <div class="login-component">
            <internal-login />
            <external-login />
        </div>

    </div>
</template>

<style scoped>
h1 {
    font-size: 40px;
    font-weight: 100;
    font-family: Arial, serif;
    letter-spacing: 2px;
    margin-bottom: 15px;
    text-align: center;
}

.login-component {
    display: flex;
}

.login-component > * {
    margin-left: 1vw;
    margin-right: 1vw;
}

.login-component > *:not(:last-child):after {
    content: "";
    position: absolute;
    top: 0;
    right: -1vw;
    width: 1px;
    height: 100%;
    background-color: black;
}
</style>
