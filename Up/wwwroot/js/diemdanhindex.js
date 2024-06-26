﻿var vue = new Vue({
    el: '#DiemDanhIndex',
    data: {
        title: 'Điểm Danh Học Viên',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        deleteDialog: false,
        dialog: false,
        dialogHoanTac: false,
        selectedLopHoc: '',
        ngayDiemDanh: new Date().toISOString().substr(0, 10),
        ngayDiemDanhOff: [],
        ngayDiemDanhUndo: [],
        selectedHocViens: [],
        itemLopHoc: [],
        isShowDatePicker: false,
        isShowDatePickerOff: false,
        isShowDatePickerUndo: false,
        search: '',
        headers: [
            { text: 'Học Viên', value: 'fullName', align: 'left', sortable: true },
            { text: 'English Name', value: 'englishName', align: 'left', sortable: true },
            {
                text: 'Có Mặt',
                align: 'left',
                sortable: false,
                value: ''
            },
            {
                text: 'Vắng',
                align: 'left',
                sortable: false,
                value: ''
            }
        ],
        diemDanhItems: [],
        selectedThang: '',
        selectedNam: '',
        itemThang: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
        itemNam: [new Date().toISOString().substr(0, 4) - 2, new Date().toISOString().substr(0, 4) - 1, new Date().toISOString().substr(0, 4) - 0, parseInt(new Date().toISOString().substr(0, 4)) + 1],
        soNgayHoc: [],
        dates: ''
    },
    async beforeCreate() {
        let that = this;
        await axios.get('/LopHoc/GetAvailableLopHocAsync')
            .then(function (response) {
                that.itemLopHoc = response.data;
            })
            .catch(function (error) {
                console.log(error.response.data.Message);
            });
    },
    methods: {
        async onTinhDiemDanh() {
            let that = this;
            if (this.selectedLopHoc !== '' && this.selectedNam !== '' && this.selectedThang !== '') {
                axios.get('/DiemDanh/GetDiemDanhByLopHocAsync?LopHocId=' + that.selectedLopHoc + '&month=' + that.selectedThang + '&year=' + that.selectedNam)
                    .then(function (response) {
                        that.diemDanhItems = response.data;
                        for (let i = 0; i < that.diemDanhItems.length; i++) {
                            for (let j = 0; j < that.diemDanhItems[i].thongKeDiemDanh.length; j++) {
                                if (that.diemDanhItems[i].thongKeDiemDanh[j].duocNghi !== true) {
                                    that.diemDanhItems[i].thongKeDiemDanh[j].duocNghi = false;
                                }
                            }
                        }
                    })
                    .catch(function (error) {
                        console.log(error.response.data.Message);
                    });

                await axios.get('/DiemDanh/GetSoNgayHoc?LopHocId=' + that.selectedLopHoc + '&month=' + that.selectedThang + '&year=' + that.selectedNam)
                    .then(function (response) {
                        that.soNgayHoc = response.data;
                    })
                    .catch(function (error) {
                        console.log(error.response.data.Message);
                    });
            }
        },

        async onDiemDanhNew(hocVienId, column, isOff) {
            let that = this;
            await axios.get('/DiemDanh/DiemDanhTungHocVienNewAsync?HocVienId=' + hocVienId + '&LopHocId=' + this.selectedLopHoc + '&day=' + column + '&month=' + this.selectedThang + '&year=' + this.selectedNam + '&IsOff=' + isOff)
                .then(function (response) {
                    console.log(response);
                    if (response.data.status === "OK") {
                        that.snackbar = true;
                        that.messageText = 'Điểm danh thành công !!!';
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
                    that.messageText = 'Điểm danh lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                });
        },

        async GetHocVienByLopHoc() {
            let that = this;
            await axios.get('/DiemDanh/GetHocVienByLopHocAsync?LopHocId=' + this.selectedLopHoc)
                .then(function (response) {
                    that.hocVienList = response.data;
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });
        },

        async DiemDanhCaLop() {
            let that = this;
            await axios({
                method: 'post',
                url: '/DiemDanh/DiemDanhTatCaAsync',
                data: {
                    LopHocId: this.selectedLopHoc,
                    NgayDiemDanh: this.ngayDiemDanh,
                    IsOff: false
                }
            })
            .then(function (response) {
                console.log(response);
                if (response.data.status === "OK") {
                    that.snackbar = true;
                    that.messageText = 'Điểm danh thành công !!!';
                    that.color = 'success';

                    let [year, month, day] = that.ngayDiemDanh.split('-');
                    for (let hocVien of that.diemDanhItems) {
                        for (let diemdanh of hocVien.thongKeDiemDanh) {
                            let [yearDD, monthDD, dayDD] = diemdanh.dates.split('-');
                            if (diemdanh.day === parseInt(day) && year === yearDD && month === monthDD) {
                                diemdanh.isOff = true;
                                diemdanh.duocNghi = false;
                            }
                        }
                    }
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
                that.messageText = 'Điểm danh lỗi: ' + error.response.data.Message;
                that.color = 'error';
            });
        },

        async LopNghi() {
            let that = this;
            await axios({
                method: 'post',
                url: '/DiemDanh/LopNghiAsync',
                data: {
                    LopHocId: this.selectedLopHoc,
                    NgayDiemDanh: this.ngayDiemDanh
                }
            })
                .then(function (response) {
                    if (response.data.status === "OK") {
                        that.snackbar = true;
                        that.messageText = 'Điểm danh thành công !!!';
                        that.color = 'success';

                        let [year, month, day] = that.ngayDiemDanh.split('-');
                        for (let hocVien of that.diemDanhItems) {
                            for (let diemdanh of hocVien.thongKeDiemDanh) {
                                let [yearDD, monthDD, dayDD] = diemdanh.dates.split('-');
                                if (diemdanh.day === parseInt(day) && year === yearDD && month === monthDD) {
                                    diemdanh.duocNghi = true;
                                }
                            }
                        }
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
                    that.messageText = 'Điểm danh lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                });
        },

        async onSaveHocVienOff() {
            let that = this;
            await axios({
                method: 'post',
                url: '/DiemDanh/SaveHocVienOffAsync',
                data: {
                    LopHocId: this.selectedLopHoc,
                    HocVienIds: this.selectedHocViens,
                    NgayDiemDanhs: this.ngayDiemDanhOff
                }
            })
                .then(function (response) {
                    if (response.data.status === "OK") {
                        that.snackbar = true;
                        that.messageText = 'Điểm danh thành công !!!';
                        that.color = 'success';

                        for (let ngay of that.ngayDiemDanhOff) {
                            let [year, month, day] = ngay.split('-');
                            for (let hocVien of that.diemDanhItems) {
                                for (let diemdanh of hocVien.thongKeDiemDanh) {
                                    let [yearDD, monthDD, dayDD] = diemdanh.dates.split('-');
                                    if (diemdanh.day === parseInt(day) && year === yearDD && month === monthDD && that.selectedHocViens.includes(hocVien.hocVienId.toString())) {
                                        diemdanh.duocNghi = true;
                                    }
                                }
                            }
                        }

                        that.dialog = false;
                        that.selectedHocViens = [];
                        that.ngayDiemDanhOff = [];
                        that.dates = '';
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
                    that.messageText = 'Điểm danh lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                });
        },

        async onSaveHocVienHoanTac() {
            let that = this;
            await axios({
                method: 'post',
                url: '/DiemDanh/SaveHocVienHoanTacAsync',
                data: {
                    LopHocId: this.selectedLopHoc,
                    HocVienIds: this.selectedHocViens,
                    NgayDiemDanhs: this.ngayDiemDanhUndo
                }
            })
                .then(function (response) {
                    if (response.data.status === "OK") {
                        that.snackbar = true;
                        that.messageText = 'Hoàn tác thành công !!!';
                        that.color = 'success';

                        for (let ngay of that.ngayDiemDanhUndo) {
                            let [year, month, day] = ngay.split('-');
                            for (let hocVien of that.diemDanhItems) {
                                for (let diemdanh of hocVien.thongKeDiemDanh) {
                                    let [yearDD, monthDD, dayDD] = diemdanh.dates.split('-');
                                    if (diemdanh.day === parseInt(day) && year === yearDD && month === monthDD && that.selectedHocViens.includes(hocVien.hocVienId.toString())) {
                                        diemdanh.duocNghi = false;
                                        diemdanh.isOff = false;
                                    }
                                }
                            }
                        }

                        that.dialogHoanTac = false;
                        that.selectedHocViens = [];
                        that.ngayDiemDanhUndo = [];
                        that.dates = '';
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
                    that.messageText = 'Điểm danh lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                });
        },

        async UndoLopNghi() {
            let that = this;
            await axios({
                method: 'post',
                url: '/DiemDanh/UndoLopNghiAsync',
                data: {
                    LopHocId: this.selectedLopHoc,
                    NgayDiemDanh: this.ngayDiemDanh
                }
            })
                .then(function (response) {
                    console.log(response);
                    if (response.data.status === "OK") {
                        that.snackbar = true;
                        that.messageText = 'Undo thành công !!!';
                        that.color = 'success';

                        let [year, month, day] = that.ngayDiemDanh.split('-');
                        for (let hocVien of that.diemDanhItems) {
                            for (let diemdanh of hocVien.thongKeDiemDanh) {
                                let [yearDD, monthDD, dayDD] = diemdanh.dates.split('-');
                                if (diemdanh.day === parseInt(day) && year === yearDD && month === monthDD) {
                                    diemdanh.duocNghi = false;
                                    diemdanh.isOff = false;
                                }
                            }
                        }
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
                    that.messageText = 'Undo lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                });
        },

        async onCoMat(item) {
            let that = this;
            await axios({
                method: 'post',
                url: '/DiemDanh/DiemDanhTungHocVienAsync',
                data: {
                    LopHocId: this.selectedLopHoc,
                    NgayDiemDanh: this.ngayDiemDanh,
                    IsOff: false,
                    HocVienId: item.hocVienId
                }
            })
                .then(function (response) {
                    console.log(response);
                    if (response.data.status === "OK") {
                        that.snackbar = true;
                        that.messageText = 'Điểm danh có mặt thành công !!!';
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
                    that.messageText = 'Điểm danh lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                });
        },

        async onVang(item) {
            let that = this;
            await axios({
                method: 'post',
                url: '/DiemDanh/DiemDanhTungHocVienAsync',
                data: {
                    LopHocId: this.selectedLopHoc,
                    NgayDiemDanh: this.ngayDiemDanh,
                    IsOff: true,
                    HocVienId: item.hocVienId
                }
            })
                .then(function (response) {
                    console.log(response);
                    if (response.data.status === "OK") {
                        that.snackbar = true;
                        that.messageText = 'Điểm danh vắng thành công !!!';
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
                    that.messageText = 'Điểm danh lỗi: ' + error.response.data.Message;
                    that.color = 'error';
                });
        }
    }
});