<template>
  <VContainer fluid>
    <LoadSpinner v-if="isPageLoading" />
    <VLayout
      v-else
      justify-center>
      <VFlex
        xs12
        sm8>
        <div class="title text-xs-center my-3">
          {{ question.inquiry }}
        </div>
        <VList>
          <template v-for="(option, index) in voteOptions">
            <VListTile
              :key="option.id"
              avatar
              @click="handleClick(option)">
              <VListTileAvatar>
                <img :src="option.avatar">
              </VListTileAvatar>
              <VListTileTitle>{{ option.name }}</VListTileTitle>
            </VListTile>
            <VDivider
              v-if="index + 1 < voteOptions.length"
              :key="`divider-${index}`" />
          </template>
        </VList>
      </VFlex>
    </VLayout>
    <VoteModal
      :question-id="question.id"
      :vote-option-id="selectedOption.id"
      :vote-option-name="selectedOption.name"
      :modal-status.sync="modalStatus" />
  </VContainer>
</template>

<script>
import { QuestionRequests, VoteOptionRequests } from '../requests';
import VoteModal from '../components/VoteModal';
import LoadSpinner from '../components/LoadSpinner';

export default {
  components: { VoteModal, LoadSpinner },
  data: () => ({
    isPageLoading: false,
    modalStatus: false,
    question: {
      id: 0,
      inquiry: '',
    },
    selectedOption: {
      id: 0,
      name: '',
    },
    voteOptions: [],
  }),
  async beforeMount() {
    await this.routeVoteFlow();
  },
  async mounted() {
    this.isPageLoading = true;

    await this.readActiveQuestion();
    await this.readActiveVoteOptions();

    this.$nextTick(() => {
      this.isPageLoading = false;
    });
  },
  methods: {
    handleClick(option) {
      this.selectedOption = option;
      this.modalStatus = true;
    },
    async readActiveQuestion() {
      try {
        const { data } = await QuestionRequests.readActiveQuestion();
        this.question = data;
      } catch (e) {
        console.error(e);
      }
    },
    async readActiveVoteOptions() {
      try {
        const { data } = await VoteOptionRequests.readActiveVoteOptions();
        this.voteOptions = data
          .map((x, idx) => ({ ...x, avatar: `https://i.pravatar.cc/${150 + idx}` }))
          .sort((a, b) => (a.name.toLowerCase() > b.name.toLowerCase() ? 1 : 0));
      } catch (e) {
        console.error(e);
      }
    },
  },
};
</script>
