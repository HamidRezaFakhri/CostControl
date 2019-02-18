/// <reference path="Index.js" />

var serviceUrl = "http://localhost:5001/api/";

var middleUrl = serviceUrl + "OverCost/";

function handleException(request, message,
    error) {
    var msg = "";
    msg += "Code: " + request.status + "\n";
    msg += "Text: " + request.statusText + "\n";
    if (request.responseJSON !== null) {
        msg += "Message" +
            request.responseJSON.Message + "\n";
    }
    alert(msg);
}

$(document).ready(function () {
    debugger;
    $("#LoadingStatus").html("در حال بارگذاری ...");
    loadData();
});

$(document).ajaxStart(function () {
    $("#wait").css("display", "block");
});

$(document).ajaxComplete(function () {
    $("#wait").css("display", "none");
});

function trdblclick(id) {
    if (id !== undefined && id >= 0) {
        editEntityModal(id, '/OverCost/EditOverCost/', 'مرکز هزینه');
    }
}

function loadData() {
    $.getJSON(middleUrl + "Get?PageNumber=1&PageSize=10&searchKey=null&SortOrder=id&token=1", function (result) {
        //$.getJSON(middleUrl + "Get", function (result) {
        debugger;
        if (result.data === 'undefined' || result.data === null || result.data.length === 0) {
            $("#LoadingStatus").html(" ");
        }

        $.each(result, function (key, value) {
            debugger;
            if (key === "data") {
                $.each(value, function (i, val) {
                    var item = '<tr id="tr_' + val.id + '" ondblclick="trdblclick(' + val.id + ')">' +
                        '<td>' + val.id + '</td>' +
                        '<td id="td_Name">' + val.name + '</td>' +
                        '<td id="td_Name">' + val.code + '</td>' +
                        '<td>' +
                        '<a class="btn btn-sm btn-warning" data-id=\'' + val.id + '\' onClick="editEntityModal(' + val.id + ', \'/OverCost/EditOverCost/\'' + ', \'مرکز هزینه\'' + ')"> <i class="fa fa-edit"> ویرایش </i> </a>' +
                        '<span> | </span>' +
                        '<a class="btn btn-sm btn-danger" data-id=\'' + val.id + '\' onClick="deleteEntityModal(' + val.id + ', \'مرکز هزینه\'' + ', \'' + val.name + '\'' + ', deleteOverCost' + ')"> <i class="fa fa-trash"> حذف </i> </a>' +
                        '</td>' +
                        '</tr>';
                    $("#setOverCostList").append(item);
                    $("#LoadingStatus").html(" ");
                });
            }

            if (key === "message") {
                var msg = value.split(',');
                $.each(msg, function (index, pageValue) {
                    //debugger;
                    var k = $.trim(pageValue.split("=")[0]);
                    var v = $.trim(pageValue.split("=")[1]);

                    //console.log(k);
                    //console.log(v);

                    if (k === "totalRowCount")
                        $("#totalRowCount").val(v);
                    else if (k === "pageSize")
                        $("#pageSize").val(v);
                    else if (k === "currentPage")
                        $("#currentPage").val(v);
                    else if (k === "totalPage")
                        $("#totalPage").val(v);
                    else if (k === "sortOrder")
                        $("#sortOrder").val(v);
                    else if (k === "sortDirection")
                        $("#sortDirection").val(v);
                    else if (k === "searchKey")
                        $("#searchKey").val(v);
                    else if (k === "hasPreviousPage")
                        $("#hasPreviousPage").val(v);
                    else if (k === "hasNextPage")
                        $("#hasNextPage").val(v);
                });
            }

            if (key === "TotalCount") {
                $("#TotalCount").val(value);
            }

            if (key === "PageSize") {
                $("#PageSize").val(value);
            }

            if (key === "CurrentPage") {
                $("#CurrentPage").val(value);
            }

            if (key === "TotalPage") {
                $("#TotalPages").val(value);
            }

            if (key === "SortOrder") {
                $("#OrderBy").val(value);
            }

            if (key === "SortDirection") {
                $("#SortDirection").val(value);
            }

            if (key === "result") {
                if (key === false) {
                    alert("hhhhhhh111111111");
                }
            }
        });
    });
}

function deleteOverCost(id) {
    //if (confirm("Do you want to delete?")) {
    debugger;
    $.ajax({
        url: middleUrl + "Delete?id=" + id,
        type: 'POST',
        dataType: 'json',
        failure: function (err) {
            alert(err.status + "<----fail----->" + err.responseText);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('#LoadingStatus').html('Error...' + jqXHR.responseText + '<>' + textStatus + ' <> ' + errorThrown.statusText);
            handleException(jqXHR, textStatus, errorThrown);
        },
        success: function (data) {
            //if (data.status !== "ok") {
            //    alert('nok!' + data.result);
            //}
        },
        complete: function (data) {
            if (data.state === '401') {
                alert(data.statusText);
            }
        },
        statusCode: {
            404: function () {
                alert('page not found!');
            }
        }
    }).done(function () {
        $("#tr_" + id).addClass("danger");
        $("#tr_" + id).hide("slow");
    });
}

function validate() {
    var isValid = true;
    if ($('#txtName').val().trim() === "") {
        $('#txtName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }
    return isValid;
}