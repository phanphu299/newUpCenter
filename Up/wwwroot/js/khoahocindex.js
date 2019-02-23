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
    async beforeCreate() {
        let that = this;
        await axios.get('/category/GetKhoaHocAsync')
            .then(function (response) {
                that.khoaHocItems = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });
    },
    methods: {
        async onUpdate(item) {
            let that = this;
            await axios({
                    method: 'put',
                    url: '/category/UpdateKhoaHocAsync',
                    data: {
                        Name: item.name,
                        KhoaHocId: item.khoaHocId
                    }
                })
                .then(function (response) {
                    console.log(response);
                    that.snackbar = true;
                    that.messageText = 'Cập nhật thành công !!!';
                    that.color = 'success';
                })
                .catch(function (error) {
                    console.log(error);
                    that.snackbar = true;
                    that.messageText = 'Cập nhật lỗi: ' + error;
                    that.color = 'error';
                });
        },

        async onSave(item) {
            if (this.newItem === '') {
                this.alert = true;
            }
            else {
                this.dialog = false;
                let that = this;
                await axios({
                        method: 'post',
                        url: '/category/CreateKhoaHocAsync',
                        data: {
                            Name: this.newItem
                        }
                    })
                    .then(function (response) {
                        that.khoaHocItems.splice(0, 0, response.data);
                        that.snackbar = true;
                        that.messageText = 'Thêm mới thành công !!!';
                        that.color = 'success';
                    })
                    .catch(function (error) {
                        console.log(error);
                        that.snackbar = true;
                        that.messageText = 'Thêm mới lỗi: ' + error;
                        that.color = 'error';
                    });
            }
        }
    }
});