var vue = new Vue({
    el: '#NgayHocIndex',
    data: {
        title: 'Quản Lý Ngày Học',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        deleteDialog: false,
        dialog: false,
        alert: false,
        search: '',
        newItem: '',
        itemToDelete: {},
        headers: [
            {
                text: 'Action',
                align: 'left',
                sortable: false,
                value: ''
            },
            { text: 'Ngày Học', value: 'name', align: 'left', sortable: false },
            { text: 'Ngày Tạo', value: 'createdDate', align: 'left', sortable: false },
            { text: 'Người Tạo', value: 'createdBy', align: 'left', sortable: false },
            { text: 'Ngày Sửa', value: 'updatedDate', align: 'left', sortable: false },
            { text: 'Người Sửa', value: 'updatedBy', align: 'left', sortable: false }
        ],
        khoaHocItems: []
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/category/GetNgayHocAsync')
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
                url: '/category/UpdateNgayHocAsync',
                data: {
                    Name: item.name,
                    NgayHocId: item.ngayHocId
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
                    url: '/category/CreateNgayHocAsync',
                    data: {
                        Name: that.newItem
                    }
                })
                    .then(function (response) {
                        console.log(response);
                        that.khoaHocItems.splice(0, 0, response.data);
                        that.snackbar = true;
                        that.messageText = 'Thêm mới thành công !!!';
                        that.color = 'success';
                        that.newItem = '';
                    })
                    .catch(function (error) {
                        console.log(error);
                        that.snackbar = true;
                        that.messageText = 'Thêm mới lỗi: ' + error;
                        that.color = 'error';
                    });
            }
        },

        async onDelete(item) {
            console.log(item)
            let that = this;
            await axios({
                method: 'delete',
                url: '/category/DeleteNgayHocAsync',
                data: {
                    NgayHocId: item.ngayHocId
                }
            })
                .then(function (response) {
                    console.log(response);
                    that.khoaHocItems.splice(that.khoaHocItems.indexOf(item), 1);
                    that.snackbar = true;
                    that.messageText = 'Xóa thành công !!!';
                    that.color = 'success';
                    that.deleteDialog = false;
                })
                .catch(function (error) {
                    console.log(error);
                    that.snackbar = true;
                    that.messageText = 'Xóa lỗi: ' + error;
                    that.color = 'error';
                });
        }
    }
});