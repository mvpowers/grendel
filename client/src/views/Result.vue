<template>
  <div>
    <div class="title text-xs-center ma-3">
      {{ question.inquiry }}
    </div>
    <ResultGraph
      v-if="voteLabels.length && voteValues.length"
      :values="voteValues"
      :labels="voteLabels" />
    <template v-for="comment in voteComments">
      <VCard
        :key="comment.id"
        class="elevation-4 ma-3">
        <VCardTitle class="font-weight-light font-italic">
          Vote for {{ comment.voteFor }}
        </VCardTitle>
        <VCardText class="body-1 text-xs-center">
          {{ comment.text }}
        </VCardText>
        <VCardActions>
          <VListTile class="grow">
            <VLayout
              align-center
              justify-end>
              <div
                class="like-button"
                @click="likeCommentToggle(comment)">
                <VIcon class="red--text mr-1">
                  favorite
                </VIcon>
                <span class="grey--text">
                  {{ comment.likeCount }}
                </span>
              </div>
            </VLayout>
          </VListTile>
        </VCardActions>
      </VCard>
    </template>
  </div>
</template>

<script>
import ResultGraph from '../components/ResultGraph';
import { QuestionRequests, VoteOptionRequests, VoteRequests } from '../requests';

export default {
  name: 'Result',
  components: { ResultGraph },
  data: () => ({
    question: {
      inquiry: '',
    },
    votes: [],
    voteOptions: [],
  }),
  computed: {
    voteCount() {
      const votes = this.voteOptions.map((x) => {
        const voteOptionId = x.id;
        const voteCount = this.votes.filter(y => y.voteOptionId === x.id).length;

        return { voteOptionId, voteCount };
      });

      return votes.filter(x => x.voteCount > 0);
    },
    voteLabels() {
      return this.voteCount.map((x) => {
        const voteOption = this.voteOptions.find(y => y.id === x.voteOptionId);
        return voteOption.name;
      });
    },
    voteValues() {
      return this.voteCount.map(x => x.voteCount);
    },
    voteComments() {
      const votesWithComments = this.votes.filter(x => x.comment);

      return votesWithComments.map((x) => {
        const voteOption = this.voteOptions.find(y => y.id === x.voteOptionId);
        return {
          id: x.id,
          voteFor: voteOption.name,
          text: x.comment,
          likeCount: x.likeCount,
        };
      });
    },
  },
  async beforeMount() {
    await this.routeVoteFlow();
  },
  async mounted() {
    await this.readActiveVoteOptions();
    await this.readActiveQuestion();
    await this.readActiveVotes();
  },
  methods: {
    async readActiveQuestion() {
      try {
        const { data } = await QuestionRequests.readActiveQuestion();
        this.question = data;
      } catch (e) {
        console.error(e);
      }
    },
    async readActiveVotes() {
      try {
        const { data } = await VoteRequests.readActiveVotes();
        this.votes = data;
      } catch (e) {
        console.error(e);
      }
    },
    async readActiveVoteOptions() {
      try {
        const { data } = await VoteOptionRequests.readActiveVoteOptions();
        this.voteOptions = data;
      } catch (e) {
        console.error(e);
      }
    },
    likeCommentToggle(comment) {
      this.$toast.info(`like toggled ${comment.id}`);
    },
  },
};
</script>

<style>
  .like-button {
    cursor: pointer;
  }
</style>
