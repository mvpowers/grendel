<template>
  <div>
    <div class="title text-xs-center ma-3">
      {{ question.inquiry }}
    </div>
    <VAlert
      :value="votes.length === 0"
      class="mx-3"
      color="warning"
      icon="priority_high">
      Either no one voted or something is jacked.
    </VAlert>
    <ResultGraph
      v-if="voteLabels.length && voteValues.length"
      :values="voteValues"
      :labels="voteLabels" />
    <template v-for="voteComment in voteComments">
      <VCard
        :key="voteComment.id"
        class="elevation-4 ma-3">
        <VCardTitle class="font-weight-light font-italic">
          Vote for {{ voteComment.voteOptionName }}
        </VCardTitle>
        <VCardText class="body-1 text-xs-center">
          {{ voteComment.comment }}
        </VCardText>
        <VCardActions>
          <VListTile class="grow">
            <VLayout
              align-center
              justify-end>
              <div
                class="like-button"
                @click="likeCommentToggle(voteComment)">
                <VIcon :class="{ 'red--text': voteComment.currentUserLike }">
                  favorite
                </VIcon>
                <span class="grey--text ml-1">
                  {{ voteComment.likeCount }}
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
import {
  QuestionRequests,
  VoteOptionRequests,
  VoteRequests,
  LikeRequests,
} from '../requests';

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
          ...x,
          voteOptionName: voteOption.name,
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
    async likeCommentToggle(vote) {
      if (vote.currentUserLike) {
        await this.deleteLike(vote.id);
      } else {
        await this.createLike(vote.id);
      }
    },
    async createLike(voteId) {
      try {
        const createLikeRequest = { voteId };
        const { data } = await LikeRequests.createLike(createLikeRequest);

        const oldVoteIdx = this.votes.findIndex(x => x.id === data.id);
        this.votes.splice(oldVoteIdx, 1, data);
      } catch (e) {
        console.error(e);
      }
    },
    async deleteLike(voteId) {
      try {
        const deleteLikeRequest = { voteId };
        const { data } = await LikeRequests.deleteLike(deleteLikeRequest);

        const oldVoteIdx = this.votes.findIndex(x => x.id === voteId);
        this.votes.splice(oldVoteIdx, 1, data);
      } catch (e) {
        console.error(e);
      }
    },
  },
};
</script>

<style>
  .like-button {
    cursor: pointer;
  }
</style>
