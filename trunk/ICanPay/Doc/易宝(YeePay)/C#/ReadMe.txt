Ŀ¼�ṹ��
����֧�� ---
		| --- Index.html:��ҳ(����֧�����ԣ�����״̬��ѯ)
		| --- Req.aspx:֧���������ʾ��
		| --- Callback.aspx:֧���������ʾ������
		| --- QueryOrderStatus.aspx:��ѯ����״̬����ʾ��
		
		| --- [Bin]
					| --- com.yeepay.dll �ױ�֧����װDll
		
		API����˵�����������ռ�com.yeepay��
		
		����֧�������
			������Buy.CreateForm(merchantId, keyValue, orderId, amount, cur, productId, productCat, productDesc, merchantCallbackURL, addressFlag, sMctProperties, frpId, period, periodType, formName)
			�������ͣ�String
			����˵����
				merchantId		�̼ұ��
				keyValue		�̼���Կ
				orderId			������
				amount			�������
				cur			���� �����:CNY
				productId		��Ʒ����/id
				productCat		��Ʒ����
				productDesc		��Ʒ˵��
				merchantCallbackURL	�ص�url
				sMctProperties		�̼���չ��Ϣ
				frpId			����id�����Ϊ�յ��ױ�֧�����أ���ο������б�.xls
				period			������Ч��
				periodType		������Ч�ڵ�λ
				formName		�����ı�����

		����֧������url
			������Buy.CreateForm(merchantId, keyValue, orderId, amount, cur, productId, productCat, productDesc, merchantCallbackURL, addressFlag, sMctProperties, frpId)
			�������ͣ�String
			����˵����
				merchantId		�̼ұ��
				keyValue		�̼���Կ
				orderId			������
				amount			�������
				cur			���� �����:CNY
				productId		��Ʒ����/id
				productCat		��Ʒ����
				productDesc		��Ʒ˵��
				merchantCallbackURL	�ص�url
				sMctProperties		�̼���չ��Ϣ
				frpId			����id�����Ϊ�յ��ױ�֧�����أ���ο������б�.xls
				
		У�鷵�����ݰ�
			������Buy.VerifyCallback(merchantId, keyValue, sCmd, sErrorCode, sTrxId, amount, cur, productId, orderId, userId, mp, bType, svrHmac)
			�������ͣ�bool

		��ѯ����״̬
			������Buy.QueryOrder(merchantId, keyValue, orderId)
			�������ͣ�QueryOrdResult
			����˵����
				merchantId	�̻����
				keyValue		�̼���Կ
				orderId			�̼Ҷ�����