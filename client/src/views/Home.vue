<template>
  <div>
    <LoginForm
      v-if="hasLogin"
      @toggleHasLogin="toggleHasLogin" />
    <RegisterForm
      v-else
      @toggleHasLogin="toggleHasLogin" />
  </div>
</template>

<script>
import LoginForm from '../components/LoginForm';
import RegisterForm from '../components/RegisterForm';

export default {
  name: 'Home',
  components: {
    LoginForm,
    RegisterForm,
  },
  data: () => ({
    hasLogin: true,
  }),
  methods: {
    toggleHasLogin() {
      this.hasLogin = !this.hasLogin;
    },
  },
};
</script>
