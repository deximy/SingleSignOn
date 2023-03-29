<script setup lang="ts">
import {IsAuthenticated} from "../apis/Authentication";
import {ref} from "vue";

const is_authenticated = ref(false);
IsAuthenticated().then(result => is_authenticated.value = result);
</script>

<template>
    <div class="index">
        <div v-if="!is_authenticated">
            <span>这是一个什么都没有的首页，</span><router-link to="/login">点击跳转到登录页</router-link>
        </div>
        <div v-else>
            <span>这是一个什么都没有的首页，你已登录！</span>
        </div>
    </div>
</template>

<style scoped>
</style>
