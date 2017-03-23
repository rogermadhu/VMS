$(document).ready(function () {
    $("#log_username").focus();
    $("#log_username").focusout(function () {
        if ($("#log_username").val().length > 0) {
            $("#errLogin").text("*");
            bool = true;
        }
        else {
            $("#errLogin").text("Username Required");
            bool = false;
        }
    });
    
    $("#log_password").focusout(function () {
        if ($("#log_password").val().length > 0) {
            $("#errPassword").text("*");
            bool = true;
        }
        else {
            $("#errPassword").text("Password Required");
            bool = false;
        }
    });
});

$("#submit").on("click", function () {
    if (validateFields()) {
        $.ajax({
            type: "POST",
            url: "login/login",
            data: JSON.stringify({ un: $('#log_username').val(), pw: $("#log_password").val() }),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result == 'FALSE') {
                    $("#msg").text("USERNAME OR PASSWORD INCORRECT");
                }
                else if (result == 'PENDING') {
                    $("#msg").text("WE ARE CHECKING YOUR SIF FORM. WAIT FOR OUR CONFIRMATION EMAIL.");
                }
                else
                {
                    window.location = result;
                }
            },
            error: function (err) {
            }
        });
    }
});

function validateFields() {
    var bool = false;

    if ($("#log_username").val().length > 0) {
        $("#errLogin").text("*");
        bool = true;
    }
    else {
        $("#errLogin").text("Username Required");
        bool = false;
    }

    if ($("#log_password").val().length > 0) {
        $("#errPassword").text("*");
        bool = true;
    }
    else {
        $("#errPassword").text("Password Required");
        bool = false;
    }

    return bool;
}


$("#btn_login").on("click", function () {
    ClearLoginText();
    if (ValidateLoginFields()) {
        var params = { un: $('#log_username').val(), pw: $("#log_password").val() };
        var url = "login.aspx/LoginValidate";
        PostData(url, params, "login");
    }
});
$("#btn_register").on("click", function () {
    ClearCreateText();
    if (ValidateCreateFields()) {
        var params = { un: $('#user_name').val(), pw: $("#pwd").val(), fn: $("#first_name").val(), mn: $("#middle_name").val(), lm: $("#last_name").val(), em: $("#email").val() };
        var url = "login.aspx/CreateUser";
        PostData(url, params, "create");
    }
});

$("#pwd").keyup(function () {
    $("#reg_span_pwd1").text("");
    $("#reg_span_pwd2").text("");
    if ((document.getElementById('pwd').value) != (document.getElementById('pwdConfirm').value)) {
        $("#reg_span_pwd1").append("Passwords do not match").css("font-weight", "bold").css("color", "red");
        $("#reg_span_pwd2").append("Passwords do not match").css("font-weight", "bold").css("color", "red");
    }
});
$("#pwdConfirm").keyup(function () {
    $("#reg_span_pwd1").text("");
    $("#reg_span_pwd2").text("");
    if ((document.getElementById('pwd').value) != (document.getElementById('pwdConfirm').value)) {
        $("#reg_span_pwd1").append("Passwords do not match").css("font-weight", "bold").css("color", "red");
        $("#reg_span_pwd2").append("Passwords do not match").css("font-weight", "bold").css("color", "red");
    }
});

$("#email").keyup(function () {
    $("#reg_span_email1").text("");
    $("#reg_span_email2").text("");
    if ((document.getElementById('email').value) != (document.getElementById('email_confirm').value)) {
        $("#reg_span_email1").append("Email Address do not match").css("font-weight", "bold").css("color", "red");
        $("#reg_span_email2").append("Email Address do not match").css("font-weight", "bold").css("color", "red");
    }
    if (!validateEmail(document.getElementById('email').value)) {
        flag = false;
        $("#reg_span_email1").append("<br />Please Enter Valid Email Address").css("font-weight", "bold").css("color", "red");
    }
    else {
        var params = { email: $('#email').val() };
        var url = "login.aspx/CheckEmail";
        PostData(url, params, "email");
    }
});
$("#email_confirm").keyup(function () {
    $("#reg_span_email1").text("");
    $("#reg_span_email2").text("");
    if ((document.getElementById('email').value) != (document.getElementById('email_confirm').value)) {
        $("#reg_span_email1").append("Email address do not match").css("font-weight", "bold").css("color", "red");
        $("#reg_span_email2").append("Email address do not match").css("font-weight", "bold").css("color", "red");
    }
    if (!validateEmail(document.getElementById('email').value)) {
        flag = false;
        $("#reg_span_email2").append("<br />Please Enter Valid Email Address").css("font-weight", "bold").css("color", "red");
    }
    else {
        var params = { email: $('#email').val() };
        var url = "login.aspx/CheckEmail";
        PostData(url, params, "email");
    }
});

