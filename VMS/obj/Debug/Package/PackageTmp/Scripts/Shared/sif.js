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

function removeProducts(caller) {
    $(caller).parent().parent().remove();
}

function addProducts(caller) {
    $(caller).removeClass("Btn-Custom-Add");
    var content =
        "<div class=\"col-md-12\">" +
            "<div class=\"col-xs-1\" style=\"padding: 3px 0 3px 0;\">" +
                "<span class=\"input-group-addon borderMod\" style=\"white-space: pre-wrap;\">NAME</span>" +
            "</div>" +
            "<div class=\"col-xs-3\" style=\"padding: 0px;\">" +
                "<div class=\"input-group\">" +
                    "<input autofocus class=\"col-sm-4 form-control products\" type=\"text\" onblur=\"validationOnFlyCustom(this, $(this).next()); return false;\" />" +
                    "<span class=\"input-group-addon alert-warning ValidationOK errRroducts\">*</span>" +
                "</div>" +
            "</div>" +
            "<div class=\"col-xs-2\" style=\"padding: 0px; text-align:center;\">" +
                "<div class=\"btn-group\" data-toggle=\"buttons\">" +
                    "<label class=\"btn btn-default active\">" +
                        "<input type=\"radio\" name=\"options\" id=\"product\" autocomplete=\"off\" checked>Product " +
                    "</label> " +
                    "<label class=\"btn btn-default\"> " +
                        "<input type=\"radio\" name=\"options\" id=\"service\" autocomplete=\"off\"> Service " +
                    "</label> " +
                "</div> " +
            "</div> " +
            "<div class=\"col-xs-5\">" +
                "<div class=\"col-xs-3\" style=\"font-size: smaller; text-align: center;\">CATALOGUE (OPTIONAL)</div>" +
                "<div class=\"col-xs-9\">" +
                    "<input type=\"file\" class=\"btn btn-default btn-file\" data-show-preview=\"false\" style=\"padding: 2px; text-align:right\" multiple accept=\".jpeg,.png,.jpg\" />" +
                "</div>" +
            "</div>" +
            "<div class=\"col-xs-1 pull-right\" style=\"text-align:center;\">" +
                "<button class=\"btn btn-primary Btn-Custom-Add \" onclick=\"addProducts(this);return false;\">ADD MORE</button>" +
            "</div>" +
        "</div>";
    $(caller).text(' REMOVE ');
    $(caller).removeClass("Btn-Custom-Add");
    $(caller).addClass("Btn-Custom-Remove");
    $(caller).attr("onclick", "removeProducts(this)");
    $("#productsContainer").append(content);
}

function validateFields() {
    if ($("form").find(".alert-danger").length > 0) {
        $("html, body").animate({ scrollTop: $($("form").find(".alert-danger").first()).offset().top - 60 }, "slow");
        return false;
    }
    else {
        return true;
    }
}

function getDesignation(id) {
    return $("#" + id).find(".active").find("input").val().trim();
}

function openNav() {
    $("html, body").animate({ scrollTop: 0 }, "slow");
    $("#msg").text("");
    $("#msg").append("<h2>PLEASE WAIT</h2><h4>SUBMITTING DATA</h4><br /><br /><h4>DO NOT CLOSE INTERNET CONNECTION!</h4>");

    document.getElementById("overlay").style.width = "100%";
    document.getElementById("overlay").style.height = "100%";
    document.getElementById('overlay').style.position = "absolute";
    document.getElementById("overlay").style.zIndex = "2000";
}

function closeNav() {
    document.getElementById("overlay").style.width = "0%";
    document.getElementById("overlay").style.display = "none";
}

