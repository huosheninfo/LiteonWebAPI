function gos(){
	window.location.href="bdapp://map/newsassistant";
}
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

	Model.prototype.ReturnBtnClick = function(event) {
		justep.Shell.showPage("login");
	};

	Model.prototype.image1Click = function(event) {
		this.comp("messageDialog1").show({type:'OKCancel'})
	};

	Model.prototype.messageDialog1OK = function(event) {
		justep.Util.hint("是");
	};

	Model.prototype.messageDialog1Cancel = function(event) {
		justep.Util.hint("否");
	};

	return Model;
});