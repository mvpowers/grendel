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
              <VToolbarTitle>Join the Club</VToolbarTitle>
              <VSpacer />
              <VTooltip bottom>
                <VBtn
                  slot="activator"
                  icon
                  large
                  @click="$emit('toggleHasLogin')">
                  <VIcon large>
                    meeting_room
                  </VIcon>
                </VBtn>
                <span>Login</span>
              </VTooltip>
            </VToolbar>
            <VCardText>
              <VForm>
                <VTextField
                  v-model="formName"
                  prepend-icon="person"
                  name="name"
                  label="Name"
                  type="text" />
                <VTextField
                  v-model="formPhone"
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
              <VBtn color="primary">
                Register
              </VBtn>
            </VCardActions>
          </VCard>
        </VFlex>
      </VLayout>
    </VContainer>
  </VContent>
</template>

<script>
export default {
  name: 'RegisterForm',
  data: () => ({
    formName: '',
    formPhone: '',
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