function send() {
    if (validateFields()) {
        openNav();
        var
            orgName = $('#txtOrgName').val(),
            orgType = $("#ddlOrgType").multipleSelect("getSelects"),
            orgDOE = $('#txtOrgDOE').val(),
            orgEmail = $('#txtOrgEmail').val(),

            orgHOAStreet = $("#txtOrgHOAStreet").val(),
            orgHOACity = $("#txtOrgHOACity").val(),
            orgHOAThana = $("#txtOrgHOAThana").val(),
            orgHOACountry = $("#txtOrgHOACountry").val(),

            orgSOAStreet = $("#txtOrgSOAStreet").val(),
            orgSOACity = $("#txtOrgSOACity").val(),
            orgSOAThana = $("#txtOrgSOAThana").val(),
            orgSOACountry = $("#txtOrgSOACountry").val(),

            orgFAStreet = $("#txtOrgFAStreet").val(),
            orgFACity = $("#txtOrgFACity").val(),
            orgFAThana = $("#txtOrgFAThana").val(),
            orgFACountry = $("#txtOrgFACountry").val(),

            orgWHDAStreet = $("#txtOrgWHDAStreet").val(),
            orgWHDACity = $("#txtOrgWHDACity").val(),
            orgWHDAThana = $("#txtOrgWHDAThana").val(),
            orgWHDACountry = $("#txtOrgWHDACountry").val(),

            orgContactPrimaryDesig = getDesignation("contactHeadDesignation"),
            orgContactPrimaryName = $("#txtOrgContactPrimaryName").val(),
            orgContactPrimaryCell = $("#txtOrgContactPrimaryPhone").val(),
            orgContactPrimaryEmail = $("#txtOrgContactPrimaryEmail").val(),

            orgContactSecondaryDesig = getDesignation("contactSecondaryDesignation"),
            orgContactSecondaryName = $("#txtOrgContactSecondaryName").val(),
            orgContactSecondaryCell = $("#txtOrgContactSecondaryPhone").val(),
            orgContactSecondaryEmail = $("#txtOrgContactSecondaryEmail").val(),

            orgContactRepresentativeDesig = getDesignation("contactRepresentativeDesignation"),
            orgContactRepresentativeName = $("#txtOrgContactRepresentativeName").val(),
            orgContactRepresentativePhone = $("#txtOrgContactRepresentativePhone").val(),
            orgContactRepresentativeEmail = $("#txtOrgContactRepresentativeEmail").val(),

            orgWebsite = $("#txtOrgWebsite").val();

        var fd = new FormData();

        var products = $("#productsContainer").find('input[type="file"]');

        for (var i = 0; i < products.length; i++) {
            for (var j = 0; j < products[i].files.length; j++) {
                fd.append($(products[i]).parent().parent().prev().prev().first().children().children().first().val().replace(/ /g, '') + "_proFile_" + i, products[i].files[j]);
            }
        }
        if ($("#fupOrgContactRepresentative")[0].files.length > 0) {
            //fd.append($("#txtOrgName").val() + "_vCard_" + $("#txtOrgContactRepresentativeName").val(), $("#fupOrgContactRepresentative")[0].files[0]);
            fd.append($("#txtOrgContactRepresentativeName").val().replace(/ /g, '') + "_vCard", $("#fupOrgContactRepresentative")[0].files[0]);
        }
        var other_data = $('form').serializeArray();
        $.each(other_data, function (key, input) {
            fd.append(input.name, input.value);
        });

        fd.append("orgname", orgName);
        fd.append("orgtype", orgType);
        fd.append("orgdoe", orgDOE);
        fd.append("orgemail", orgEmail);
        fd.append("orghoastreet", orgHOAStreet);
        fd.append("orghoacity", orgHOACity);
        fd.append("orghoathana", orgHOAThana);
        fd.append("orghoacountry", orgHOACountry);
        fd.append("orgsoastreet", orgSOAStreet);
        fd.append("orgsoacity", orgSOACity);
        fd.append("orgsoathana", orgSOAThana);
        fd.append("orgsoacountry", orgSOACountry);
        fd.append("orgfastreet", orgFAStreet);
        fd.append("orgfacity", orgFACity);
        fd.append("orgfathana", orgFAThana);
        fd.append("orgfacountry", orgFACountry);
        fd.append("orgwhdastreet", orgWHDAStreet);
        fd.append("orgwhdacity", orgWHDACity);
        fd.append("orgwhdathana", orgWHDAThana);
        fd.append("orgwhdacountry", orgWHDACountry);
        fd.append("orgcontactprimarydesig", orgContactPrimaryDesig);
        fd.append("orgcontactprimaryname", orgContactPrimaryName);
        fd.append("orgcontactprimarycell", orgContactPrimaryCell);
        fd.append("orgcontactprimaryemail", orgContactPrimaryEmail);
        fd.append("orgcontactsecondarydesig", orgContactSecondaryDesig);
        fd.append("orgcontactsecondaryname", orgContactSecondaryName);
        fd.append("orgcontactsecondarycell", orgContactSecondaryCell);
        fd.append("orgcontactsecondaryemail", orgContactSecondaryEmail);
        fd.append("orgcontactrepresentativedesig", orgContactRepresentativeDesig);
        fd.append("orgcontactrepresentativename", orgContactRepresentativeName);
        fd.append("orgcontactrepresentativephone", orgContactRepresentativePhone);
        fd.append("orgcontactrepresentativeemail", orgContactRepresentativeEmail);
        fd.append("orgwebsite", orgWebsite);
        var products = $("#productsContainer").find("input:text");

        for (var i = 0; i < products.length; i++) {
            fd.append("product" + i, ($(products[i]).val()));
            fd.append("productType" + i, $(products[i]).parent().parent().next().find(".active").text().trim());
        }

        $.ajax({
            url: '/vms/sif/SubmitAction',
            //url: '/sif/SubmitAction',
            data: fd,
            contentType: false,
            processData: false,
            type: 'POST',
            success: function (data) {
                if (data.response == "success") {
                    window.location = "/vms/sif/Success";
                    return true;
                }
                else if (data.response == "email") {
                    $("#msg").text("");
                    $("#msg").append("<h2>ERROR</h2></br><h4>Company email address already exists.</h4><br /><h3>Contact administrator for details.</h3>");
                    return false;
                }
                else {
                    $("#msg").text("");
                    $("#msg").append("<h2>ERROR</h2></br><h4>This company name is already registered.</h4><br /><h3>Contact administrator for details.</h3>");
                    return false;
                }
            }
            , error: function () {
                $("#msg").text("");
                $("#msg").append("<h2>ERROR</h2></br><h4>Please provide all the information and try again.</h4>");
            }
        });
        return false;
        closeNav();
    }
    return false;
}

