<template>
  <VContainer fluid>
    <VLayout justify-center>
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
import { QuestionRequests, SessionRequests, VoteOptionRequests } from '../requests';
import VoteModal from '../components/VoteModal';

export default {
  components: { VoteModal },
  data: () => ({
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
    await this.readUserSession();
    await this.readActiveQuestion();
    await this.readActiveVoteOptions();
  },
  methods: {
    handleClick(option) {
      this.selectedOption = option;
      this.modalStatus = true;
    },
    async readUserSession() {
      try {
        const { data } = await SessionRequests.getUserSession();
        console.log(data);
      } catch (e) {
        console.error(e);
      }
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
        this.voteOptions = data.map((x, idx) => ({ ...x, avatar: `http://i.pravatar.cc/${150 + idx}` }));
      } catch (e) {
        console.error(e);
      }
    },
  },
};
</script>
