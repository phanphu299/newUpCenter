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
        isShowDatePickerLopHoc3: false,
        isShowDatePickerLopHoc4: false,
        dialog: false,
        alert: false,
        alertEdit: false,
        loadingHocVien: false,
        itemsHocVien: [],
        itemLopHoc: [],
        searchHocVien: null,
        rules: [
            v => /^[0-9]*$/.test(v) || "Chỉ được nhập số"
        ],
        mustNumber: v => !isNaN(v) || 'Chỉ được nhập số',
        alertMessage: '',
        search: '',
        newItem: {
            hocVien: {},
            hocPhi: 0,
            fromDate: new Date().toISOString().substr(0, 10),
            toDate: new Date().toISOString().substr(0, 10),
            lopHoc: {},
            fromDateLopHoc: new Date().toISOString().substr(0, 10),
            toDateLopHoc: new Date().toISOString().substr(0, 10),
            ghiChu: ""
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
            { text: 'Tên', value: 'name', align: 'left', sortable: false },
            { text: 'Học Phí', value: 'hocPhi', align: 'left', sortable: false },
            { text: 'Hết Hiệu Lực?', value: 'isDisabled', align: 'left', sortable: false },
            { text: 'Ghi Chú', value: 'ghiChu', align: 'left', sortable: false },
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
                console.log(error.response.data.Message);
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

        async onAddLopHoc() {
            if (this.newItem.lopHoc !== '') {
                let isExisting = false;
                this.arrayLopHoc.map(item => {
                    if (item.lopHoc.lopHocId === this.newItem.lopHoc.lopHocId) {
                        isExisting = true;
                    }
                });

                if (isExisting === false) {
                    this.arrayLopHoc.push({
                        lopHoc: this.newItem.lopHoc,
                        fromDate: this.newItem.fromDateLopHoc,
                        toDate: this.newItem.toDateLopHoc
                    });
                }
            }
            else {
                this.snackbar = true;
                this.messageText = 'Phải chọn lớp học trước khi thêm !!!';
                this.color = 'error';
            }
        },

        async onAddLopHocEdit() {
            if (this.itemToEdit.lopHoc !== undefined) {
                let isExisting = false;
                this.itemToEdit.lopHocList.map(item => {
                    if (item.lopHoc.lopHocId === this.itemToEdit.lopHoc.lopHocId) {
                        isExisting = true;
                    }
                });

                if (isExisting === false) {
                    this.itemToEdit.lopHocList.push({
                        lopHoc: this.itemToEdit.lopHoc,
                        fromDate: this.itemToEdit.fromDateLopHoc,
                        toDate: this.itemToEdit.toDateLopHoc
                    });
                }
            }
            else {
                this.snackbar = true;
                this.messageText = 'Phải chọn lớp học trước khi thêm !!!';
                this.color = 'error';
            }
        },

        async onXoaLopHoc(item) {
            this.arrayLopHoc = this.arrayLopHoc
                .filter(x => x.lopHoc.lopHocId !== item.lopHoc.lopHocId);
        },

        async onXoaLopHocEdit(item) {
            this.itemToEdit.lopHocList = this.itemToEdit.lopHocList
                .filter(x => x.lopHoc.lopHocId !== item.lopHoc.lopHocId);
        },

        async onChangeHocVien() {
            let that = this;
            that.itemLopHoc = [];
            that.arrayLopHoc = [];
            await axios.get('/LopHoc/GetLopHocByHocVienIdAsync?HocVienId=' + this.newItem.hocVien.hocVienId)
                .then(function (response) {
                    that.itemLopHoc = response.data;
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });
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
                    console.log(error.response.data.Message);
                });
        },

        formatNumber(val) {
            return val.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
        },

        async mappingEditItem(item) {
            let that = this;
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

            this.itemToEdit.fromDateLopHoc = new Date().toISOString().substr(0, 10);

            this.itemToEdit.toDateLopHoc = new Date().toISOString().substr(0, 10);

            await axios.get('/LopHoc/GetLopHocByHocVienIdAsync?HocVienId=' + this.itemToEdit.hocVienId)
                .then(function (response) {
                    that.itemLopHoc = response.data;
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });
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
                    HocPhiTronGoiId: item.hocPhiTronGoiId,
                    LopHocList: item.lopHocList,
                    GhiChu: item.ghiChu
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
                    console.log(error.response.data.Message);
                    that.snackbar = true;
                    that.messageText = 'Cập nhật lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                });
            that.dialogEdit = false;
        },

        forceFileDownloadPdf(response, name) {
            const url = window.URL.createObjectURL(new Blob([response.data]));
            const link = document.createElement('a');
            link.href = url;
            link.setAttribute('download', 'bienlaitrongoi_' + name + '.pdf'); //or any other extension
            document.body.appendChild(link);
            link.click();
        },

        async onExportBienLai(item) {
            let that = this;
            await axios
                ({
                    url: '/HocPhi/ExportBienLaiTronGoi',
                    method: 'put',
                    responseType: 'blob', // important
                    data: {
                        HocVienId: item.hocVienId,
                        HocPhi: item.hocPhi,
                        FromDate: item.fromDate,
                        ToDate: item.toDate
                    }
                })
                .then(function (response) {
                    that.forceFileDownloadPdf(response, item.name);
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });
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
                        HocVienId: that.newItem.hocVien.hocVienId,
                        HocPhi: that.newItem.hocPhi,
                        FromDate: that.newItem.fromDate,
                        ToDate: that.newItem.toDate,
                        LopHocList: that.arrayLopHoc,
                        GhiChu: that.newItem.ghiChu
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
                                    console.log(error.response.data.Message);
                                });

                            that.snackbar = true;
                            that.messageText = 'Thêm mới thành công !!!';
                            that.color = 'success';
                            that.newItem.hocVien = [];
                            that.newItem.hocPhi = 0;
                            that.newItem.fromDate = new Date().toISOString().substr(0, 10);
                            that.newItem.toDate = new Date().toISOString().substr(0, 10);
                            that.arrayLopHoc = [];
                            that.itemLopHoc = [];
                            that.newItem.ghiChu = "";
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
                    console.log(error.response.data.Message);
                    that.snackbar = true;
                    that.messageText = 'Cập nhật lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                });
        },

        async onDelete(item) {
            let that = this;
            await axios({
                method: 'delete',
                url: '/hocphi/DeleteHocPhiTronGoiAsync',
                data: {
                    HocPhiTronGoiId: item.hocPhiTronGoiId
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