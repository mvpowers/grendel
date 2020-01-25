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
                  @click="$emit('toggleHasLogin')">
                  <VIcon large>
                    person_add
                  </VIcon>
                </VBtn>
                <span>Register</span>
              </VTooltip>
            </VToolbar>
            <VCardText>
              <VForm>
                <VTextField
                  prepend-icon="person"
                  name="login"
                  label="Login"
                  type="text" />
                <VTextField
                  id="password"
                  prepend-icon="lock"
                  name="password"
                  label="Password"
                  type="password" />
              </VForm>
            </VCardText>
            <VCardActions>
              <VSpacer />
              <VBtn color="primary">
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
export default {
  data: () => ({
    drawer: null,
  }),
};
</script>
