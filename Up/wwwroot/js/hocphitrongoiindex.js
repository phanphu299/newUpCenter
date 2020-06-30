var vue = new Vue({
    el: '#HocPhiTronGoiIndex',
    data: {
        title: 'Quản Lý Học Phí Trọn Gói',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        deleteDialog: false,
        isShowDatePicker: false,
        isShowDatePicker2: false,
        isShowDatePicker3: false,
        isShowDatePicker4: false,
        isShowDatePickerLopHoc: false,
        isShowDatePickerLopHoc2: false,
        dialog: false,
        alert: false,
        alertEdit: false,
        loadingHocVien: false,
        itemsHocVien: [],
        searchHocVien: null,
        rules: [
            v => /^[0-9]*$/.test(v) || "Chỉ được nhập số"
        ],
        mustNumber: v => !isNaN(v) || 'Chỉ được nhập số',
        alertMessage: '',
        search: '',
        newItem: {
            hocVien: [],
            hocPhi: 0,
            fromDate: new Date().toISOString().substr(0, 10),
            toDate: new Date().toISOString().substr(0, 10),
            lopHoc: {},
            fromDateLopHoc: new Date().toISOString().substr(0, 10),
            toDateLopHoc: new Date().toISOString().substr(0, 10),
        },
        itemLopHoc: [],
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
            { text: 'Tên', value: 'fullName', align: 'left', sortable: false },
            { text: 'Học Phí', value: 'hocPhi', align: 'left', sortable: false },
            { text: 'Hết Hiệu Lực?', value: 'isDisabled', align: 'left', sortable: false },
            { text: 'Từ Ngày', value: 'fromDate', align: 'left', sortable: false },
            { text: 'Đến Ngày', value: 'toDate', align: 'left', sortable: false },
            { text: 'Ngày Tạo', value: 'createdDate', align: 'left', sortable: false },
            { text: 'Người Tạo', value: 'createdBy', align: 'left', sortable: false },
            { text: 'Ngày Sửa', value: 'updatedDate', align: 'left', sortable: false },
            { text: 'Người Sửa', value: 'updatedBy', align: 'left', sortable: false }
        ],
        khoaHocItems: [],
        itemToEdit: {},
        arrayLopHoc: []
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/hocphi/GetHocPhiTronGoiAsync')
            .then(function (response) {
                that.khoaHocItems = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });
    },

    watch: {
        searchHocVien(val) {
            val && val !== this.newItem.hocVien && this.querySelections(val)
        }
    },

    methods: {
        remove(item) {

            const index = this.newItem.hocVien.indexOf(item)
            if (index >= 0) this.newItem.hocVien.splice(index, 1)
        },

        async querySelections(v) {
            this.loadingHocVien = true;

            let that = this;
            await axios.get('/hocvien/GetHocVienByNameAsync?name=' + v)
                .then(function (response) {
                    that.itemsHocVien = response.data;
                    that.loadingHocVien = false;
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        formatNumber(val) {
            return val.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
        },

        mappingEditItem(item) {
            this.itemToEdit = Object.assign({}, item);
            this.editedIndex = this.khoaHocItems.indexOf(item);

            if (this.itemToEdit.fromDate !== "") {
                let [dayKG, monthKG, yearKG] = this.itemToEdit.fromDate.split('/');
                this.itemToEdit.fromDate = yearKG + '-' + monthKG + '-' + dayKG;
            }

            if (this.itemToEdit.toDate !== "") {
                let [dayKG, monthKG, yearKG] = this.itemToEdit.toDate.split('/');
                this.itemToEdit.toDate = yearKG + '-' + monthKG + '-' + dayKG;
            }
        },

        async onUpdate(item) {
            let that = this;

            await axios({
                method: 'put',
                url: '/hocphi/UpdateHocPhiTronGoiAsync',
                data: {
                    HocPhi: item.hocPhi,
                    FromDate: item.fromDate,
                    ToDate: item.toDate,
                    HocPhiTronGoiId: item.hocPhiTronGoiId
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
            if (this.newItem.hocVien.length === 0) {
                this.alertMessage = "Phải chọn ít nhất 1 học viên";
                this.alert = true;
            }
            else if (isNaN(this.newItem.hocPhi) || this.newItem.hocPhi === '') {
                this.alertMessage = "Chỉ được nhập số";
                this.alert = true;
            }
            else {
                this.dialog = false;
                let that = this;
                await axios({
                    method: 'post',
                    url: '/hocphi/CreateHocPhiTronGoiAsync',
                    data: {
                        HocVienIds: that.newItem.hocVien.map(item => item.hocVienId),
                        HocPhi: that.newItem.hocPhi,
                        FromDate: that.newItem.fromDate,
                        ToDate: that.newItem.toDate
                    }
                })
                    .then(function (response) {
                        that.alert = false;
                        if (response.data.status === "OK") {
                            axios.get('/hocphi/GetHocPhiTronGoiAsync')
                                .then(function (response) {
                                    that.khoaHocItems = response.data;
                                })
                                .catch(function (error) {
                                    console.log(error);
                                });

                            that.snackbar = true;
                            that.messageText = 'Thêm mới thành công !!!';
                            that.color = 'success';
                            that.newItem.hocVien = [];
                            that.newItem.hocPhi = 0;
                            that.newItem.fromDate = new Date().toISOString().substr(0, 10);
                            that.newItem.toDate = new Date().toISOString().substr(0, 10);
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

        async onToggle(item) {
            let that = this;
            await axios({
                method: 'put',
                url: '/hocphi/ToggleHocPhiTronGoiAsync',
                data: {
                    HocPhiTronGoiId: item
                }
            })
                .then(function (response) {
                    if (response.data.status === "OK") {
                        that.snackbar = true;
                        that.messageText = 'Cập nhật thành công !!!';
                        that.color = 'success';
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
        },

        async onDelete(item) {
            let that = this;
            await axios({
                method: 'put',
                url: '/hocphi/DeleyeHocPhiTronGoiAsync',
                data: {
                    HocPhiTronGoiId: item.HocPhiTronGoiId
                }
            })
                .then(function (response) {
                    if (response.data.status === "OK") {
                        that.khoaHocItems.splice(that.khoaHocItems.indexOf(item), 1);
                        that.snackbar = true;
                        that.messageText = 'Xóa thành công !!!';
                        that.color = 'success';
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