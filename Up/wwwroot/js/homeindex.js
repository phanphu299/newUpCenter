Vue.component('line-chart', {
    extends: VueChartJs.Line,
    props: {
        chartdata: {
            type: Object,
            default: null
        },
        options: {
            type: Object,
            default: null
        }
    },
    mounted() {
        this.renderChart(this.chartdata, this.options);
    }

});

var vue = new Vue({
    el: '#HomeIndex',
    data: {
        tab: null,
        loaded: false,
        chartdata: {
            labels: ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'],
            datasets: [
                {
                    label: 'Giao Tiếp',
                    data: [],
                    borderColor: '#4caf50',
                    pointBackgroundColor: '#4caf50',
                    borderWidth: 1,
                    pointBorderColor: '#4caf50',
                    backgroundColor: 'transparent'
                },
                {
                    label: 'Thiếu Nhi',
                    data: [],
                    borderColor: '#1976d2',
                    pointBackgroundColor: '#1976d2',
                    borderWidth: 1,
                    pointBorderColor: '#1976d2',
                    backgroundColor: 'transparent'
                },
                {
                    label: 'Chứng Chỉ Quốc Tế',
                    data: [],
                    borderColor: '#f44336',
                    pointBackgroundColor: '#f44336',
                    borderWidth: 1,
                    pointBorderColor: '#f44336',
                    backgroundColor: 'transparent'
                }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false
        }

    },

    async mounted() {
        this.loaded = false;
        var that = this;
        try {
            await axios.get('/ThongKe/GetThongKeHocVienAsync')
                .then(function (response) {
                    that.chartdata.datasets[0].data = response.data.giaoTiep;
                    that.chartdata.datasets[1].data = response.data.thieuNhi;
                    that.chartdata.datasets[2].data = response.data.quocTe;
                    that.loaded = true;
                })
                .catch(function (error) {
                    console.log(error);
                });
        } catch (e) {
            console.error(e);
        }
    },

    methods: {
        
    }
});