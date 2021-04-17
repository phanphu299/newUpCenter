var intervalTimer;

var vue = new Vue({
    el: '#ChallengeIndex',
    data: {
        title: 'Wellcome to UP Challenges',
        messageText: '',
        color: '',
        timeout: 3000,
        snackbar: false,
        alert: false,
        message: '',
        trigram: '',
        hocVien: '',
        thuThachItems: [],
        selectedThuThach: '',
        showTrigram: true,
        showIntroduce: false,
        showExam: false,
        showResult: false,
        timeLeft: '00:00',
        endTime: '0',
        cauHoiItems: [],
        result: [],
        e6: 1,
        steps: 1,
        isPass: false,
        score: 0,
        resultList: [],
        dialogResult: false
    },
    async beforeCreate() {
    },

    watch: {
        steps(val) {
            if (this.e6 > val) {
                this.e6 = val
            }
        }
    },

    methods: {
        nextStep(n) {
            if (n === this.steps) {
                this.e6 = 1
            } else {
                this.e6 = n + 1
            }
        },

        async onSubmitTrigram(item) {
            if (item === '') {
                that.snackbar = true;
                that.messageText = 'ID is required';
                that.color = 'error';
            }
            else {
                this.dialog = false;
                let that = this;
                await axios({
                    method: 'post',
                    url: '/Challenge/GetHocVienAsync?trigram=' + item,
                    data: {
                    }
                })
                    .then(function (response) {
                        if (response.data.status === "OK") {
                            that.hocVien = response.data.result;
                            that.showTrigram = false;
                            that.showIntroduce = true;
                            axios.get('/Challenge/GetThuThachHocVienAsync?hocVienId=' + that.hocVien.hocVienId)
                                .then(function (response) {
                                    that.thuThachItems = response.data;
                                })
                                .catch(function (error) {
                                    console.log(error.response.data.Message);
                                });
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
                        that.messageText = 'Lỗi: ' + error.response.data.Message;
                        that.color = 'error';
                    });
            }
        },

        async letStart() {
            var that = this;
            this.showIntroduce = false;
            let time = this.selectedThuThach.thoiGianLamBai * 60;
            this.steps = this.selectedThuThach.soCauHoi;

            await axios.get('/Challenge/GetCauHoiAsync?thuThachId=' + that.selectedThuThach.thuThachId)
                .then(function (response) {
                    that.cauHoiItems = response.data;
                    that.showExam = true;
                    that.setTime(time);
                })
                .catch(function (error) {
                    console.log(error.response.data.Message);
                });
        },

        async onTraLoi(cauHoi, dapAn) {
            var that = this;
            var isExisting = this.result.find(x => x.cauHoiId == cauHoi.cauHoiId && cauHoi.stt);
            if (isExisting)
                this.result = this.result.filter(item => item !== isExisting);

            this.result.push({
                cauHoiId: cauHoi.cauHoiId,
                cauHoi: cauHoi.name,
                stt: cauHoi.stt,
                dapAnId: dapAn.dapAnId,
                dapAn: dapAn.name
            })

        },

        async onSubmit() {
            var that = this;
            var dapAnDungs = this.cauHoiItems.map((item, index) => {
                return {
                    cauHoiId: item.cauHoiId,
                    name: item.name,
                    stt: item.stt,
                    dapAn: item.dapAns.find(x => x.isTrue)
                }
            });

            this.result.forEach((item) => {
                var isCorrect = dapAnDungs.find(x =>
                    x.cauHoiId == item.cauHoiId &&
                    x.stt == item.stt &&
                    x.dapAn.dapAnId == item.dapAnId);
                if (isCorrect)
                    that.score++;
            })

            this.resultList = dapAnDungs.map((item) => {
                var choice = that.result.find(x =>
                    x.cauHoiId == item.cauHoiId &&
                    x.stt == item.stt
                );

                let selectedChoice = '';
                if (choice)
                    selectedChoice = choice.dapAn;

                return {
                    name: item.name,
                    stt: item.stt,
                    dapAnDung: item.dapAn.name,
                    dapAnHocVien: selectedChoice
                }
            });

            this.showExam = false;
            this.showResult = true;
            if (that.score >= that.selectedThuThach.minGrade)
                that.isPass = true;

            await axios({
                method: 'post',
                url: '/Challenge/LuuKetQuaAsync',
                data: {
                    HocVienId: that.hocVien.hocVienId,
                    ThuThachId: that.selectedThuThach.thuThachId,
                    LanThi: 0,
                    IsPass: that.isPass,
                    Score: that.score
                }
            })
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });
        },

        setTime(seconds) {
            clearInterval(intervalTimer);
            this.timer(seconds);
        },
        timer(seconds) {
            const now = Date.now();
            const end = now + seconds * 1000;
            this.displayTimeLeft(seconds);

            this.selectedTime = seconds;
            this.displayEndTime(end);
            this.countdown(end);
        },
        countdown(end) {
            intervalTimer = setInterval(() => {
                const secondsLeft = Math.round((end - Date.now()) / 1000);

                if (secondsLeft === 0) {
                    this.endTime = 0;
                    this.onSubmit();
                }

                if (secondsLeft < 0) {
                    clearInterval(intervalTimer);
                    return;
                }
                this.displayTimeLeft(secondsLeft)
            }, 1000);
        },

        displayTimeLeft(secondsLeft) {
            const minutes = Math.floor((secondsLeft % 3600) / 60);
            const seconds = secondsLeft % 60;

            this.timeLeft = `${zeroPadded(minutes)}:${zeroPadded(seconds)}`;
        },

        displayEndTime(timestamp) {
            const end = new Date(timestamp);
            const hour = end.getHours();
            const minutes = end.getMinutes();

            this.endTime = `${hourConvert(hour)}:${zeroPadded(minutes)}`
        }
    }
});

function zeroPadded(num) {
    // 4 --> 04
    return num < 10 ? `0${num}` : num;
}

function hourConvert(hour) {
    // 15 --> 3
    return (hour % 12) || 12;
}