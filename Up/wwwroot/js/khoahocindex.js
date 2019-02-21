var vue = new Vue({
    el: '#KhoaHocIndex',
    data: {
        title: 'Quản Lý Khóa Học',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        dialog: false,
        alert: false,
        search: '',
        newItem: '',
        headers: [
            {
                text: 'Action',
                align: 'left',
                sortable: false,
                value: ''
            },
            { text: 'Tên Khóa Học', value: 'name', align: 'left', sortable: false },
            { text: 'Ngày Tạo', value: 'createdDate', align: 'left', sortable: false },
            { text: 'Người Tạo', value: 'createdBy', align: 'left', sortable: false },
            { text: 'Ngày Sửa', value: 'updatedDate', align: 'left', sortable: false },
            { text: 'Người Sửa', value: 'updatedBy', align: 'left', sortable: false }
        ],
        khoaHocItems: []
    },
    created() {
        let that = this;
        axios.get('/category/GetKhoaHocAsync')
            .then(function (response) {
                that.khoaHocItems = response.data
            })
            .catch(function (error) {
                console.log(error);
            });
    },
    methods: {
        onUpdate(item) {
            console.log(item);
            this.snackbar = true;
            this.messageText = 'test';
            this.color = 'success';
        },

        onSave(item) {
            if (this.newItem == '') {
                this.alert = true;
            }
            else {
                console.log(this.newItem)
                this.dialog = false;
            }
        }
    }
});