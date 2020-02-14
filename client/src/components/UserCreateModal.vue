<template>
  <div>
    <VDialog
      persistent
      :value="modalStatus">
      <VCard>
        <VCardText class="title text-xs-center">
          Create User
        </VCardText>
        <VCardText>
          <VForm
            id="login-form"
            @submit.prevent="handleSubmit"
            @cancel.prevent="handleCancel">
            <VTextField
              id="user-phone"
              v-model="formPhone"
              prepend-icon="phone"
              name="username"
              label="Phone"
              type="phone" />
            <VTextField
              id="user-name"
              v-model="formName"
              prepend-icon="lock"
              name="user-name"
              label="Name"
              type="text" />
          </VForm>
        </VCardText>
        <VSpacer />
        <VCardActions>
          <VBtn
            color="secondary"
            flat="flat"
            @click="handleCancel">
            Cancel
          </VBtn>
          <VBtn
            color="green darken-1"
            flat="flat"
            @click="handleSubmit">
            Submit
          </VBtn>
        </VCardActions>
      </VCard>
    </VDialog>
  </div>
</template>

<script>
import { UserRequests } from '../requests';
import { digitize, digitLength } from '../helpers';

export default {
  name: 'UserCreateModal',
  props: {
    modalStatus: {
      type: Boolean,
      required: true,
    },
  },
  data: () => ({
    formName: '',
    formPhone: '',
  }),
  methods: {
    resetForm() {
      this.formName = '';
      this.formPhone = '';
    },
    handleCancel() {
      this.$emit('update:modalStatus', false);
      this.resetForm();
    },
    async handleSubmit() {
      if (this.formName.length < 3) {
        this.$toast.error('Name is required');
        return;
      }

      const phoneLength = digitLength(this.formPhone);
      if (phoneLength !== 10) {
        this.$toast.error('Phone is invalid. 10 Digits Required.');
        return;
      }

      await this.createVote();
      this.$emit('update:modalStatus', false);
      this.resetForm();
    },
    async createVote() {
      try {
        const userCreateRequest = {
          phone: digitize(this.formPhone),
          name: this.formName,
        };

        await UserRequests.createUser(userCreateRequest);
        this.$toast.success('User created');
      } catch (e) {
        this.$toast.error('Unable to create user');
        console.error(e);
      }
    },
  },
};
</script>
