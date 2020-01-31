<template>
  <VContainer fluid>
    <VLayout justify-center>
      <VFlex
        xs12
        sm8>
        <div class="title text-xs-center my-3">
          {{ question }}
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
      :selection="selectedOption"
      :modal-status.sync="modalStatus" />
  </VContainer>
</template>

<script>
import { QuestionRequests, VoteOptionRequests } from '../requests';
import VoteModal from '../components/VoteModal';

export default {
  components: { VoteModal },
  data: () => ({
    modalStatus: false,
    question: '',
    selectedOption: {},
    voteOptions: [],
  }),
  async mounted() {
    await this.readActiveQuestion();
    await this.readActiveVoteOptions();
  },
  methods: {
    handleClick(option) {
      this.selectedOption = option;
      this.modalStatus = true;
    },
    async readActiveQuestion() {
      try {
        const { data } = await QuestionRequests.readActiveQuestion();
        this.question = data.inquiry;
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
