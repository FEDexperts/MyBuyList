$(document).ready(function () {

    $("#popuplogin").dialog({
        autoOpen: false,
        title: "כניסת חברים",
        width: 400,
        height: 230,
        resizable: false,
        modal: true,
        show: 'fade',
        hide: 'fade',
        buttons: { "אישור": Login, "ביטול": CloseLogin },
        open: function () {
            $('.ui-dialog-title').css('float', 'right');
            $('.ui-dialog-title').css('margin-right', '20px');
            $('.ui-dialog-titlebar').css('width', '92%');
        }
    });

    $("#message").dialog({
        autoOpen: false,
        width: 300,
        resizable: false,
        modal: true,
        buttons: { "ביטול": function () { $("#message").dialog("close"); } }
    });

    /*Header Login*/
    $('#header a.login').css('cursor', 'pointer');

    $('#header a.login').click(function () {
        OpenLoginDialog(function () {
            window.location = window.location.href;
        });
    });

    CheckLogin();
})

function CloseLogin() {
    $("#popuplogin").dialog("close");
    SetSearchOption(1);
}

var loginCallbackFunction;

function OpenLoginDialog(callback) {

    loginCallbackFunction = callback;

    $("#TextUserName").val('');
    $("#TextPassword").val('');
    $("#popuplogin").dialog('open');
}

function isLoggedIn(callback)
{
    var data = { method: 'IsLoggedIn' };
    $.post('Handler.ashx', data, function (data)
    {
        if (data != '') {
            var user = $.parseJSON(data);
            callback(user.UserId)
        }
        else {
            callback(null);
        }
    });
}

function CheckLogin() {
    var data = { method: 'IsLoggedIn' };
    $.post('Handler.ashx', data, function (data) {
        if (data != '') {
            var user = $.parseJSON(data);
            window.userId = user.UserId;

            var loginBtn = $('#header a.login');
            loginBtn.unbind('click');

            loginBtn.switchClass('login', 'logout');
            loginBtn.html('יציאה');
            $('.HelloUser').html('שלום, ' + user.FirstName + ' ' + user.LastName);

            loginBtn.click(function () {
                Logout($(this));
            });

        }
        else {
            window.userId = null;
        }
    });
}

function Login() {
    var userName = $("#TextUserName").val();
    var password = $("#TextPassword").val();

    var data = { method: 'Login', UserName: userName, Password: password };
    $.post('Handler.ashx', data, function (data) {

        if (data != '' && data != 'null') {
            var user = $.parseJSON(data);
            window.userId = user.UserId;
            switch (user.UserTypeId) {
                case 1:
                    location.href = 'Admin/Admin.aspx';
                    break;
                case 2:
                    var loginBtn = $('#header a.login');
                    loginBtn.unbind('click');
                    loginBtn.switchClass('login', 'logout');
                    loginBtn.html('יציאה');
                    $('.HelloUser').html('שלום, ' + user.FirstName + ' ' + user.LastName);
                    $("#popuplogin").dialog('close');

                    if (loginCallbackFunction != null)
                        loginCallbackFunction();

                    loginBtn.click(function () {
                        Logout($(this));
                    });
                    break;
            }
        }
        else {
            alert('Login failed');
            return;
        }
    });
}

function Logout(logoutBtn) {
    logoutBtn.unbind('click');
    var data = { method: 'Logout' };
    $.post('Handler.ashx', data, function (data) {
        logoutBtn.switchClass('logout', 'login');
        logoutBtn.html('כניסה');
        $('.HelloUser').html('שלום, אורח');

        ResetSearch(1);

        logoutBtn.click(function () {
            OpenLoginDialog();
        });

        window.location = 'default.aspx';
        window.userId = null;
    });
}