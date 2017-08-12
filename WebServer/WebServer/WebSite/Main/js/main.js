
//如果有回调函数，则采用异步的方式，如果没有，则采用非异步的方式返回
var WebMain = {
    Get: function (className, methodName, data, callback) {
        return ajax.call(this, className, methodName, data, 'Get', callback)
    },

    Post: function (className, methodName, data, callback) {
        return ajax.call(this, className, methodName, data, 'Post', callback)
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
            if (callback) {
                callback(returnData);
            }
            else
            {
                result = returnData;
            }
        },
        error: function (request, textStatus, errorThrown) {
            callback(404);
        }
    });

    return result;
}
