<template>
  <VContainer fluid>
    <VLayout justify-center>
      <VFlex
        xs12
        sm8>
        <h1>Admin Center</h1>
        <VContent>
          <VTabs v-model="activeTab">
            <!-- SESSION TAB -->
            <VTab key="session">
              session
            </VTab>
            <VTabItem key="session">
              <VCard flat>
                <VCardTitle>
                  <h3>Manual Actions</h3>
                </VCardTitle>
                <VCardText>
                  <VLayout justify-center>
                    <VBtn
                      color="success"
                      @click="openStartSessionConfirm">
                      Start Session
                    </VBtn>
                    <VBtn color="error">
                      End Session
                    </VBtn>
                  </VLayout>
                </VCardText>
              </VCard>
            </VTabItem>

            <!-- USER TAB -->
            <VTab key="user">
              user
            </VTab>
            <VTabItem key="user">
              <VCard flat>
                <VCardText>
                  user
                </VCardText>
              </VCard>
            </VTabItem>
          </VTabs>
        </VContent>
      </VFlex>
    </VLayout>
  </VContainer>
</template>

<script>
import { SessionRequests } from '../requests';

export default {
  name: 'Admin',
  data: () => ({
    activeTab: null,
  }),
  methods: {
    async startSession() {
      try {
        await SessionRequests.startSession();
        this.$toast.success('Session started');
        localStorage.clear();
      } catch (e) {
        this.$toast.error('Session start failed');
        console.error(e);
      }
    },
    async openStartSessionConfirm() {
      try {
        const confirm = await this.$swal.fire({
          title: 'Are you sure?',
          text: 'Manually start new session',
          icon: 'warning',
          showCancelButton: true,
        });

        if (confirm) this.startSession();
      } catch (e) {
        this.$toast.error('Error: openStartSessionConfirm');
        console.error(e);
      }
    },
  },
};
</script>

<style scoped>

</style>