$(document).ready(function () {
    //working fine
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
        $("#errOrgType").text("*");
        $("#txtOrgType").removeClass("alert-danger");
        $select.append($opt).multipleSelect("refresh");
    });

    $("#txtOrgName").focus();
    regexPattern("alphabet", "#txtOrgName");
    $("#txtOrgName").focusout(function () { validationOnFly("id", this, "id", $(this).next()); });
    regexPattern("alphabet", "#txtOrgType");
    $("#txtOrgType").focusout(function () {
        if ($("#ddlOrgType").multipleSelect("getSelects") == "") {
            $("#errOrgType").text("This Field Is Required.");
            $("#txtOrgType").addClass("alert-danger");
        }
        else {
            $("#errOrgType").text("*");
            $("#txtOrgType").removeClass("alert-danger");
        }
    });
    regexPattern("date", "#txtOrgDOE");
    $("#txtOrgDOE").datepicker({
        format: "dd/mm/yyyy"
    });
    $("#txtOrgDOE").focusout(function () { validationOnFly("id", this, "id", $(this).next()); });
    regexPattern("email", "#txtOrgEmail");
    $("#txtOrgEmail").focusout(function () { if (($(this).attr("class")).indexOf("danger") !== -1) { } else { validationOnFly("id", this, "id", $(this).next()); } });

    regexPattern("address", "#txtOrgHOAStreet");
    $("#txtOrgHOAStreet").focusout(function () { validationOnFly("id", this, "id", "errOrgHOAStreet"); });
    regexPattern("alphabet&number", "#txtOrgHOACity");
    $("#txtOrgHOACity").focusout(function () { validationOnFly("id", this, "id", "errOrgHOAStreet"); });
    regexPattern("alphabet", "#txtOrgHOAThana");
    $("#txtOrgHOAThana").focusout(function () { validationOnFly("id", this, "id", "errOrgHOAStreet"); });
    regexPattern("alphabet", "#txtOrgHOACountry");
    $("#txtOrgHOACountry").focusout(function () { validationOnFly("id", this, "id", "errOrgHOAStreet"); });
    
    regexPattern("alphabet", "#txtOrgContactPrimaryName");
    $("#txtOrgContactPrimaryName").focusout(function () { validationOnFly("id", this, "id", $(this).next()); });
    regexPattern("number", "#txtOrgContactPrimaryPhone");
    $("#txtOrgContactPrimaryPhone").focusout(function () { validationOnFly("id", this, "id", $(this).next()); });
    regexPattern("email", "#txtOrgContactPrimaryEmail");
    $("#txtOrgContactPrimaryEmail").focusout(function () { if (($(this).attr("class")).indexOf("danger") !== -1) { } else { validationOnFly("id", this, "id", $(this).next()); } });
    
    regexPattern("alphabet", "#txtOrgContactRepresentativeName");
    $("#txtOrgContactRepresentativeName").focusout(function () { validationOnFly("id", this, "id", $(this).next()); });
    regexPattern("number", "#txtOrgContactRepresentativePhone");
    $("#txtOrgContactRepresentativePhone").focusout(function () { validationOnFly("id", this, "id", $(this).next()); });
    regexPattern("email", "#txtOrgContactRepresentativeEmail");
    $("#txtOrgContactRepresentativeEmail").focusout(function () { if (($(this).attr("class")).indexOf("danger") !== -1) { } else { validationOnFly("id", this, "id", $(this).next()); } });
});







