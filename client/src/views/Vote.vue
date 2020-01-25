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
import VoteModal from '../components/VoteModal';

export default {
  components: { VoteModal },
  data: () => ({
    modalStatus: false,
    question: 'Who stole the cookies from the cookie jar?',
    selectedOption: {},
    voteOptions: [
      { id: 1, name: 'Bob', avatar: 'http://i.pravatar.cc/150' },
      { id: 2, name: 'Tony', avatar: 'http://i.pravatar.cc/151' },
      { id: 3, name: 'Rob', avatar: 'http://i.pravatar.cc/152' },
      { id: 4, name: 'Jerry', avatar: 'http://i.pravatar.cc/153' },
      { id: 5, name: 'Tom', avatar: 'http://i.pravatar.cc/154' },
    ],
  }),
  methods: {
    handleClick(option) {
      this.selectedOption = option;
      this.modalStatus = true;
    },
  },
};
</script>
