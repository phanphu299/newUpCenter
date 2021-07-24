using System.Collections.Generic;
using System.Text;
using Up.Models;

namespace Up
{
    public static class TemplateGenerator
    {
        public static string GetHocPhiTronGoiTemplate()
        {
            var sb = new StringBuilder();
            sb.Append(@"
            <html>
              <head>
              </head>
              <body style='background-image: url(https://upenglishvietnam.com/img/transparent-logo.png);
    background-size: 500px;
    background-position: center center;
    background-repeat: no-repeat;'>
                <div class='container'>
                  <div class='row mt-2' style='display: inline-flex;'>
                    <div>
                      <h6>CÔNG TY TNHH MTV GIÁO DỤC VÀ ĐÀO TẠO QUÂN NGUYỄN</h6>
                      <p style='font-style: italic; font-size: 14px'>
                        82, Ngô Chí Quốc, tổ 85, khu phố 13, phường Phú Cường, TP.TDM, Bình
                        Dương.
                      </p>
                    </div>
                    <div>
                      <h5>Số: {0}</h5>
                    </div>
                  </div>

                  <div class='row justify-content-center' style='text-align: center;'>
                    <h1>BIÊN LAI THU TIỀN</h1>
                  </div>

                  <div class='row justify-content-center' style='text-align: center;'>
                    <p style='font-style: italic'>Ngày {1} tháng {2} năm {3}</p>
                  </div>

                  <div class='row'>
                    <p style='word-spacing: 8px;'>Họ và tên người nộp tiền: {4}</p>
                  </div>
                  <div class='row'>
                    <p style='word-spacing: 8px;'>Ngày sinh: {5}</p>
                  </div>
                  <div class='row'>
                    <p style='word-spacing: 8px;'>CMND: {10}</p>
                  </div>
                  <div class='row'>
                    <p style='word-spacing: 8px;'>Địa chỉ: {6}</p>
                  </div>
                  <div class='row'>
                    <p style='word-spacing: 8px;'>Số tiền thu: <b>{7}</b></p>
                  </div>
                  <div class='row'>
                    <p style='word-spacing: 8px;'>Nội dung thu: Học phí trọn gói từ {8} đến {9}</p>
                  </div>

                  <div style='text-align: center; float: right; margin-right: 30px'>
                    <b>Người thu tiền</b>
                    <br/>
                    (Ký và ghi rõ họ tên)
                  </div>

                  <p style='font-style: italic; margin-top: 9rem'>
                      * Người nộp tiền vui lòng kiểm tra kỹ thông tin trước khi duỵệt mẫu,
                      giữ lại biên lai cẩn thận và xuất trình khi công ty yêu cầu
                    </p>
                </div>
              </body>
            </html>
            ");
            return sb.ToString();
        }

        public static string GetHocPhiTheoThangTemplate()
        {
            var sb = new StringBuilder();
            sb.Append(@"
            <html>
              <head>
              </head>
              <body style='background-image: url(https://upenglishvietnam.com/img/transparent-logo.png);
    background-size: 500px;
    background-position: center center;
    background-repeat: no-repeat;'>
                <div class='container'>
                  <div class='row mt-2' style='display: inline-flex;'>
                    <div>
                      <h6>CÔNG TY TNHH MTV GIÁO DỤC VÀ ĐÀO TẠO QUÂN NGUYỄN</h6>
                      <p style='font-style: italic; font-size: 14px'>
                        82, Ngô Chí Quốc, tổ 85, khu phố 13, phường Phú Cường, TP.TDM, Bình
                        Dương.
                      </p>
                    </div>
                    <div>
                      <h5>Số: {0}</h5>
                    </div>
                  </div>

                  <div class='row justify-content-center' style='text-align: center;'>
                    <h1>BIÊN LAI THU TIỀN</h1>
                  </div>

                  <div class='row justify-content-center' style='text-align: center;'>
                    <p style='font-style: italic'>Ngày {1} tháng {2} năm {3}</p>
                  </div>

                  <div class='row'>
                    <p style='word-spacing: 8px;'>Họ và tên người nộp tiền: {4}</p>
                  </div>
                  <div class='row'>
                    <p style='word-spacing: 8px;'>Ngày sinh: {5}</p>
                  </div>
                  <div class='row'>
                    <p style='word-spacing: 8px;'>CMND: {9}</p>
                  </div>
                  <div class='row'>
                    <p style='word-spacing: 8px;'>Địa chỉ: {6}</p>
                  </div>
                  <div class='row'>
                    <p style='word-spacing: 8px;'>Số tiền thu: <b>{7}</b></p>
                  </div>
                  <div class='row'>
                    <p style='word-spacing: 8px;'>Nội dung thu: Nộp học phí lớp tháng {8}</p>
                  </div>

                  <div style='text-align: center; float: right; margin-right: 30px'>
                    <b>Người thu tiền</b>
                    <br/>
                    (Ký và ghi rõ họ tên)
                  </div>

                  <p style='font-style: italic; margin-top: 9rem'>
                      * Người nộp tiền vui lòng kiểm tra kỹ thông tin trước khi duỵệt mẫu,
                      giữ lại biên lai cẩn thận và xuất trình khi công ty yêu cầu
                    </p>
                </div>
              </body>
            </html>
            ");
            return sb.ToString();
        }

        public static string GetChallengeResultTemplate(IList<ChallengeResultInputModel> model)
        {
            var sb = new StringBuilder();
            sb.Append(@"
            <html>
              <head>
              </head>
              <body>
                <div class='container'>
                    <h2 style='color: #00008b;'>CHALLENGE RESULT</h2>
                    
                    <p style='margin-bottom: 0'><b>Student Name: </b> {0}</p>
                    <p style='margin-bottom: 0'><b>Student ID: </b> {1}</p>
                    <p style='margin-bottom: 0'><b>Challenge Name: </b> {2}</p>
                    <p style='margin-bottom: 0'><b>Date taken: </b> {3}</p>
                    <p style='margin-bottom: 0'><b>Time(s): </b> {4}</p>
                    
                    </br>

                    <p style='margin-bottom: 0'><b>Score: </b> {5}</p>
                    <p style='margin-bottom: 0'><b>Result: </b> {6}</p>

                    <table class='table table-bordered' style='page-break-inside:auto'>
                        <thead style='display:table-header-group'>
                          <tr style='page-break-inside:avoid; page-break-after:auto'>
                            <th>#</th>
                            <th>Question</th>
                            <th>Your Answer</th>
                            <th>Correct Answer</th>
                          </tr>
                        </thead>
                        <tbody>
            ");

            foreach(var item in model)
            {
                string[] args = new string[] {
                    item.Stt.ToString(),
                    item.Name,
                    item.DapAnHocVien,
                    item.DapAnDung
                };

                sb.Append(string.Format(@"
                    <tr style='page-break-inside:avoid; page-break-after:auto'>
                        <td style='padding: 0 4px'><b>{0}</b></td>
                        <td style='padding: 0 4px'>{1}</td>
                        <td style='padding: 0 4px'>{2}</td>
                        <td style='padding: 0 4px'>{3}</td>
                    </tr>
                ", args));
            }

            sb.Append(@"</tbody>
                    </table>
                </div>
              </body>
            </html>");

            return sb.ToString();
        }
    }
}
