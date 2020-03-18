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
        isShowDatePickerNgayHoc: false,
        isShowDatePickerNgayHoc2: false,
        search: '',
        newItem: {
            fullName: "",
            englishName: "",
            phone: "",
            otherPhone: "",
            facebookAccount: "",
            parentFullName: "",
            parentPhone: "",
            quanHe: "",
            lopHoc: '',
            isAppend: false,
            ngaySinh: "",
            ngayHoc: ''
        },
        selectedThang: '',
        selectedNam: '',
        itemThang: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
        itemNam: [new Date().toISOString().substr(0, 4) - 2, new Date().toISOString().substr(0, 4) - 1, new Date().toISOString().substr(0, 4) - 0],
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
            { text: 'SĐT Khác', value: 'otherPhone', align: 'left', sortable: true },
            { text: 'Facebook', value: 'facebookAccount', align: 'left', sortable: true },
            { text: 'Họ Tên Phụ Huynh', value: 'parentFullName', align: 'left', sortable: true },
            { text: 'SĐT Phụ Huynh', value: 'parentPhone', align: 'left', sortable: true },
            { text: 'Quan Hệ', value: 'quanHe', align: 'left', sortable: true },
            { text: 'Ngày Tạo', value: 'createdDate', align: 'left', sortable: true },
            { text: 'Người Tạo', value: 'createdBy', align: 'left', sortable: true },
            { text: 'Ngày Sửa', value: 'updatedDate', align: 'left', sortable: true },
            { text: 'Người Sửa', value: 'updatedBy', align: 'left', sortable: true }
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
        selectedArrayLopHoc: [],
        soNgayHoc: [],
        arrayNgayHocLopHoc: []
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
        async onAddNgayHocLopHoc() {
            if (this.newItem.ngayHoc !== '' && this.newItem.lopHoc !== '') {
                let isExisting = false;
                this.arrayNgayHocLopHoc.map(item => {
                    if (item.lopHoc.lopHocId === this.newItem.lopHoc.lopHocId) {
                        isExisting = true;
                    }
                });

                if (isExisting === false) {
                    this.arrayNgayHocLopHoc.push({
                        lopHoc: this.newItem.lopHoc,
                        ngayHoc: this.newItem.ngayHoc
                    });
                }
            }
            else {
                this.snackbar = true;
                this.messageText = 'Phải chọn cả ngày học và lớp học trước khi thêm !!!';
                this.color = 'error';
            }
        },

        async onXoaNgayHocLopHoc(item) {
            this.arrayNgayHocLopHoc = this.arrayNgayHocLopHoc
                .filter(x => x.lopHoc.lopHocId !== item.lopHoc.lopHocId);
        },

        async onAddNgayHocLopHocForEdit(item) {
            if (this.itemToEdit.ngayHoc !== '' && this.itemToEdit.lopHoc !== '') {
                let isExisting = false;
                item.lopHoc_NgayHocList.map(item => {
                    if (item.lopHoc.lopHocId === this.itemToEdit.lopHoc.lopHocId) {
                        isExisting = true;
                    }
                });

                if (isExisting === false) {
                    item.lopHoc_NgayHocList.push({
                        lopHoc: this.itemToEdit.lopHoc,
                        ngayHoc: this.itemToEdit.ngayHoc
                    });
                }
            }
            else {
                this.snackbar = true;
                this.messageText = 'Phải chọn cả ngày học và lớp học trước khi thêm !!!';
                this.color = 'error';
            }
        },

        async onXoaNgayHocLopHocForEdit(item) {
            this.itemToEdit.lopHoc_NgayHocList = this.itemToEdit.lopHoc_NgayHocList
                .filter(x => x.lopHoc.lopHocId !== item.lopHoc.lopHocId);
        },

        async onUpdate(item) {
            let that = this;
            if (item.fullName === '' || item.phone === '') {
                this.alertEdit = true;
            }
            else if (item.lopHoc_NgayHocList.length === 0) {
                this.snackbar = true;
                this.messageText = 'Phải chọn lớp học và ngày học !!!';
                this.color = 'error';
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
                        OtherPhone: item.otherPhone,
                        FacebookAccount: item.facebookAccount,
                        ParentFullName: item.parentFullName,
                        ParentPhone: item.parentPhone,
                        QuanHeId: item.quanHeId,
                        NgaySinh: item.ngaySinh,
                        LopHocIds: item.lopHocIds,
                        LopHoc_NgayHocList: item.lopHoc_NgayHocList
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

        async onToggleAppend(item) {
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

            if (this.itemToEdit.ngaySinh !== "") {
                let [dayKG, monthKG, yearKG] = this.itemToEdit.ngaySinh.split('/');
                this.itemToEdit.ngaySinh = yearKG + '-' + monthKG + '-' + dayKG;
            }

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
                            that.snackbar = true;
                            that.messageText = 'Thêm mới thành công !!!';
                            that.color = 'success';
                            that.selectedLopHoc = "";
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
            if (this.newItem.fullName === '' || this.newItem.phone === '') {
                this.alert = true;
            }
            else if (this.arrayNgayHocLopHoc.length === 0) {
                this.snackbar = true;
                this.messageText = 'Phải chọn lớp học và ngày học !!!';
                this.color = 'error';
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
                        OtherPhone: that.newItem.otherPhone,
                        FacebookAccount: that.newItem.facebookAccount,
                        NgaySinh: that.newItem.ngaySinh,
                        ParentFullName: that.newItem.parentFullName,
                        ParentPhone: that.newItem.parentPhone,
                        QuanHeId: that.newItem.quanHe,
                        LopHoc_NgayHocList: that.arrayNgayHocLopHoc
                    }
                })
                    .then(function (response) {
                        console.log(response);
                        if (response.data.status === "OK") {
                            that.khoaHocItems.splice(0, 0, response.data.result);
                            that.snackbar = true;
                            that.messageText = 'Thêm mới thành công !!!';
                            that.color = 'success';
                            that.newItem.fullName = '';
                            that.newItem.englishName = '';
                            that.newItem.phone = '';
                            that.newItem.otherPhone = '';
                            that.newItem.facebookAccount = '';
                            that.newItem.ngaySinh = '';

                            that.newItem.parentFullName = '';
                            that.newItem.quanHe = '';
                            that.newItem.lopHoc = [];
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
            if (this.selectedLopHoc !== '' && this.selectedNam !== '' && this.selectedThang !== '') {
                await axios.get('/DiemDanh/GetDiemDanhByHocVienAndLopHocAsync?HocVienId=' + that.itemToDiemDanh.hocVienId + '&LopHocId=' + that.selectedLopHoc + '&month=' + that.selectedThang + '&year=' + that.selectedNam)
                    .then(function (response) {
                        that.diemDanhItems = response.data;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });

                await axios.get('/DiemDanh/GetSoNgayHoc?LopHocId=' + that.selectedLopHoc + '&month=' + that.selectedThang + '&year=' + that.selectedNam)
                    .then(function (response) {
                        that.soNgayHoc = response.data;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            }
        },

        forceFileDownload(response, name) {
            const url = window.URL.createObjectURL(new Blob([response.data]));
            const link = document.createElement('a');
            link.href = url;
            link.setAttribute('download', name + '.xlsx'); //or any other extension
            document.body.appendChild(link);
            link.click();
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
            if (!that.$refs.myFiles.files.length) {
                that.snackbar = true;
                that.messageText = 'Phải chọn file để import !!!';
                that.color = 'error';
                return;
            }

            const fr = new FileReader();
            fr.readAsDataURL(that.$refs.myFiles.files[0]);
            fr.addEventListener('load', () => {
                axios
                    ({
                        url: '/HocVien/Import',
                        method: 'post',
                        data: {
                            File: fr.result,
                            Name: that.$refs.myFiles.files[0].name
                        }
                    })
                    .then(function (response) {
                        if (response.data.status === "OK") {
                            for (let i = 0; i < response.data.result.length; i++) {
                                that.khoaHocItems.splice(0, 0, response.data.result[i]);
                            }

                            that.snackbar = true;
                            that.messageText = response.data.message;
                            that.color = 'success';

                            that.dialogImport = false;
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
                        that.messageText = 'Import lỗi: ' + error;
                        that.color = 'error';
                    });
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