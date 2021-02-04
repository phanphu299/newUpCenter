var vue = new Vue({
    el: '#HocPhiIndex',
    data: {
        title: 'Quản Lý Học Phí',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        dialogEdit: false,
        deleteDialog: false,
        alertMessage: '',
        alertEdit: false,
        rules: [
            v => /^[0-9]*$/.test(v) || "Chỉ được nhập số"
        ],
        mustNumber: v => !isNaN(v) || 'Chỉ được nhập số',
        dialog: false,
        alert: false,
        search: '',
        newItem: {
            gia: '',
            ghiChu: '',
            ngayApDung: new Date().toISOString().substr(0, 10)
        },
        itemToDelete: {},
        headers: [
            {
                text: 'Action',
                align: 'left',
                sortable: false,
                value: ''
            },
            { text: 'Học Phí', value: 'gia', align: 'left', sortable: true },            
            { text: 'Ngày Áp Dụng', value: 'ngayApDung', align: 'left', sortable: true },
            { text: 'Ghi Chú', value: 'ghiChu', align: 'left', sortable: true },
            { text: 'Ngày Tạo', value: 'createdDate', align: 'left', sortable: true },
            { text: 'Người Tạo', value: 'createdBy', align: 'left', sortable: true },
            { text: 'Ngày Sửa', value: 'updatedDate', align: 'left', sortable: true },
            { text: 'Người Sửa', value: 'updatedBy', align: 'left', sortable: true }
        ],
        khoaHocItems: [],
        isShowDatePicker: false,
        isShowDatePicker2: false,
        itemToEdit: {},
        editedIndex: -1
    },

    async beforeCreate() {
        let that = this;
        await axios.get('/category/GetHocPhiAsync')
            .then(function (response) {
                that.khoaHocItems = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });
    },
    methods: {

        formatNumber(val) {
            return val.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
        },

        async onUpdate(item) {
            let that = this;
            if (item.gia === '' || item.hocPhiId === '') {
                this.alertEdit = true;
                this.alertMessage = "Không được bỏ trống";
            }
            else if (isNaN(item.gia)) {
                this.alertMessage = "Chỉ được nhập số";
                this.alertEdit = true;
            }
            else {
                await axios({
                    method: 'put',
                    url: '/category/UpdateHocPhiAsync',
                    data: {
                        Gia: item.gia,
                        HocPhiId: item.hocPhiId,
                        GhiChu: item.ghiChu,
                        NgayApDung: item.ngayApDung
                    }
                })
                .then(function (response) {
                    if (response.data.status === "OK") {
                        console.log(response);
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
            }
        },

        mappingEditItem(item) {
            this.editedIndex = this.khoaHocItems.indexOf(item);
            this.itemToEdit = Object.assign({}, item);

            if (this.itemToEdit.ngayApDung !== "") {
                let [dayKG, monthKG, yearKG] = this.itemToEdit.ngayApDung.split('/');
                this.itemToEdit.ngayApDung = yearKG + '-' + monthKG + '-' + dayKG;
            }
        },
        
        async onSave(item) {
            if (this.newItem.gia === '') {
                this.alertMessage = "Không được bỏ trống";
                this.alert = true;
            }
            else if (isNaN(this.newItem.gia)) {
                this.alertMessage = "Chỉ được nhập số";
                this.alert = true;
            }
            else {
                this.dialog = false;
                let that = this;
                await axios({
                    method: 'post',
                    url: '/category/CreateHocPhiAsync',
                    data: {
                        Gia: that.newItem.gia,
                        GhiChu: that.newItem.ghiChu,
                        NgayApDung: that.newItem.ngayApDung
                    }
                })
                .then(function (response) {
                    console.log(response);
                    if (response.data.status === "OK") {
                        that.khoaHocItems.splice(0, 0, response.data.result);
                        that.snackbar = true;
                        that.messageText = 'Thêm mới thành công !!!';
                        that.color = 'success';
                        that.newItem.gia = 0;
                        that.newItem.ghiChu = '';
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
                url: '/category/DeleteHocPhiAsync',
                data: {
                    HocPhiId: item.hocPhiId
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