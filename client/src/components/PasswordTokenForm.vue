<template>
  <VCard class="elevation-12">
    <VToolbar
      dark
      color="primary">
      <VToolbarTitle>Password Reset</VToolbarTitle>
    </VToolbar>
    <VCardText>
      <VForm
        @submit.prevent="createUserResetToken"
        @cancel="goBack">
        <VTextField
          id="reset-form"
          v-model.number="formPhone"
          prepend-icon="phone"
          name="phone"
          label="Phone"
          type="tel" />
      </VForm>
    </VCardText>
    <VCardActions>
      <VBtn
        color="secondary"
        type="cancel"
        form="reset-form"
        @click="goBack">
        Back
      </VBtn>
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
import { routes } from '../constants';

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
        this.$swal({
          icon: 'info',
          title: 'Password Recovery Sent',
          text: 'Please follow the link sent to your phone to update your password',
          showConfirmButton: true,
        });
        this.$router.push({ name: routes.HOME });
        console.info('TODO: remove console', data);
      } catch (e) {
        console.error(e);
      }
    },
    goBack() {
      this.$router.push({ name: routes.HOME });
    },
  },
};
</script>
