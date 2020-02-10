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
                  v-model="phone"
                  prepend-icon="phone"
                  name="phone"
                  label="Phone"
                  type="phone" />
                <VTextField
                  id="password"
                  v-model="password"
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
import router from '@/router';
import { UserRequests } from '../requests';
import { localStorageKeys, routes } from '../constants';

export default {
  data: () => ({
    drawer: null,
    phone: '',
    password: '',
    resetRoute: routes.RESET,
  }),
  methods: {
    async authenticateUser() {
      if (!this.phone || !this.password) return;

      try {
        const userAuthRequest = {
          phone: parseInt(this.phone, 10),
          password: this.password,
        };

        const { data } = await UserRequests.authenticateUser(userAuthRequest);
        localStorage.setItem(localStorageKeys.AUTH_TOKEN, data.token.toString());
        router.push({ name: routes.VOTE });
      } catch (e) {
        console.error(e);
      }
    },
  },
};
</script>
