//Để phân biệt lúc Add và Edit , 1:Add, 0:Edit
var flag = 0;
//Show Modal.
function addNewOrder() {
    flag = 1;
    $("#newOrderModal").modal();
}
//Auto hiển thị giá tiền tương ứng của loại tiền công
$("#IDTienCong").change(function () {

    var IDTienCong = $("#IDTienCong").val();
    $.ajax({
        type: 'GET',
        data: { IDTienCong: IDTienCong },
        url: '@Url.Action("GetInfoTienCong", "PhieuSuaChuas")',
        success: function (result) {
            //var info = JSON.parse(result);
            for (var i = 0; i < result.length; i++) {
                $("#TienCong").val(result[i].TienCong);
            }
        }
    });
});
//Auto hiển thị giá tiền tương ứng của loại phụ tùng
$("#IDPhuTung").change(function () {

    var IDPhuTung = $("#IDPhuTung").val();
    $.ajax({
        type: 'GET',
        data: { IDPhuTung: IDPhuTung },
        url: '@Url.Action("GetInfoPhuTung", "PhieuSuaChuas")',
        success: function (result) {
            for (var i = 0; i < result.length; i++) {
                $("#DonGia").val(result[i].GiaPhuTung);
            }
        }
    });
});
//Add Multiple Order.
$("#addToList").click(function (e) {
    e.preventDefault();

    if ($.trim($("#IDPhuTung").val()) == "" || $.trim($("#DonGia").val()) == "" || $.trim($("#SoLuongBan").val()) == "") {
        alert("Vui lòng nhập thông tin chi tiết")
        return;
    }

    var IDPhuTung = $("#IDPhuTung").val(),
        DonGia = $("#DonGia").val(),
        SoLuongBan = $("#SoLuongBan").val(),
        IDTienCong = $("#IDTienCong").val(),
        NoiDung = $("#NoiDung").val(),
        TienCong = $("#TienCong").val(),
        detailsTableBody = $("#detailsTable tbody");

    var productItem = '<tr><td>' + IDPhuTung + '</td><td>' + SoLuongBan + '</td><td>' + DonGia + '</td><td>' + IDTienCong + '</td><td>' + NoiDung + '</td><td>' + (parseFloat(DonGia) * parseInt(SoLuongBan) + parseInt(TienCong)) + '</td><td><a data-itemId="0" href="#" class="deleteItem">Remove</a></td></tr>';

    detailsTableBody.append(productItem);
    clearItem();
});
//After Add A New Order In The List, Clear Clean The Form For Add More Order.
function clearItem() {
    $("#IDPhuTung").val('');
    $("#DonGia").val('');
    $("#SoLuongBan").val('');
    $("#IDTienCong").val('');
    $("#NoiDung").val('');
}
// After Add A New Order In The List, If You Want, You Can Remove It.
$(document).on('click', 'a.deleteItem', function (e) {
    e.preventDefault();
    var $self = $(this);
    if ($(this).attr('data-itemId') == "0") {
        $(this).parents('tr').css("background-color", "#ff6347").fadeOut(800, function () {
            $(this).remove();
        });
    }
});
//After Click Save Button Pass All Data View To Controller For Save Database
function saveOrder(data) {
    return $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: "/PhieuSuaChuas/Save",
        data: data,
        success: function (result) {
            alert(result);
            location.reload();
        },
        error: function () {
            alert("Error!")
        }
    });
}
//Collect Multiple Order List For Pass To Controller
$("#saveOrder").click(function (e) {

    e.preventDefault();

    var chitietphieusuaArr = [];
    chitietphieusuaArr.length = 0;

    $.each($("#detailsTable tbody tr"), function () {
        chitietphieusuaArr.push({
            IDPhuTung: $(this).find('td:eq(0)').html(),
            SoLuongBan: $(this).find('td:eq(1)').html(),
            DonGia: $(this).find('td:eq(2)').html(),
            IDTienCong: $(this).find('td:eq(3)').html(),
            NoiDung: $(this).find('td:eq(4)').html(),
            ThanhTien: $(this).find('td:eq(5)').html()
        });
    });


    var data = JSON.stringify({
        IDPhieu: IDPhieu,
        idPhieuTN: $("#idPhieuTN").val(),
        date: $("#date").val(),
        chitietphieusua: chitietphieusuaArr
    });

    $.when(saveOrder(data)).then(function (response) {
        console.log(response);
    }).fail(function (err) {
        console.log(err);
    });
});
$(function () {
    $("#date").datepicker({ dateFormat: 'dd/mm/yy' });
});
//Date Format When Using Ajax Get Datetime
function ToJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    return (dt.getDate() + 1) + "/" + dt.getMonth() + "/" + dt.getFullYear();
}
//Edit
var IDPhieu;
$("#newOrderModal").on('show.bs.modal', function (event) {

    if (flag == 0) {
        var button = $(event.relatedTarget);
        IDPhieu = button.data('whatever');
        $("#ModalTitle").html("Update Phiếu Sửa Chữa");

        $.ajax({
            type: "GET",
            url: '@Url.Action("GetInfoChiTietPhieu","PhieuSuaChuas")',
            dataType: 'json',
            data: { IDPhieu: IDPhieu },
            success: function (result) {
                $("#idPhieuTN option:selected").text(result.IDPhieuTN + " - " + result.TenChuXe);
                $("#idPhieuTN option:selected").val(result.IDPhieuTN);
                $("#date").val(ToJavaScriptDate(result.NgaySuaChua));
                for (var i = 0; i < result.chiTietPhieuSua.length; i++) {
                    var Data = '<tr><td>' + result.chiTietPhieuSua[i].IDPhuTung + '</td><td>' + result.chiTietPhieuSua[i].SoLuongBan + '</td><td>' + result.chiTietPhieuSua[i].DonGia + '</td><td>' + result.chiTietPhieuSua[i].IDTienCong + '</td><td>' + result.chiTietPhieuSua[i].NoiDung + '</td><td>' + result.chiTietPhieuSua[i].ThanhTien + '</td><td><a data-itemId="0" href="#" class="deleteItem">Remove</a></td></tr>';
                    $("#detailsTable tbody").append(Data);
                }
            }
        });
    }
});
$('body').on('hidden.bs.modal', '#newOrderModal', function () {
    location.reload();
});
function DeleteRecord(IDPhieu) {
    $("#PhieuID").val(IDPhieu);
    $("#DeleteConfirmation").modal("show");
}
function ConfirmDelete() {
    var IDPhieu = $("#PhieuID").val();
    $.ajax({
        type: "POST",
        data: { IDPhieu: IDPhieu },
        url: "/PhieuSuaChuas/DeleteConfirmation",
        success: function (result) {
            $("#DeleteConfirmation").modal("hide");
            $(".row_" + IDPhieu).remove();
        }
    })
}
function Details(IDPhieu) {
    $("#Details").modal("show");
    $.ajax({
        type: "GET",
        data: { IDPhieu: IDPhieu },
        url: "/PhieuSuaChuas/GetDetails",
        success: function (result) {
            for (var i = 0; i < result.length; i++) {
                var Data = '<tr><td>' + result[i].IDPhuTung + '</td><td>' + result[i].SoLuongBan + '</td><td>' + result[i].DonGia + '</td><td>' + result[i].IDTienCong + '</td><td>' + result[i].NoiDung + '</td><td>' + result[i].ThanhTien + '</td></tr>';
                $("#details tbody").append(Data);
            }
        }
    });
}
$('#Details').on('hide.bs.modal', function () {
    $("#details tbody tr").remove();
})
$("#txtSearch").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: '@Url.Action("GetSearchValue","PhieuSuaChuas")',
            dataType: "json",
            data: { search: $("#txtSearch").val() },
            success: function (data) {
                response($.map(data, function (item) {
                    return { label: item.TenChuXe, value: item.TenChuXe };
                }));
            },
            error: function (xhr, status, error) {
                alert("Error");
            }
        });
    }
});
$('#txtSearch').on('keypress', function (event) {
    var regex = new RegExp("^[a-zA-Z0-9]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});