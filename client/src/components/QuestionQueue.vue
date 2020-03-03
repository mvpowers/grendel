<template>
  <div>
    <LoadSpinner v-if="isFetching" />
    <div v-else>
      <VAlert
        v-if="queuedQuestions.length === 0"
        :value="true"
        class="mx-3 text-xs-center"
        color="warning"
        icon="priority_high">
        No questions are in queue.
      </VAlert>
      <VCard
        v-else
        flat>
        <VCardTitle>
          <h3 class="mx-auto">
            Question Queue
          </h3>
        </VCardTitle>
        <VCardText>
          <VList three-line>
            <template v-for="question in queuedQuestions">
              <VListTile :key="question.id">
                <VListTileContent>
                  <VListTileSubTitle>{{ question.inquiry }}</VListTileSubTitle>
                </VListTileContent>
                <VListTileAction>
                  <VIcon
                    color="red darken-2"
                    class="delete-icon"
                    @click="openQuestionDeleteConfirm(question)">
                    remove_circle
                  </VIcon>
                </VListTileAction>
              </VListTile>
              <VDivider :key="`div_${question.id}`" />
            </template>
          </VList>
        </VCardText>
      </VCard>
    </div>
  </div>
</template>
<script>
import { QuestionRequests } from '../requests';
import LoadSpinner from './LoadSpinner';

export default {
  name: 'QuestionQueue',
  components: { LoadSpinner },
  props: {
    isFetching: {
      required: true,
      type: Boolean,
    },
    queuedQuestions: {
      required: true,
      type: Array,
    },
  },
  methods: {
    async openQuestionDeleteConfirm(question) {
      try {
        const confirm = await this.$swal.fire({
          title: 'Confirm Question Removal',
          text: question.inquiry,
          icon: 'warning',
          showCancelButton: true,
        });

        if (confirm.value) this.deleteQuestion(question.id);
      } catch (e) {
        this.$toast.error('Error: openStartSessionConfirm');
        console.error(e);
      }
    },
    async deleteQuestion(questionId) {
      try {
        await QuestionRequests.deleteQuestion(questionId);
        this.$emit('question-delete', questionId);
        this.$toast.success('Question removed');
      } catch (e) {
        this.$toast.error('Error: deleteQuestion');
        console.error(e);
      }
    },
  },
};
</script>
<style>
  .delete-icon {
    cursor: pointer;
  }
</style>
