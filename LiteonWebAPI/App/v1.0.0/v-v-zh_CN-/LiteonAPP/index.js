//define( RequestPath ="http://192.168.199.208/api/")
//define( RequestPath ="http://localhost:61542/api/")
define( RequestPath ="http://liteonapi.azurewebsites.net/api/")
define(function(require) {
	
	var $ = require("jquery");
	var justep = require("$UI/system/lib/justep");
	var ShellImpl = require('$UI/system/lib/portal/shellImpl');
	
	var Model = function() {
		this.callParent();
		var shellImpl = new ShellImpl(this, {
			"contentsXid" : "pages",
			"pageMappings" : {
				"login" : {
					url : require.toUrl('./login.w')
				},
				"registered" : {
					url : require.toUrl('./registeMobile.w')
				},
				"regSucc" : {
					url : require.toUrl('./RegisteSucc.w')
				},
				"Forget" : {
					url : require.toUrl('./Forget.w')
				},
				"Home" : {
					url : require.toUrl('./Home.w')
				}
				
			}
		});
		
	};

	Model.prototype.modelLoad = function(event) {
	};

	return Model;
});