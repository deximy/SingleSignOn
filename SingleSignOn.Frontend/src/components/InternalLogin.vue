<script setup lang="ts">
import {NInput} from "naive-ui";
import {NButton} from "naive-ui";

import {useMessage} from "naive-ui";
const message = useMessage();

const HandleInternalLogin = () => {
    message.info("还没写完，在做了在做了");
};
</script>

<template>
    <div class="login-internal">
        <n-input
            placeholder="请输入用户名"
        />
        <n-input
            placeholder="请输入密码"
            type="password"
        />
        <n-button @click="HandleInternalLogin">登录</n-button>
    </div>
</template>

<style scoped>
.login-internal {
    display: flex;
    flex-direction: column;
    position: relative;
}

.login-internal > *:not(:last-child) {
    margin-bottom: 2vh;
}
</style>
