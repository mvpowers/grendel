<template>
  <VLayout
    align-center
    justify-center>
    <VFlex
      xs12
      sm8
      md4>
      <VAlert
        :value="true"
        class="mx-3 text-xs-center"
        color="warning"
        icon="priority_high">
        Uh oh! Looks like something may be jacked.
      </VAlert>
    </VFlex>
  </VLayout>
</template>

<script>
export default {
  name: 'WarningAlert',
};
</script>

<style scoped>

</style>
