function validationOnFlyC(element, errorElement, flag) {
    ret = flag;
    if ($(element).val().trim() == "") {
        $(element).addClass("alert-danger");
        $(errorElement).text("This Field Is Required.");
        if (flag == 0) {
            $("html, body").animate({ scrollTop: $(element).offset().top - 60 }, "slow");
            ret = 1;
        }
    }
    else {
        $(errorElement).text("*");
        $(element).removeClass("alert-danger");
    }
    return ret;
}
function validationOnFlyCustom(element, errorElement) {
    if ($(element).val().trim() == "") {
        $("html, body").animate({ scrollTop: $(element).offset().top - 60 }, "slow");
        $(element).addClass("alert-danger");
        $(errorElement).text("This Field Is Required.");
    }
    else {
        $(errorElement).text("*");
        $(element).removeClass("alert-danger");
    }
}
function validationOnFly(elementType, elementKey, errorEType, errorKey)
{
    if (elementType == "class") {
        if ($(elementKey).length > 0) {
            elementKey = "." + $(elementKey).attr("class").split(' ').pop();
        }
        else {
            elementKey = "." + elementKey;
        }
    }
    else if (elementType == "id") {
        if ($(elementKey).length > 0) {
            elementKey = "#" + $(elementKey).attr("id");
        }
        else {
            elementKey = "#" + elementKey;
        }
    }

    if (errorEType == "class") {
        if ($(errorKey).length > 0) {
            errorKey = "." + $(errorKey).attr("class").split(' ').pop();
        }
        else {
            errorKey = "." + errorKey;
        }
    }
    else if (errorEType == "id") {
        if ($(errorKey).length > 0) {
            errorKey = "#" + $(errorKey).attr("id");
        }
        else {
            errorKey = "#" + errorKey;
        }
    }

    if ($(elementKey).val().trim() == "") {
        $(errorKey).text("This Field Is Required.");
        $("html, body").animate({ scrollTop: $(elementKey).offset().top - 60 }, "slow");
        $(elementKey).addClass("alert-danger");
    }
    else {
        $(errorKey).text("*");
        $(elementKey).removeClass("alert-danger");
    }
}

function validateAll() {
    var ret = true;
    scrolled = 0;
    console.log("b4 all " + scrolled);
    if (($("#txtTradeLicense").val() == "") || $("#txtTradeLicense").val() == null) {
        scrolled = validationOnFlyC("#txtTradeLicense", "#errTradeLicense", scrolled);
        ret = false;
    }
    if (($("#txtTinNo").val() == "") || $("#txtTinNo").val() == null) {
        scrolled = validationOnFlyC("#txtTinNo", "#errTinNo", scrolled);
        ret = false;
    }
    if (($("#txtTelNo").val() == "") || $("#txtTelNo").val() == null) {
        scrolled = validationOnFlyC("#txtTelNo", "#errTelNo", scrolled);
        ret = false;
    }
    if (($("#txtCellNo").val() == "") || $("#txtCellNo").val() == null) {
        scrolled = validationOnFlyC("#txtCellNo", "#errCellNo", scrolled);
        ret = false;
    }
    if (($("#ddlOrgType").multipleSelect("getSelects") == "") || ($("#ddlOrgType").multipleSelect("getSelects") == null)) {
        scrolled = validationOnFlyC("#txtOrgType", "#errOrgType", scrolled);
        ret = false;
    }
    if (($("#ddlOrgCategory").multipleSelect("getSelects") == "") || ($("#ddlOrgCategory").multipleSelect("getSelects") == null)) {
        $("#ddlOrgCategory").addClass("alert-danger");
        $("#errOrgCategory").text("This Field Is Required.");
        if (scrolled == 0) {
            $("html, body").animate({ scrollTop: $("#errOrgCategory").offset().top - 60 }, "slow");
            scrolled = 1;
        }
        ret = false;
    }
    if (($("#txtYOE").val() == "") || $("#txtYOE").val() == null) {
        scrolled = validationOnFlyC("#txtYOE", "#errYOE", scrolled);
        ret = false;
    }
    if (($("#txtCellNo").val() == "") || $("#txtCellNo").val() == null) {
        scrolled = validationOnFlyC("#txtCellNo", "#errCellNo", scrolled);
        ret = false;
    }
    if (($("#txtBankName").val() == "") || $("#txtBankName").val() == null) {
        scrolled = validationOnFlyC("#txtBankName", "#errBankName", scrolled);
        ret = false;
    }
    if (($("#txtBankAccountNumber").val() == "") || $("#txtBankAccountNumber").val() == null) {
        scrolled = validationOnFlyC("#txtBankAccountNumber", "#errAccountNo", scrolled);
        ret = false;
    }

    // CERTIFICATION FILES BEGIN
    var cer = $("#attachmentCertification").find("input:text");
    for (var i = 0; i < cer.length; i++) {
        if (($(cer[i]).val() == "") || ($(cer[i]).val() == null)) {
            $(cer[i]).addClass("alert-danger");
            if (scrolled == 0) {
                $("html, body").animate({ scrollTop: $(cer[i]).offset().top - 60 }, "slow");
                scrolled = 1;
            }
            ret = false;
        }
        else {
            if ($(cer[i]).parent().next().children()[0].files.length == 0) {
                $(cer[i]).parent().next().children().addClass("alert-danger");
                if (scrolled == 0) {
                    $("html, body").animate({ scrollTop: $(cer[i]).offset().top - 60 }, "slow");
                    scrolled = 1;
                }
                ret = false;
            }
        }
    }
    // CERTIFICATION FILES END

    return ret;
}

