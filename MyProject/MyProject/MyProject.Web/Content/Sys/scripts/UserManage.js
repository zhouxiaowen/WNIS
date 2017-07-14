
$(function () {
    $("#BtnRefresh").click(function () {
        BindGrid();
    });

    $("#BtnAdd").click(function () {
        AddClick();
    });

    $("#BtnSubmit").click(function () {
        SubmitClick();
    });

    $("#BtnCancel").click(function () {
        layer.closeAll();
    });

    $("#BtnSearch").click(function () {
        BindGrid();
    });

    $("#TxtKey").bind('keydown', function (event) {
        if (event.keyCode === 13) {
            BindGrid();
        }
    });

    BindGrid();
});
function BindGrid() {
    if ($.fn.dataTable.isDataTable('#GridUser')) {
        $("#GridUser").DataTable().ajax.reload();
    }
    else {
    $("#GridUser").DataTable(
        {
            "bPaginate": true, //翻页功能
            "bLengthChange": true, //改变每页显示数据数量
            "bFilter": false, //过滤功能
            "bInfo": true,//页脚信息
            "bAutoWidth": true,//自动宽度
            "bServerSide": true,//服务端处理分页
            "language": {
                "sInfo": "从第 _START_ 条到第 _END_ 条，总 _TOTAL_ 条",
                "oPaginate": {
                    "sPrevious": "前一页",
                    "sNext": "下一页"
                }
            },
            "fnCreatedRow": function (row, data, dataIndex) {
                $(row).children('td').eq(0).attr('style', 'text-align: center;')
                $(row).children('td').eq(1).attr('style', 'text-align: center;')
            },
            ajax: function (data, callback, settings) {
                var input = $.dataTablesToAbpInput(data, settings);
                input.Key = $("#TxtKey").val();

                abp.ui.setBusy("#main",
                    abp.services.app.sysUser.querySysUser(input)
                        .done(function (data) {
                            if (data.Success) {
                                if (data.TargetUrl != null) {
                                    window.location = data.TargetUrl;
                                }
                                else {
                                    if (data.Result != null) {
                                        var resonse = $.dataTablesFromAbpOutput(input, data.Result, settings);
                                        callback(resonse);
                                    }
                                }
                            }
                            else {
                                abp.message.warn(data.error.message);
                            }
                        })
                        .fail(function (result) {
                            abp.message.error(result);
                        }));
            },

            "columns":
            [
                {
                    "data": null, "render": function (data, display, row, setting) {
                        return "<a style='font-size:14px;cursor:pointer;' onclick='EditClick(\"" + setting.row + "\")'><i class='fa fa-edit'></i></a>";
                    }
                },
                {
                    "data": null, "render": function (data, display, row, setting) {
                        return "<a style='color:red;font-size:14px;cursor:pointer;' onclick='DelClick(\"" + row.UId + "\",\"" + row.Name + "信息\")'><i class='fa fa-trash-o'></i></a>";
                    }
                },
                { "data": "Code" },
                { "data": "Name" },
                { "data": "LastTime" },
                { "data": "LastIP" },
                {
                    "data": "Status", "render": function (data, display, row, setting) {
                        if (data == 1) return "有效";
                        else return "<font color='red'>无效</font>"
                    }
                }
            ]
        });
}
}

//添加信息
function AddClick() {
    ClearValue();
    FormValidator();
    LayerShow("添加信息");
}
//编辑信息
function EditClick(index) {
    //var index = $(e.target).closest(this).closest("tr").index();
    BindValue(index);
    FormValidator();
    LayerShow("编辑信息");
}

//弹出编辑框
function LayerShow(title) {
    //页面层
    layer.open({
        type: 1,
        title: title,
        skin: 'layui-layer-rim', //加上边框
        area: ['420px', '300px'], //宽高
        content: $("#SpanEdit")
    });
}

//清除控件值
function ClearValue() {
    $("#TxtName").val("");
    $("#TxtPassword").val("");
    $("#TxtCode").val("");
    $("#TxtId").val("");
    $("#SelStatus").val(1);
}
//绑定控件值
function BindValue(index) {
    var rowdata = $("#GridUser").DataTable().data()[index];
    $("#TxtName").val(rowdata.Name);
    $("#TxtCode").val(rowdata.Code);
    $("#TxtId").val(rowdata.UId);
    $("#TxtPassword").val("");
    $("#SelStatus").val(rowdata.Status)
}
//添加表单验证
function FormValidator() {
    if ($("#EditForm").data('bootstrapValidator') != null) {
    $("#EditForm").data('bootstrapValidator').resetForm();
// $('#EditForm').data('bootstrapValidator', null);
    } else {
    $("#EditForm").bootstrapValidator({
        message: '这个值没有被验证',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            // invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            uname: {
                message: '用户名验证失败',
                validators: {
                    notEmpty: {
                        message: '用户名不能为空'
                    },
                    //stringLength: {
                    //    min: 6,
                    //    max: 18,
                    //    message: '用户名长度必须在6到18位之间'
                    //},
                    //regexp: {
                    //    regexp: /^[a-zA-Z0-9_]+$/,
                    //    message: '用户名只能包含大写、小写、数字和下划线'
                    //}
                }
            },
            ucode:{
                validators: {
                    notEmpty: {
                        message: '工号不能为空'
                    }
                }
            },
            password: {
                validators: {
                    notEmpty: {
                        message: '密码不能为空'
                    }
                }
            }
        }
    });

    $("#EditForm").data('bootstrapValidator').resetForm();
    }



}

//提交修改
function SubmitClick() {
    $('#EditForm').data('bootstrapValidator').validate();  
    if (!$('#EditForm').data('bootstrapValidator').isValid()) {
        layer.msg("信息填写不完整。");
        return;
    } else {
        //通过校验，可进行提交等操作
        var input = new Object();
        input.UId = $("#TxtId").val();;
        input.Code = $("#TxtCode").val();
        input.Name = $("#TxtName").val();
        input.Password = $("#TxtPassword").val();
        input.Status = $("#SelStatus").val();

        abp.ui.setBusy("#main",
            abp.services.app.sysUser.addOrUpdateSysUser(input)
                .done(function (data) {
                    if (data.Success) {
                        if (data.Result.Message == null || data.Result.Message == "") {
                            abp.notify.success("操作成功!");
                            BindGrid();
                            layer.closeAll();
                        }
                        else {
                            layer.msg(data.Result.Message);
                            //abp.message.warn(data.Result.Message);
                        }
                    }
                })
                .fail(function (result) {
                    layer.msg(result);
                })
        )
        return false;
    }

}


//是否删除
function DelClick(id, message) {
    layer.confirm("删除" + message + "？", {
        btn: ['删除', '取消'] //按钮
    }, function () {
        var input = new Object();
        input.UId = id;
        abp.ui.setBusy("#main",
            abp.services.app.sysUser.deleteSysUser(input)
                .done(function (data) {
                    BindGrid();
                    layer.msg('删除成功');
                })
                .fail(function (result) {
                    layer.msg("删除失败");
                })
        )
        
    }, function () {
        //layer.msg('删除失败');
    });
}
