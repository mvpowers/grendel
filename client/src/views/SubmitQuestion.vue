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
              <VToolbarTitle>Submit a Question</VToolbarTitle>
            </VToolbar>
            <VCardText>
              <VForm
                @submit.prevent="submitQuestion"
                @cancel="goBack">
                <VTextarea
                  id="submit-question-form"
                  v-model="formQuestion"
                  prepend-icon="add_comment"
                  name="question"
                  label="Question"
                  type="text" />
              </VForm>
            </VCardText>
            <VCardActions>
              <VBtn
                color="secondary"
                type="cancel"
                form="submit-question-form"
                @click="goBack">
                Back
              </VBtn>
              <VSpacer />
              <VBtn
                color="primary"
                type="submit"
                form="submit-question-form"
                @click="submitQuestion">
                Submit Question
              </VBtn>
            </VCardActions>
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
      try {
        const createQuestionRequest = {
          inquiry: this.formQuestion,
        };

        QuestionRequests.createQuestion(createQuestionRequest);
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
