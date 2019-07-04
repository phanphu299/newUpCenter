var vue = new Vue({
    el: '#HocVienIndex',
    data: {
        title: 'Quản Lý Học Viên',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        deleteDialog: false,
        dialog: false,
        dialogEdit: false,
        dialogDiemDanh: false,
        dialogNgayHoc: false,
        dialogThemLop: false,
        dialogImport: false,
        alert: false,
        alertEdit: false,
        alertNgayHoc: false,
        isShowDatePicker: false,
        isShowDatePicker2: false,
        isShowDatePickerBatDau: false,
        isShowDatePickerKetThuc: false,
        search: '',
        newItem: {
            fullName: "",
            englishName: "",
            phone: "",
            facebookAccount: "",
            parentFullName: "",
            parentPhone: "",
            quanHe: "",
            parentFacebookAccount: "",
            lopHoc: [],
            isAppend: false,
            ngaySinh: new Date().toISOString().substr(0, 10)
        },
        itemToDelete: {},
        itemToEdit: {},
        itemToDiemDanh: {},
        itemToNgayHoc: {},
        itemToThemLop: {},
        editedIndex: -1,
        headers: [
            {
                text: 'Action',
                align: 'left',
                sortable: false,
                value: ''
            },
            { text: 'Họ Tên', value: 'fullName', align: 'left', sortable: true },
            { text: 'Lớp Học', value: '', align: 'left', sortable: false },
            { text: 'Ngày Sinh', value: 'ngaySinh', align: 'left', sortable: true },
            { text: 'English Name', value: 'englishName', align: 'left', sortable: true },
            { text: 'SĐT', value: 'phone', align: 'left', sortable: true },
            { text: 'Facebook', value: 'facebookAccount', align: 'left', sortable: true },
            { text: 'Họ Tên Phụ Huynh', value: 'parentFullName', align: 'left', sortable: true },
            { text: 'SĐT Phụ Huynh', value: 'parentPhone', align: 'left', sortable: true },
            { text: 'Quan Hệ', value: 'quanHe', align: 'left', sortable: true },
            { text: 'Facebook Phụ Huynh', value: 'parentFacebookAccount', align: 'left', sortable: true },
            { text: 'Chèn', value: 'isAppend', align: 'left', sortable: true },
            { text: 'Ngày Tạo', value: 'createdDate', align: 'left', sortable: true },
            { text: 'Người Tạo', value: 'createdBy', align: 'left', sortable: true },
            { text: 'Ngày Sửa', value: 'updatedDate', align: 'left', sortable: true },
            { text: 'Người Sửa', value: 'updatedBy', align: 'left', sortable: true }
        ],
        headersDiemDanh: [
            { text: 'Ngày Học', value: 'ngayDiemDanh', align: 'left', sortable: false },
            { text: 'Vắng', value: '', align: 'left', sortable: false },
            { text: 'Lớp Được Nghỉ', value: '', align: 'left', sortable: false }
        ],
        khoaHocItems: [],
        diemDanhItems: [],
        ngayHocItem: {
            ngayBatDau: "",
            ngayKetThuc: ""
        },
        itemQuanHe: [],
        itemLopHoc: [],
        itemDiemDanh: [],
        itemThemLop: [],
        itemNgayHoc: [],
        selectedLopHoc: {},
        selectedArrayLopHoc: []
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/HocVien/GetHocVienAsync')
            .then(function (response) {
                that.khoaHocItems = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });

        await axios.get('/category/GetQuanHeAsync')
            .then(function (response) {
                that.itemQuanHe = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });

        await axios.get('/LopHoc/GetAvailableLopHocAsync')
            .then(function (response) {
                that.itemLopHoc = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });
    },

    methods: {
        async onUpdate(item) {
            let that = this;
            if (item.fullName === '' || item.englishName === '' || item.phone === '' ||
                item.facebookAccount === '' || item.ngaySinh === '') {
                this.alertEdit = true;
            }
            else {
                await axios({
                    method: 'put',
                    url: '/HocVien/UpdateHocVienAsync',
                    data: {
                        HocVienId: item.hocVienId,
                        FullName: item.fullName,
                        EnglishName: item.englishName,
                        Phone: item.phone,
                        FacebookAccount: item.facebookAccount,
                        ParentFullName: item.parentFullName,
                        QuanHeId: item.quanHeId,
                        NgaySinh: item.ngaySinh,
                        ParentPhone: item.parentPhone,
                        ParentFacebookAccount: item.parentFacebookAccount,
                        LopHocIds: item.lopHocIds
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

        async onToggleAppend (item) {
            let that = this;
            await axios({
                method: 'put',
                url: '/HocVien/UpdateChenAsync',
                data: {
                    HocVienId: item
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

            let [dayKG, monthKG, yearKG] = this.itemToEdit.ngaySinh.split('/');
            this.itemToEdit.ngaySinh = yearKG + '-' + monthKG + '-' + dayKG;

        },

        mappingDiemDanhItem(item) {
            this.editedIndex = this.khoaHocItems.indexOf(item);
            this.itemToDiemDanh = Object.assign({}, item);   

            let that = this;
            axios.get('/LopHoc/GetLopHocByHocVienIdAsync?HocVienId=' + item.hocVienId)
                .then(function (response) {
                    that.itemDiemDanh = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        mappingThemLopItem(item) {
            this.editedIndex = this.khoaHocItems.indexOf(item);
            this.itemToThemLop = Object.assign({}, item);

            let that = this;
            axios.get('/LopHoc/GetGraduatedAndCanceledLopHocAsync')
                .then(function (response) {
                    that.itemThemLop = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        mappingNgayHocItem(item) {
            this.editedIndex = this.khoaHocItems.indexOf(item);
            this.itemToNgayHoc = Object.assign({}, item);
            this.selectedLopHoc = null;
            this.ngayHocItem.ngayBatDau = "";
            this.ngayHocItem.ngayKetThuc = "";

            let that = this;
            axios.get('/LopHoc/GetLopHocByHocVienIdAsync?HocVienId=' + item.hocVienId)
                .then(function (response) {
                    that.itemNgayHoc = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        async onThemLop(item) {
            this.dialogThemLop = false;
            let that = this;
            await axios({
                method: 'post',
                url: '/HocVien/AddHocVienToLopCuAsync',
                data: {
                    LopHocId: that.selectedArrayLopHoc,
                    HocVienId: item.hocVienId
                }
            })
                .then(function (response) {
                    console.log(response);
                    if (response.data.status === "OK") {
                        that.khoaHocItems.splice(0, 0, response.data.result);
                        that.snackbar = true;
                        that.messageText = 'Thêm mới thành công !!!';
                        that.color = 'success';
                        that.selectedArrayLopHoc = [];
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
        },

        async onThemNgayHoc(item) {
            if (this.ngayHocItem.ngayBatDau !== "" && this.selectedLopHoc !== null) {
                this.dialogNgayHoc = false;
                let that = this;
                await axios({
                    method: 'post',
                    url: '/HocVien/CreateUpdateHocVien_ngayHocAsync',
                    data: {
                        LopHocId: that.selectedLopHoc,
                        HocVienId: item.hocVienId,
                        NgayBatDau: that.ngayHocItem.ngayBatDau,
                        NgayKetThuc: that.ngayHocItem.ngayKetThuc
                    }
                })
                    .then(function (response) {
                        console.log(response);
                        if (response.data.status === "OK") {
                            that.khoaHocItems.splice(0, 0, response.data.result);
                            that.snackbar = true;
                            that.messageText = 'Thêm mới thành công !!!';
                            that.color = 'success';
                            that.selectedArrayLopHoc = [];
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
            else {
                this.alertNgayHoc = true;
            }
        },

        async onSave(item) {
            if (this.newItem.fullName === '' || this.newItem.englishName === '' || this.newItem.phone === '' ||
                this.newItem.facebookAccount === '' || this.newItem.ngaySinh === '') {
                this.alert = true;
            }
            else {
                this.dialog = false;
                let that = this;
                await axios({
                    method: 'post',
                    url: '/HocVien/CreateHocVienAsync',
                    data: {
                        FullName: that.newItem.fullName,
                        EnglishName: that.newItem.englishName,
                        Phone: that.newItem.phone,
                        FacebookAccount: that.newItem.facebookAccount,
                        ParentPhone: that.newItem.parentPhone,
                        NgaySinh: that.newItem.ngaySinh,
                        ParentFullName: that.newItem.parentFullName,
                        ParentFacebookAccount: that.newItem.parentFacebookAccount,
                        QuanHeId: that.newItem.quanHe,
                        IsAppend: that.newItem.isAppend,
                        LopHocIds: that.newItem.lopHoc
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
                url: '/HocVien/DeleteHocVienAsync',
                data: {
                    HocVienId: item.hocVienId
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
        },

        async GetDiemDanhByHocVien() {
            let that = this;
            await axios.get('/DiemDanh/GetDiemDanhByHocVienAndLopHocAsync?HocVienId=' + that.itemToDiemDanh.hocVienId + '&LopHocId=' + that.selectedLopHoc)
                .then(function (response) {
                    that.diemDanhItems = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        forceFileDownload(response, name) {
            const url = window.URL.createObjectURL(new Blob([response.data]));
            const link = document.createElement('a');
            link.href = url;
            link.setAttribute('download', name + '.xlsx'); //or any other extension
            document.body.appendChild(link);
            link.click();
        },

        async onExport() {
            let that = this;
            await axios
                ({
                    url: '/HocVien/Export',
                    method: 'get',
                    responseType: 'blob' // important
                })
                .then(function (response) {
                    that.forceFileDownload(response, 'DanhSachHocVien');
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        async onExportTemplate() {
            let that = this;
            await axios
                ({
                    url: '/HocVien/ExportTemplate?LopHocId=' + that.selectedLopHoc,
                    method: 'get',
                    responseType: 'blob' // important
                })
                .then(function (response) {
                    that.forceFileDownload(response, 'MauImportHocVien');
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        async onImport() {
            let that = this;
            console.log(this.$refs.myFiles.files[0]);
            await axios
                ({
                    url: '/HocVien/Import',
                    method: 'post',
                    responseType: 'blob', // important
                    data: {
                        formFile: that.$refs.myFiles.files[0]
                    },
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    }
                })
                .then(function (response) {
                    console.log(response);
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        async GetNgayHocByHocVien() {
            let that = this;
            await axios.get('/HocVien/GetHocVien_LopHocByHocVienAsync?HocVienId=' + that.itemToNgayHoc.hocVienId + '&LopHocId=' + that.selectedLopHoc)
                .then(function (response) {
                    if (response.data !== null) {
                        that.ngayHocItem = response.data;
                        let [dayKG, monthKG, yearKG] = that.ngayHocItem.ngayBatDau.split('/');
                        that.ngayHocItem.ngayBatDau = yearKG + '-' + monthKG + '-' + dayKG;

                        if (that.ngayHocItem.ngayKetThuc !== "") {
                            let [dayKG2, monthKG2, yearKG2] = that.ngayHocItem.ngayKetThuc.split('/');
                            that.ngayHocItem.ngayKetThuc = yearKG2 + '-' + monthKG2 + '-' + dayKG2;
                        }
                    }
                    else {
                        this.ngayHocItem.ngayBatDau = "";
                        this.ngayHocItem.ngayKetThuc = "";
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
        }
    }
});