 �           T   
   �  S      �  S        S   2   ;  S   <   d  S   F   �  S   P   �  T   Z   �  S   d     S   n   J  S   x   k  S   �   �  T   �   �  S   �   �  S   �     S   �   /  S   �   R  T   �   v  T   �  �  T   
 DB2EXTSEC [/u[sers] usergroup /a[dmins] admingroup] [/oldadmins oldadmngrp
            /oldusers oldusergrp /file inputfile] [/verbose]] | [/r[eset]] |
           [/h[elp]|?]

 ���У�

   -u[sers] usergroup    Ҫ����/ʹ�õ� DB2 �û���
                         ��ȱʡֵ��DB2USERS����
   -a[dmins] admingroup  Ҫ����/ʹ�õ� DB2 ������
                         ��ȱʡֵ��DB2ADMNS����
   -oldusers oldusergrp  Ҫ���ĵľ� DB2 �û������ơ�
   -oldadmins oldadmngrp Ҫ���ĵľ� DB2 ���������ơ�
   -file inputfile       ������ʾ��Ҫ����������Ȩ�������ļ�/Ŀ¼���ļ���
   -verbose              ���������Ϣ
   -r[eset]              ������ǰ�����ĸ��ģ�ע�⣺����δ������������ʱ���˲����������ã���
   -h[elp]|-?            ��ʾ�˰����ļ���

 ʾ����

 ������չ��ȫ�Բ�ʹ������ mydom\db2users �� mydom\db2admns ������ DB2 ����

 db2extsec /u mydom\db2users /a mydom\db2admns

 ����չ��ȫ�Իָ�Ϊ��ǰ�����á����������˵����

 db2extsec /reset

 ����Ҫ����������������չ��ȫ�ԣ����һ�Ҫ�������� db2admns ��
 db2users �е� c:\mylist.lst ������ʾ���ļ�/Ŀ¼�İ�ȫ��
 ����Ϊ���� mydom\db2admns �� mydom\db2users��

 db2extsec /users mydom\db2users /admins mydom\db2admns /oldadmins db2admns
           /oldusers db2users /file c:\mylist.lst

 ע�⣺�����ļ��ĸ�ʽΪ������ʾ��

    * This is a comment
    D:\MYBACKUPDIR
    D:\MYEXPORTDIR
    D:\MYMISCFILE\myfile.dat

    * This is another comment
    E:\MYOTHERBACKUPDIR                 * This is more comments
    E:\MYOTHEREXPORTDIR
  DB2EXTSEC: Drive/Share %1S ��֧����չ���ԡ� DB2EXTSEC: ����ʧ�ܣ�%1S=%2S �� DB2EXTSEC: δ�ܶ�ȡ DBM ���ã�rc=%1S �� DB2EXTSEC: δ�ܴ��� "%1S" �������Ȩ�ޡ� DB2EXTSEC: �Ҳ����� "%2S" ָ���ľɵ� "%1S" ������ DB2EXTSEC: δ������ "%1S" �� ACE�� DB2EXTSEC: ������ InstallPath �µ�Ŀ¼�Ĵ����� DB2EXTSEC: ָ���� %3S ʱ����Ҫ %1S �� %2S ������ DB2EXTSEC: �򲻿��� "%2S" ָ���������ļ� "%1S"�� DB2EXTSEC: ָ���Ĳ��� %1S ��Ч�� DB2EXTSEC: ������������� %1S �� DB2EXTSEC: ����һ�� DPF ʵ����������ʹ�����顣 DB2EXTSEC: ���棬δ���л���ʵ�� %1S ��RC=%2S ���� DB2EXTSEC: ���ڴ���ʵ�� %1S... DB2EXTSEC: ���ڴ�����װ���� %1S... DB2EXTSEC: ���ڴ��������ļ� %1S... DB2EXTSEC: ���ڴ������� DB2 ����... DB2EXTSEC: ���ڴ�����ֹ����ֹ���� DB2 ʵ��������һ�Ρ� �ѳɹ���� DB2EXTSEC ��� 