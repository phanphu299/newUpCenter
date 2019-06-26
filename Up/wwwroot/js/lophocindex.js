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
            giaoVien: "",
            sach: [],
            ngayKhaiGiang: new Date().toISOString().substr(0, 10)
        },
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
            { text: 'Sách', value: '', align: 'left', sortable: true },
            { text: 'Giáo Viên Chủ Nhiệm', value: 'giaoVien', align: 'left', sortable: true },
            { text: 'Khóa Học', value: 'khoaHoc', align: 'left', sortable: true },
            { text: 'Ngày Học', value: 'ngayHoc', align: 'left', sortable: true },
            { text: 'Giờ Học', value: 'gioHoc', align: 'left', sortable: true },
            { text: 'Học Phí', value: 'hocPhi', align: 'left', sortable: true },
            { text: 'Hủy Lớp', value: 'isCanceled', align: 'left', sortable: true },
            { text: 'Tốt Nghiệp', value: 'isGraduated', align: 'left', sortable: true },
            { text: 'Ngày Khai Giảng', value: 'ngayKhaiGiang', align: 'left', sortable: true },
            { text: 'Ngày Kết Thúc', value: 'ngayKetThuc', align: 'left', sortable: true },
            { text: 'Ngày Tạo', value: 'createdDate', align: 'left', sortable: true },
            { text: 'Người Tạo', value: 'createdBy', align: 'left', sortable: true },
            { text: 'Ngày Sửa', value: 'updatedDate', align: 'left', sortable: true },
            { text: 'Người Sửa', value: 'updatedBy', align: 'left', sortable: true }
        ],
        headersDiemDanh: [
            { text: 'Học Viên', value: 'hocVien', align: 'left', sortable: false },
            { text: 'Ngày Học', value: 'ngayDiemDanh', align: 'left', sortable: false },
            { text: 'Vắng', value: '', align: 'left', sortable: false },
            { text: 'Lớp Được Nghỉ', value: '', align: 'left', sortable: false }
        ],
        khoaHocItems: [],
        itemKhoaHoc: [],
        itemGioHoc: [],
        itemNgayHoc: [],
        itemHocPhi: [],
        itemSach: [],
        itemGiaoVien: [],
        diemDanhItems: []
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/LopHoc/GetLopHocAsync')
            .then(function (response) {
                that.khoaHocItems = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });

        await axios.get('/category/GetKhoaHocAsync')
            .then(function (response) {
                that.itemKhoaHoc = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });

        await axios.get('/category/GetNgayHocAsync')
            .then(function (response) {
                that.itemNgayHoc = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });

        await axios.get('/category/GetGioHocAsync')
            .then(function (response) {
                that.itemGioHoc = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });

        await axios.get('/category/GetHocPhiAsync')
            .then(function (response) {
                that.itemHocPhi = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });

        await axios.get('/category/GetSachAsync')
            .then(function (response) {
                that.itemSach = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });

        await axios.get('/GiaoVien/GetGiaoVienAsync')
            .then(function (response) {
                that.itemGiaoVien = response.data;
            })
            .catch(function (error) {
                console.log(error);
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
                        HocPhiId: item.hocPhiId,
                        GiaoVienId: item.giaoVienId,
                        NgayKhaiGiang: item.ngayKhaiGiang,
                        NgayKetThuc: item.ngayKetThuc,
                        IsCanceled: item.isCanceled,
                        IsGraduated: item.isGraduated,
                        SachIds: item.sachIds
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
                        console.log(error);
                        that.snackbar = true;
                        that.messageText = 'Cập nhật lỗi: ' + error;
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
                    console.log(error);
                    that.snackbar = true;
                    that.messageText = 'Cập nhật lỗi: ' + error;
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
                    console.log(error);
                    that.snackbar = true;
                    that.messageText = 'Cập nhật lỗi: ' + error;
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

            let that = this;
            axios.get('/DiemDanh/GetDiemDanhByLopHocAsync?LopHocId=' + item.lopHocId)
                .then(function (response) {
                    that.diemDanhItems = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
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
                        HocPhiId: that.newItem.hocPhi,
                        NgayKhaiGiang: that.newItem.ngayKhaiGiang,
                        SachIds: that.newItem.sach,
                        GiaoVienId: that.newItem.giaoVien
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
                            that.newItem.giaoVien = "";
                            that.newItem.sach = [];
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
                    console.log(error);
                    that.snackbar = true;
                    that.messageText = 'Xóa lỗi: ' + error;
                    that.color = 'error';
                });
        }
    }
});