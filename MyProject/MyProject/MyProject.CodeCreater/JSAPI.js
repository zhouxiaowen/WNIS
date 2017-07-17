

	 
   
//setBusy调用方法
abp.ui.setBusy("#main",
	abp.services.app.SysMenuModule.QuerySysMenuModule(input)
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
		})
);

	 
   
//setBusy调用方法
abp.ui.setBusy("#main",
	abp.services.app.SysMenuModule.QuerySysMenuModule(input)
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
		})
);

	 
   
//setBusy调用方法
abp.ui.setBusy("#main",
	abp.services.app.SysMenuModule.QuerySysMenuModule(input)
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
		})
);

	 
   
//setBusy调用方法
abp.ui.setBusy("#main",
	abp.services.app.SysMenuModule.QuerySysMenuModule(input)
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
		})
);

	 
   
//setBusy调用方法
abp.ui.setBusy("#main",
	abp.services.app.SysMenuModule.QuerySysMenuModule(input)
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
		})
);


