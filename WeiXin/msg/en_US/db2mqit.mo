  �  �      T   �  2   T     �   T       T     z  T   $  �  T   .  �  T   8  )  T   B  �  T   L  N  T   �  .  T   �  [  T   �  v  T   �  �  T     �  T     �  T     &  T      �  T   *  �  T   4    T   >  +  T   R  T  T   \  }  T   �  [  T   �  �  T   �  �  T   �  '	  T   �  �	  T   �  D
  T   �  �
  T     k  T     D  T     �  T   &     T   0  ,  T   :  N  T   D  o  T   N  �  T   X  �  T   b  *  T   l  e  T   v  �  T   �  �  T   �    T   �  �  T   �  �  T   �  �  T   �    T   �  [  T   �  �  T   �  8  T   �  {  T   �  �  T   �  �  T   �  �  T   �    T   �  D  T     m  T     �  T     ,  T      S  T   *  p  T   4  �  T   >  �  T   H  �  T   R     T   f    T   p  7  T   z  H  T   �  �  T   �  �  T   �  �  T   �    T   �     T   �  �  T   �  M  T   �  �  T   �  �  T   �  �  T   �  7  T   �  O  T   �  �  T   �  �  T   �    T   �  T  T   �  7  T   �  c  T   �    T   �  �  T   �    T   �    T   �  <  T   �  I  T   �  s  T     �  T     �  T     �  T   "    T   ,  A  T   6  �  T   @    T   J  u  T   T  �  T   ^  �  T   h  �  T   r  
   T   |  +   T   �  T   T   �  |   T   �  �   T   �  �   T   �  �   T   �   !  T   �  K!  T   �  u!  T   �  �!  T   �  �!  T   �  �!  T   �  �!  T     �"  T   �  �"  T   �  #  T   �  4#  T   �  O#  T   �  W#  T   �  q#  T   �  �#  T   �  �#  T   �  �#  T   �  '$  T   �  P$  T      �$  T   
  %  T     6%  T     T%  T   (  }%  T   2  �%  T   <  �%  T   P  �%  T   Z  $&  T   d  @&  T   n  Q&  T   x  '  T     *** Unexpected error while checking AIX oslevel 
 *** enable_MQFunctions Failed.
      Please note, if running this command on AIX, oslevel must be 4.3 or higher.
      The current oslevel is %s 
 *** Error -- while loading library amt.dll.
      Please ensure that AMI is installed 
 *** Error -- while loading library mqm.dll.
      Please ensure that MQSeries is installed 
 *** Error -- while loading library db2cli.dll.
      Please ensure that DB2 is installed    enable_MQFunctions Failed   *** Unexpected error while checking MQSeries Version 
 *** enable_MQFunctions Failed.
     Please ensure MQ 5.2 or later is installed.
     Current MQSeries version is: %s 
 *** enable_MQFunctions Failed.
     mqver was not found.
     Please ensure MQ 5.2 or later is installed.
     Please ensure the current user is privileged to run 'mqver'. 
============================================================
  Usage: enable_MQFunctions -n dbName -u uID -p password
                    [-novalidate] [-force]
 ============================================================   *** disable_MQFunction finished with error    Native Error Code = %ld   (%d) db2mq UDFs were found   *** Error -- while allocating an environment handle   *** Error -- while allocating a connection handle   *** Error -- while setting connectAttr 
 *** Error -- while connecting to %s,
      Make sure that user(%s) and password(%s) are valid and that the DB2
      instance has started.   *** Error -- while allocating a statement handle   MQSeries functions already exist ...      -- Drop MQ Functions ...   *** Error -- while dropping functions    *** disable_MQFunctions Failed .....   
============================================================
  Usage: disable_MQFunctions -n dbName -u uID -p password
                    [-v 0pc | 1pc | all]
============================================================ 
 *** Problem -- while calling command db2mqCheck.
      This command should only be used internally. 
 *** Problem -- while checking MQ.
      Please ensure that the user has permission to create/start queue manager.
      Please ensure that the user has permission to create a queue.
      Please ensure that MQSeries is installed properly. 
 *** Warning: 1pc is deprecated and is replaced with 2pc.
     Enter 'Y' to continue, or any other reply to abort:  
 *** Problem -- while checking the current working dirctory.
      Please ensure you are in the directory $DBINSTHOME/sqllib/cfg. 
 *** Problem -- Can not find repository file mq%camt.xml in current directory.
      Please ensure that you are in the directory $DBINSTHOME/sqllib/cfg. 
 *** Problem -- Can not find repository file mq/amthost.xml in current directory
      Please ensure that you are in the directory $DBINSTHOME/sqllib/cfg. 
 **** Warning -- an existing amt.xml was found in AMT_DATA_PATH.
       enable_MQFunctions will continue, but may fail during validation. 
 *** Warning -- amt.xml or amthost.xml were not found in AMT_DATA_PATH.
      Please ensure that you have permission to write to AMT_DATA_PATH.
      Enable_MQFunctions will continue, but may fail during validation. 
 *** Warning -- while creating amt.dtd file.
      enable_MQFunctions will continue, but may fail during validation       enable_MQFunctions will continue, but may fail during validation   *** enable_MQFunction finished with error   Can not open file (%s) to write   Can not open file (%s) to read 
 *** Error -- while checking AMI.
      amInitialize failed cc=%d, rc=%d   *** Error -- while connecting to %s (reason code -- %d)   *** Please wait: creating queue manager (%s) ......    *** Please wait: starting the queue manager (%s) ......  
 *** Error -- while connecting to %s (reason code -- %d)
      crtmqm/strmqm has been issued   *** Error -- while creating queue   Unexpected Error -- command server did not start 
 *** Error -- while creating local queue.
      Error returned by the MQSeries Command Server:
      Completion Code = %ld, Reason = %ld   Can not open file (%s) to write.   Can not open file (%s) to read.   *** Error -- while validating DB2MQ functions.      Please ensure db2mq.dll is in the directory sqllib\function      Please ensure db2mq is in the directory  
 *** Error -- while validating MQ functions using the default configuration.
      Please ensure that the following actions are complete:
      1. You created the configuration tables and objects in the database.
      2. The 'DB2.DEFAULT.SERVICE' entry in the MQService table has the following values:
             queueMgrName: %s
             queueName: %s
      3. The 'DB2.DEFAULT.POLICY' entry in the MQPolicy table has the following value: 
             connectionName: connectionDB2MQ 
      4. Ensure that the MQHost table has an entry for connectionDB2MQ and the queueMgrName is: %s.
      5. You enabled federation on DB Manager if '-v 1pc', '-v all', or no -v is specified. 
 UNEXPECTED FAILURE (SQLSTATE=%s) (SQLCODE=%ld)
  (errorMsg: %s)   Failed    (%d) MQ UDFs of the selected version or schema were found.   Validate successfully.   *** Error -- while allocating an environment handle   *** Error -- while allocating a connection handle   *** Error -- while setting connectAttr 
 *** Error -- while connecting to %s
      Make sure that user(%s) and password(%s) are valid and that the DB2
      instance has started.   *** Error -- while allocating a statement handle   MQSeries functions already exist ...   Reinstall MQ functions ...      -- Drop MQ Functions ...   *** Error -- while dropping functions       -- Create MQ Functions ...   *** Error -- while creating functions    Validate MQ Functions ...   Do You Want To Continue?   Please Input:  
============================================================
Usage: enable_MQFunctions -n dbName -u uID -p password
                    [-v 0pc|1pc|all]
                    [-xml [xmlSize]] [-c clobSize]
                    [-novalidate] [-echo] [-force]
============================================================   *** Please allow a few seconds to clean up the system ......  Internal error: %s with invalid version '%d'  Internal fatal error: %s  Internal error: %s 
  The MQHost table does not exist or the connectionName (%s) is missing from the table.
  Please create the configuration table or tables by using the script that is provided 
  before you issue the command again.    Internal warning: Inserting the value (%s) into table (%s) failed. Error Possible!    Internal warning: The database object (%s) already exists. Error Possible!    Problems creating the database object (%s).    Problems dropping the database object (%s).    SQL error or warning occurred.  Processing can continue... Native Error Code = %ld   Inspect the SQL error or warning conditions that occurred.  Internal error: DB2MQ_uninstall action_stmt is invalid (%d) 
  Only a value of 'all', '0pc', or '1pc' is allowed
  for the -v option.    Internal error: The function call sqlogmblk failed with (%d). 
  An error occurred during a configuration check,
  The MQPolicy table does not exist, or the table is empty.
  Create the configuration database objects using the script that is provided
  before you issue the command again.   *** enable_MQFunction finished with error    Native Error Code = %ld  
 Create Temporary table space failed!
 This error could be ignored if dxxMqGen() and dxxMqRetrieve() will not be used.   SQLExecDirect Failed(%d)  Failed   (%d) mqxml Functions were found  Failed (%d)   (%d) mqxml stored procudures were found   (%d) db2mq Functions were found   Validate successfully.   *** Error -- while allocating an environment handle   *** Error -- while allocating a connection handle   *** Error -- while setting connectAttr 
 *** Error -- while connecting to %s,
      Make sure that user(%s) and password(%s) are valid and that the DB2
      instance has started.   *** Error -- while allocating a statement handle 
 *** Error -- while checking DB2MQ UDF.
      Please ensure that ALL 67 DB2MQ UDFs (include CLOB type) are enabled   MQSeries XML functions already exist ...   Reinstall MQXML functions ...      -- Drop MQXML Functions ...   *** Error -- while dropping functions     -- Create MQXML Functions ...   *** Error -- while creating functions    Created MQXML Functions Successfully.   MQSeries XML procedures already exist ...   Reinstall MQXML procedures ...   -- Drop MQXML stored Procedures ...   *** Error -- while dropping stored procedures       -- Create MQXML stored Procedures ...   *** Error -- while creating procedures         Create MQXML procedures Successfully.   *** enable_MQFunctions Failed .....     Do You Want To Continue?    Please Input:  
 ============================================================
   Usage: enable_mqxml -n dbName -u uID -p password [-force]
  ============================================================   *** Please allow a few seconds to clean up the system ......   *** disable_mqxml finished with error    Native Error Code = %ld   SQLExecDirect Failed(%d)  Failed   SQLFetch failed .......   (%d) mqxml Functions were found   (%d) mqxml stored procudures were found   *** Error -- while allocating an environment handle   *** Error -- while allocating a connection handle   *** Error -- while setting connectAttr 
 *** Error -- while connecting to %s
      Make sure that user(%s) and password(%s) are valid and that the DB2
      instance has started.   *** Error -- while allocating a statement handle   MQSeries functions already exist ...      -- Drop MQ Functions ...   *** Error -- while dropping functions    MQSeries procedures already exist ...      -- Drop MQ stored Procedures ...   *** Error -- while dropping stored procedures    *** disable_MQFunctions Failed .....    Do You Want To Continue ?   Please Input:  
============================================================
  Usage: disable_mqxml -n dbName -u uID -p password
 ============================================================   *** Please allow a few seconds to clean up the system ...... 