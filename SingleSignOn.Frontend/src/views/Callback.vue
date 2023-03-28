<script setup lang="ts">
import {GetJwtBySteamToken} from "../apis/JwtController";

import {useRoute} from "vue-router";

let route = useRoute();
if (route.params.external_id_provider === "steam")
{
    GetJwtBySteamToken(window.location.search);
}
</script>

<template>
    <div>
        <span>正在验证身份，请稍后……</span>
    </div>
</template>

<style scoped>
</style>
