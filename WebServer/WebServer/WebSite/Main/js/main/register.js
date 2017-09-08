$(document).ready(function () {
    WebMain.Init(3);

    $(".i-checks").iCheck({
        checkboxClass: "icheckbox_square-green",
        radioClass: "iradio_square-green"
    });
});

//回车提交
$(function () {
    $(document).keydown(function (e) {
        if (e.keyCode == "13") {
            Register()
        }
    })
})

function Identify() {
    var email = $("#email").val();
    var data = new Array();
    data[0] = email;
    data[1] = 0;

    WebMain.Post("SysUser", "Identify", data, function (returnData) {
        if (returnData == {}) return;

        if (returnData.Status == 0) {
            toastr.success("提示", "邮件已发送");

            //一分钟可点击一次发送邮件
            $("#identifyBtn").attr('disabled', true);
            setTimeout(function () {
                $("#identifyBtn").attr('disabled', false);
            }, 60000);
        } else {
            toastr.error("提示", returnData.StatusValue);
        }
    });
}

//注册
function Register() {
    var userPassword1 = $("#userPassword1").val();
    var userPassword2 = $("#userPassword2").val();
    if (userPassword1 != userPassword2) {
        toastr.error("提示", "密码不相同");
        return;
    }
    var protocolCb = $("#protocolCb").is(':checked');
    if (protocolCb == false) {
        toastr.warning("提示", "协议未勾选");
        return;
    }

    var userName = $("#userName").val();
    var fullName = $("#userName").val();
    var email = $("#email").val();
    var identifyCode = $("#identifyCode").val();

    var pwd = md5(userPassword1);

    //方法参数
    var data = new Array();
    data[0] = userName;
    data[1] = pwd;
    data[2] = fullName;
    data[3] = 1;
    data[4] = email;
    data[5] = identifyCode;

    WebMain.Post("SysUser", "Register", data, function (returnData) {
        if (returnData == {}) return;

        if (returnData.Status == 0) {
            WebMain.Alert("注册成功", "点击OK跳转登录页面，%s后自动跳转登录页面..", "timer", "OK", function () {
                window.location.href = '/Main/login.html';
            });
        } else {
            toastr.error("提示", returnData.StatusValue);
        }
    });
}