function submitt() {
    var orgName = $('#txtOrgName').val();
    var orgType = $("#ddlOrgType").multipleSelect("getSelects");

    var fd = new FormData();

    var file_data = $('input[type="file"]')[0].files; // for multiple files
    for (var i = 0; i < file_data.length; i++) {
        fd.append("file_" + i, file_data[i]);
    }
    var other_data = $('form').serializeArray();
    $.each(other_data, function (key, input) {
        fd.append(input.name, input.value);
    });
    fd.append("orgname", orgName);
    fd.append("orgType", orgType);
    $.ajax({
        url: '/sif/SubmitAction',
        data: fd,
        contentType: false,
        processData: false,
        type: 'POST',
        success: function (data) {
            console.log(data);
        }
    });
    return false;
}

function fieldValidation(fieldElementId, errorElementId) {
    if ($("#" + fieldElementId).val().trim() == "") {
        $("#" + errorElementId).text("This Field Is Required.");
        $("html, body").animate({ scrollTop: $("#" + fieldElementId).offset().top - 60 }, "slow");
        $("#" + fieldElementId).addClass("alert-danger");
    }
    else {
        $("#" + errorElementId).text("*");
        $("#" + fieldElementId).removeClass("alert-danger");
    }
}

function fieldValidationDDL() {
    if ($("#ddlOrgType").multipleSelect("getSelects") == "") {
        $("#errOrgType").text("This Field Is Required.");
        $("html, body").animate({ scrollTop: $("#ddlOrgType").offset().top - 60 }, "slow");
        $("#txtOrgType").addClass("alert-danger");
    }
    else {
        $("#errOrgType").text("*");
        $("#errOrgType").removeClass("alert-danger");
        $("#txtOrgType").removeClass("alert-danger");
    }
}

