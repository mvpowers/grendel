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
              <template v-for="question in questionList">
                <VListTile
                  :key="question.id"
                  @click="goToResultHistory(question.id)">
                  <VListTileContent>
                    <VListTileTitle>{{ question.inquiry }}</VListTileTitle>
                    <VListTileSubTitle>{{ question.timeAsked }}</VListTileSubTitle>
                  </VListTileContent>
                </VListTile>
                <VDivider :key="`div+${question.id}`" />
              </template>
            </VList>
          </VCard>
        </VFlex>
      </VLayout>
    </VContainer>
  </VContent>
</template>

<script>
import moment from 'moment';
import { QuestionRequests } from '../requests';
import { routes } from '../constants';

export default {
  name: 'ResultHistory',
  data: () => ({
    questionList: [],
  }),
  async mounted() {
    await this.readQuestionHistory();
  },
  methods: {
    async readQuestionHistory() {
      try {
        const { data } = await QuestionRequests.readQuestionHistory();
        this.questionList = data.map(x => ({
          ...x,
          timeAsked: moment(x.timeAsked).format('LL'),
        }));
      } catch (e) {
        console.error(e);
      }
    },
    goToResultHistory(questionId) {
      this.$router.push({ name: routes.RESULT, query: { question_id: questionId } });
    },
    goBack() {
      this.$router.back();
    },
  },
};
</script>

<style scoped>

</style>
