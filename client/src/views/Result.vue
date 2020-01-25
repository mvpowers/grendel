<template>
  <div>
    <div class="title text-xs-center ma-3">
      {{ question }}
    </div>
    <ResultGraph
      :values="values"
      :labels="labels" />
    <template v-for="comment in comments">
      <VCard
        :key="comment.id"
        class="elevation-4 ma-3">
        <VCardTitle class="font-weight-light font-italic">
          Vote for {{ comment.voteFor }}
        </VCardTitle>
        <VCardText class="body-1 text-xs-center">
          {{ comment.text }}
          <div class="text-xs-center mt-3">
            <VRating
              color="yellow darken-3"
              background-color="grey darken-1"
              empty-icon="$vuetify.icons.ratingFull"
              hover />
          </div>
        </VCardText>
      </VCard>
    </template>
  </div>
</template>

<script>
import ResultGraph from '../components/ResultGraph';

export default {
  name: 'Result',
  components: { ResultGraph },
  data: () => ({
    question: 'Who stole the cookies from the cookie jar?',
    labels: ['Bob', 'Jerry', 'Rob'],
    values: [3, 7, 5],
    comments: [
      { id: 1, voteFor: 'Bob', text: 'look at comment' },
      { id: 2, voteFor: 'Tom', text: 'merry christmas' },
      { id: 3, voteFor: 'Jerry', text: 'filthy animal' },
    ],
  }),
};
</script>
