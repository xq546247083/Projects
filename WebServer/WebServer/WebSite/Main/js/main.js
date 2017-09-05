//如果有回调函数，则采用异步的方式，如果没有，则采用非异步的方式返回
var WebMain = {
    //初始化,检测数据
    //flag: 0：登录页面，1：检测数据,一般界面，2：重新登录,3:注册页面
    Init: function (flag) {
        return init.call(this, flag);
    },
    Cookie: function (userName, pwdExpiredTime) {
        return cookie.call(this, userName, pwdExpiredTime);
    },
    //ajax请求
    Get: function (className, methodName, data, callback) {
        return ajax.call(this, className, methodName, data, 'Get', callback);
    },
    Post: function (className, methodName, data, callback) {
        return ajax.call(this, className, methodName, data, 'Post', callback);
    },
    //封装sweetalert,
    //title：标题
    //type：类型
    //btnaText：按钮a的文本
    //callbacka：按钮a的回调函数
    //btnbText：按钮b的文本
    //callbackb：按钮b的回调函数
    //btncText：按钮c的文本
    //callbackc：按钮c的回调函数
    Alert: function (title, content, type, btnaText, callbacka, btnbText, callbackb, btncText, callbackc) {
        return alertFunc.call(this, title, content, type, btnaText, callbacka, btnbText, callbackb, btncText, callbackc);
    }
}

//初始化,检测数据
function init(flag) {
    var result = {}
    checkdata(flag);

    //设置默认的提示框
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "onclick": null,
        "showDuration": "2000",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    return result;
}

function cookie(userName, pwdExpiredTime) {
    if (userName == null) {
        $.cookie('UserName', '', { expires: -1, path: '/' });
    } else {
        $.cookie("UserName", userName, { expires: 30, path: '/' });
    }

    if (pwdExpiredTime == null) {
        $.cookie('PwdExpiredTime', '', { expires: -1, path: '/' });
    } else {
        $.cookie("PwdExpiredTime", pwdExpiredTime, { expires: 30, path: '/' });
    }
}

//检测用户数据
function checkdata(flag, curDate) {
    var userName = $.cookie("UserName");
    var pwdExpiredTime = $.cookie("PwdExpiredTime");
    var curDate = Date.parse(new Date());

    //如果检测数据，那么如果没有用户名，则登录
    if (flag == 1) {
        if (userName == null || userName == "") {
            window.location.href = '/Main/login.html';
        } else if (pwdExpiredTime < curDate || pwdExpiredTime == null) {
            //如果有用户名，但是过期了，则重登录
            window.location.href = '/Main/lockscreen.html';
        }
    } else if (flag == 0) {
        //如果为登录页面，且密码过期，则重登录
        if (userName != null && userName != "") {
            if (pwdExpiredTime < curDate || pwdExpiredTime == null) {
                window.location.href = '/Main/lockscreen.html';
            } else {
                window.location.href = '/Main/index.html';
            }
        }
    } else if (flag == 2) {
        if (userName == null || userName == "") {
            window.location.href = '/Main/login.html';
        }
    }
}

function ajax(className, methodName, data, type, callback) {
    var result = {}

    var userName = $.cookie("UserName");

    //调用参数
    var params = {
        ClassName: className,
        MethodName: methodName,
        UserName: userName,
        Data: data
    };

    var paramStr = JSON.stringify(params);
    $.ajax({
        dataType: "text",
        type: type,
        async: !callback ? false : true,
        url: "../API/ClientHandler.ashx",
        data: paramStr,
        success: function (returnData) {
            result = returnData;

            //如果有回调函数，则调用回调函数来处理数据
            if (callback) {
                callbackHandle(result, callback);
            }
        },
        error: function (request) {
            if (request.status == 500) {
                window.location.href = '/Main/500.html';
            } else {
                window.location.href = '/Main/404.html';
            }
        }
    });

    //如果没有回调函数，则处理数据
    if (!callback)
        return handle(result);
}

//处理回调函数的数据
function callbackHandle(returnData, callback) {
    var data = handle(returnData);

    if (callback)
        callback(data);
}

//处理返回值
function handle(returnData) {
    var data = JSON.parse(returnData);

    //如果登录超时，直接跳转
    if (data.Status == 7) {
        var userName = $.cookie("UserName");
        if (userName == null || userName == "") {
            window.location.href = '/Main/login.html';
        } else {
            window.location.href = '/Main/lockscreen.html';
        }
        data = {}
    } else {
        //做其他事情
    }

    //如果返回了过期时间
    if (data.PwdExpiredTime != null)
    {
        $.cookie("PwdExpiredTime", data.PwdExpiredTime, { expires: 30, path: '/' });
    }

    return data;
}

//封装提示框
function alertFunc(title, content, type, btnaText, callbacka, btnbText, callbackb, btncText, callbackc) {
    switch (type) {
        case "success":
            swal({
                title: title,
                text: content,
                type: type
            });
            break;
        case "error":
            swal({
                title: title,
                text: content,
                type: type
            });
            break;
        case "warn":
            swal({
                title: title,
                text: content,
                type: "warning",
                showCancelButton: true,
                cancelButtonText: "取消",
                confirmButtonColor: "#DD6B55",
                confirmButtonText: btnaText,
                closeOnConfirm: false
            }, function () {
                if (callbacka)
                    callbacka();
            });
            break;
        case "customwarn":
            swal({
                title: title,
                text: content,
                type: "warning",
                showCancelButton: true,
                cancelButtonText: btnbText,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: btnaText,
                closeOnConfirm: false,
                closeOnCancel: false
            }, function (isConfirm) {
                if (isConfirm) {
                    if (callbacka)
                        callbacka();
                } else {
                    if (callbackb)
                        callbackb();
                }
            });
            break;
        case "timer":
            swalFunc(title, content, type, btnaText, callbacka, 3000);
            break;
        default:
            swal({
                title: title,
                text: content
            });
    }
}

//递归函数用来显示时间提示框
function swalFunc(title, content, type, btnaText, callbacka, i) {
    var currentContent = content.replace("%s", i / 1000 + "s");

    swal({
        title: title,
        text: currentContent,
        timer: 1000,
        showConfirmButton: true,
        confirmButtonText: btnaText
    }, function (isConfirm) {
        //如果点击了确认，则直接返回
        if (isConfirm) {
            if (callbacka)
                callbacka();
        }

        //继续循环
        i = i - 1000;
        if (i >= 1000) {
            swalFunc(title, content, type, btnaText, callbacka, i);
        } else {
            if (callbacka)
                callbacka();
        }
    });
}