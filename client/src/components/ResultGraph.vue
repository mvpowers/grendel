<script>
import { Doughnut } from 'vue-chartjs';

export default {
  name: 'ResultGraph',
  extends: Doughnut,
  props: {
    values: {
      type: Array,
      required: true,
    },
    labels: {
      type: Array,
      required: true,
    },
  },
  data() {
    return {
      options: {
        responsive: true,
        maintainAspectRatio: false,
      },
      data: {
        labels: this.$props.labels,
        datasets: [
          {
            data: this.$props.values,
            backgroundColor: [
              '#7293CB',
              '#E1974C',
              '#84BA5B',
              '#D35E60',
              '#808585',
              '#9067A7',
              '#AB6857',
              '#CCC210',
            ],
          },
        ],
      },
    };
  },
  mounted() {
    this.renderChart(this.data, this.options);
  },
};
</script>
