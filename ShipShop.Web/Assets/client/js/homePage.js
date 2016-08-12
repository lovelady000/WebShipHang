$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "api/region/getallnopaging",
        //data: JSON.stringify(loginModel),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result, function (i, item) {
                $('#VungNguoiGui').append($('<option>', {
                    value: JSON.stringify(item),
                    text: item.Name
                }));
                $('#VungNguoiNhan').append($('<option>', {
                    value: JSON.stringify(item),
                    text: item.Name
                }));
            });
            var store = sessionStorage.getItem('order');

            if (store != null) {
                var obj = JSON.parse(store);
                console.log(obj);
                $('#SDTNguoiGui').val(obj.order.SenderMobile);
                $('#VungNguoiGui').val(obj.order.SenderRegion);
                $('#DiaChiNguoiGui').val(obj.order.SenderAddress);
                $('#SDTNguoiNhan').val(obj.order.ReceiverMobile);
                $('#VungNguoiNhan').val(obj.order.ReceiverRegion);
                $('#DiaChiNguoiNhan').val(obj.order.ReceiverAddress);
                $('#PhiThuHo').val(obj.order.PayCOD);

                $tb = $('#OrderDetail');
                var arr = obj.orderDetail;
                for (var i = 0; i < arr.length; ++i) {
                    var name = arr[i].NameProduct;
                    var url = arr[i].UrlProductDetail;
                    var note = arr[i].Note;

                    if (url != '') {
                        $tb.find('tbody').append('<tr><td align="center"><a href="javascript:" class="removeOrderDetail">Xóa</a></td><td><a href="' + url + '" target="_blank">' + name + '</a></td><td>' + note + '</td></tr>');
                    }
                    else {
                        $tb.find('tbody').append('<tr><td align="center"><a href="javascript:" class="removeOrderDetail">Xóa</a></td><td>' + name + '</td><td>' + note + '</td></tr>');
                    }
                }
                sessionStorage.removeItem('order');
            }
        },
        error: function () { alert('Có lỗi sảy ra! Xin lỗi quý khách!'); }
    });




    $('#btnTaoDon').on("click", function () {

        if ($('#frmCreateOrder #userName').length > 0) {

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

            var objOrderHomePage = {
                order: {
                    SenderMobile: $('#SDTNguoiGui').val(),
                    SenderRegionID: JSON.parse($('#VungNguoiGui').val()).RegionID,
                    SenderAddress: $('#DiaChiNguoiGui').val(),
                    ReceiverMobile: $('#SDTNguoiNhan').val(),
                    ReceiverRegionID: JSON.parse($('#VungNguoiNhan').val()).RegionID,
                    ReceiverAddress: $('#DiaChiNguoiNhan').val(),
                    PayCOD: $('#PhiThuHo').val(),
                },
                listOrderDetail: orderDetail,
            };
            //sessionStorage.setItem('order', JSON.stringify(obj));
            $.ajax({
                type: "POST",
                url: "/Order/CreateOrder",
                data: JSON.stringify(objOrderHomePage),
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

            //

        } else {
            //console.log($('#VungNguoiGui').val());
            bootbox.alert("Bạn cần đăng nhập để thực hiện chứ năng", function () {

            });
        }
    })
    $('.removeOrderDetail').off('click').on('click', function (e) {
        e.preventDefault();
        $(this).closest('tr').remove();
    })
    $('#modalAddDetail #txtTenHang').change(function () {
        var value = $(this).val();
        if (value = '') {
            $('#modalAddDetail #txtTenHang').css('boder-color', 'red');
        } else {
            $('#modalAddDetail #txtTenHang').css('boder-color', '');
        }
    })
    $('#btnThemHang').click(function () {
        $tb = $('#OrderDetail');
        var name = $('#modalAddDetail #txtTenHang').val();
        if (name == '') {
            return;
        }
        $('#modalAddDetail #txtTenHang').val('');
        var url = $('#modalAddDetail #txtUrl').val();
        $('#modalAddDetail #txtUrl').val('');
        var note = $('#modalAddDetail #txtGhiChu').val();
        $('#modalAddDetail #txtGhiChu').val('');
        if (url != '') {
            $tb.find('tbody').append('<tr><td align="center"><a href="javascript:" class="removeOrderDetail">Xóa</a></td><td><a href="' + url + '" target="_blank">' + name + '</a></td><td>' + note + '</td></tr>');
        }
        else {
            $tb.find('tbody').append('<tr><td align="center"><a href="javascript:" class="removeOrderDetail">Xóa</a></td><td>' + name + '</td><td>' + note + '</td></tr>');
        }
        $('#modalAddDetail').modal('hide');

    })


})

