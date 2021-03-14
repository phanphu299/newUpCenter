var vue = new Vue({
    el: '#QuyenIndex',
    data: {
        title: 'Quản Lý Quyền',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        search: '',
        headers: [
            {
                text: 'Action',
                align: 'left',
                sortable: false,
                value: ''
            },
            { text: 'Quyền', value: 'name', align: 'left', sortable: true },
        ],
        quyenItems: [],
        dialogEditRole: false,
        itemToEdit: {},
        editedIndex: -1,
        itemRoles: [],
        dialog: false,
        alert: false,
        rules: [
            v => /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(v) || "Chỉ được nhập email"
        ]
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/Setting/GetQuyenAsync')
            .then(function (response) {
                that.quyenItems = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });
    },
    methods: {
        async mappingRoleItem(item) {
            let that = this;
            this.editedIndex = this.quyenItems.indexOf(item);
            this.itemToEdit = Object.assign({}, item);

            await axios.get('/Setting/GetRoleByQuyenIdAsync?QuyenId=' + item.quyenId)
                .then(function (response) {
                    that.itemRoles = response.data;
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });
        },
    }
});