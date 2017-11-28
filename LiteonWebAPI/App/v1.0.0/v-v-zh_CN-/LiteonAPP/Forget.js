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
			},
			error:function(a,b,c){
			
			}
		})
	};
	

	return Model;
});