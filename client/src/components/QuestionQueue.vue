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
          <VList>
            <VListTile
              v-for="(question, idx) in queuedQuestions"
              :key="question.id"
              prepend-icon="phone">
              <VListTileAvatar>{{ idx + 1 }}</VListTileAvatar>
              <VListTileContent>
                <VListTileTitle>{{ question.inquiry }}</VListTileTitle>
              </VListTileContent>
            </VListTile>
          </VList>
        </VCardText>
      </VCard>
    </div>
  </div>
</template>
<script>
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
};
</script>
