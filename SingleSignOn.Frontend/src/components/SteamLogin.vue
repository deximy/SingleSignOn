<script setup lang="ts">
import {NButton} from "naive-ui";

const HandleSteamLogin = () => {
    let realm = window.location.protocol + "//" + window.location.hostname;
    if (window.location.port.length !== 0)
    {
        realm += ":" + window.location.port;
    }
    window.location.href = `https://steamcommunity.com/openid/login?openid.claimed_id=http://specs.openid.net/auth/2.0/identifier_select&openid.identity=http://specs.openid.net/auth/2.0/identifier_select&openid.mode=checkid_setup&openid.ns=http://specs.openid.net/auth/2.0&openid.realm=${realm}&openid.return_to=${realm}/callback/steam`;
};
</script>

<template>
    <div class="steam-login">
        <n-button @click="HandleSteamLogin">通过Steam登录</n-button>
    </div>
</template>

<style scoped>
</style>
