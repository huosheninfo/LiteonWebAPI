define(function(require) {
	var $ = require("jquery");
	var justep = require("$UI/system/lib/justep");

	var MD5 = require('$UI/system/lib/base/md5');

	var Model = function() {
		this.callParent();
	};

	// 进入注册页
	Model.prototype.registeredClick = function(event) {
		justep.Shell.showPage("registered");

	};

	Model.prototype.col1Click = function(event) {
		justep.Shell.showPage("Forget");
	};
	Model.prototype.loginWeixinClicked = function(event) {
//		var self = this;
//
//		var weixin = navigator.weixin;
//
//		function saveUser(data) {
//			var user = {};
//			user.userid = data.openid;
//			user.accountType = "WX";
//			user.name = data.nickname || "NONAME";
//			justep.Shell.userType.set(user.accountType);
//			justep.Shell.userName.set(user.name);
//			localStorage.setItem("userUUID", JSON.stringify(user));
//			justep.Util.hint("登录成功");
//			setTimeout(function() {
//				justep.Shell.showPage("main");
//			}, 3000);
//		}
//
//		weixin.ssoLogin(function() {
//			weixin.getUserInfo(saveUser, function(reason) {
//				justep.Util.hint("登录失败: " + JSON.stringify(reason), {
//					"type" : "danger"
//				});
//			});
//		}, function(reason) {
//
//			justep.Util.hint("登录失败: " + JSON.stringify(reason), {
//				"type" : "danger"
//			});
//		});

	};
	// 登录按钮
	Model.prototype.loginIsmBtn = function(event) {

		md5 = new MD5();
		var userid = this.comp("nameInput").val();
		var pwd = this.comp("passwordInput").val();
		if(userid == "" ) {
			justep.Util.hint("请输入用户id");
			return ;
		}
		if(pwd == "" ) {
			justep.Util.hint("请输入密码");
			return ;
		}
		var pwd = md5.hex_md5(pwd);
		$.ajax({
			url : RequestPath + "Login/Login",
			type : "post",
			data : JSON.stringify({
				userid :userid.toUpperCase(),
				pwd :pwd
			}),
			contentType : "application/json",
			success : function(data) {
				console.info(data);
				justep.Util.hint(data.ResponseMessage)
				if (data.ResponseState == "1") {
					justep.Shell.showPage("Home");
				}
				

			}
		})
	};

	Model.prototype.modelLoad = function(event) {
		var userid = justep.Util.getCookie("userid");
		if (userid != null && userid != undefined && userid != "") {
			justep.Shell.showPage("registered");
		}

	};

	return Model;
});