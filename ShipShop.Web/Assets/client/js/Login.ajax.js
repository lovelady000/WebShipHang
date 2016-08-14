$(document).ready(function () {
    $('#frmLogin #btnSubmit').on("click", function () {
        if ($('#frmLogin #UserName').val().length == 0 || $('#frmLogin #Password').val().length == 0) {
            $('#frmLogin #lblMsg').text('Số điện thoại và mật khẩu không được để trống!');
        } else {
            var loginModel = {
                UserName: $('#frmLogin #UserName').val(),
                Password: $('#frmLogin #Password').val(),
                RememberMe: $('#frmLogin #RememberMe').is(":checked"),
            };
            $.ajax({
                type: "POST",
                url: "/Account/Login",
                data: JSON.stringify(loginModel),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.Code === 1) {
                        //
                        var orderDetail = [];
                        $tb = $('#OrderDetail');
                        $tb.find('tbody').find('tr').each(function () {
                            var obj = {};
                            $tr = $(this)
                            if ($tr.find('td').eq(1).find('a').length == 0) {
                                obj.NameProduct = $tr.find('td').eq(1).text();
                                obj.UrlProductDetail = '';
                                obj.Note = $tr.find('td').eq(2).text();
                            } else {

                                obj.NameProduct = $tr.find('td').eq(1).text();
                                obj.UrlProductDetail = $tr.find('td').eq(1).find('a').attr('href');
                                obj.Note = $tr.find('td').eq(2).text();
                            }
                            orderDetail.push(obj);
                        });


                        var obj = {
                            order: {
                                SenderMobile: $('#SDTNguoiGui').val(),
                                SenderRegion: $('#VungNguoiGui').val(),
                                SenderAddress: $('#DiaChiNguoiGui').val(),
                                ReceiverMobile: $('#SDTNguoiNhan').val(),
                                ReceiverRegion: $('#VungNguoiNhan').val(),
                                ReceiverAddress: $('#DiaChiNguoiNhan').val(),
                                PayCOD: $('#PhiThuHo').val(),
                                Note:$('#GhiChu').val(),
                            },
                            orderDetail: orderDetail,
                        };
                        console.log(obj);
                        sessionStorage.setItem('order', JSON.stringify(obj));
                        //


                        window.location.reload();
                    } else {
                        $('#frmLogin #lblMsg').text(result.Msg);
                    }
                },
                error: function () { alert('Có lỗi sảy ra! Xin lỗi quý khách!'); }
            });
        }
    });
});