  �           T   
     S      P  S      w  S   2   �  S   <   �  S   F   &  S   P   P  T   Z   �  S   d   �  S   n   	  S   x   C	  S   �   p	  T   �   �	  S   �    
  S   �   &
  S   �   P
  S   �   x
  T   �   �
  T   �  �
  T   
 DB2EXTSEC [/u[sers] usergroup /a[dmins] admingroup] [/oldadmins oldadmngrp
            /oldusers oldusergrp /file inputfile] [/verbose]] | [/r[eset]] |
           [/h[elp]|?]

 where:

   -u[sers] usergroup    The DB2 users group to be created/used
                         (default: DB2USERS).
   -a[dmins] admingroup  The DB2 admins group to be created/used
                         (default: DB2ADMNS).
   -oldusers oldusergrp  The old DB2 users group name to be changed.
   -oldadmins oldadmngrp The old DB2 admins group name to be changed.
   -file inputfile       File listing additional files/directories for which
                         the permissions needs to be updated.
   -verbose              Output extra information
   -r[eset]              Reverse the previous changes (NOTE: THIS WILL ONLY
                         WORK IF NO OTHER CHANGES HAS BEEN MADE).
   -h[elp]|-?            Display this help.


 Examples:

 To enable extended security and use the domain groups mydom\db2users and
 mydom\db2admns to protect your DB2 objects:

 db2extsec /u mydom\db2users /a mydom\db2admns

 To reset extended security to it's previous setting. Please see note above

 db2extsec /reset

 To enable extended security as above but also change the security group for
 the files/directories listed in c:\mylist.lst from local group db2admns and
 db2users to domain groups mydom\db2admns and mydom\db2users.

 db2extsec /users mydom\db2users /admins mydom\db2admns /oldadmins db2admns
           /oldusers db2users /file c:\mylist.lst

 Note: The format of the input file is as follows

    * This is a comment
    D:\MYBACKUPDIR
    D:\MYEXPORTDIR
    D:\MYMISCFILE\myfile.dat

    * This is another comment
    E:\MYOTHERBACKUPDIR                 * This is more comments
    E:\MYOTHEREXPORTDIR
  DB2EXTSEC: Drive/Share %1S does not support extended attributes. DB2EXTSEC: processing failed, %1S=%2S. DB2EXTSEC: Unable to read the DBM configuration, rc=%1S. DB2EXTSEC: Unable to create "%1S" group and add rights. DB2EXTSEC: Old "%1S" group name specified by "%2S" not found. DB2EXTSEC: Unable to add "%1S" group ACE. DB2EXTSEC: Skip processing of directories under the InstallPath. DB2EXTSEC: The %1S and %2S parameters are required when %3S is specified. DB2EXTSEC: Cannot open input file "%1S" specified by "%2S". DB2EXTSEC: Invalid parameter %1S specified. DB2EXTSEC: Unexpected error %1S encountered. DB2EXTSEC: This is a DPF instance, it is recommended that you use domain groups. DB2EXTSEC: Warning, unable to switch to instance %1S (RC=%2S). DB2EXTSEC: Processing instance %1S... DB2EXTSEC: Processing install copy %1S... DB2EXTSEC: Processing input file %1S... DB2EXTSEC: Processing additional DB2 services... DB2EXTSEC: Processing stopped. Stop all DB2 instances and try again. The DB2EXTSEC command completed successfully. 