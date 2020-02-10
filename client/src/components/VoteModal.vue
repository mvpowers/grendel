<template>
  <div>
    <VDialog
      persistent
      :value="modalStatus">
      <VCard>
        <VCardText class="title text-xs-center">
          Vote for
          <span class="font-italic">
            {{ voteOptionName }}
          </span>?
        </VCardText>
        <VCardText>
          <VTextarea
            v-model="voteComment"
            rows="2"
            placeholder="Comment (optional)" />
        </VCardText>
        <VSpacer />
        <VCardActions>
          <VBtn
            color="blue darken-1"
            flat="flat"
            @click="handleCancel">
            Cancel
          </VBtn>
          <VBtn
            color="green darken-1"
            flat="flat"
            @click="handleSubmit">
            Submit
          </VBtn>
        </VCardActions>
      </VCard>
    </VDialog>
  </div>
</template>

<script>
import router from '../router';
import { VoteRequests } from '../requests';
import { routes } from '../constants';

export default {
  name: 'VoteModal',
  props: {
    modalStatus: {
      type: Boolean,
      required: true,
    },
    voteOptionName: {
      type: String,
      required: true,
    },
    voteOptionId: {
      type: Number,
      required: true,
    },
    questionId: {
      type: Number,
      required: true,
    },
  },
  data: () => ({
    voteComment: '',
  }),
  methods: {
    handleCancel() {
      this.$emit('update:modalStatus', false);
      this.voteComment = '';
    },
    async handleSubmit() {
      await this.createVote();
      this.$emit('update:modalStatus', false);
      this.voteComment = '';
    },
    async createVote() {
      try {
        const voteCreateRequest = {
          comment: this.voteComment,
          questionId: this.questionId,
          voteOptionId: this.voteOptionId,
        };

        await VoteRequests.createVote(voteCreateRequest);
        this.$toast.success('Vote submitted');
        router.push({ name: routes.WAIT });
      } catch (e) {
        console.error(e);
      }
    },
  },
};
</script>
