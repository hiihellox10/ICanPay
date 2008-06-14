目录结构：
在线支付 ---
		| --- Index.html:首页(在线支付测试，订单状态查询)
		| --- Req.aspx:支付请求代码示例
		| --- Callback.aspx:支付结果返回示例代码
		| --- QueryOrderStatus.aspx:查询订单状态代码示例
		
		| --- [Bin]
					| --- com.yeepay.dll 易宝支付封装Dll
		
		API函数说明：（命名空间com.yeepay）
		
		创建支付请求表单
			方法：Buy.CreateForm(merchantId, keyValue, orderId, amount, cur, productId, productCat, productDesc, merchantCallbackURL, addressFlag, sMctProperties, frpId, period, periodType, formName)
			返回类型：String
			参数说明：
				merchantId		商家编号
				keyValue		商家密钥
				orderId			订单号
				amount			订单金额
				cur			币种 人民币:CNY
				productId		商品名称/id
				productCat		商品描述
				productDesc		商品说明
				merchantCallbackURL	回调url
				sMctProperties		商家扩展信息
				frpId			银行id，如果为空到易宝支付网关，请参考银行列表.xls
				period			订单有效期
				periodType		订单有效期单位
				formName		创建的表单名称

		创建支付请求url
			方法：Buy.CreateForm(merchantId, keyValue, orderId, amount, cur, productId, productCat, productDesc, merchantCallbackURL, addressFlag, sMctProperties, frpId)
			返回类型：String
			参数说明：
				merchantId		商家编号
				keyValue		商家密钥
				orderId			订单号
				amount			订单金额
				cur			币种 人民币:CNY
				productId		商品名称/id
				productCat		商品描述
				productDesc		商品说明
				merchantCallbackURL	回调url
				sMctProperties		商家扩展信息
				frpId			银行id，如果为空到易宝支付网关，请参考银行列表.xls
				
		校验返回数据包
			方法：Buy.VerifyCallback(merchantId, keyValue, sCmd, sErrorCode, sTrxId, amount, cur, productId, orderId, userId, mp, bType, svrHmac)
			返回类型：bool

		查询订单状态
			方法：Buy.QueryOrder(merchantId, keyValue, orderId)
			返回类型：QueryOrdResult
			参数说明：
				merchantId	商户编号
				keyValue		商家密钥
				orderId			商家订单号