<script setup lang="ts">
import {Global} from "../apis/Global";
import {IsAuthenticated} from "../apis/Authentication";
import {ref} from "vue";
import {useRoute} from "vue-router";

const route = useRoute();

const is_authenticated = ref(false);
IsAuthenticated().then(
    result => {
        is_authenticated.value = result;
        setTimeout(
            () => {
                if (result)
                {
                    window.location.href = `${route.query.redirect}/?jwts=${localStorage.getItem("token")}`;
                }
            },
            3000
        );
    }
);

window.addEventListener(
    "visibilitychange",
    () => {
        if (!document.hidden)
        {
            location.reload();
        }
    }
);
</script>

<template>
    <div class="authentication">
        <div v-if="!is_authenticated">
            <span>你还没有登录，请不要关闭当前页面，<a :href="`${Global.realm}/login`" target="_blank">点击打开新标签页登录</a>后刷新该页面！</span>
        </div>
        <div v-else>
            <span>登录成功，等待返回原应用……</span>
        </div>
    </div>
</template>

<style scoped>
</style>