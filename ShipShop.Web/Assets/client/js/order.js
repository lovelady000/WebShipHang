var error = "Có lỗi xảy ra! Xin lỗi quý khách! Vui lòng F5 lại trình duyệt và liên hệ với người quản trị. Xin cảm ơn!";
function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}
$('#sandbox-container .input-group.date').datepicker({
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
            var COD = Number($('#StatusOrder-' + id).closest('tr').find('td').eq(5).text().replace(/,/g, ''));
            var TotalCod = Number($('#TotalCOD').text().replace(/,/g, ''));

            $('#TotalCOD').text(addCommas(TotalCod - COD));
            $('#StatusOrder-' + id).html("<a href=\"javascript: \" onclick=\"ReCancelOrder(" + id + "); \">Hoàn tác</a>");
            toastr.success("Hủy đơn hàng thành công!");
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

            var COD = Number($('#StatusOrder-' + id).closest('tr').find('td').eq(5).text().replace(/,/g, ''));
            var TotalCod = Number($('#TotalCOD').text().replace(/,/g, ''));

            $('#TotalCOD').text(addCommas(TotalCod + COD));
            $('#StatusOrder-' + id).html("<a href=\"javascript: \" onclick=\"CancelOrder(" + id + "); \">Hủy</a>");
            toastr.success("Hoàn tác đơn hàng thành công!");
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
            error: function () { alert(error); }
        });
    }
}
$('#typeOfOrder').change(function () {
    window.location.href = "?typeOfOrder=" + $('#typeOfOrder').val();
})

$('#UserAddress').change(function () {
    var obj = {
        Address: $('#UserAddress').val(),
    };
    $.ajax({
        type: "POST",
        url: "/Account/ChangeProfileAccount",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.code == 1) {
                toastr.success('Sửa thông tin tài khoản thành công!');
            } else {
                bootbox.alert("Thực hiện không thành công.Quý khách vui lòng F5 lại trang và thử lại!", function () {
                });
            }
        },
        error: function () {
            bootbox.alert(error, function () {
            });
        }
    });
})
