var vue = new Vue({
    el: '#LopHocIndex',
    data: {
        title: 'Quản Lý Lớp Học',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        deleteDialog: false,
        dialog: false,
        dialogEdit: false,
        dialogDiemDanh: false,
        alert: false,
        alertEdit: false,
        isShowDatePicker: false,
        isShowDatePicker2: false,
        isShowDatePicker3: false,
        search: '',
        searchDiemDanh: '',
        newItem: {
            name: "",
            khoaHoc: "",
            ngayHoc: "",
            gioHocFrom: "",
            gioHocTo: "",
            ngayKhaiGiang: new Date().toISOString().substr(0, 10)
        },
        selectedThang: '',
        selectedNam: '',
        itemThang: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
        itemNam: [new Date().toISOString().substr(0, 4) - 2, new Date().toISOString().substr(0, 4) - 1, new Date().toISOString().substr(0, 4) - 0],
        itemToDelete: {},
        itemToDiemDanh: {},
        itemToEdit: {},
        editedIndex: -1,
        headers: [
            {
                text: 'Action',
                align: 'left',
                sortable: false,
                value: ''
            },
            { text: 'Tên Lớp Học', value: 'name', align: 'left', sortable: true },
            { text: 'Khóa Học', value: 'khoaHoc', align: 'left', sortable: true },
            { text: 'Ngày Học', value: 'ngayHoc', align: 'left', sortable: true },
            { text: 'Giờ Học', value: 'gioHoc', align: 'left', sortable: true },
            { text: 'Hủy Lớp', value: 'isCanceled', align: 'left', sortable: true },
            { text: 'Tốt Nghiệp', value: 'isGraduated', align: 'left', sortable: true },
            { text: 'Ngày Khai Giảng', value: 'ngayKhaiGiang', align: 'left', sortable: true },
            { text: 'Ngày Kết Thúc', value: 'ngayKetThuc', align: 'left', sortable: true },
            { text: 'Ngày Tạo', value: 'createdDate', align: 'left', sortable: true },
            { text: 'Người Tạo', value: 'createdBy', align: 'left', sortable: true },
            { text: 'Ngày Sửa', value: 'updatedDate', align: 'left', sortable: true },
            { text: 'Người Sửa', value: 'updatedBy', align: 'left', sortable: true }
        ],
        khoaHocItems: [],
        itemKhoaHoc: [],
        itemGioHoc: [],
        itemNgayHoc: [],
        diemDanhItems: [],
        soNgayHoc:[]
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/LopHoc/GetLopHocAsync')
            .then(function (response) {
                that.khoaHocItems = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });

        await axios.get('/category/GetKhoaHocAsync')
            .then(function (response) {
                that.itemKhoaHoc = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });

        await axios.get('/category/GetNgayHocAsync')
            .then(function (response) {
                that.itemNgayHoc = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });

        await axios.get('/category/GetGioHocAsync')
            .then(function (response) {
                that.itemGioHoc = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });
        
    },

    methods: {
        DisplayGioHoc: function (value) {
            return value.from + " - " + value.to;
        },

        async onUpdate(item) {
            let that = this;
            if (item.name === '' || item.khoaHocId === '' || item.ngayHocId === '' || item.gioHocId === '' || item.giaoVienId === '') {
                this.alertEdit = true;
            }
            else {
                await axios({
                    method: 'put',
                    url: '/LopHoc/UpdateLopHocAsync',
                    data: {
                        Name: item.name,
                        LopHocId: item.lopHocId,
                        KhoaHocId: item.khoaHocId,
                        GioHocId: item.gioHocId,
                        NgayHocId: item.ngayHocId,
                        NgayKhaiGiang: item.ngayKhaiGiang,
                        NgayKetThuc: item.ngayKetThuc,
                        IsCanceled: item.isCanceled,
                        IsGraduated: item.isGraduated
                    }
                })
                    .then(function (response) {
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
            }
        },

        async onToggleCanceled(item) {
            let that = this;
            console.log(item);

            await axios({
                method: 'put',
                url: '/LopHoc/UpdateHuyLopAsync',
                data: {
                    LopHocId: item
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
                    console.log(error.response.data.Message);
                    that.snackbar = true;
                    that.messageText = 'Cập nhật lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                });
        },

        async onToggleGraduated(item) {
            let that = this;
            console.log(item);

            await axios({
                method: 'put',
                url: '/LopHoc/UpdateTotNghiepAsync',
                data: {
                    LopHocId: item
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
                    console.log(error.response.data.Message);
                    that.snackbar = true;
                    that.messageText = 'Cập nhật lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                });
        },

        mappingEditItem(item) {
            this.editedIndex = this.khoaHocItems.indexOf(item);
            this.itemToEdit = Object.assign({}, item);

            let [dayKG, monthKG, yearKG] = this.itemToEdit.ngayKhaiGiang.split('/');
            this.itemToEdit.ngayKhaiGiang = yearKG + '-' + monthKG + '-' + dayKG;


            if (this.itemToEdit.ngayKetThuc !== "") {
                let [dayKT, monthKT, yearKT] = this.itemToEdit.ngayKetThuc.split('/');
                this.itemToEdit.ngayKetThuc = yearKT + '-' + monthKT + '-' + dayKT;
            }
        },

        mappingDiemDanhItem(item) {
            this.editedIndex = this.khoaHocItems.indexOf(item);
            this.itemToDiemDanh = Object.assign({}, item);
        },

        async onTinhDiemDanh() {
            if (this.selectedNam !== '' && this.selectedThang !== '') {
                let that = this;
                axios.get('/DiemDanh/GetDiemDanhByLopHocAsync?LopHocId=' + that.itemToDiemDanh.lopHocId + '&month=' + that.selectedThang + '&year=' + that.selectedNam)
                    .then(function (response) {
                        that.diemDanhItems = response.data;
                    })
                    .catch(function (error) {
                        console.log(error.response.data.Message);
                    });

                await axios.get('/DiemDanh/GetSoNgayHoc?LopHocId=' + that.itemToDiemDanh.lopHocId + '&month=' + that.selectedThang + '&year=' + that.selectedNam)
                    .then(function (response) {
                        that.soNgayHoc = response.data;
                    })
                    .catch(function (error) {
                        console.log(error.response.data.Message);
                    });
            }
        },

        async onSave(item) {
            if (this.newItem.name === '' || this.newItem.khoaHoc === '' || this.newItem.ngayHoc === '' || this.newItem.gioHoc === '' || this.newItem.giaoVien === '') {
                this.alert = true;
            }
            else {
                this.dialog = false;
                let that = this;
                await axios({
                    method: 'post',
                    url: '/LopHoc/CreateLopHocAsync',
                    data: {
                        Name: that.newItem.name,
                        KhoaHocId: that.newItem.khoaHoc,
                        GioHocId: that.newItem.gioHoc,
                        NgayHocId: that.newItem.ngayHoc,
                        NgayKhaiGiang: that.newItem.ngayKhaiGiang
                    }
                })
                    .then(function (response) {
                        console.log(response);
                        if (response.data.status === "OK") {
                            that.khoaHocItems.splice(0, 0, response.data.result);
                            that.snackbar = true;
                            that.messageText = 'Thêm mới thành công !!!';
                            that.color = 'success';
                            that.newItem.name = '';
                            that.newItem.khoaHoc = '';
                            that.newItem.gioHoc = '';
                            that.newItem.ngayHoc = '';
                            that.newItem.ngayKhaiGiang = new Date().toISOString().substr(0, 10);
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

        formatNumber(val) {
            return val.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
        },

        async onDelete(item) {
            let that = this;
            await axios({
                method: 'delete',
                url: '/LopHoc/DeleteLopHocAsync',
                data: {
                    LopHocId: item.lopHocId
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
                    console.log(error.response.data.Message);
                    that.snackbar = true;
                    that.messageText = 'Xóa lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                });
        }
    }
});