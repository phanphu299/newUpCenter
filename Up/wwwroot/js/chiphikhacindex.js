var vue = new Vue({
    el: '#ChiPhiKhacIndex',
    data: {
        title: 'Quản Lý Chi Phí Khác',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        deleteDialog: false,
        isShowDatePicker: false,
        isShowDatePicker2: false,
        dialog: false,
        alert: false,
        alertEdit: false,
        rules: [
            v => /^[0-9]*$/.test(v) || "Chỉ được nhập số"
        ],
        mustNumber: v => !isNaN(v) || 'Chỉ được nhập số',
        alertMessage: '',
        search: '',
        newItem: {
            name: "",
            gia: "",
            ngayChiPhi: new Date().toISOString().substr(0, 10)
        },
        itemToDelete: {},
        dialogEdit: false,
        editedIndex: -1,
        headers: [
            {
                text: 'Action',
                align: 'left',
                sortable: false,
                value: ''
            },
            { text: 'Tên', value: 'name', align: 'left', sortable: false },
            { text: 'Giá', value: 'gia', align: 'left', sortable: false },
            { text: 'Ngày Chi Phí', value: '_NgayChiPhi', align: 'left', sortable: false },
            { text: 'Ngày Tạo', value: 'createdDate', align: 'left', sortable: false },
            { text: 'Người Tạo', value: 'createdBy', align: 'left', sortable: false },
            { text: 'Ngày Sửa', value: 'updatedDate', align: 'left', sortable: false },
            { text: 'Người Sửa', value: 'updatedBy', align: 'left', sortable: false }
        ],
        khoaHocItems: [],
        itemToEdit: {}
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/category/GetOtherExpenseAsync')
            .then(function (response) {
                that.khoaHocItems = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });

        console.log(that.khoaHocItems)
    },
    methods: {
        formatNumber(val) {
            return val.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
        },

        mappingEditItem(item) {
            this.itemToEdit = Object.assign({}, item);
            this.editedIndex = this.khoaHocItems.indexOf(item);

            if (this.itemToEdit._NgayChiPhi !== "") {
                let [dayKG, monthKG, yearKG] = this.itemToEdit._NgayChiPhi.split('/');
                this.itemToEdit.ngayChiPhi = yearKG + '-' + monthKG + '-' + dayKG;
            }
        },

        async onUpdate(item) {
            let that = this;
            
            await axios({
                method: 'put',
                url: '/category/UpdateOtherExpenseAsync',
                data: {
                    Name: item.name,
                    Gia: item.gia,
                    NgayChiPhi: item.ngayChiPhi,
                    ChiPhiKhacId: item.chiPhiKhacId
                }
            })
                .then(function (response) {
                    if (response.data.status === "OK") {
                        that.snackbar = true;
                        that.messageText = 'Cập nhật thành công !!!';
                        that.color = 'success';
                        Object.assign(that.khoaHocItems[that.editedIndex], response.data.result);
                    }
                    else {
                        that.snackbar = true;
                        that.messageText = response.data.message;
                        that.color = 'error';
                    }
                })
                .catch(function (error) {
                    console.log(error);
                    that.snackbar = true;
                    that.messageText = 'Cập nhật lỗi: ' + error;
                    that.color = 'error';
                });
            that.dialogEdit = false;
        },

        async onSave(item) {
            if (this.newItem.name === '') {
                this.alertMessage = "Không được bỏ trống";
                this.alert = true;
            }
            else if (isNaN(this.newItem.gia) || this.newItem.gia === '') {
                this.alertMessage = "Chỉ được nhập số";
                this.alert = true;
            }
            else {
                this.dialog = false;
                let that = this;
                await axios({
                    method: 'post',
                    url: '/category/CreateOtherExpenseAsync',
                    data: {
                        Name: that.newItem.name,
                        Gia: that.newItem.gia,
                        NgayChiPhi: that.newItem.ngayChiPhi
                    }
                })
                    .then(function (response) {
                        this.alert = false;
                        if (response.data.status === "OK") {
                            that.khoaHocItems.splice(0, 0, response.data.result);
                            that.snackbar = true;
                            that.messageText = 'Thêm mới thành công !!!';
                            that.color = 'success';
                            that.newItem.name = '';
                            that.newItem.gia = "";
                            that.newItem.ngayChiPhi = new Date().toISOString().substr(0, 10);
                        }
                        else {
                            that.snackbar = true;
                            that.messageText = response.data.message;
                            that.color = 'error';
                        }
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
            let that = this;
            await axios({
                method: 'delete',
                url: '/category/DeleteOtherExpenseAsync',
                data: {
                    ChiPhiKhacId: item.chiPhiKhacId
                }
            })
                .then(function (response) {
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
                    console.log(error);
                    that.snackbar = true;
                    that.messageText = 'Xóa lỗi: ' + error;
                    that.color = 'error';
                });
        }
    }
});