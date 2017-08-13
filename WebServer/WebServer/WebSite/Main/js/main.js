//如果有回调函数，则采用异步的方式，如果没有，则采用非异步的方式返回
var WebMain = {
    Get: function (className, methodName, data, callback) {
        return ajax.call(this, className, methodName, data, 'Get', callback);
    },
    Post: function (className, methodName, data, callback) {
        return ajax.call(this, className, methodName, data, 'Post', callback);
    },
    Init: function () {
        return init.call();
    }
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
                window.location.href = '500.html';
            } else {
                window.location.href = '404.html';
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

    callback(data)
}

//处理返回值
function handle(returnData) {
    var data = JSON.parse(returnData);

    //如果登录超时，直接跳转
    if (data.Status == 7) {
        window.location.href = 'lockscreen.html';
        data = {}
    } else {
        //做其他事情
    }

    return data;
}

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
        "timeOut": "7000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
    //检测是否登录，超时等等

    return result;
}