<template>
  <VCard class="elevation-12">
    <VToolbar
      dark
      color="primary">
      <VToolbarTitle>Password Reset</VToolbarTitle>
    </VToolbar>
    <VCardText>
      <VForm
        id="password-reset-form"
        @submit.prevent="resetUserPassword">
        <VTextField
          v-model="formName"
          disabled
          prepend-icon="person"
          name="name"
          label="Name"
          type="text" />
        <VTextField
          v-model="formPhone"
          disabled
          prepend-icon="phone"
          name="phone"
          label="Phone"
          type="tel" />
        <VTextField
          id="password"
          v-model="formPassword"
          prepend-icon="lock"
          name="password"
          label="Password"
          type="password" />
        <VTextField
          id="confirm-password"
          v-model="formConfirmPassword"
          :prepend-icon="passwordMatch ? 'check_circle_outline' : 'highlight_off'"
          name="confirm-password"
          label="Confirm Password"
          type="password" />
      </VForm>
    </VCardText>
    <VCardActions>
      <VSpacer />
      <VBtn
        color="primary"
        type="submit"
        form="password-reset-form">
        Reset
      </VBtn>
    </VCardActions>
  </VCard>
</template>

<script>
import { UserRequests } from '../requests';
import { routes } from '../constants';

export default {
  name: 'PasswordResetForm',
  data: () => ({
    formName: null,
    formPhone: null,
    formPassword: '',
    formConfirmPassword: '',
  }),
  computed: {
    passwordMatch() {
      return this.formPassword === this.formConfirmPassword && this.formPassword.length > 5;
    },
  },
  async mounted() {
    await this.getUserFromResetToken();
  },
  methods: {
    async getUserFromResetToken() {
      try {
        const { data } = await UserRequests.getUserFromResetToken(this.$route.query.token);
        this.formName = data.name;
        this.formPhone = data.phone;
      } catch (e) {
        console.error(e);
      }
    },
    async resetUserPassword() {
      const resetRequest = {
        newPassword: this.formPassword,
        newPasswordConfirm: this.formConfirmPassword,
        userResetToken: this.$route.query.token,
      };

      try {
        await UserRequests.resetUserPassword(resetRequest);
        this.$toast.success('Password updated');
        this.$router.push({ name: routes.HOME });
      } catch (e) {
        console.error(e);
      }
    },
  },
};
</script>

<style scoped>

</style>
