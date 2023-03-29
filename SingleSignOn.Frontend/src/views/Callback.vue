<script setup lang="ts">
import {GetJwtBySteamToken} from "../apis/JwtController";

import {useRoute, useRouter} from "vue-router";
import {useMessage} from "naive-ui";

let route = useRoute();
let router = useRouter();
let message = useMessage();

if (route.params.external_id_provider === "steam")
{
    GetJwtBySteamToken(window.location.search).then(
        result => {
            if (result)
            {
                message.success("登录成功！");
            }
            else
            {
                message.error("登录失败，请重试！");
            }
            setTimeout(
                () => {
                    router.push("/");
                },
                3000
            );
        }
    );

}
</script>

<template>
    <div>
        <span>正在验证身份，请稍后……</span>
    </div>
</template>

<style scoped>
</style>