function PostData(URL, Data, Type) {
    $.ajax
    ({
        type: "POST",
        url: URL,
        data: JSON.stringify(Data),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (Type == "login") {
                var json_obj = $.parseJSON(result.d);
                if (json_obj["response"] == true) { redirect(json_obj["red"]); }
                else
                { $("#log_username_span").append("Wrong Username or Password").css("color", "red").css("font-weight", "bold").focus(true); }
            }
            if (Type == "create") {
                if (result.d == true) { $("#reg_span_username").append("Registration Successful. Please verify your email.").css("color", "green").css("font-weight", "bold").focus(true); }
                else
                { $("#reg_span_username").append("<br />Sorry this Email is already registered.").css("color", "red").css("font-weight", "bold").focus(true); }
            }
            if (Type == "email") {
                if (result.d == false) { $("#reg_span_email1").append("<br />This email address is already registered").css("font-weight", "bold").css("color", "red"); }
            }
        },
        error: function (err) {
        }
    });
}

//$(document).ready(function () {
//var i = 0, j = 0;
//var products;
//var div = $("#productsContainer");
//$(div).find("input:text, input:file")
//  .each(function () {
//      if ($(this).attr('class').includes("form-control")) {
//          products[i][j] = $(this).val();
//          j++;
//      }
//      if ($(this).attr('class').includes("btn-file")) {
//          products[i][j] = $(this).val();
//          j++;
//      }
//      i++;
//      j = 0;
//  });
//console.log(products.length);
//for (var x = 0; x < products.length; x++) {
//    console.log(products[x] + " " + i);
//}
//console.log(i);
//console.log(products[0][0] "-" i);

//working fine
//$('#txtOrgDOE').datepicker()
//$("#txtOrgName").on("focusout", function () { fieldValidation("txtOrgName", "errOrgName"); });
//$("#errOrgType").on("focusout", fieldValidationDDL);
//$("#txtOrgType").on("focusout", fieldValidationDDL);
//$(".ms-drop").on("focusout", function () {
//    if ($("#ddlOrgType").multipleSelect("getSelects") == "") {
//        $("#errOrgType").text("This Field Is Required.");
//        $("html, body").animate({ scrollTop: $("#ddlOrgType").offset().top - 60 }, "slow");
//        $("#txtOrgType").addClass("alert-danger");
//    }
//    else {
//        $("#errOrgType").text("*");
//        $("#errOrgType").removeClass("alert-danger");
//        $("#txtOrgType").removeClass("alert-danger");
//    }
//})
//$("#txtOrgDOE").on("focusout", function () { fieldValidation("txtOrgDOE", "errOrgDOE"); });
//});

//var div = $("#productsContainer");
//var pro = $(div).find("input:text, input:file");

//for (var i = 0; i < pro.length; i++) {
//    if ($(pro[i]).attr('class').includes("form-control")) {
//        console.log("pName:" + pro[i].value);
//        products[0].push("asdad")
//    }
//    else if ($(pro[i]).attr('class').includes("btn-file")) {
//        for (var j = 0; j < pro[i].files.length; j++) {
//            // append for multiple files in one file uploader
//            console.log("pFile:" + pro[i].files);
//        }
//    }
//}
//console.log(products[0][0]);

//var i = 0, j = 0;
//var products;
//var div = $("#productsContainer");
//$(div).find("input:text, input:file").each(function () {
//    if ($(this).attr('class').includes("form-control")) {
//        products[i][j] = $(this).val();
//        j++;
//    }
//    if ($(this).attr('class').includes("btn-file")) {
//        products[i][j] = $(this).val();
//        j++;
//    }
//    i++;
//    j = 0;
//});
//console.log(oroducts);
//console.log(orgName);
//var params = { email: $('#email').val() };
//var url = "login.aspx/CheckEmail";
//PostData(url, params, "email");