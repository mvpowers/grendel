<template>
  <div>
    <VDialog
      persistent
      :value="modalStatus">
      <VCard>
        <VCardText class="title text-xs-center">
          Vote for
          <span class="font-italic">
            {{ selection.name }}
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
export default {
  name: 'VoteModal',
  props: {
    modalStatus: {
      type: Boolean,
      required: true,
    },
    selection: {
      type: Object,
      required: true,
    },
  },
  data: () => ({
    voteComment: '',
  }),
  methods: {
    handleCancel() {
      console.warn('vote cancelled');
      this.$emit('update:modalStatus', false);
      this.voteComment = '';
    },
    handleSubmit() {
      console.warn('vote submitted', this.selection);
      this.$emit('update:modalStatus', false);
      this.voteComment = '';
    },
  },
};
</script>
