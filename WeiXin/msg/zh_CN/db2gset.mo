 t         S        S      O  T      b  S      �  T      .  T      Y  T      �  S   	   �  T   
     S      +  S      Y  S      �  S      �  T      �  T      �  S   d   )  T   e   �  T   f   
  T   g   �  T   h   !  T   i     T   j   4  T   k   �  T   l   i  T   m   �  T   n   H  T   o     T   q   �  T   s   �  T   t   6  T   u   �  T   v   �  T   w   �  T   x   �  T   y     T   z   �  T   {   �  T   |   �!  T   }   "  T   ~   �"  T      P#  T   ,  j#  S   -  �#  S   .  �#  S   /  �#  S   0  $  S   1  $  S   2  �$  S   3  �$  S   4  $%  S   5  Q%  S   6  �%  S   7  �%  T   �  �%  S   �  �%  S   �  �%  S   �  )&  T   �  J&  S   �  h&  T   �  �&  T   �  �&  T   �  '  T   �  t'  T   �  �'  T   �  �'  S   �  �'  T   �  �'  T   �  (  T   �  (  T   �  0(  T      G(  T     Z(  T     k(  T     �(  T   X  �(  S   Y  v)  T   Z  �)  S   [  �)  S   \  *  T   ]  *  S   ^  B*  T   _  T*  T   `  z*  T   a  �*  T   b  �*  S   c  �+  T   d  F,  S   e  �,  T   f  �,  S   g  �,  T   h  �,  T   i  -  T   j  1-  T   k  \-  T   

�÷���%1S <connect options> <command>

<connect options>
    CONNECT TO database-name [ USER user-id USING password ]

<command>
    GET GEOMETRY STATISTICS
        { FOR COLUMN table-schema . table-name ( column-name )
            [ USING GRID SIZES ( grid-size-1, grid-size-2, grid-size-3 ) ] |
          FOR INDEX index-schema . index-name [ DETAIL ] }
        [ ANALYZE number { ROWS | PERCENT } [ ONLY ] ]
        [ SHOW [ MINIMUM BOUNDING RECTANGLE ] HISTOGRAM [ WITH n BUCKETS ] ]
        [ ADVISE [ GRID SIZES ] ]

 

������%1S
�ǿռ���ͼ������%2S
�յļ���ͼ������%3S
��ֵ����%4S
 δ��ȷ�����ݷ�Χ�� �����漰�ķ�Χ��
    X ����Сֵ��%1S
    X �����ֵ��%2S
    Y ����Сֵ��%3S
    Y �����ֵ��%4S
 

ֱ��ͼ��
----------
    MBR ��С             ����ͼ�μ���
    -------------------- -------------------- ע����MBR ��С�� 0���㣩���� ST_Point ֵ�� 

��ѯ���ڴ�С��         ����������С��                ������Ŀ�ɱ���
--------------------   -----------------------------   ---------------------- ��С %1S��%2S �Ҳ���������Ŀ�� 

���񼶱� %1S
------------
 �����С                                ��%1S ����ͼ����                              ��%1S
������Ŀ��                              ��%2S
 ռ�õ�����Ԫ����                      ��%3S
��������Ŀ/����ͼ�Ρ�����              ��%4S
������ͼ��/����Ԫ�񡱱���            ��%5S
ÿ������Ԫ�����󼸺�ͼ����          ��%1S
ÿ������Ԫ�����С����ͼ����          ��%2S
 û�м���ͼ�δ����˼���������� ÿ������ͼ�ε�������Ŀ������ ������Ŀ��    ��%1S
--------------- %2S
����ֵ        ��%3S
�ٷֱȣ�%5S��  ��%4S
 

�÷��� db2se alter_cs [-h]
       db2se alter_srs [-h]
       db2se create_cs [-h]
       db2se create_srs [-h]
       db2se disable_autogc [-h]
       db2se disable_db [-h]
       db2se drop_cs [-h]
       db2se drop_srs [-h]
       db2se enable_autogc [-h]
       db2se enable_db [-h]
       db2se export_shape [-h]
       db2se import_shape [-h]
       db2se upgrade [-h]
       db2se register_gc [-h]
       db2se register_spatial_column [-h]
       db2se remove_gc_setup [-h]
       db2se restore_indexes [-h]
       db2se run_gc [-h]
       db2se save_indexes [-h]
       db2se setup_gc [-h]
       db2se shape_info [-h]
       db2se unregister_gc [-h]
       db2se unregister_spatial_column [-h]

 

�÷���db2se alter_cs db_name
              [-userId user_id -pw password]
              -coordsysName cs_name
              [-definition def_string]
              [-organization org_name]
              [-organizationCoordsysId org_cs_id]
              [-description description_string]

 

