 D
  �1      T   �1  M  T   �1  ~  T   �1  �  T   �1    T   �1  m  T   �1  �  T   �1  �  T   �1  ,  T   �1  r  T   �1  �  T   �1     T   �1  �  T   �1  �  T   �1  8  T   �1  w  T   �1  �  T   �1  *  T   �1  p  T    2  �  T   2  �  T   2  �  T   2  %  T   2  O  T   2  y  T   2  �  T   2  �  T   2  �  T   	2  	  T   
2  9	  T   2  `	  T   2  x	  T   2  �	  T   2  �	  T   2  �	  T   2   
  T   2  T
  T   2  ~
  T   2  �
  T   2  �
  T   2  �
  T   2  �
  T   2  "  T   2  Q  T   d2  �  T   e2  �  T   f2  �  T   g2  �  V   h2  �  T   i2  o  T   j2  y  T   k2  �  V   �2  �  T   �2  i  S   �2  �  T   �2  �  T   �2  �  S   �2  �  S   �2  �  T   �2  �  T   �2    T   �2  9  T   �2  w  T   �2  �  T   �2  �  T   <  1  T   	<  B  T   
<  W  T   <  l  T   <  �  T   <  �  T   <  �  T   <  �  T   <    T   <  8  T   <  e  T   <  �  T   <  �  T   <  �  T   <  B  T   <  l  T   <  �  T   <  �  T   �>  �  T   �>  -  V   �>  N  V   �>  a  T   �>  �  T   �>  �  T   hB  �  T   iB  �  T   jB  )  T   kB  R  T   lB  �  T   mB  B  T   nB  Y  T   oB  �  T   pB  �  T   qB    T   rB  L  T   sB  �  T   tB  �  T   uB  �  T   vB  �  T   wB  ?  T   xB  A  T   yB  Q  T   zB  �  T   {B  �  T   |B    T   }B  T  T   ~B  �  T   B  �  T   �B  	  T   �B  Z  T   �B  \  T   �B  m  T   �B  �  T   �B  �  T   �B  �  T   �B    T   �B  :  T   �B  <  T   �B  Q  T   �B  o  T   �B  �  T   �B  �  T   �B  �  T   �B  �  T   �B  +  T   �B  W  T   �B  �  T   �B  �  T   �B  �  T   �B  D  T   �B  �  T   �B  �  T   �B  �  T   �B  ,  T   �B  [  T   �B  �  T   �B  �  T   �B  �  T   �B  �  T   �B  �  T   �B     T   �B  L   T   �B  �   T   �B  �   T   �B  �   T   �B  �   T   �B  �   T   �B  �   T   �B  �   T   �B  E!  T   �B  z!  T   �B  �!  T   �B  �!  T   �B  �!  T   �B  �!  T   �B  "  T   �B  U"  T   �B  �"  T   �B  �"  T   �B  �"  T   �B  �"  T   �B  #  T   �B  	#  T   �B  #  T   0C  <#  T   1C  i#  T   2C  �#  T   3C  �#  T   4C  %$  T   5C  g$  T   6C  �$  T   7C  �$  T   8C  %  T   9C  ]%  T   :C  ^%  T   ;C  y%  T   <C  �%  T   =C  �%  T   >C  5&  T   ?C  Y&  T   @C  Z&  T   AC  o&  T   BC  �&  T   CC  �&  T   DC  �&  T   EC  �&  T   FC  �&  T   GC  ''  T   HC  d'  S   vC  y'  T   wC  �'  T   xC  �'  T   yC  �'  T   zC  �'  T   {C  �'  T   |C  (  T   }C  F(  T   ~C  v(  T   C  �(  T   �C  �(  T   �C  )  T   �C  /)  T   �C  R)  T   �C  �)  T   �C  �)  T   �C  �)  T   �C  	*  T   �C  0*  T   �C  e*  T   �C  �*  T   �C  �*  T   �C  �*  T   �C  "+  T   PF  Y+  T   
db2convert -h;

db2convert -d <database-name>
           [ -stopBeforeSwap | -continue ]
           [ -u <table-creator-ID> ]
           [ -z <schema-name>
             [ -t <table-name> ] ]
           [ -ts <target-tablespace-name> |
            [-dts <data-tablespace-name> -its <index-tablespace-name>]]
           [ -sts <source-tablespace-name> ]
           [ -opt <ADMIN_MOVE_TABLE-options> ]
           [ -trace ]
           [ -usr <user-ID> -pw <password> ]
           [ -force ]
           [ -o <output-file-name> ]
           [ -check ]
           [ -cancel ]

����
----------
 
-d <database-name>
ָ������ɵı����ڵ����ݿ⡣ 
-stopBeforeSwap
ָ��ֻӦ��Ӧ�� ADMIN_MOVE_TABLE �洢���̵�
INIT��COPY �� REPLAY �׶Ρ� 
-continue
ָ��Ӧ�ü������ڽ����к���ֹͣ��ת��������һֱ����ɡ� 
-u <table-creator-ID>
ָ���û���ʶ�����ָ���� -u����ô��ֻת�����û���ʶ�����ı��� 
-z <schema-name>
ָ��ģʽ���ơ����ָ���� -z����ô��ֻת����ָ����ģʽ�еı��� 
-t <table-name>
ָ��Ҫת���ı������ơ� 
-ts <target-tablespace-name>
ָ���������д�������ɵı��ı��ռ����ơ� 
-dts <data-tablespace-name>
ָ������ɵı��е��������ڵı��ռ����ơ� 
-its <index-tablespace-name>
ָ������ɵı��е��������ڵı��ռ����ơ� 
-sts <source-tablespace-name>
ָ��Դ���ռ�����ơ����ָ���� -sts����ô��ֻת����ָ���ı��ռ��еı��� 
-opt <ADMIN_MOVE_TABLE-options>
ָ�������ݸ� ADMIN_MOVE_TABLE �洢���̵�ѡ�
�����ڶ��ŷָ��б���ָ��ѡ������磬
-opt 'COPY_USE_LOAD, COPY_STATS, KEEP'�� 
-trace
ָ�������� ADMIN_MOVE_TABLE ������������ϡ� 
-usr <user-ID>
ָ�� db2convert ����������¼Զ��ϵͳʱ���û���ʶ�� 
-pw <password>
ָ�� db2convert ����������¼Զ��ϵͳʱ�����롣 
-force
ָ����ת�����б���������������²���ת������Щ����
 - ��ά��Ⱥ (MDC) ��
 - ����ʱ�伯Ⱥ (ITC) ��
 - ������ 
-o <output-file-name>
ָ�����������Ϣ������������ָ�����ļ��� 
-check
ָ��������ʾת��˵�������ָ���˴˲�������ô����ת���κα���
 
-cancel
ָ������ȡ��֮ǰ����ʧ�ܵ�ת��������ʵ�ó��򽫻��Ƴ������м����ݡ�
 
�쳣����ת��˵��
-----------------------------------------  ����     -����������ɵı����������ת����     -�޷�ת�����ñ��Ǿ��廯��ѯ�� (MQT)��     -�޷�ת�����ñ�����������ȫ����ʱ����     -�޷�ת�����ñ����Ѵ�����ȫ����ʱ����     -�޷�ת�����ñ��Ƕ�ά��Ⱥ�� (MDC) ����     -�޷�ת�����ñ��ǲ���ʱ�伯Ⱥ����     -�޷�ת�����ñ������ݷ�������     -��ǿ��ת����ά��Ⱥ (MDC) ����     -��ǿ��ת������ʱ�伯Ⱥ (ITC) ����     -��ǿ��ת����������     -�������Ϊ NOT ENFORCED Ӧ�õ��µ�������ɵı���     -����Ϊ NOT ENFORCED Ӧ�ü��Լ����     -���ӱ���ɾ������������     -�޷�ת�����ñ��ռ���������ɵı������ݡ�     -�޷�ת������ָ���ı��ռ���������ɵı������ݡ�     -�޷�ת�����ñ���������֧�ֵ������͡�     -�޷�ת�����ñ������ͱ���     -�޷�ת�����ñ��Ƿ�Χ��Ⱥ������     -�޷�ת�����ñ�����ͼ��     -�޷�ת�����ñ��Ǳ�����     -�޷�ת����������ɵı���֧�ִ�������     -�޷�ת�������廯��ѯ�� (MQT) �����˸ñ���     -�޷�ת����������ɵı���֧��������Ȩ�������롣 
���� 1 �Լ�������ת����
���� 2 ���˳���
 ���ڼ�������ת��... 
��ת���ı�
----------------------------- ��������"%1S"�� *****************************************************************************************************************  ��������  ���ջ��ܣ� db2convert ʵ�ó����ܴ��ļ� "%1S" ��������� 
db2dsdcfgfill ���

��������������ļ� db2dsdriver.cfg

�﷨��
-------

db2dsdcfgfill { -i <instance-name> |
                -p <instance-path> |
                -migrateCliIniFor.NET -db2cliFile <path> }
              [ -o ]

����

db2dsdcfgfill -h


������
-----------

-i <instance-name>
ָ�����ݿ������ʵ�������ơ�

-p <instance-path>
ָ�����ݿ������ʵ��Ŀ¼������·����

-migrateCliIniFor.NET
���� Windows ����ϵͳ����֧�֡�ָ��Ӧ�� db2cli.ini �ļ��и��Ƶ� db2dsdriver.cfg �ļ��е���Ŀ��

-db2cliFile <path>
���� Windows ����ϵͳ����֧�֡�ָ�� db2cli.ini �ļ�������·����

-o <output-path>
ָ�� db2dsdcfgfill ����� db2dsdriver.cfg �����ļ���·����

-h <help>
��ʾ�����﷨��Ϣ��            ��Ա %1S ����ϸ����            ժҪ����            -------------- ���������Ŀ��%1S �� %1S �еĳ�Ա���� �����ѻ�״̬�ĳ�Ա����         �Ż��������ĵ�ǰֵ�ͽ���ֵ         �Ż�����������ǰ��Ӧ�õ�ֵ  �Ż��������ҳ                           (OPT_BUFFPAGE) = %s  �Ż��������                             (OPT_SORTHEAP) = %s  �Ż��������б�                           (OPT_LOCKLIST) = %s  �Ż������������                         (OPT_MAXLOCKS) = %s ����ʽ�Ựѡ� -c: ��ʵָ�������� -r: �ع�ָ�������� -f: ����ָ�������� -l: ��ʾ���в�ȷ������Ҫ��ʾ�ض���ȷ���������� -l ѡ���ָ��������ı�š� -q: �˳�����ʽ�Ự�� -h: ��ʾ��Խ���ʽ�Ự�İ��� �ڿͻ��������� LIST INDOUBT TRANSACTIONS ����ʱ������ ��������ʽ�Ựʱ���ز�ȷ�������״̬�����ǣ� ����ڷ������ϸ���״̬��������ˢ�¿ͻ����ϵ��������� ��ȷ�������״̬�ڷ������Ϳͻ����ϲ�ͬ������ô������ ����ִ�� LIST INDOUBT TRANSACTIONS ��������� ��ʾΪ��Чѡ������в���������������£��᷵��һ��������Ϣ��ָʾ�� ���·��� LIST INDOUBT TRANSACTIONS ��� ע�⣺Ӧ��������ʾ����ı���ǲ��̶��ġ��˳� ��ǰ����ʽ�Ự�󣬵����´η��� LIST INDOUBT TRANSACTIONS ����ʱ�����ſ��ܲ�ͬ�� 
db2checkCOL <database-alias>
            [-checkFP]
            [<userid>  <password>]

ѡ�

<database-alias>
ָ���������ݿ�ı�����

-checkFP
�� db2checkCOL ʵ�ó���ԭΪ�����޶�������֮����ʹ��ʶ���޷����ʵı�

<userid>
ָ�� db2checkCOL ʵ�ó����������������ݿ���û���ʶ��

<password>
ָ�������û���ʶ�����롣 ���󣺱� "%1S"��"%2S" �ܵ�Ӱ�졣 ���ڷ����� "%1S"�� ��ʼ�׶� 1���� 3 ���׶Σ�����ѯĿ¼�� ��ʼ�׶� 2���� 3 ���׶Σ������ر������� ��ʼ�׶� 3���� 3 ���׶Σ��������������� CREATE DATABASE database-name [AT DBPARTITIONNUM | [AUTOMATIC STORAGE {NO | YES}] [ON drive[{,drive}...][DBPATH ON drive]] [ALIAS database-alias] [USING CODESET codeset TERRITORY territory] [COLLATE USING {SYSTEM | IDENTITY | IDENTITY_16BIT | COMPATIBILITY | NLSCHAR | UCA400_NO | UCA400_LSK | UCA400_LTH | language-aware-collation | locale-sensitive-collation}] [PAGESIZE integer [K]] [NUMSEGS numsegs] [DFT_EXTENT_SZ dft_extentsize] [RESTRICTIVE] [ENCRYPT [Encryption Options] [Master Key Options] [CATALOG TABLESPACE tblspace-defn] [USER TABLESPACE tblspace-defn] [TEMPORARY TABLESPACE tblspace-defn] [WITH "comment-string"]] [AUTOCONFIGURE [USING config-keyword value [{,config-keyword value}...]] [APPLY {DB ONLY | DB AND DBM | NONE}]]   language-aware-collation, locale-sensitive-collation: Ҫ�˽�ѡ���ȫ���б����������Ϣ��������Ϊ��CREATE DATABASE ��������⡣   tblspace-defn��   MANAGED BY { SYSTEM USING ('string' [ {,'string'} ... ] ) |   DATABASE USING ({FILE | DEVICE} 'string' number-of-pages   [ {,{FILE | DEVICE} 'string' number-of-pages} ... ]) | AUTOMATIC STORAGE}   [EXTENTSIZE number-of-pages] [PREFETCHSIZE number-of-pages]   [OVERHEAD number-of-milliseconds] [TRANSFERRATE number-of-milliseconds]   [NO FILE SYSTEM CACHING | FILE SYSTEM CACHING]   [AUTORESIZE {NO | YES}] [INITIALSIZE integer {K |M |G}]   [INCREASESIZE integer {PERCENT |K |M |G}] [MAXSIZE {NONE | integer {K |M |G}}]   config-keyword��   MEM_PERCENT��WORKLOAD_TYPE��NUM_STMTS��TPM��ADMIN_PRIORITY��   NUM_LOCAL_APPS��NUM_REMOTE_APPS��ISOLATION �� BP_RESIZEABLE��   Encryption Options��   CIPHER {AES | 3DES} [MODE CBC] KEY LENGTH key-length   Master Key Options��   MASTER KEY LABEL label-name RESTORE DATABASE source-db-alias { restore-options | CONTINUE | ABORT }  restore-options��   [USER username [USING password]]   [Restore-Inventory-Clause][INCREMENTAL [AUTOMATIC | ABORT]]   [Media-Target-Clause][TAKEN AT date-time]   [[TO target-directory] | [ON drive[{,drive}...][DBPATH ON drive]]]   [TRANSPORT [STAGE IN staging-db-alias] [USING STOGROUP stogroup-name]]   [INTO target-db-alias]   [LOGTARGET {{directory | DEFAULT} | {{INCLUDE | EXCLUDE} [FORCE]}}]   [NEWLOGPATH {directory | DEFAULT}] [WITH num-buff BUFFERS]   [BUFFER buffer-size] [REPLACE HISTORY FILE] [REPLACE EXISTING]   [REDIRECT [GENERATE SCRIPT file-name]] [PARALLELISM n]   [COMPRLIB lib-name] [COMPROPTS options-string]   [ENCRLIB lib-name] [ENCROPTS options-string]   [NO ENCRYPT | ENCRYPT [Encryption Options] [Master Key Options]]   [WITHOUT ROLLING FORWARD] [WITHOUT PROMPTING]  Restore-Inventory-Clause��   rebuild-options |   TABLESPACE [ONLINE] |   TABLESPACE (tblspace-name [ {,tblspace-name} ... ])   [SCHEMA (schema-name [ {,schema-name} ... ])] [ONLINE] |   HISTORY FILE [ONLINE] |   LOGS [ONLINE] |   COMPRESSION LIBRARY [ONLINE]  Media-Target-Clause��   USE {TSM | XBSA}     [OPEN num-sess SESSIONS] | SNAPSHOT [ {LIBRARY | SCRIPT} filename] ]     [OPTIONS {options-string | options-filename}] |    LOAD lib-name [OPEN num-sess SESSIONS]     [OPTIONS {options-string | options-filename}] |    FROM dir/dev [{,dir/dev} ... ]  rebuild-options��   REBUILD WITH {ALL TABLESPACES IN {DATABASE | IMAGE} [EXCEPT TABLESPACE   (tblspace-name [ {,tblspace-name} ... ])] | TABLESPACE (tblspace-name   [ {,tblspace-name} ... ])}  Encryption Options��   CIPHER {AES | 3DES} [MODE CBC] KEY LENGTH key-length   Master Key Options��   MASTER KEY LABEL label-name RECOVER DATABASE database-alias [TO {isotime [USING LOCAL TIME | USING UTC TIME] [ON ALL DBPARTITIONNUMS] | END OF LOGS [On-DbPartitionNum-Clause]}] [USER username [USING password]] [USING HISTORY FILE (history-file [{, history-file ON DBPARTITIONNUM db-partition-number} ... ])] [OVERFLOW LOG PATH (log-directory [{,log-directory ON DBPARTITIONNUM db-partition-number} ... ])] [COMPRLIB lib-name] [COMPROPTS options-string] [ENCRLIB lib-name] [ENCROPTS options-string] [RESTART] [NO ENCRYPT | ENCRYPT [Encryption Options] [Master Key Options]]  On-DbPartitionNum-Clause��   ON {{DBPARTITIONNUM | DBPARTITIONNUMS} (db-partition-number   [TO  db-partition-number] , ... ) | ALL DBPARTITIONNUMS [EXCEPT    {DBPARTITIONNUM | DBPARTITIONNUMS} (db-partition-number    [TO db-partition-number] , ...)]}  Encryption Options��   CIPHER {AES | 3DES} [MODE CBC] KEY LENGTH key-length   Master Key Options��   MASTER KEY LABEL label-name ���ݹ� ZLOAD FROM <filename> OF  [ DEL [ALF | ENL | CRLF ] | INT | SPANNED] [WITH <utilityid> ] [ MESSAGES <filename> ] USING <loadstmt>   ʵ�ó����ʶ = %1S UPDATE COMMAND OPTIONS USING {options ...}   ѡ�    a {ON|OFF}             ��ʾ SQLCA    b {ON|OFF}             �Զ���    c {ON|OFF}             �Զ���ʵ    d {ON|OFF}             ��������ʾ XML ����    e {ON {C|S} | OFF}     ��ʾ SQLCODE/SQLSTATE    i {ON|OFF}             ��ʾ XML ���ݲ���������    l {ON filename | OFF}  �������¼����ʷ��¼�ļ���    m {ON|OFF}             ��ʾ��Ӱ�������    n {ON|OFF}             ��ȥ�����ַ�    o {ON|OFF}             ��ʾ���    p {ON|OFF}             ��ʾ db2 ����ʽ��ʾ��    q {ON|OFF}             �����ո�ͻ��з�    r {ON filename | OFF}  ��������汣�浽�ļ�    s {ON|OFF}             �������ʱִֹͣ��    v {ON|OFF}             �ش���ǰ����    w {ON|OFF}             ��ʾ FETCH/SELECT ������Ϣ    x {ON|OFF}             ��ֹ��ӡ�б���    y {ON|OFF}             �ӷ�������ȡ SQL ��Ϣ�ı�    z {ON filename | OFF}  ������������浽�ļ�  ���̻����Զ�ջ����        (PL_STACK_TRACE) = %s  �����ݹ�                                         = %s  FCM ��������С                            (FCM_BUFFER_SIZE) = %s 