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