�÷���db2se alter_srs db_name
              [-userId user_id -pw password]
              -srsName srs_name
              [-srsId srs_id]
              [-xOffset x_offset]
              [-xScale x_scale]
              [-yOffset y_offset]
              [-yScale y_scale]
              [-zOffset z_offset]
              [-zScale z_scale]
              [-mOffset m_offset]
              [-mScale m_scale]
              [-coordsysName cs_name]
              [-description description_string]

 

�÷���db2se create_cs db_name
              [-userId user_id -pw password]
              -coordsysName cs_name
              -definition define_string
              [-organization organization]
              [-organizationCoordsysId org_cs_id]
              [-description description_string]

 

�÷���db2se create_srs db_name
              [-userId user_id -pw password]
              -srsName srs_name
              -srsId srs_id
              [-xOffset x_offset
              -xScale x_scale
              [-yOffset y_offset]
              [-yScale y_scale]
              [-zOffset z_offset]
              [-zScale z_scale]
              [-mOffset m_offset]
              [-mScale m_scale]
              -coordsysName cs_name
              [-description description_string]

 

�÷���db2se create_srs db_name
              [-userId user_id -pw password]
              -srsName srs_name
              -srsId srs_id
              -xMin x_min
              -xMax x_max
              -xScale x_scale
              -yMin y_min
              -yMax y_max
              [-yScale y_scale]
              -zMin z_min
              -zMax z_max
              [-zScale z_scale]
              -mMin m_min
              -mMax m_max
              [-mScale m_scale]
              -coordsysName cs_name
              [-description description_string]

 

�÷���db2se disable_autogc db_name
              [-userId user_id -pw password]
              [-tableSchema table_schema]
              -tableName table_name
              -columnName column_name

 

�÷���db2se disable_db db_name
              [-userId user_id -pw password]
              [-force value]

 

�÷���db2se drop_cs db_name
              [-userId user_id -pw password]
              -coordsysName cs_name

 

�÷���db2se drop_srs db_name
              [-userId user_id -pw password]
              -srsName srs_name

 

�÷���db2se enable_autogc db_name
              [-userId user_id -pw password]
              [-tableSchema table_schema]
              -tableName table_name
              -columnName column_name

 

�÷���db2se enable_db db_name
              [-userId user_id -pw password]
              [-tableCreationParameters tcParams]

 

�÷���        db2se export_shape db_name
              [-userId user_id -pw password]
              -fileName file_name
              [-appendFlag append_flag]
              [-outputColumnNames column_names]
              -selectStatement statement
              [-messagesFile msg_file]
              [-client client_flag]

 

�÷���        db2se import_shape db_name
              [-userId user_id -pw password]
              -fileName file_name
              [-inputAttrColumns input_columns]
              -srsName srs_name
              [-tableSchema table_schema]
              -tableName table_name
              [-tableAttrColumns attr_columns]
              [-createTableFlag create_flag]
              [-tableCreationParameters tc_params]
              -spatialColumn spatial_column
              [-typeSchema type_schema]
              [-typeName type_name]
              [-inlineLength length]
              [-idColumn id_column]
              [-idColumnIsIdentity id_flag]
              [-restartCount rs_count]
              [-commitScope commit_count]
              [-exceptionFile efile_name]
              [-messagesFile mfile_name]
              [-client client_flag]

 

�÷���  db2se upgrade db_name
              [-userId user_id -pw password]
              [-tableCreationParameters tcParams]
              [-force value]
              [-messagesFile mFile_name]

 

�÷���db2se register_gc db_name
              [-userId user_id -pw password]
              -geocoderName geocoder_name
              [-functionSchema function_schema]
              {-functionName function_name | -specificName specific_name}
              [-defaultParameterValues default_param_values]
              [-parameterDescriptions param_descriptions]
              [-vendor vendor]
              [-description description_string]

 

�÷���db2se register_spatial_column db_name
              [-userId user_id -pw password]
              [-tableSchema schema]
              -tableName table_name
              -columnName column_name
              -srsName srs_name
              [-computeExtents value]

 

�÷���db2se remove_gc_setup db_name
              [-userId user_id -pw password]
              [-tableSchema table_schema]
              -tableName table_name
              -columnName column_name

 

�÷���db2se restore_indexes db_name
              [-userId user_id -pw password]
              [-messagesFile mFile_name]

 

�÷���db2se run_gc db_name
              [-userId user_id -pw password]
              [-tableSchema table_schema]
              -tableName table_name
              -columnName column_name
              [-geocoderName geocoder_name]
              [-parameterValues parameter_values]
              [-whereClause where_clause]
              [-commitScope commit_scope]

 

�÷���db2se save_indexes db_name
              [-userId user_id -pw password]
              [-messagesFile mFile_name]

 

�÷���db2se setup_gc db_name
              [-userId user_id -pw password]
              [-tableSchema table_schema]
              -tableName table_name
              -columnName column_name
              -geocoderName geocoder_name
              [-parameterValues parameter_values]
              [-autogeocodingColumns auto_gc_Columns]
              [-whereClause where_clause]
              [-commitScope commit_scope]

 

�÷���db2se shape_info -fileName file_name
              [-database db [-userId user_id -pw password]]

 

�÷���db2se unregister_gc db_name
              [-userId user_id -pw password]
              -geocoderName geocoder_name

 

�÷���db2se unregister_spatial_column db_name
              [-userId user_id -pw password]
              [-tableSchema schema]
              -tableName table
              -columnName column

 �����������ݿ⡣��ȴ�... ��ʼ�� %1S �е��뵽�� "%2S"."%3S" ��... δ�ܲ������ %2S �еĿ� %1S����һ�У�%3S���� ��ʼ��ʵ�� %1S...���� %2S �У���һ��Ϊ��%3S���� �ɹ�����ʵ�˿� %1S�� δ����ʵ�� %1S�� ����ɵ���...
������                   = %1S
�ѳ��Ե�����             = %2S
����ʵ������             = %3S
�Ѿܾ�������             = %4S
�Ѳ��뵫��δ��ʵ������   = %5S
 ÿ�� INSERT ���ʹ�� %1S �С� ����״�ļ��ĵ� %3S �����ҵ�����Ч %1S ֵ "%2S"����ʹ�� NULL ֵ�����档 �ɹ��ش�����Ҫ�����ݵ������ı� "%1S"."%2S"�� �ɹ�����֤��Ҫ�����ݵ������ı� "%1S"."%2S" �Ľṹ�� �Ա� "%1S"."%2S" ȡ�������˼�¼�� δȷ�� ���ڿ�ʼ�������ļ� %1S... SELECT ��� "%1S" ��Ч�� ����ɵ���...
�ѳ��Ե����� = %1S
�ѵ��������� = %2S
��ʧ�ܵ����� = %3S
 ��δ���� SELECT ����е������С� ����� "%1S" ��������������� ������͸�ԭӦ�ó���������ж��� ��ԭ��Ӧ�ó���������ж��� ����Ӧ�ó������� DB2 Spatial Extender ������������ԡ�����ɾ��Ӧ�ó������֮�����ִ�������� Ӧ�����°����г��������Ϊ���Ǵ����� DB2 Spatial Extender ������Щ�����������ڼ�����Ѿ������˸��ġ� ���ڱ������пռ������� ���ڸ�ԭ���пռ������� %1S "%2S"."%3S" ȡ���ڶ��� "%4S"."%5S"�� ���ڶ����ݿ������������ȴ�... ���ݿ��д��ڵĿռ��У� ���ݿ��в����ڿռ��� ���ݿ��д��ڵĿռ������� ���ݿ��в����ڿռ����� ��ע������������� *** �޷����� *** ���ڶԵ������������������� ���ڶ԰�װ�ĵ���������������� 
��״�ļ���Ϣ
----------------------
�ļ�����                   = %1S
�ļ����ȣ�16 λ�֣�        = %2S
��״�ļ��汾               = %3S
��״����                   = %4S��%5S��
��¼��                     = %6S ��״�ļ�ֻ��������״ �� û�б߽����á� 
��С X ���� = %1S
��� X ���� = %2S
��С Y ���� = %3S
��� Y ���� = %4S ��С Z ���� = %1S
��� Z ���� = %2S ��״û�� Z ���ꡣ ��С M ���� = %1S
��� M ���� = %2S ��״û�� M ���ꡣ ��������״�����ļ�����չ��Ϊ .shx���� ������״�����ļ�����չ��Ϊ .shx���� �����������ļ�����չ��Ϊ .dbf���� �����ļ���Ϣ
--------------------------
dBase �ļ�����                 = %1S
���һ�θ��µ�����             = %2S
��¼��                         = %3S
ͷ�е��ֽ���                   = %4S
ÿ����¼�е��ֽ���             = %5S
����                           = %6S 
�к�           ����             ��������        ����    С��λ
-------------  ---------------  --------------  ------  -------    %1S             %2S               %3S (%4S)         %5S      %6S      �����ڰ�����״���ݵ�����ϵ������ļ��� ����ϵ���壺"%1S" .prj �ļ��е�����ϵ��Ч�� ���ݵĿռ�ο�ϵ
------------------------------------ �Ҳ������ݵĿռ�ο�ϵ�� ���ݵ�����ϵ
----------------------------- �Ҳ������ݵ�����ϵ�� 