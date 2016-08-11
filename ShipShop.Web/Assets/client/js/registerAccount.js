$(document).ready(function () {
    $password = $('#frmRegister #Password');
    $rePassword = $('#frmRegister #rePassword');
    $lblMsg = $('#frmRegister #lblMsg');

    $password.keyup(function () {
        if ($(this).val() != $rePassword.val()) {
            $rePassword.css('border-color', 'red');
        } else {
            $rePassword.css('border-color', '');
        }
    });
    $rePassword.keyup(function () {
        if ($(this).val() != $password.val()) {
            $rePassword.css('border-color', 'red');
        } else {
            $rePassword.css('border-color', '');
        }
    });

    $('#frmRegister #btnRegister').on("click", function () {
        var register = {
            UserName: $('#frmRegister #UserName').val(),
            Password: $password.val(),
            Vendee: $('#frmRegister #Vendee').is(":checked"),
            WebOrShopName: $('#frmRegister #WebOrShopName').val(),
            Address: $('#frmRegister #Address').val(),
            RegionID: $('#frmRegister #RegionID').val(),
        };

        var strError = "";
        if (isNaN(register.UserName) || register.UserName.length < 10) {
            $lblMsg.text('Số điện thoại không đúng!');
            return;
        }
        if (register.Password != $rePassword.val()) {
            $lblMsg.text('Mật khẩu nhập lại không khớp!');
            return;
        }

        var error = true;


        if (!error) {
            $('#frmRegister #lblMsg').text(strError);
        } else {

            $.ajax({
                type: "POST",
                url: "/Account/Register",
                data: JSON.stringify(register),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.Code === 1) {
                        window.location.reload();
                    } else {
                        $password.val('');
                        $rePassword.val('');
                        $lblMsg.text(result.Msg);
                    }
                },
                error: function () { alert('Có lỗi sảy ra! Xin lỗi quý khách!'); }
            });
        }
    });
});