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
		$('#' + this.getIDByXID('span2')).text("您的帐号为：" + this.getContext().getRequestParameter('userid'));

	};

	Model.prototype.ReturnBtnClick = function(event) {
		justep.Shell.showPage("login");
	};

	return Model;
});