function ClearLoginText() {
    $("#log_username_span").text("");
    $("#log_password_span").text("");
}
function ValidateLoginFields() {
    var flag = true;
    if ($("#log_username").val().length < 1) {
        $("#log_username_span").append("Please enter User Name.").css("font-weight", "bold").css("color", "red").focus(true);
        flag = false;
    }
    if ($("#log_password").val().length < 1) {
        $("#log_password_span").append("Please enter Password.").css("font-weight", "bold").css("color", "red");
        flag = false;
    }
    return flag;
}

function ClearCreateText() {
    $("#reg_span_username").text("");
    $("#reg_span_pwd1").text("");
    $("#reg_span_pwd2").text("");
    $("#reg_span_pwd3").text("");
    $("#reg_span_fname").text("");
    $("#reg_span_lname").text("");
    $("#reg_span_email1").text("");
    $("#reg_span_email2").text("");
    $("#reg_span_email3").text("");
}
function ValidateCreateFields() {
    var flag = true;
    if ($("#user_name").val().length < 1) {
        $("#reg_span_username").append("Please enter User Name.").css("font-weight", "bold").css("color", "red").focus(true);
        flag = false;
    }
    if ($("#pwd").val().length < 1) {
        $("#reg_span_pwd1").append("Please enter Password.").css("font-weight", "bold").css("color", "red");
        flag = false;
    }
    if ($("#first_name").val().length < 1) {
        $("#reg_span_fname").append("Please enter Your First Name.").css("font-weight", "bold").css("color", "red").focus(true);
        flag = false;
    }
    if ($("#last_name").val().length < 1) {
        $("#reg_span_lname").append("Please enter Your Last Name.").css("font-weight", "bold").css("color", "red");
        flag = false;
    }
    if ($("#email").val().length < 1) {
        $("#reg_span_email1").append("Please enter Email Address.").css("font-weight", "bold").css("color", "red").focus(true);
        flag = false;
    }

    if ((document.getElementById('pwd').value) != (document.getElementById('pwdConfirm').value)) {
        flag = false;
        $("#reg_span_pwd1").append("Passwords do not match").css("font-weight", "bold").css("color", "red");
    }
    if ((document.getElementById('email').value) != (document.getElementById('email_confirm').value)) {
        flag = false;
        $("#reg_span_email1").append("Email Address do not match").css("font-weight", "bold").css("color", "red");
    }

    if (!validateEmail(document.getElementById('email').value)) {
        flag = false;
        $("#reg_span_email1").append("<br />Please Enter Valid Email Address").css("font-weight", "bold").css("color", "red");
    }
    return flag;
}
function validateEmail($email) {
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    return emailReg.test($email);
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

function redirect(url) {
    if (typeof IE_fix != "undefined") // IE8 and lower fix to pass the http referer
    {
        document.write("redirecting..."); // Don't remove this line or appendChild() will fail because it is called before document.onload to make the redirect as fast as possible. Nobody will see this text, it is only a tech fix.
        var referLink = document.createElement("a");
        referLink.href = url;
        document.body.appendChild(referLink);
        referLink.click();
    }
    else { window.location.replace(url); }
}


//function PostData(URL, Data, Type) {
//    $.ajax
//        ({
//            type: "POST",
//            url: URL,
//            data: JSON.stringify(Data),
//            contentType: "application/json;charset=utf-8",
//            dataType: "json",
//            success: function (result) {
//                if (Type == "login") {
//                    var json_obj = $.parseJSON(result.d);
//                    if (json_obj["response"] == true) { $("#log_username_span").append("Got it! Your are a member.").css("color", "green").css("font-weight", "bold").focus(true); }
//                    else
//                    { $("#log_username_span").append("Wrong Username or Password").css("color", "red").css("font-weight", "bold").focus(true); }
//                }
//                if (Type == "create") {
//                    if (result.d == true) { $("#reg_span_username").append("Registration Successful. Please verify your email.").css("color", "green").css("font-weight", "bold").focus(true); }
//                    else
//                    { $("#reg_span_username").append("<br />Sorry this Email is already registered.").css("color", "red").css("font-weight", "bold").focus(true); }
//                }
//                if (Type == "email") {
//                    if (result.d == false) { $("#reg_span_email1").append("<br />This email address is already registered").css("font-weight", "bold").css("color", "red"); }
//                }
//            },
//            error: function (err) {
//            }
//        });
//}
//var params = {un: $('#log_username').val(), pw: $("#log_password").val()};
//$.ajax
//  ({
//      type: "POST",
//      url: "login.aspx/LoginValidate",
//      data: JSON.stringify(params),
//      contentType: "application/json;charset=utf-8",
//      dataType: "json",
//      success: function (result) {
//          console.log(result.d);
//          if (result.d == true)
//          { $("#log_username_span").append("Got IT YOU ARE A MEMBER").css("color", "red").css("font-weight", "bold").focus(true); }
//          else
//          { $("#log_username_span").append("Wrong Username or Password").css("color", "red").css("font-weight", "bold").focus(true); }
//          //alert(result.d);
//      },
//      error: function (err) {
//      }
//  });