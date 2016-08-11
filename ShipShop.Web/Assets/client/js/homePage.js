$(document).ready(function () {
    $('#btnTaoDon').on("click", function () {
        if ($('#frmCreateOrder #userName').length > 0) {

            alert('đã đăng nhập');

        } else {
            bootbox.alert("Bạn cần đăng nhập để thực hiện chứ năng", function () {

            });
        }
    })

})