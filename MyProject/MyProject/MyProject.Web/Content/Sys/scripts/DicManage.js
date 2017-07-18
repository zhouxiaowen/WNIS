$(function () {

    //刷新按钮
    $("#btn_Dic_Refresh").on('click', Dic.initDic);
    //新增字典
    $("#btn_Dic_Add").on('click', Dic.fun_BackReturn);
    //新增项目
    $("#btn_Detail_Add").on('click', function () {
        Dic.editDicDetail("");
    });
    $("#txt_dicName").on('change', function () {
        $("#txt_dicPym").val(leachword(makePy($("txt_dicName").val())));
    })
    $("#txt_detailName").on('change', function () {
        $("#txt_detailPym").val(leachword(makePy($("txt_detailName").val())));
    })
    $("input[name='px']").keyup(function () {  //keyup事件处理 
        $(this).val($(this).val().replace(/\D|^0/g, ''));
    }).bind("paste", function () {  //CTR+V事件处理 
        $(this).val($(this).val().replace(/\D|^0/g, ''));
    }).css("ime-mode", "disabled");  //CSS设置输入法不可用
    Dic.initDic();
});

var Dic = (function () {
    var dicTypeData, dicItemData, dicDetailData; //当前字典大类,字典项目,字典明细信息
    var _curDicType, _curDicItem; //当前字典大类,字典项目

    //加载字典大类Table
    function initDicTypeTable() {
        if ($.fn.dataTable.isDataTable('#table_DicType')) {
            $("#table_DicType").DataTable().ajax.reload();
        }
        else {
            var table = $("#table_DicType").DataTable(
             {
                 ajax: function (data, callback, settings) {
                     var input = $.dataTablesToAbpInput(data, settings);
                     abp.ui.setBusy("#main",
                          abp.services.app.Sys.QueryDicTypeList(input)
                         .done(function (data) {
                             if (data.Success) {
                                 if (data.Result != null && data.Result != "") {
                                     var Items = data.Result.Items;
                                     dicTypeData = Items;
                                     var response = {};
                                     response.draw = input.Sequence;
                                     response.recordsTotal = Items.length;
                                     response.recordsFiltered = Items.length;
                                     response.data = Items;
                                     callback(response);

                                     bindDicTypeTableEvent();
                                     //清空字典项目表,字典明细表内容, 禁用按钮
                                     if ($.fn.dataTable.isDataTable('#table_DicItem')) {
                                         $("#table_DicItem").DataTable().rows().remove().draw();
                                     }
                                     if ($.fn.dataTable.isDataTable('#table_DicDetail')) {
                                         $("#table_DicDetail").DataTable().rows().remove().draw();
                                     }
                                     $("#lbl_zdmc").html("");
                                 }
                                 else {
                                     abp.message.warn("获取字典大类信息出错。");
                                 }
                             }
                             else {
                                 abp.message.warn(data.Error.Message);
                             }
                         })
                     .fail(function (result) {
                         abp.message.error(result);
                     }));
                 },
                 "fnCreatedRow": function (row, data, dataIndex) {
                     $(row).children('td').eq(0).attr('style', 'text-align: left;')
                     $(row).children('td').eq(1).attr('style', 'text-align: left;')
                 },
                 columns: [
                             { "data": "DicName" },
                             { "data": "PX" }
                 ],
                 scrollX: false,
                 serverSide: false,
                 paging: false,
                 "bFilter": false, //过滤功能
                 info: false,
                 buttons: [
                'selectRows'
                 ]
             });
        }
    }
    //加载字典项目Table,参数:dlid(字典大类ID)
    function initDicItemTable() {
        if ($.fn.dataTable.isDataTable('#table_DicItem')) {
            $("#table_DicItem").DataTable().ajax.reload();
        }
        else {
            var table = $("#table_DicItem").DataTable(
             {
                 ajax: function (data, callback, settings) {
                     var input = $.dataTablesToAbpInput(data, settings);
                     input.ZDDLID = _curDicType;
                     abp.ui.setBusy("#main",
                          abp.services.app.Sys.QueryDicItemList(input)
                         .done(function (data) {
                             if (data.Success) {
                                 if (data.Result != null && data.Result != "") {
                                     var Items = data.Result.Items;
                                     $("#dicCount").val(data.Result.TotalCount);
                                     dicItemData = Items;
                                     var response = {};
                                     response.draw = input.Sequence;
                                     response.recordsTotal = Items.length;
                                     response.recordsFiltered = Items.length;
                                     response.data = Items;
                                     callback(response);
                                     bindDicItemTableEvent();
                                     //清空字典项目表,字典明细表内容, 禁用按钮
                                     if ($.fn.dataTable.isDataTable('#table_DicDetail')) {
                                         $("#table_DicDetail").DataTable().rows().remove().draw();
                                     }
                                 }
                                 else {
                                     abp.message.warn("获取字典项目信息出错。");
                                 }
                             }
                             else {
                                 abp.message.warn(data.Error.Message);
                             }
                         })
                     .fail(function (result) {
                         abp.message.error(result);
                     }));
                 },
                 "fnCreatedRow": function (row, data, dataIndex) {
                     $(row).children('td').eq(0).attr('style', 'text-align: left;')
                     $(row).children('td').eq(1).attr('style', 'text-align: left;')
                     $(row).children('td').eq(2).attr('style', 'text-align: left;')
                     $(row).children('td').eq(3).attr('style', 'text-align: left;')
                     $(row).children('td').eq(4).attr('style', 'text-align: left;')
                 },
                 columns: [
                             { "data": "DicCode" },
                             { "data": "DicName" },
                             { "data": "PYM" },
                             { "data": "PX" },
                             { "data": "Remark" }
                 ],
                 scrollX: false,
                 serverSide: false,
                 paging: false,
                 info: false,
                 buttons: [
                'selectRows'
                 ]
             });
        }
    }
    //加载字典项目Table
    function initDicDetailTable() {
        if ($.fn.dataTable.isDataTable('#table_DicDetail')) {
            $("#table_DicDetail").DataTable().ajax.reload();
        }
        else {
            var table = $("#table_DicDetail").DataTable(
             {
                 ajax: function (data, callback, settings) {
                     var input = $.dataTablesToAbpInput(data, settings);
                     input.ZDXMID = _curDicItem;
                     abp.ui.setBusy("#main",
                          abp.services.app.Sys.QueryDicDetailList(input)
                         .done(function (data) {
                             if (data.Success) {
                                 if (data.Result != null && data.Result != "") {
                                     var Items = data.Result.Items;
                                     dicDetailData = Items;
                                     var response = {};
                                     response.draw = input.Sequence;
                                     response.recordsTotal = Items.length;
                                     response.recordsFiltered = Items.length;
                                     response.data = Items;
                                     callback(response);
                                     //绑定字典项目表格事件
                                     $("#table_DicDetail tbody").find("input").off('click').on("click", function () {
                                         fun_SelectReturnDetail(this);
                                     });
                                 }
                                 else {
                                     abp.message.warn("获取字典明细信息出错。");
                                 }
                             }
                             else {
                                 abp.message.warn(data.Error.Message);
                             }
                         })
                     .fail(function (result) {
                         abp.message.error(result);
                     }));
                 },
                 "fnCreatedRow": function (row, data, dataIndex) {
                     $(row).children('td').eq(0).attr('style', 'text-align: center;')
                     $(row).children('td').eq(1).attr('style', 'text-align: left;')
                     $(row).children('td').eq(2).attr('style', 'text-align: left;')
                     $(row).children('td').eq(3).attr('style', 'text-align: left;')
                     $(row).children('td').eq(4).attr('style', 'text-align: left;')
                     $(row).children('td').eq(5).attr('style', 'text-align: left;')
                     $(row).children('td').eq(6).attr('style', 'text-align: left;')
                 },
                 columns: [
                        {
                            "data": null, "render": function (data, display, row, setting) {
                                return "<a style='font-size:14px;cursor:pointer;' onclick='Dic.editDicDetail(\"" + data.ID + "\")'><i class='fa fa-edit'></i></a>";
                            }
                        },
                        { "data": "Code" },
                        { "data": "Name" },
                        { "data": "PYM" },
                        { "data": "PX" },
                        { "data": "Remark" },
                        {
                            "data": "Status", "render": function (data, display, row, setting) {
                                if (data == 1) return "有效";
                                else return "<font color='red'>无效</font>"
                            }
                        }
                 ],
                 scrollX: false,
                 serverSide: false,
                 paging: false,
                 info: false,
                 buttons: [
                'selectRows'
                 ]
             });
        }
    }

    //行事件--绑定字典大类点击事件
    function bindDicTypeTableEvent() {
        //绑定字典大类表格事件
        $("#table_DicType tbody").find("tr").off('click').on("click", function () {
            if ($(this).find("td:first").hasClass("selectedtr")) {
                $(this).find("td").removeClass("selectedtr");
            } else {
                //清除当前表格其他选中行
                $(this).parent().find("td").removeClass("selectedtr");
                //清除其他表格选中行
                $("#table_DicItem tbody").find("td").removeClass("selectedtr");
                $("#table_DicDetail tbody").find("td").removeClass("selectedtr");
                //选中当前行
                $(this).find("td").addClass("selectedtr");
                //清空字典项目表,字典明细表内容, 禁用按钮
                if ($.fn.dataTable.isDataTable('#table_DicItem')) {
                    $("#table_DicItem").DataTable().rows().remove().draw();
                }
                if ($.fn.dataTable.isDataTable('#table_DicDetail')) {
                    $("#table_DicDetail").DataTable().rows().remove().draw();
                }
                $("#lbl_zdmc").html("");
                fun_SelectDicType($(this).index());
            }
        });
    }
    //行事件--绑定字典项目事件
    function bindDicItemTableEvent() {
        //绑定字典项目表格事件
        $("#table_DicItem tbody").find("tr").off('click').on("click", function () {
            if ($(this).find("td:first").hasClass("selectedtr")) {
                $(this).find("td").removeClass("selectedtr");
                //清空字典项目表,字典明细表内容, 禁用按钮
                if ($.fn.dataTable.isDataTable('#table_DicDetail')) {
                    $("#table_DicDetail").DataTable().rows().remove().draw();
                }
            } else {
                //清除当前表格其他选中行
                $(this).parent().find("td").removeClass("selectedtr");
                //清除其他表格选中行
                $("#table_DicDetail tbody").find("td").removeClass("selectedtr");
                //选中当前行
                $(this).find("td").addClass("selectedtr");
                _curItemIndex = $(this).index();
                fun_SelectDicItem($(this).index());
            }
        });
    }

    //行事件--选中字典大类触发事件
    function fun_SelectDicType(index) {
        if (dicTypeData != null && dicTypeData.length > 0) {
            var dlid = dicTypeData[index].DicCode;
            _curDicType = dlid;
            initDicItemTable()
        }
    }
    //行事件--选中字典项目触发事件
    function fun_SelectDicItem(index) {
        if (dicItemData != null && dicItemData.length > 0) {
            var mxdata = dicItemData[index];
            _curDicItem = mxdata.DicCode;
            $("#lbl_zdmc").html(mxdata.DicName);
            initDicItemDetailTable()
        }
    }
    //新增字典
    function fun_addDicItem() {
        $('#EditDicItemForm').data('bootstrapValidator').validate();
        if (!$('#EditDicItemForm').data('bootstrapValidator').isValid()) {
            layer.msg("信息填写不完整。");
            return;
        } else {
            var input = {};
            input.DicTypeCode = _curDicType;//所属字典分类
            input.DicCode = $("#lbl_zdCode").html();
            input.DicName = $("#txt_dicName").val();
            input.DicPYM = $("#txt_dicPym").val();
            input.DicPX = $("#txt_dicPx").val();
            input.DicRemark = $("#txt_dicRemark").val();
            abp.ui.setBusy("#main",
            abp.services.app.Sys.AddDicItem(input)//新增字典
            .done(function (finfo) {
                if (finfo.Success) {
                    if (finfo.Result.Message == null || finfo.Result.Message == "") {
                        initDicItemTable();//刷新字典
                        layer.closeAll();
                        abp.notify.success("添加字典成功!");
                    }
                    else {
                        abp.message.warn(finfo.Result.Message);
                    }
                }
                else {
                    abp.message.warn(finfo.Error.Message);
                }
            })
            .fail(function (result) {
                abp.message.error(result);
            })
            );
        }
    }
    //编辑字典项目
    function fun_editDicDetail(id) {
        ClearValue();
        DicDetailFormValidator();
        $("#btn_detail_Submit").off('click').on('click', fun_saveDicDetail);
        if (id == null || id == "") {
            $("#txt_detailCode").prop('disabled', false);
            $("#detailId").val("");
            //页面层
            layer.open({
                type: 1,
                title: "新增字典项目",
                skin: 'layui-layer-rim', //加上边框
                area: ['420px', '400px'], //宽高
                content: $("#EditDicDetail")
            });
        }
        else {
            abp.ui.setBusy("#main",
            abp.services.app.Sys.GetDicDetailRecord({ ID: id })//获取字典
            .done(function (finfo) {
                if (finfo.Success) {
                    if (finfo.Result != null && finfo.Result != "") {
                        initDicItemTable();//刷新字典
                        $("#txt_detailCode").prop('disabled', true);
                        $("#detailId").val(id);
                        $("#txt_detailCode").val(finfo.Result.Code);
                        $("#txt_detailName").val(finfo.Result.Name);
                        $("#txt_detailPym").val(finfo.Result.PYM);
                        $("#txt_detailPx").val(finfo.Result.PX);
                        $("#txt_detailRemark").val(finfo.Result.Remark);
                        $("#sel_detailStatus").val(finfo.Result.Status);
                        //页面层
                        layer.open({
                            type: 1,
                            title: "编辑字典项目",
                            skin: 'layui-layer-rim', //加上边框
                            area: ['420px', '400px'], //宽高
                            content: $("#EditDicDetail")
                        });
                    }
                    else {
                      
                        abp.message.warn("获取字典项目信息失败");
                    }
                }
                else {
                    abp.message.warn(finfo.Error.Message);
                }
            })
            .fail(function (result) {
                abp.message.error(result);
            })
            );
        }

        $("#btn_dic_Submit").off('click').on('click', fun_addDicItem);

    }
    //保存字典项目
    function fun_saveDicDetail() {
        $('#EditDicDetailForm').data('bootstrapValidator').validate();
        if (!$('#EditDicDetailForm').data('bootstrapValidator').isValid()) {
            layer.msg("信息填写不完整。");
            return;
        } else {
            var input = {};
            if ($("#detailId").val() != "") {
                input.ID = $("#detailId").val();
            }
            input.DicCode = _curDicItem;//所属字典代码
            input.Code = $("#txt_detailCode").html();
            input.Name = $("#txt_detailName").val();
            input.PYM = $("#txt_detailPym").val();
            input.PX = $("#txt_detailPx").val();
            input.Remark = $("#txt_detailRemark").val();
            input.Status = $("#sel_detailStatus").val();
            abp.ui.setBusy("#main",
            abp.services.app.Sys.SaveDicDetail(input)//编辑字典项目
            .done(function (finfo) {
                if (finfo.Success) {
                    if (finfo.Result.Message == null || finfo.Result.Message == "") {
                        initDicDetailTable();//刷新字典
                        layer.closeAll();
                        abp.notify.success("添加项目成功!");
                    }
                    else {
                        abp.message.warn(finfo.Result.Message);
                    }
                }
                else {
                    abp.message.warn(finfo.Error.Message);
                }
            })
            .fail(function (result) {
                abp.message.error(result);
            })
            );
        }
    }
    //清除控件值
    function ClearValue() {
        $("#txt_dicName").val("");
        $("#txt_dicPym").val("");
        $("#txt_dicPx").val("");
        $("#txt_dicRemark").val("");

        $("#txt_detailCode").val("");
        $("#txt_detailName").val("");
        $("#txt_detailPym").val("");
        $("#txt_detailPx").val("");
        $("#txt_detailRemark").val("");
        $("#sel_detailStatus").val(1);
    }
    //添加字典验证
    function DicItemFormValidator() {
        if ($("#EditDicItemForm").data('bootstrapValidator') != null) {
            $("#EditDicItemForm").data('bootstrapValidator').resetForm();
        } else {
            $("#EditDicItemForm").bootstrapValidator({
                message: '这个值没有被验证',
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    // invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    dicname: {
                        validators: {
                            notEmpty: {
                                message: '字典名称不能为空'
                            }
                        }
                    }
                }
            });

            $("#EditDicItemForm").data('bootstrapValidator').resetForm();
        }
    }
    //添加字典明细表单验证
    function DicDetailFormValidator() {
        if ($("#EditDicDetailForm").data('bootstrapValidator') != null) {
            $("#EditDicDetailForm").data('bootstrapValidator').resetForm();
        } else {
            $("#EditDicDetailForm").bootstrapValidator({
                message: '这个值没有被验证',
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    // invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    dcode: {
                        validators: {
                            notEmpty: {
                                message: '代码不能为空'
                            }
                        }
                    },
                    dname: {
                        validators: {
                            notEmpty: {
                                message: '名称不能为空'
                            }
                        }
                    }
                }
            });

            $("#EditDicDetailForm").data('bootstrapValidator').resetForm();
        }
    }
    return {
        //初始化字典查询界面
        initDic: function () {
            //初始化字典大类表格
            initDicTypeTable();
        },
        //添加字典
        addDicItem: function () {
            ClearValue();
            DicItemFormValidator();
            $("#lbl_zdCode").html("Dic" + $("#dicCount").val());
            $("#btn_dic_Submit").off('click').on('click', fun_addDicItem);
            //页面层
            layer.open({
                type: 1,
                title: "新增字典",
                skin: 'layui-layer-rim', //加上边框
                area: ['420px', '400px'], //宽高
                content: $("#EditDicItem")
            });
        },
        //编辑项目
        editDicDetail: function (id) {
            fun_editDicDetail(id);
        }

    };
})();