function removeMachineries(caller) {
    $(caller).parent().parent().remove();
}
function addMachineries(caller) {
    var content =
        "<tr>"+
            "<td><input type=\"text\" class=\"form-control\" name=\"txtMachineriesName\" /></td>" +
            "<td><input type=\"text\" class=\"form-control\" name=\"txtMachineriesBrand\" /></td>" +
            "<td><input type=\"text\" class=\"form-control\" name=\"txtMachineriesOrigin\" /></td>" +
            "<td><input type=\"text\" class=\"form-control\" name=\"txtMachineriesPurpose\" /></td>" +
            "<td><input type=\"text\" class=\"form-control\" name=\"txtMachineriesQuantity\" /></td>" +
            "<td><input type=\"text\" class=\"form-control\" name=\"txtMachineriesUOM\" /></td>" +
            "<td><input type=\"button\" class=\"btn btn-primary\" onclick=\"addMachineries(this); return false;\" value=\"ADD MORE\" /></td>"+
        "</tr>";
    $(caller).val("REMOVE");
    $(caller).removeClass("btnMachineriesAdd");
    $(caller).addClass("btnMachineriesRemove");
    $(caller).attr("onclick", "removeMachineries(this);return false;");
    $("#machineries").append(content);
}
function regexPattern(type, id) {
    if (type == "alphabet&number") {
        $(id).keyup(function () {
            var newValue = $(this).val().replace(/[^a-zA-Z0-9_ ]*$/, '');
            $(this).val(newValue);
        });
    }
    if (type == "number") {
        $(id).keyup(function () {
            var newValue = $(this).val().replace(/[^0-9_ ]*$/, '');
            $(this).val(newValue);
        });
    }
    if (type == "alphabet") {
        $(id).keyup(function () {
            var newValue = $(this).val().replace(/[^a-zA-Z_ ]*$/, '');
            $(this).val(newValue);
        });
    }
    if (type == "date") {
        $(id).keyup(function () {
            var newValue = $(this).val().replace(/[^0-9_\/\-]*$/, '');
            $(this).val(newValue);
        });
    }
    if (type == "address") {
        $(id).keyup(function () {
            var newValue = $(this).val().replace(/[^a-zA-Z0-9_ #\/\-]*$/, '');
            $(this).val(newValue);
        });
    }
    if (type == "email") {
        $(id).keyup(function validateEmail() {
            var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if ($(this).val() == '' || !re.test($(this).val())) {
                $(this).addClass("alert-danger");
                $(this).next().text("Enter Valid Email.");
                $(this).next().addClass("alert-warning");
            }
            else {
                $(this).removeClass("alert-danger");
                $(this).next().text("*");
                //$(this).next().removeClass("alert-warning");
            }
        });
    }
}

function removeAttachmentCertification(caller) {
    $(caller).parent().parent().remove();
}
function addAttachmentCertification(caller) {
    var content =
        "<tr>" +
            "<td><input type=\"text\" class=\"form-control\" name=\"txtattachmentCertificationName\" /></td>" +
            "<td><input type=\"file\" class=\"btn btn-default btn-file\" name=\"attachmentCertificationFile\" data-show-preview=\"false\" style=\"padding: 2px; text-align:left; max-width: none; width: 100% !important;\" multiple accept=\".jpeg,.png,.jpg\" /></td>" +
            "<td class=\"TextCenter\"><span class=\"ValidationOK errAttachmentCertificationFile\">*</span></td>" +
            "<td class=\"TextCenter\"><input type=\"button\" class=\"btn btn-primary btnContractsRemove\" onclick=\"removeAttachmentCertification(this); return false;\"  value=\"REMOVE\" /></td>" +
        "</tr>";
    $("#attachmentCertification").append(content);
}

function removeIntContacts(caller) {
    $(caller).parent().parent().remove();
}
function addIntContacts(caller) {
    var content =
        "<tr>" +
            "<td><input type=\"text\" class=\"form-control\" name=\"txtContractsOrgName\" /></td>" +
            "<td><input type=\"text\" class=\"form-control\" name=\"txtContractsValue\" /></td>" +
            "<td><input type=\"text\" class=\"form-control\" name=\"txtContractsYear\" /></td>" +
            "<td><input type=\"text\" class=\"form-control\" name=\"txtContractsProducts\" /></td>" +
            "<td><input type=\"text\" class=\"form-control\" name=\"txtContractsDestination\" /></td>" +
            "<td><input type=\"button\" class=\"btn btn-primary btnContractsAdd\" value=\"ADD MORE\" onclick=\"addIntContacts(this); return false;\" /></td>" +
        "</tr>";
    $(caller).val("REMOVE");
    $(caller).removeClass("btnContractsAdd");
    $(caller).addClass("btnContractsRemove");
    $(caller).attr("onclick", "removeIntContacts(this);return false;");
    $("#intContacts").append(content);
}

function removeEmployee(caller) {
    $(caller).parent().parent().remove();
}
function addEmployee(caller) {
    var content =
        "<tr>" +
            "<td><input type=\"text\" class=\"form-control\" name=\"txtEmpExpArea\" /></td>" +
            "<td><input type=\"text\" class=\"form-control\" name=\"txtEmpNumber\" /></td>" +
            "<td><input type=\"text\" class=\"form-control\" name=\"txtEmpRemarks\" /></td>" +
            "<td><input type=\"button\" class=\"btn btn-primary btnEmpAdd\" value=\"ADD MORE\" onclick=\"addEmployee(this);return false;\" /></td>" +
        "</tr>";
    $(caller).val("REMOVE");
    $(caller).removeClass("btnEmpAdd");
    $(caller).addClass("btnEmpRemove");
    $(caller).attr("onclick", "removeEmployee(this);return false;");
    $("#empInfo").append(content);
}

function removeProducts(caller) {
    $(caller).parent().parent().remove();
}
function addProducts(caller) {
    var content =
        '<tr>'+
            '<td><input type="text" class="form-control" name="txtProduct" /></td>' +
            '<td><input type="text" class="form-control" name="txtDescription" /></td>' +
            '<td class="text-center">' +
                '<div class="btn-group" data-toggle="buttons"> ' +
                    '<label class="btn btn-default active"> ' +
                        '<input type="radio" name="product" autocomplete="off" checked>Product ' +
                    '</label> ' +
                    '<label class="btn btn-default"> ' +
                        '<input type="radio" name="service" autocomplete="off"> Service ' +
                    '</label> ' +
                '</div> ' +
            '</td> ' +
            '<td><input type="file" class="btn btn-default btn-file" data-show-preview="false" style="padding: 2px; text-align:right;" accept=".jpeg,.png,.jpg,.pdf" /></td> ' +
            '<td><input type="button" class="btn btn-primary btnProductAdd" value="ADD MORE" onclick="addProducts(this);return false;" /></td>' +
        '</tr>';
    $(caller).val("REMOVE");
    $(caller).removeClass("btnProductAdd");
    $(caller).addClass("btnProductRemove");
    $(caller).attr("onclick", "removeProducts(this);return false;");
    $("#productsInfo").append(content);
}

function processForm() {
    if (validateAll()) {
        var fd = new FormData();

        //// BIND IMAGES START
        // Append ANNUAL REPORT IMAGES
        var annualReport = $('#fupAnnualReport');
        for (var i = 0; i < annualReport.length; i++) {
            if (annualReport[i].files.length > 0) {
                for (var j = 0; j < annualReport[i].files[j].length; j++) {
                    fd.append("annualReport_" + i + "-" + annualReport[i].files[i].name.replace(/ /g, ''), annualReport[i].files[j]);
                }
            }
        }

        //// Append PRODUCTS IMAGES
        //var p = $("#productsInfo tr");
        //for (var i = 0; i < p.length; i++) {
        //    f = $(p[i]).find("input:file");
        //    if (f[0].files.length > 0) {
        //        fd.append("product_" + $(p[i]).children().first().find("input:text").val() + "_" + i + "-" + f[0].files[0].name, f[0].files[0]);
        //    }
        //}

        // Append VENDOR_CERTIFICATION IMAGES
        var p = $("#attachmentCertification tr");
        for (var i = 0; i < p.length; i++) {
            f = $(p[i]).find("input:file");
            for (var j = 0; j < f[0].files.length > 0; j++) {
                if (f[0].files.length > 0) {
                    fd.append("cer_" + $(p[i]).children().first().find("input:text").val().replace(/ /g, '') + "_" + i + "-" + f[0].files[j].name.replace(/ /g, ''), f[0].files[j]);
                }
            }
        }
        //// BIND IMAGES END

        var other_data = $('form').serializeArray();
        $.each(other_data, function (key, input) {
            fd.append(input.name, input.value);
        });

        var tradeLicense = $("#txtTradeLicense").val();
        fd.append("tradeLicense", tradeLicense);

        var tinNo = $("#txtTinNo").val();
        fd.append("tinNo", tinNo);

        var telNo = $("#txtTelNo").val();
        fd.append("telNo", telNo);

        var cellNo = $("#txtCellNo").val();
        fd.append("cellNo", cellNo);

        var faxNo = $("#txtFaxNo").val();
        fd.append("faxNo", faxNo);

        var email = $("#txtEmail").val();
        fd.append("email", email);

        var orgType = $("#ddlOrgType").multipleSelect("getSelects");
        fd.append("orgType", orgType);

        var orgCategory = $("#ddlOrgCategory").multipleSelect("getSelects")
        fd.append("orgCategory", orgCategory);

        var yoe = $("#txtYOE").val();
        fd.append("yoe", yoe);

        var totalEmployee = $("#txtTTLEmployeeNo").val();
        fd.append("totalEmployee", totalEmployee);

        // TABLE: VENDOR_EMPLOYEE_INFORMATION
        // DATAGET: #EmpInfo
        var ei = $("#empInfo tr");
        var empCount = ei.length;
        fd.append("expEmpCount", empCount);

        for (var i = 0; i < ei.length; i++) {
            //console.log($(ei[i]).children());
            for (var j = 0; j < $(ei[i]).children().length; j++) {
                if (!($($(ei[i]).children()[j]).children().hasClass("btn"))) {
                    //console.log($($(ei[i]).children()[j]).children().val());
                    if (j == 0) {
                        fd.append("expEmpArea" + i, $($(ei[i]).children()[j]).children().val());
                    }
                    if (j == 1) {
                        fd.append("expEmpNumber" + i, $($(ei[i]).children()[j]).children().val());
                    }
                    if (j == 2) {
                        fd.append("expEmpRemarks" + i, $($(ei[i]).children()[j]).children().val());
                    }
                }
            }
        }

        var annualSalesY1 = $("#aSYear1").val();
        fd.append("annualSalesY1", annualSalesY1);

        var annualSalesY1Amnt = $("#aSYear1Amnt").val();
        fd.append("annualSalesY1Amnt", annualSalesY1Amnt);

        var annualSalesY2 = $("#aSYear2").val();
        fd.append("annualSalesY2", annualSalesY2);

        var annualSalesY2Amnt = $("#aSYear2Amnt").val();
        fd.append("annualSalesY2Amnt", annualSalesY2Amnt);

        var annualSalesY3 = $("#aSYear3").val();
        fd.append("annualSalesY3", annualSalesY3);

        var annualSalesY3Amnt = $("#aSYear3Amnt").val();
        fd.append("annualSalesY3Amnt", annualSalesY3Amnt);

        var bankName = $("#txtBankName").val();
        fd.append("bankName", bankName);

        var bankAddress = $("#txtBankAddress").val();
        fd.append("bankAddress", bankAddress);

        var bankAccountNumber = $("#txtBankAccountNumber").val();
        fd.append("bankAccountNumber", bankAccountNumber);

        var bankAccountName = $("#txtBankAccountName").val();
        fd.append("bankAccountName", bankAccountName);

        var bankSwiftCode = $("#txtBankSwiftCode").val();
        fd.append("bankSwiftCode", bankSwiftCode);

        var bankRoutingNo = $("#txtBankRoutingNo").val();
        fd.append("bankRoutingNo", bankRoutingNo);

        // FILE UPLOAD Financial Audit Report

        // // var qac = $("#txtQAC").val();
        var listOfc = $("#txtListOfc").val();
        fd.append("listOfc", listOfc);

        var listImportCountries = $("#txtListImportCountries").val();
        fd.append("listImportCountries", listImportCountries);

        //// TABLE: PRODUCT_MAP
        //// DATA GET: #productsInfo
        //var p = $("#productsInfo tr");
        //var productsCounter = p.length;
        //fd.append("productsCounter", productsCounter);

        //for (var i = 0; i < p.length; i++) {
        //    var j = 0;
        //    $(p[i]).find('td').each(function () {
        //        if (!($($(p[i]).children()[j]).children().hasClass("btn"))) {
        //            if (j == 0) {
        //                fd.append("productName" + i, $(this).find("input").val());
        //            }
        //            if (j == 1) {
        //                fd.append("productDescripton" + i, $(this).find("input").val());
        //            }
        //            if (j == 2) {
        //                fd.append("productType" + i, $(this).find("input").parent().parent().find(".active").children().attr("name"));
        //            }
        //            j++;
        //        }
        //    });
        //}

        // TABLE: CONTRACT
        // DATA GET: #intContacts
        var ic = $("#intContacts tr");
        var icCount = ic.length;
        fd.append("intContactsCount", icCount);

        for (var i = 0; i < ic.length; i++) {
            //console.log($(ic[i]).children());
            for (var j = 0; j < $(ic[i]).children().length; j++) {
                if (!($($(ic[i]).children()[j]).children().hasClass("btn"))) {
                    if (j == 0) {
                        fd.append("intContactsOrg" + i, $($(ic[i]).children()[j]).children().val());
                    }
                    if (j == 1) {
                        fd.append("intContactsVal" + i, $($(ic[i]).children()[j]).children().val());
                    }
                    if (j == 2) {
                        fd.append("intContactsYr" + i, $($(ic[i]).children()[j]).children().val());
                    }
                    if (j == 3) {
                        fd.append("intContactsGoods" + i, $($(ic[i]).children()[j]).children().val());
                    }
                    if (j == 4) {
                        fd.append("intContactsDestination" + i, $($(ic[i]).children()[j]).children().val());
                    }
                }
            }
        }

        // TABLE: VENDOR_CERTIFICATION
        // DATA GET: #attachmentCertification
        var attachment = $("#attachmentCertification tr");
        var attachmentCount = attachment.length;
        fd.append("attachmentCount", attachmentCount);
        for (var i = 0; i < attachment.length; i++) {
            //console.log($(attachment[i]).children());
            for (var j = 0; j < $(attachment[i]).children().length; j++) {
                if (j == 0) {
                    fd.append("attachmentName" + i, $($(attachment[i]).children()[j]).children().attr("value"));
                }
            }
        }

        // TABLE: Machineries
        // DATA GET: #machineries
        var machine = $("#machineries tr");
        var machineCount = machine.length;
        fd.append("machineCount", machineCount);
        for (var i = 0; i < machine.length; i++) {
            //console.log($(machine[i]).children());
            for (var j = 0; j < $(machine[i]).children().length; j++) {
                if (!($($(machine[i]).children()[j]).children().hasClass("btn"))) {
                    if (j == 0) {
                        fd.append("machineName" + i, $($(machine[i]).children()[j]).children().val());
                    }
                    if (j == 1) {
                        fd.append("machineBrand" + i, $($(machine[i]).children()[j]).children().val());
                    }
                    if (j == 2) {
                        fd.append("machineOrigin" + i, $($(machine[i]).children()[j]).children().val());
                    }
                    if (j == 3) {
                        fd.append("machinePurpose" + i, $($(machine[i]).children()[j]).children().val());
                    }
                    if (j == 4) {
                        fd.append("machineQuantity" + i, $($(machine[i]).children()[j]).children().val());
                    }
                    if (j == 5) {
                        fd.append("machineUOM" + i, $($(machine[i]).children()[j]).children().val());
                    }
                }
            }
        }

        $.ajax({
            url: 'Submit',
            data: fd,
            contentType: false,
            processData: false,
            type: 'POST',
            success: function (data) {
                console.log(data);
                window.location = "Success";
                return false;
            }
        });
    }
    return false;
}


$(document).ready(function () {
    $("#ddlOrgType").multipleSelect({
        onOpen: function () {
            $("#errOrgType").text("*");
            $("#txtOrgType").removeClass("alert-danger");
        },
        onClose: function () {
            if ($("#ddlOrgType").multipleSelect("getSelects") == "") {
                $("#errOrgType").text("This Field Is Required.");
                $("#txtOrgType").addClass("alert-danger");
            }
            else {
                $("#errOrgType").text("*");
                $("#txtOrgType").removeClass("alert-danger");
            }
        }
    });
    $("#bTypeAdd").click(function () {
        var $select = $("#ddlOrgType"),
            $input = $("#txtOrgType"),
            $selected = $("#refreshSelected"),
            value = $.trim($input.val()),
            $opt = $("<option />", {
                value: value,
                text: " " + value
            });
        if (!value) {
            $input.focus();
            return;
        }
        $opt.prop("selected", true);
        $input.val("");
        $select.append($opt).multipleSelect("refresh");
    });

    $("#ddlOrgCategory").multipleSelect({
        onOpen: function () {
            $("#errOrgCategory").text("*");
        },
        onClose: function () {
            if ($("#ddlOrgCategory").multipleSelect("getSelects") == "") {
                $("#errOrgCategory").text("This Field Is Required.");
            }
            else {
                $("#errOrgCategory").text("*");
            }
        }
    });

    $("#txtTradeLicense").focusout(function () { validationOnFly("id", this, "id", $(this).next()); });
    $("#txtTinNo").focusout(function () { validationOnFly("id", this, "id", $(this).next()); });
    $("#txtTelNo").focusout(function () { validationOnFly("id", this, "id", $(this).next()); });
    $("#txtCellNo").focusout(function () { validationOnFly("id", this, "id", $(this).next()); });
    regexPattern("date", "#txtYOE");
    $("#txtYOE").datepicker({
        format: "dd/mm/yyyy"
    });
    $("#txtYOE").focusout(function () { validationOnFly("id", this, "id", $(this).next()); });
    regexPattern("alphabet", "#txtOrgType");
    $("#ddlOrgCategory").focusout(function () {
        if ($("#ddlOrgCategory").multipleSelect("getSelects") == "") {
            $("#errOrgCategory").text("This Field Is Required.");
        }
        else {;
            $("#errOrgCategory").text("*");
        }
    });
    regexPattern("number", "#txtTTLEmployeeNo");
    var exp = $("input[name='txtEmpNumber']");
    for (var i = 0; i < exp.length; i++) {
        $(exp[i]).keyup(function () {
            var newValue = $(this).val().replace(/[^0-9_ ]*$/, '');
            $(this).val(newValue);
        });
    }
    regexPattern("number", "#aSYear1");
    regexPattern("number", "#aSYear2");
    regexPattern("number", "#aSYear3");
    regexPattern("number", "#aSYear1Amnt");
    regexPattern("number", "#aSYear2Amnt");
    regexPattern("number", "#aSYear3Amnt");
    $("#txtBankName").focusout(function () { validationOnFly("id", this, "id", $(this).next()); });
    $("#txtBankAccountNumber").focusout(function () { validationOnFly("id", this, "id", $(this).next()); });
});