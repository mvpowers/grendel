<template>
  <VCard class="elevation-12">
    <VToolbar
      dark
      color="primary">
      <VToolbarTitle>Password Reset</VToolbarTitle>
    </VToolbar>
    <VCardText>
      <VForm>
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
      <VBtn color="primary">
        Reset
      </VBtn>
    </VCardActions>
  </VCard>
</template>

<script>
export default {
  name: 'PasswordResetForm',
  data: () => ({
    formPassword: '',
    formConfirmPassword: '',
  }),
  computed: {
    passwordMatch() {
      return this.formPassword === this.formConfirmPassword && this.formPassword.length > 5;
    },
  },
};
</script>

<style scoped>

</style>
