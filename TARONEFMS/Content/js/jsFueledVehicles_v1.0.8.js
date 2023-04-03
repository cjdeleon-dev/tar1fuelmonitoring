function loadDataToDataTable() {

    let myURI = "";

    if (location.hostname != "app.tarelco1ph.com") {
        myURI = "/fuelmonapp/FueledVehicles/FueledVehiclesData/";
    } else
        myURI = "/FueledVehicles/FueledVehiclesData/";

    $.ajax({
        type: "GET",
        url: myURI,
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var body;
            if (result.data != "[]") {
                $.each(result.data, function (i, data) {
                    body = '<tr>';
                    body += '<td class="text-center">' + data.Id + '</td>';
                    body += '<td hidden=hidden>' + data.VehicleId + '</td>';
                    body += '<td>' + data.Vehicle + '</td>';
                    body += '<td>' + data.VehicleNo + '</td>';
                    body += '<td>' + data.PlateNo + '</td>';
                    body += '<td>' + data.DateFueled + '</td>';
                    body += '<td style="text-align:right;">' + data.Liter + '</td>';
                    body += '<td style="text-align:right;">' + data.Amount + '</td>';
                    body += '<td>' + data.DateEntry + '</td>';
                    body += '</tr>';
                    //append content
                    $("#tblFVs tbody").append(body);
                });

            }
        /*DataTables instantiation.*/
            $("#tblFVs").DataTable({
                "Paginate": true,
            });
        },
        error: function () {
            alert();
        }
    });
}

function showModal() {
    $('#myModal').modal({ backdrop: 'static', keyboard: false });

    loadCboVehicles();
    $('#divPrivate').hide();

    $('#myModal').modal('show');
}

function closeModal() {
    $('#myModal').modal('hide');
}

function showExportModal() {
    $('#myExportModal').modal({ backdrop: 'static', keyboard: false });
    $('#myExportModal').modal('show');
}

function closeExportModal() {
    $('#myExportModal').modal('hide');
}

function previewReport() {

    let myURI = "";

    if (location.hostname != "app.tarelco1ph.com") {
        myURI = "/fuelmonapp/FueledVehicles/RRReportView?id=";
    } else
        myURI = "/FueledVehicles/RRReportView?id=";

    //when there's a records
    $('#divRptParams').hide();

    var parent = $('embed#fvpdf').parent();
    var newElement = '<embed src="' + myURI + parseInt(id) + '"  width="100%" height="700px" type="application/pdf" id="fvpdf">';

    $('embed#rrpdf').remove();
    parent.append(newElement);
}


