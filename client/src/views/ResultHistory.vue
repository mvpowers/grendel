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
              <VToolbarTitle>Result History</VToolbarTitle>
            </VToolbar>
            <VList two-line>
              <template>
                <VListTile>
                  <VListTileContent>
                    <VListTileTitle>Who is most likely to eat a donkey?</VListTileTitle>
                    <VListTileSubTitle>wed october 2, 2020</VListTileSubTitle>
                  </VListTileContent>
                </VListTile>
                <VDivider />
              </template>
            </VList>
          </VCard>
        </VFlex>
      </VLayout>
    </VContainer>
  </VContent>
</template>

<script>
import { QuestionRequests } from '../requests';

export default {
  name: 'SubmitQuestion',
  data: () => ({
    formQuestion: 'Who is most likely to ',
  }),
  methods: {
    async submitQuestion() {
      if (this.formQuestion.length < 15) {
        this.$toast.error('Invalid question');
        return;
      }

      if (this.formQuestion === 'Who is most likely to ') {
        this.$toast.error('Invalid question');
        return;
      }

      try {
        const createQuestionRequest = {
          inquiry: this.formQuestion,
        };

        await QuestionRequests.createQuestion(createQuestionRequest);
        this.$toast.success('Question submitted');
        this.$router.back();
      } catch (e) {
        console.error(e);
      }
    },
    goBack() {
      this.$router.back();
    },
  },
};
</script>

<style scoped>

</style>
