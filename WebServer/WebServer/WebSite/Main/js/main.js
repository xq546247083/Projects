//如果有回调函数，则采用异步的方式，如果没有，则采用非异步的方式返回
var WebMain = {
    //初始化,检测数据
    Init: function () {
        return init.call();
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
function init() {
    var result = {}

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
    //检测是否登录，超时等等

    return result;
}

function ajax(className, methodName, data, type, callback) {
    var result = {}

    //调用参数
    var params = {
        ClassName: className,
        MethodName: methodName,
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
        error: function (request, textStatus, errorThrown) {
            if (request.status == 500) {
                var userName = window.GetCookie("UserName");
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
        window.location.href = '/Main/lockscreen.html';
        data = {}
    } else {
        //做其他事情
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