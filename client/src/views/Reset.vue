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
          <PasswordResetForm v-if="$route.query.token" />
          <PasswordTokenForm v-else />
        </VFlex>
      </VLayout>
    </VContainer>
  </VContent>
</template>

<script>
import PasswordResetForm from '../components/PasswordResetForm';
import PasswordTokenForm from '../components/PasswordTokenForm';

export default {
  name: 'Reset',
  components: { PasswordResetForm, PasswordTokenForm },
};
</script>
