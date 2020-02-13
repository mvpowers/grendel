<template>
  <VContent>
    <VContainer
      fluid
      fill-height>
      <VLayout
        align-center
        justify-center>
        <VFlex
          xs12
          sm8
          md4>
          <VCard class="elevation-12">
            <VToolbar
              dark
              color="primary">
              <VToolbarTitle>Login</VToolbarTitle>
              <VSpacer />
              <VTooltip bottom>
                <VBtn
                  slot="activator"
                  icon
                  large
                  @click="$router.push({ name: resetRoute })">
                  <VIcon large>
                    help_outline
                  </VIcon>
                </VBtn>
                <span>Forgot Password</span>
              </VTooltip>
            </VToolbar>
            <VCardText>
              <VForm
                id="login-form"
                @submit.prevent="authenticateUser">
                <VTextField
                  id="username"
                  v-model="formPhone"
                  prepend-icon="phone"
                  name="username"
                  label="Phone"
                  type="phone" />
                <VTextField
                  id="password"
                  v-model="formPassword"
                  prepend-icon="lock"
                  name="password"
                  label="Password"
                  type="password" />
              </VForm>
            </VCardText>
            <VCardActions>
              <VSpacer />
              <VBtn
                color="primary"
                type="submit"
                form="login-form">
                Login
              </VBtn>
            </VCardActions>
          </VCard>
        </VFlex>
      </VLayout>
    </VContainer>
  </VContent>
</template>

<script>
import router from '../router';
import { UserRequests } from '../requests';
import { localStorageKeys, mutations, routes } from '../constants';

export default {
  data: () => ({
    drawer: null,
    formPhone: '',
    formPassword: '',
    resetRoute: routes.RESET,
  }),
  methods: {
    async authenticateUser() {
      if (!this.formPhone || !this.formPassword) {
        this.$toast.error('Phone and password cannot be empty');
        return;
      }

      try {
        const phoneNumbersOnly = this.formPhone.match(/\d+/g).map(Number).join('');

        const userAuthRequest = {
          phone: parseInt(phoneNumbersOnly, 10),
          password: this.formPassword,
        };

        const { data } = await UserRequests.authenticateUser(userAuthRequest);
        localStorage.setItem(localStorageKeys.AUTH_TOKEN, data.token.toString());
        this.$store.commit(mutations.SET_USER_INFO, data);
        router.push({ name: routes.VOTE });
      } catch (e) {
        console.error(e);
      }
    },
  },
};
</script>
