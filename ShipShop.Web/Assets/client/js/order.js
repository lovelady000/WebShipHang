﻿$('#sandbox-container .input-group.date').datepicker({
    format: "dd/mm/yyyy"
});
function CancelOrder(id) {
    var obj = {
        ID: id,
    };
    $.ajax({
        type: "POST",
        url: "/Order/CancelOrder",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            //window.location.reload();
            $('#StatusOrder-' + id).html("<a href=\"javascript: \" onclick=\"ReCancelOrder(" + id + "); \">Hoàn tác</a>");
            bootbox.alert("Hủy đơn hàng thành công!", function () {
            });
        },
        error: function () {
            bootbox.alert("Thực hiện không thành công.Quý khách vui lòng F5 lại trang và thử lại!", function () {
            });
        }
    });
};
function ReCancelOrder(id) {
    var obj = {
        ID: id,
    };
    $.ajax({
        type: "POST",
        url: "/Order/ReCancelOrder",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            // window.location.reload();
            $('#StatusOrder-' + id).html("<a href=\"javascript: \" onclick=\"CancelOrder(" + id + "); \">Hủy</a>");
            bootbox.alert("Hoàn tác đơn hàng thành công!", function () {
            });
        },
        error: function () {
            //alert('Có lỗi sảy ra! Xin lỗi quý khách!');
            bootbox.alert("Thực hiện không thành công.Quý khách vui lòng F5 lại trang và thử lại!", function () {
            });
        }
    });
};
function changePage(page) {
    var url = window.location;
    var search = url.search;
    var pathname = url.pathname;
    if (search.length > 0) {
        search = search.slice(1);
        if (search.indexOf('&') != -1) {
            var arrParams = search.split('&');
            for (var i = 0 ; i < arrParams.length; ++i) {
                var arrNameAndValue = arrParams[i].split('=');
                if (arrNameAndValue[0] == "page") {
                    arrParams.splice(i, 1);
                }
            }
            window.location.href = pathname + "?" + arrParams.join('&') + "&page=" + page;
        }
        else {
            if (search.split('=')[0] == "page") {
                window.location.href = pathname + "?page=" + page;
            } else {
                window.location.href = pathname + search + "?page=" + page;
            }
        }
    } else {
        window.location.href = pathname + "?page=" + page;
    }
}
$('#ModalChangePass').on('hidden.bs.modal', function () {
    $('#ModalChangePass input').val('');
})


$password = $('#frmChangePass #newPassword');
$rePassword = $('#frmChangePass #rePassword');
$lblMsg = $('#frmChangePass #lblMsg');
$oldPassword = $('#frmChangePass #oldPassword');

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


function changePass() {
    if ($oldPassword.val() == "") {
        $lblMsg.text('Mật khẩu cũ không được bỏ trống!');
        return;
    }
    if ($password.val() != $rePassword.val()) {
        $lblMsg.text('Mật khẩu nhập lại không trùng khớp!');
        return;
    } else {
        var obj = {
            OldPassword: $oldPassword.val(),
            NewPassword: $password.val(),
            RePassword: $rePassword.val(),
        };
        $.ajax({
            type: "POST",
            url: "/Account/ChangePassword",
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.code == 1) {
                    $lblMsg.html('<span style="color:green;">' + result.msg + '</span>');
                    $('#ModalChangePass input').val('');
                } else {
                    $lblMsg.html('<span style="color:red;">' + result.msg + '</span>');
                }
            },
            error: function () { alert('Có lỗi sảy ra! Xin lỗi quý khách!'); }
        });
    }
}
$('#typeOfOrder').change(function () {
    window.location.href = "?typeOfOrder=" + $('#typeOfOrder').val();
})
