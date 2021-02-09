var vue = new Vue({
    el: '#GiaoVienIndex',
    data: {
        title: 'Quản Lý Nhân Viên',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        deleteDialog: false,
        dialogEdit: false,
        dialog: false,
        alert: false,
        alertEdit: false,
        search: '',
        newItem: {
            name: '',
            phone: '',
            teachingRate: 0,
            tutoringRate: 0,
            basicSalary: 0,
            facebookAccount: '',
            diaChi: '',
            initialName: '',
            cmnd: '',
            loaiGiaoVien: '',
            loaiCheDo: '',
            mucHoaHong: 0,
            ngayLamViecId: '',
            ngayBatDau: '',
            ngayKetThuc: '',
            nganHang: ''
        },
        itemToDelete: {},
        itemToEdit: {},
        editedIndex: -1,
        alertMessage: '',
        headers: [
            {
                text: 'Action',
                align: 'left',
                sortable: false,
                value: ''
            },
            { text: 'Tên Nhân Viên', value: 'name', align: 'left', sortable: true },
            { text: 'Vị Trí/Chế Độ', value: 'loaiGiaoVien', align: 'left', sortable: true },
            { text: 'Số Điện Thoại', value: 'phone', align: 'left', sortable: true },
            { text: 'Teaching Rate', value: 'teachingRate', align: 'left', sortable: true },
            { text: 'Tutoring Rate', value: 'tutoringRate', align: 'left', sortable: true },
            { text: 'Lương Cơ Bản', value: 'basicSalary', align: 'left', sortable: true },
            { text: 'Mức Hoa Hồng', value: 'mucHoaHong', align: 'left', sortable: true },
            { text: 'STK Ngân Hàng', value: 'nganHang', align: 'left', sortable: true },
            { text: 'Facebook', value: 'facebookAccount', align: 'left', sortable: true },
            { text: 'Địa Chỉ', value: 'diaChi', align: 'left', sortable: true },
            { text: 'Initial Name', value: 'initialName', align: 'left', sortable: true },
            { text: 'CMND', value: 'cmnd', align: 'left', sortable: true },
            { text: 'Ngày Làm Việc', value: 'ngayLamViec', align: 'left', sortable: true },
            { text: 'Ngày Bắt Đầu', value: 'ngayBatDau', align: 'left', sortable: true },
            { text: 'Ngày Kết Thúc', value: 'ngayKetThuc', align: 'left', sortable: true },
            { text: 'Ngày Tạo', value: 'createdDate', align: 'left', sortable: true },
            { text: 'Người Tạo', value: 'createdBy', align: 'left', sortable: true },
            { text: 'Ngày Sửa', value: 'updatedDate', align: 'left', sortable: true },
            { text: 'Người Sửa', value: 'updatedBy', align: 'left', sortable: true }
        ],
        khoaHocItems: [],
        itemLoaiGiaoVien: [],
        itemLoaiCheDo: [],
        itemNgayLamViec: [],
        arrayLoaiNVandCD: [],
        isShowDatePickerBatDau: false,
        isShowDatePickerKetThuc: false,
        isShowDatePickerBatDau2: false,
        isShowDatePickerKetThuc2: false
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/GiaoVien/GetGiaoVienAsync')
            .then(function (response) {
                that.khoaHocItems = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });

        await axios.get('/Category/GetNgayLamViecAsync')
            .then(function (response) {
                that.itemNgayLamViec = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });

        await axios.get('/Category/GetLoaiGiaoVienAsync')
            .then(function (response) {
                that.itemLoaiGiaoVien = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });

        await axios.get('/Category/GetLoaiCheDoAsync')
            .then(function (response) {
                that.itemLoaiCheDo = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });
    },
    filters: {
        truncate: function (text, length, suffix) {
            return text.substring(0, length) + suffix;
        }
    },
    methods: {
        async onAddLoaiNVCD() {
            if (this.newItem.loaiGiaoVien !== '' && this.newItem.loaiCheDo !== '') {
                let isExisting = false;
                this.arrayLoaiNVandCD.map(item => {
                    if (item.loaiGiaoVien.loaiGiaoVienId === this.newItem.loaiGiaoVien.loaiGiaoVienId) {
                        isExisting = true;
                    }
                });

                if (isExisting === false) {
                    this.arrayLoaiNVandCD.push({
                        loaiGiaoVien: this.newItem.loaiGiaoVien,
                        loaiCheDo: this.newItem.loaiCheDo
                    });
                }
            }
            else {
                this.snackbar = true;
                this.messageText = 'Phải chọn cả loại nhân viên và chế độ trước khi thêm !!!';
                this.color = 'error';
            }
        },

        async onXoaLoaiNVCD(item) {
            this.arrayLoaiNVandCD = this.arrayLoaiNVandCD
                .filter(x => x.loaiGiaoVien.loaiGiaoVienId !== item.loaiGiaoVien.loaiGiaoVienId);
        },

        async onAddLoaiNVCDForEdit(item) {
            if (this.itemToEdit.loaiGiaoVienId !== undefined && this.itemToEdit.loaiCheDoId !== undefined) {
                let isExisting = false;
                item.loaiNhanVien_CheDo.map(i => {
                    if (i.loaiGiaoVien.loaiGiaoVienId === this.itemToEdit.loaiGiaoVienId.loaiGiaoVienId) {
                        isExisting = true;
                    }
                });

                if (isExisting === false) {
                    item.loaiNhanVien_CheDo.push({
                        loaiGiaoVien: this.itemToEdit.loaiGiaoVienId,
                        loaiCheDo: this.itemToEdit.loaiCheDoId
                    });
                }
            }
            else {
                this.snackbar = true;
                this.messageText = 'Phải chọn cả loại nhân viên và chế độ trước khi thêm !!!';
                this.color = 'error';
            }
        },

        async onXoaLoaiNVCDForEdit(item) {
            this.itemToEdit.loaiNhanVien_CheDo = this.itemToEdit.loaiNhanVien_CheDo
                .filter(x => x.loaiGiaoVien.loaiGiaoVienId !== item.loaiGiaoVien.loaiGiaoVienId);
        },

        mappingEditItem(item) {
            this.editedIndex = this.khoaHocItems.indexOf(item);
            this.itemToEdit = Object.assign({}, item);

            if (this.itemToEdit.ngayBatDau !== "") {
                let [dayKG, monthKG, yearKG] = this.itemToEdit.ngayBatDau.split('/');
                this.itemToEdit.ngayBatDau = yearKG + '-' + monthKG + '-' + dayKG;
            }

            if (this.itemToEdit.ngayKetThuc !== "") {
                let [dayKG, monthKG, yearKG] = this.itemToEdit.ngayKetThuc.split('/');
                this.itemToEdit.ngayKetThuc = yearKG + '-' + monthKG + '-' + dayKG;
            }
        },

        async onUpdate(item) {
            if (item.ngayLamViecId === '' || item.ngayLamViec === '' || item.ngayBatDau === '' || item.name === '' || item.phone === '' || item.diaChi === '' || item.initialName === '' || item.cmnd === '' || item.basicSalary === '') {
                this.alertMessage = "Không được bỏ trống";
                this.alertEdit = true;
            }
            else if (isNaN(item.mucHoaHong) || isNaN(item.teachingRate) || isNaN(item.tutoringRate) || isNaN(item.basicSalary)) {
                this.alertMessage = "Teaching Rate, Tutoring Rate, Basic Salary và Mức Hoa Hồng chỉ được nhập số";
                this.alertEdit = true;
            }
            else if (item.loaiNhanVien_CheDo.length === 0) {
                this.snackbar = true;
                this.messageText = 'Phải chọn loại nhân viên và chế độ !!!';
                this.color = 'error';
            }
            else {
                let that = this;
                await axios({
                    method: 'put',
                    url: '/giaovien/UpdateGiaoVienAsync',
                    data: {
                        GiaoVienId: item.giaoVienId,
                        Name: item.name,
                        Phone: item.phone,
                        TeachingRate: item.teachingRate,
                        TutoringRate: item.tutoringRate,
                        BasicSalary: item.basicSalary,
                        FacebookAccount: item.facebookAccount,
                        DiaChi: item.diaChi,
                        InitialName: item.initialName,
                        CMND: item.cmnd,
                        LoaiNhanVien_CheDo: item.loaiNhanVien_CheDo,
                        MucHoaHong: item.mucHoaHong,
                        NgayBatDau: item.ngayBatDau,
                        NgayKetThuc: item.ngayKetThuc,
                        NgayLamViecId: item.ngayLamViecId,
                        NganHang: item.nganHang
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
            }
        },

        async onSave(item) {
            if (this.newItem.ngayLamViecId === '' || this.newItem.ngayLamViec === '' || this.newItem.ngayBatDau === '' || this.newItem.name === '' || this.newItem.phone === '' || this.newItem.diaChi === '' || this.newItem.initialName === '' || this.newItem.cmnd === '') {
                this.alertMessage = "Không được bỏ trống";
                this.alert = true;
            }
            else if (isNaN(this.newItem.mucHoaHong) || isNaN(this.newItem.teachingRate) || isNaN(this.newItem.tutoringRate) || isNaN(this.newItem.basicSalary)) {
                this.alertMessage = "Teaching Rate, Tutoring Rate, Basic Salary và Mức Hoa Hồng chỉ được nhập số";
                this.alert = true;
            }
            else if (this.arrayLoaiNVandCD.length === 0) {
                this.snackbar = true;
                this.messageText = 'Phải chọn loại nhân viên và chế độ !!!';
                this.color = 'error';
            }
            else {
                this.dialog = false;
                let that = this;
                await axios({
                    method: 'post',
                    url: '/giaovien/CreateGiaoVienAsync',
                    data: {
                        Name: that.newItem.name,
                        Phone: that.newItem.phone,
                        TeachingRate: that.newItem.teachingRate,
                        TutoringRate: that.newItem.tutoringRate,
                        BasicSalary: that.newItem.basicSalary,
                        FacebookAccount: that.newItem.facebookAccount,
                        DiaChi: that.newItem.diaChi,
                        InitialName: that.newItem.initialName,
                        CMND: that.newItem.cmnd,
                        LoaiNhanVien_CheDo: that.arrayLoaiNVandCD,
                        MucHoaHong: that.newItem.mucHoaHong,
                        NgayBatDau: that.newItem.ngayBatDau,
                        NgayKetThuc: that.newItem.ngayKetThuc,
                        NgayLamViecId: that.newItem.ngayLamViecId,
                        NganHang: that.newItem.nganHang
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
                            that.newItem.phone = '';
                            that.newItem.teachingRate = 0;
                            that.newItem.tutoringRate = 0;
                            that.newItem.basicSalary = 0;
                            that.newItem.facebookAccount = '';
                            that.newItem.diaChi = '';
                            that.newItem.initialName = '';
                            that.newItem.cmnd = '';
                            that.arrayLoaiNVandCD = [];
                            that.newItem.loaiGiaoVien = '';
                            that.newItem.loaiCheDo = '';
                            that.newItem.mucHoaHong = 0;
                            that.newItem.ngayLamViecId = '';
                            that.newItem.ngayBatDau = '';
                            that.newItem.ngayKetThuc = '';
                            that.newItem.nganHang = '';
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
                url: '/giaovien/DeleteGiaoVienAsync',
                data: {
                    GiaoVienId: item.giaoVienId
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