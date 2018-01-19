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
                //alert("���ٵ���һ����������");
                //parent.layer.msg("���ٵ���һ����������");
                return false;
            }
            // �޸ĵ�ǰ���������·������н������õĿɱ༭������ԣ�id, name��
            $(this).parents("tr").nextAll().each(function () {
                // ��ȡ�����
                var txts = $(this).find("input[class='layui-input']");
                // ��ȡ��ǰ��������name�����е����֣�Ҳ���� �������� ���Ե��±�
                var index = Number(txts.eq(0).attr("name").match(/\d+/)[0]);
                txts.each(function () {
                    // �޸������е����ֵ�ֵ�� ��ǰԱ���±� -1
                    $(this).attr("id", $(this).attr("id").replace(/\d+/, index - 1));
                    $(this).attr("name", $(this).attr("name").replace(/\d+/, index - 1));
                })
            })

            // ɾ����ǰ tr ��ǩ
            $(this).parents("tr").remove();
            init();
        });
    }
    function initAddBtn() {
        $(".addReward").unbind();
        $(".addReward").click(function () {
            // ���Ƶ�ǰ tr ��ǩ
            var ptr = $(this).parents("tr").clone();
            ptr.find("input[type='text']").val("");//���
            // ��ȡ�������� name�����е����֣�Ҳ�������Ե��±�
            var index = Number($(this).parents("tr").find("input[type='text']").attr("name").match(/\d+/)[0]);
            ptr.find("input[type='text']").each(function () {
                // ������Ա���� Member �±� +1
                $(this).attr("id", $(this).attr("id").replace(/\d+/, index + 1));
                $(this).attr("name", $(this).attr("name").replace(/\d+/, index + 1));
            })
            $(this).parents("tr").after(ptr);//��ӵ���ǰtr ��ǩ���棬�γ����Ч��
            // ɾ����ǰ ����ӡ� ��ť
            $(this).remove();
            init();
        })
    }
};