function loadCboVehicles() {

    let myURI = "";

    if (location.hostname != "app.tarelco1ph.com") {
        myURI = "/fuelmonapp/FueledVehicles/GetAllVehicles/";
    } else
        myURI = "/FueledVehicles/GetAllVehicles/";


    $.ajax({
        url: myURI,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (response) {
            if (response != null) {
                $('#cboVehicle').empty();
                $('#cboVehicle').val(0);
                $('#cboVehicle').append("<option value=0>Select</option>");
                for (var i = 0; i < response.length; i++) {
                    var vp = response[i].PlateNo + ' - ' + response[i].Vehicle;
                    var opt = new Option(vp, response[i].Id);
                    $('#cboVehicle').append(opt);
                }
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}



function numberWithCommas(x) {
    setTimeout(function () {
        if (x.value.lastIndexOf(".") != x.value.length - 1) {
            var dec = x.value.split(".", 2);

            var a;

            a = dec[0].replace(/,/g, "");

            //if (dec.length == 2)

            //else
            //    a = dec[0].replace(/,/g, "");

            var nf = new Intl.NumberFormat();

            if (dec.length == 2)
                x.value = nf.format(a) + "." + dec[1];
            else
                x.value = nf.format(a);
        } else {
            return false;
        }
    }, 0);
}

function chkOnChange() {
    if ($('#chkIsPrivate').is(":checked")) {
        $('#divPrivate').show();
        $('#divCompany').hide();
        $('#cboVehicle').val(0);
    } else {
        $('#divPrivate').hide();
        $('#divCompany').show();
        $('#cboVehicle').val(0);
    }
}

function saveEntry() {

    var tsek;
    if ($('#chkIsPrivate').is(":checked")) {
        tsek = true;
    } else {
        tsek = false;
    }

    if (!tsek) {
        if ($('#cboVehicle').val() == 0) {
            alert("Invalid selected vehicle.");
            return false;
        }
    } else {
        if ($('#txtVehicle').val().trim() == '') {
            alert("Invalid Vehicle or Plate No.");
            return false;
        }
    }

    if ($('#txtStation').val().trim() == '') {
        alert("Invalid Gas Station Name.");
        return false;
    }

    if ($('#txtAddress').val().trim() == '') {
        alert("Invalid Address.");
        return false;
    }

    if ($('#dtpDateFueled').val() == '') {
        alert("Invalid date fueled.");
        return false;
    }

    if ($('#txtNoOfLiter').val() == '') {
        alert("Empty liter is not valid.");
        return false;
    } else {
        var ltr = parseFloat($('#txtNoOfLiter').val());

        if (ltr == 0) {
            alert("Invalid liter(s).");
            return false;
        }
    }

    if ($('#txtAmount').val() == '') {
        alert("Empty amount is not valid.");
        return false;
    } else {
        var amt = parseFloat($('#txtAmount').val());

        if (amt == 0) {
            alert("Invalid amount.");
            return false;
        }
    }

    if ($('#txtSINo').val() == '') {
        alert("SI Number is not valid.");
        return false;
    }

    if ($('#txtGasSlip').val() == '') {
        alert("Gas Slip Number is not valid.");
        return false;
    }


    var objdata = {
        Id: 0,
        UserId: 0,
        VehicleId: tsek==true? 0 : $('#cboVehicle').val(),
        Vehicle: tsek==true? $('#txtVehicle').val() : "",
        VehicleNo: "",
        PlateNo: "",
        DateFueled: $('#dtpDateFueled').val(),
        Liter: parseFloat($('#txtNoOfLiter').val().replace(/,/g, "")),
        Amount: parseFloat($('#txtAmount').val().replace(/,/g, "")),
        SalesInvoiceNo: $('#txtSINo').val(),
        GasSlipNo: $('#txtGasSlip').val(),
        IsCompanyVehicle: tsek,
        GasStation: $('#txtStation').val(),
        GasStationAddress: $('#txtAddress').val(),
        DateEntry: ""
    }

    let myURI = "";

    if (location.hostname != "app.tarelco1ph.com") {
        myURI = "/fuelmonapp/FueledVehicles/NewEntryFueledVehicle/";
    } else
        myURI = "/FueledVehicles/NewEntryFueledVehicle/";

    $.ajax({
        type: "POST",
        url: myURI,
        contentType: 'application/json; charset=UTF-8',
        data: JSON.stringify(objdata),
        dataType: "json",
        success: function (response) {
            var myURI2 = "";

            if (location.hostname != "app.tarelco1ph.com") {
                myURI2 = "/fuelmonapp/FueledVehicles/Index";
            } else
                myURI2 = "/FueledVehicles/Index";

            if (response.IsSuccess) {
                alert(response.ProcessMessage);
                window.location = myURI2;
                closeModal();
            }
        },
        error: function (errormessage) {
            toastr.error(errormessage.responseText);
        }
    });

}

function subExport() {
    if (isValidDateRange()) {
        //if ($('#rbUserFormat').attr('checked', 'checked')) {
        //    alert('UserFormat');
        //} else {
        //    alert('SummaryFormat');
        //}
        
        $('#frmExport').submit();
        closeExportModal();
    }
    else
        $('#validationMsg').html('Invalid Date Range');
        //toastr.error('Invalid Date Range.');
}

function isValidDateRange() {

    if ($('#dtpDateFrom').val() == '' || $('#dtpDateTo').val() == '') {
        return false;
    } else {
        if ($('#dtpDateFrom').val() > $('#dtpDateTo').val()) {
            return false;
        } else
            return true;
    }

}