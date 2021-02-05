var vue = new Vue({
    el: '#GioHocIndex',
    data: {
        title: 'Quản Lý Giờ Học',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        dialog: false,
        deleteDialog: false,
        dialogEdit: false,
        alert: false,
        alertEdit: false,
        search: '',
        newItem: {
            from: '',
            to: ''
        },
        itemToDelete: {},
        itemToEdit: {},
        editedIndex: -1,
        headers: [
            {
                text: 'Action',
                align: 'left',
                sortable: false,
                value: ''
            },
            { text: 'Giờ Bắt Đầu', value: 'from', align: 'left', sortable: true },
            { text: 'Giờ Kết Thúc', value: 'to', align: 'left', sortable: true },
            { text: 'Ngày Tạo', value: 'createdDate', align: 'left', sortable: true },
            { text: 'Người Tạo', value: 'createdBy', align: 'left', sortable: true },
            { text: 'Ngày Sửa', value: 'updatedDate', align: 'left', sortable: true },
            { text: 'Người Sửa', value: 'updatedBy', align: 'left', sortable: true }
        ],
        khoaHocItems: []
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/category/GetGioHocAsync')
            .then(function (response) {
                that.khoaHocItems = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });
    },
    methods: {
        async onUpdate(item) {
            let that = this;
            await axios({
                method: 'put',
                url: '/category/UpdateGioHocAsync',
                data: {
                    From: item.from,
                    To: item.to,
                    GioHocId: item.gioHocId
                }
            })
            .then(function (response) {
                console.log(response);
                if (response.data.status === "OK") {
                    Object.assign(that.khoaHocItems[that.editedIndex], response.data.result);
                    that.snackbar = true;
                    that.messageText = 'Cập nhật thành công !!!';
                    that.color = 'success';
                    that.dialogEdit = false;
                }
                else {
                    that.snackbar = true;
                    that.messageText = response.data.message;
                    that.color = 'error';
                    that.dialogEdit = false;
                }
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
                that.snackbar = true;
                that.messageText = 'Cập nhật lỗi: ' + error.response.data.Message;
                that.color = 'error';
                that.dialogEdit = false;
            });
        },

        mappingEditItem(item) {
            this.editedIndex = this.khoaHocItems.indexOf(item);
            this.itemToEdit = Object.assign({}, item);
        },

        async onSave(item) {
            if (this.newItem.from === '' || this.newItem.to === '') {
                this.alert = true;
            }
            else {
                this.dialog = false;
                let that = this;
                await axios({
                    method: 'post',
                    url: '/category/CreateGioHocAsync',
                    data: {
                        From: that.newItem.from,
                        To: that.newItem.to
                    }
                })
                .then(function (response) {
                    console.log(response);
                    if (response.data.status === "OK") {
                        that.khoaHocItems.splice(0, 0, response.data.result);
                        that.snackbar = true;
                        that.messageText = 'Thêm mới thành công !!!';
                        that.color = 'success';
                        that.newItem.from = '';
                        that.newItem.to = '';
                    }
                    else {
                        that.snackbar = true;
                        that.messageText = response.data.message;
                        that.color = 'error';
                    }
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                    that.snackbar = true;
                    that.messageText = 'Thêm mới lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                });
            }
        },

        async onDelete(item) {
            let that = this;
            await axios({
                method: 'delete',
                url: '/category/DeleteGioHocAsync',
                data: {
                    GioHocId: item.gioHocId
                }
            })
            .then(function (response) {
                console.log(response);
                if (response.data.status === "OK") {
                    that.khoaHocItems.splice(that.khoaHocItems.indexOf(item), 1);
                    that.snackbar = true;
                    that.messageText = 'Xóa thành công !!!';
                    that.color = 'success';
                    that.deleteDialog = false;
                }
                else {
                    that.snackbar = true;
                    that.messageText = response.data.message;
                    that.color = 'error';
                }
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
                that.snackbar = true;
                that.messageText = 'Xóa lỗi: ' + error.response.data.Message;
                that.color = 'error';
            });
        }
    }
});