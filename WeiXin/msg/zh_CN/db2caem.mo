 H   �      V   �  o   T   �  F  T   �  Y  T   �  n
  T   �  ,  T              CLI ��Ϣ��SQL ��䣺 "%1S"
                    �����룺 "%2S"
                    �����Ϣ�� "%3S"
 
db2caem��DB2 Capture Activity Event Monitor ���ݹ��ߣ�
------------------------------------------------------------------------------
�﷨��db2caem [ -d <database_name> ][ -u <user_id>  -p <password> ]
      [ query-statement-options | event-monitor-options ]
      [ -o <output_path> ][ -h ]

��ѯ���ѡ�
  [ -st <query_statement> | -sf <query_statement_file> ]
  [ -compenv <compilation_env_file> ][ -tbspname <table_space_name> ]
  [ -terminator <termination_character> ]

�¼�������ѡ�
  [ -actevm <event_monitor_name> -appid <application_id> -uowid <uow_id>
    -actid <activity_id> ]

���������

  -d <database_name>                   ָ��Ҫ���ӵ������ݿ�����ơ�


  -h                                   ��ʾ������Ϣ��ָ���˴�ѡ��
                                       ʱ����������������ѡ���
                                       �ҽ���ʾ������Ϣ��

  -o <output_path>                     db2caem ������ļ�����д��
                                       ���û�ָ����·����

  -p <password>                        ָ�������ӵ����ݿ�ʱ��ʹ��
                                       ���û���ʶ�����롣

  -u <user_id>                         ָ�����ӵ����ݿ�ʱ��ʹ�õ�
                                       �û���ʶ��

 
��ѯ���ѡ�
  -compenv <compilation_env_file>      ָ����ִ�� SQL ���ʱ��ʹ��
                                       �ı��뻷�� (comp_env_desc)��

                                       ���뻷���� BLOB �������ͣ�
                                       ����ͨ��ĳ���ļ���ָ����

  -st <query_statement>                ָ������Ϊ�䲶���¼�����
                                       �����ݵ� SQL ��䡣��������ݿ�ִ�� SQL ��䡣

  -sf <query_statement_file>           ָ����������Ϊ�䲶���¼���
                                       �������ݵ� SQL �����ļ�·����

                                       ʹ�� -terminator ѡ��ָ����
                                       �ڱ�� SQL ���Ľ�β����
                                       ������������ݿ�ִ�� SQL ��䡣

   
-tbspname <table_space_name>           ָ��Ҫ�����д�����¼���
                                       �����ı��ռ����ơ��ڷ�����
                                       �ݿ⻷���У����ڽ���������
                                       �и���Ȥ�� SQL ����������
                                       �ݿ�����ϣ����ռ�Ӧ������
                                       ������Щ���ݿ�����ϡ����
                                       δָ������ô�ڴ�����¼�
                                       ������ʱ����ʹ��ȱʡ���ռ䡣
 
  -terminator <termination_character>  ָ��ĳ���ַ������ַ�ָʾ -sf
                                       SQL �ļ��� SQL ���Ľ�����ȱ
                                       ʡֵΪ�ֺš�

�¼�������ѡ�
  -actevm <event_monitor_name>         ָ�����л�¼������������ƣ�
                                       �ü������а�������Ȥ������
                                       ���ݡ�
  -appid <application_id>              ָ��Ӧ�ó����ʶ
                                       (appl_id monitor element)��
                                       ����Ψһ��ʶ��������Ȥ�����
                                       ��Ӧ�ó���

  -uowid <uow_id>                      ָ��Ҫ������ִ�и���Ȥ�����
                                       �Ĺ�����Ԫ��ʶ
                                       (uow_id monitor element)��

  -actid <activity_id>                 ָ������Ȥ�����Ļ��ʶ
                                       ��activity_id ����Ԫ�أ���
 
      ____________________________________________________________________


                      _____     D B 2 C A E M     _____

                DB2 Capture Activity Event Monitor ���ݹ���
                              I      B      M


          DB2CAEM Tool ��һ��ʵ�ó������ڲ����¼����������ݣ�
         ������ϸ��Ϣ���ں�ֵ�Լ�ʵ��ֵ��
      ____________________________________________________________________


________________________________________________________________________________
 