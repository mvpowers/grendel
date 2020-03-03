<template>
  <VContainer fluid>
    <VLayout justify-center>
      <VFlex
        xs12
        sm8>
        <VLayout row>
          <VFlex>
            <h1>Admin Center</h1>
          </VFlex>
          <VFlex class="grow" />
          <VBtn @click="$router.back()">
            Back
          </VBtn>
        </VLayout>
        <VContent>
          <VTabs v-model="activeTab">
            <!-- SESSION TAB -->
            <VTab key="session">
              session
            </VTab>
            <VTabItem key="session">
              <QuestionQueue
                :is-fetching="isQueuedQuestionsFetching"
                :queued-questions="queuedQuestions" />

              <VCard flat>
                <VCardTitle>
                  <h3 class="mx-auto">
                    Manual Actions
                  </h3>
                </VCardTitle>
                <VCardText>
                  <VLayout justify-center>
                    <VBtn
                      :disabled="queuedQuestions.length === 0"
                      color="success"
                      @click="openStartSessionConfirm">
                      Start Session
                    </VBtn>
                    <VBtn
                      color="error"
                      @click="openEndSessionConfirm">
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
                <VCardTitle>
                  <h3 class="mx-auto">
                    Manual Actions
                  </h3>
                </VCardTitle>
                <VCardText>
                  <VLayout justify-center>
                    <VBtn
                      color="success"
                      @click="userCreateModalStatus = true">
                      Create User
                    </VBtn>
                  </VLayout>
                </VCardText>
              </VCard>
            </VTabItem>
          </VTabs>
        </VContent>
      </VFlex>
    </VLayout>
    <UserCreateModal :modal-status.sync="userCreateModalStatus" />
  </VContainer>
</template>

<script>
import UserCreateModal from '../components/UserCreateModal';
import { QuestionRequests, SessionRequests } from '../requests';
import QuestionQueue from '../components/QuestionQueue';

export default {
  name: 'Admin',
  components: { QuestionQueue, UserCreateModal },
  data: () => ({
    activeTab: null,
    userCreateModalStatus: false,
    isQueuedQuestionsFetching: false,
    queuedQuestions: [],
  }),
  async mounted() {
    await this.getQueuedQuestions();
  },
  methods: {
    async startSession() {
      try {
        await SessionRequests.startSession();
        this.$toast.success('Session started');
      } catch (e) {
        this.$toast.error('Session start failed');
        console.error(e);
      }
    },
    async endSession() {
      try {
        await SessionRequests.expireSession();
        this.$toast.success('Session ended');
      } catch (e) {
        this.$toast.error('Session end failed');
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

        if (confirm.value) this.startSession();
      } catch (e) {
        this.$toast.error('Error: openStartSessionConfirm');
        console.error(e);
      }
    },
    async openEndSessionConfirm() {
      try {
        const confirm = await this.$swal.fire({
          title: 'Are you sure?',
          text: 'Manually end session',
          icon: 'warning',
          showCancelButton: true,
        });

        if (confirm.value) this.endSession();
      } catch (e) {
        this.$toast.error('Error: openEndSessionConfirm');
        console.error(e);
      }
    },
    async getQueuedQuestions() {
      try {
        this.isQueuedQuestionsFetching = true;
        const { data } = await QuestionRequests.readQueuedQuestions();
        this.queuedQuestions = data;
      } catch (e) {
        this.$toast.error('Error: getQueuedQuestions');
        console.error(e);
      } finally {
        this.isQueuedQuestionsFetching = false;
      }
    },
  },
};
</script>
