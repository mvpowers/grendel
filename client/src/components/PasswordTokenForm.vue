<template>
  <VCard class="elevation-12">
    <VToolbar
      dark
      color="primary">
      <VToolbarTitle>Password Reset</VToolbarTitle>
    </VToolbar>
    <VCardText>
      <VForm @submit.prevent="createUserResetToken">
        <VTextField
          id="reset-form"
          v-model="formPhone"
          prepend-icon="phone"
          name="phone"
          label="Phone"
          type="tel" />
      </VForm>
    </VCardText>
    <VCardActions>
      <VSpacer />
      <VBtn
        color="primary"
        type="submit"
        form="reset-form"
        @click="createUserResetToken">
        Send Reset Link
      </VBtn>
    </VCardActions>
  </VCard>
</template>

<script>
import { UserRequests } from '../requests';

export default {
  name: 'PasswordTokenForm',
  data: () => ({
    formPhone: '',
  }),
  methods: {
    async createUserResetToken() {
      try {
        const userTokenRequest = {
          phone: parseInt(this.formPhone, 10),
        };
        const { data } = await UserRequests.createUserResetToken(userTokenRequest);
        console.info('success', data);
      } catch (e) {
        console.error(e);
      }
    },
  },
};
</script>

<style scoped>

</style>
