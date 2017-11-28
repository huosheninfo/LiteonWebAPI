define(function(require) {
	var $ = require("jquery");
	var justep = require("$UI/system/lib/justep");
	var MD5 = require('$UI/system/lib/base/md5');
	var Baas = justep.Baas;

	var Model = function() {
		this.callParent();

	};

	/***************************************************************************
	 * 页面加载
	 **************************************************************************/
	Model.prototype.modelLoad = function(event) {

	};

	/***************************************************************************
	 * 提交注册
	 **************************************************************************/
	Model.prototype.button2Click = function(event) {
		var self = this;
		md5 = new MD5();
		var cq = self.comp('txt_cq').val();
		var xm = self.comp('txt_xm').val();
		var sfz = self.comp('txt_sfz').val();
		var gh = self.comp('txt_gh').val();
		var sr = self.comp('txt_sr').val();
		var mm = $('#' + self.getIDByXID('txt_mm')).val();
		var qrmm = $('#' + self.getIDByXID('txt_qrmm')).val();

		console.info(cq)
		if (cq == "") {
			justep.Util.hint("请选择厂区")
			return;
		}
		if (xm == "") {
			justep.Util.hint("请输入姓名")
			return;
		}
		if (sfz == "") {
			justep.Util.hint("请输入身份证后6位")
			return;
		}
		if (gh == "") {
			justep.Util.hint("请输入工号")
			return;
		}
		if (mm == "") {
			justep.Util.hint("请输入密码")
			return;
		}
		if (mm != qrmm) {
			justep.Util.hint("两次密码输入不一致")
			return;
		}
		mm = md5.hex_md5(mm);
		console.info(mm)

		$.ajax({
			url : RequestPath + "Regsiter/Regsiters",
			type : "post",
			data : JSON.stringify({
				cq : cq,
				xm : xm,
				sfz : sfz,
				gh : gh,
				sr : sr,
				mm : mm
			}),
			contentType : "application/json",
			success : function(data) {
				console.info(data);
				justep.Util.hint(data.ResponseMessage)
				if (data.ResponseState == "1") {
					justep.Shell.showPage(require.toUrl("./RegisteSucc.w?userid="+data.ResponseData));
				}
				

			}
		})

	};

	/***************************************************************************
	 * 获取厂区数据
	 **************************************************************************/
	Model.prototype.cqDataCustomRefresh = function(event) {
		var cqdata = this.comp('cqData');
		$.ajax({
			url : RequestPath + "UserInfo/GetUserList",
			type : "post",
			data : JSON.stringify({
				type : 1
			}),
			contentType : "application/json",
			success : function(data) {
				console.info(data);
				cqdata.loadData(data)
			}
		})
	};
	/***************************************************************************
	 * 确认密码事件
	 **************************************************************************/
	Model.prototype.txt_qrmmChange = function(event) {

		var self = this;
		var mm = $('#' + self.getIDByXID('txt_mm')).val();
		var qrmm = $('#' + self.getIDByXID('txt_qrmm')).val();
		console.info(mm + ":" + qrmm)
		if (mm != qrmm) {
			$('#' + self.getIDByXID('div4')).html("<span style='color:red;height:32px;line-height:32px;' >✖</span>")
		} else {
			$('#' + self.getIDByXID('div4')).html("<span style='color:green;height:32px;line-height:32px;'>✔</span>")
		}
	};

	return Model;
});