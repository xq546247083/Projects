function btninit() {
    this.init = function () {
        init();
    }
    function init() {
        $(".addReward").remove();
        $(".deleteReward").last().after("<button type='button' class='layui-btn layui-btn-primary layui-btn-small addReward'><i class='layui-icon'>&#xe654;</i></button>");
        initAddBtn();
        initDeleteBtn();
    }
    function initDeleteBtn() {
        $(".deleteReward").unbind();
        $(".deleteReward").click(function () {
            var configCount = $(".deleteReward").length;
            if (configCount <= 1) {
                //alert("至少得有一个奖励配置");
                //parent.layer.msg("至少得有一个奖励配置");
                return false;
            }
            // 修改当前奖励配置下方的所有奖励配置的可编辑框的属性（id, name）
            $(this).parents("tr").nextAll().each(function () {
                // 获取输入框
                var txts = $(this).find("input[class='layui-input']");
                // 获取当前奖励配置name属性中的数字，也就是 奖励配置 属性的下标
                var index = Number(txts.eq(0).attr("name").match(/\d+/)[0]);
                txts.each(function () {
                    // 修改属性中的数字的值， 当前员工下标 -1
                    $(this).attr("id", $(this).attr("id").replace(/\d+/, index - 1));
                    $(this).attr("name", $(this).attr("name").replace(/\d+/, index - 1));
                })
            })

            // 删除当前 tr 标签
            $(this).parents("tr").remove();
            init();
        });
    }
    function initAddBtn() {
        $(".addReward").unbind();
        $(".addReward").click(function () {
            // 复制当前 tr 标签
            var ptr = $(this).parents("tr").clone();
            ptr.find("input[type='text']").val("");//清空
            // 获取复制项中 name属性中的数字，也就是属性的下标
            var index = Number($(this).parents("tr").find("input[type='text']").attr("name").match(/\d+/)[0]);
            ptr.find("input[type='text']").each(function () {
                // 新增的员工的 Member 下标 +1
                $(this).attr("id", $(this).attr("id").replace(/\d+/, index + 1));
                $(this).attr("name", $(this).attr("name").replace(/\d+/, index + 1));
            })
            $(this).parents("tr").after(ptr);//添加到当前tr 标签后面，形成添加效果
            // 删除当前 “添加” 按钮
            $(this).remove();
            init();
        })
    }
};