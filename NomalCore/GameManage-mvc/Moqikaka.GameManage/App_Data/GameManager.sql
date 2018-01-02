/*
SQLyog Ultimate v12.09 (64 bit)
MySQL - 5.5.27 : Database - gamemanage
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`gamemanage` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `gamemanage`;

/*Table structure for table `role` */

DROP TABLE IF EXISTS `role`;

CREATE TABLE `role` (
  `ID` int(10) NOT NULL AUTO_INCREMENT COMMENT '角色Id',
  `RolesName` varchar(256) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL COMMENT '角色名称',
  `Remark` varchar(256) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL COMMENT '描述',
  `Page` varchar(2048) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL COMMENT '角色具有的权限',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

/*Data for the table `role` */

insert  into `role`(`ID`,`RolesName`,`Remark`,`Page`) values (1,'管理员','老大','1,1.2,2,2.1,2.2,2.3,2.4,2.5,2.6,2.7,2.9,2.10,2.11,3,3.1,3.2,3.3,4,4.1,4.3,4.4,5,5.1,5.2,5.5,6'),(2,'所有权限','hahah','1,1.2,1.3,1.4,1.5,1.6,1.7,2.11,2.16,2.17,2.18,2.19,2.20,2.21,2.22,2.23,2,2.1,2.2,2.3,2.4,2.5,2.6,2.7,2.9,2.10,2.13,2.14,2.24,2.25,2.26,2.27,2.28,3,3.1,3.2'),(3,'合作方运营','合作方运营的高档权限','2,2.1,2.2,2.3,2.4,2.5,2.6,2.7,2.8,2.9,2.10,2.11,2.12,2.13,2.14,2.15,2.16,2.17,2.18,2.19,2.20,2.21,2.22,2.23,2.24,2.25,2.26,2.27,2.28'),(9,'所有权限','sdsd','1,1.2,1.3,1.4,1.5,1.6,1.7');

/*Table structure for table `system_config` */

DROP TABLE IF EXISTS `system_config`;

CREATE TABLE `system_config` (
  `ConfigKey` varchar(32) CHARACTER SET utf8 NOT NULL COMMENT '配置的键',
  `ConfigValue` varchar(256) CHARACTER SET utf8 NOT NULL COMMENT '配置的值',
  `ConfigDesc` varchar(256) CHARACTER SET utf8 NOT NULL COMMENT '配置描述信息',
  PRIMARY KEY (`ConfigKey`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `system_config` */

insert  into `system_config`(`ConfigKey`,`ConfigValue`,`ConfigDesc`) values ('AdminType','Mix','当前后台所属 IOS/Mix'),('AppId','HZGG','AppKey'),('CallbackConfigApiUrl','CallbackConfig.ashx','ManageCenter获取回调配置Api页面名称'),('ConfirmDiamondInput','1000','需要提示确认的输入钻石数量'),('GameChartConnectionString','\"\"','聊天服务器数据库地址'),('GameServerApiUrl','GameManage.ashx','GameServer后台接口Api页面名称'),('GameServerConnectionString','DataSource=10.1.0.10;port=3312;UserId=root;Password=moqikaka3312;Database=2_model_develop;Allow Zero Datetime=true;charset=utf8;pooling=true;MinimumPoolSize=10;maximumpoolsize=10;command timeout=60;','游戏服务器Model库地址'),('GameVersionApiUrl','GameVersionList.ashx','ManageCenter版本Api页面名称'),('ManageCenterUrl','http://managecentertest.hzgg.moqikaka.com/API/','ManageCenter地址'),('MaxDiamondInput','20000','最大输入钻石数量'),('PartnerApiUrl','PartnerList.ashx','ManageCenter合作商Api页面名称'),('Platform','还珠格格-开发测试服后台','当前后台名称'),('RefreshInfoInterval','10','刷新间隔时间'),('ReloadDynamicDataApiUrl','ReloadDynamicData.ashx','GameServer刷新d表数据Api页面名称'),('ReloadManageCenterDataApiUrl','GetManageCenterInfo.ashx','GameServer刷新ManageCenter数据Api页面名称'),('SendMailList','wuyong@moqikaka.com;xiaolingjun@moqikaka.com;munanke@moqikaka.com;wangbiao@moqikaka.com;','邮件发送通知人'),('ServerGroupApiUrl','ServerGroupList.ashx','ManageCenter服务器组Api页面名称'),('ServerGroupIdRange','0,999999','显示服务器id的范围 例如:“400,600|800,900,1000” 范围400到600 和800、900、1000'),('ServerGroupMaintainApiUrl','Maintain.ashx','ManageCenter服务器组维护Api页面名称'),('ServerListApiUrl','ServerList.ashx','ManageCenteServerListApi页面名称');

/*Table structure for table `system_data_refresh_log` */

DROP TABLE IF EXISTS `system_data_refresh_log`;

CREATE TABLE `system_data_refresh_log` (
  `ID` int(10) NOT NULL AUTO_INCREMENT COMMENT '自增id',
  `UserName` varchar(32) NOT NULL COMMENT '操作用户',
  `ServerGroupIDs` varchar(1024) NOT NULL COMMENT '操作的服务器id',
  `OperationType` tinyint(3) NOT NULL COMMENT '操作类型(1,游戏服务器 2,聊天服务器 3,中心服务器)',
  `HaveError` bit(1) NOT NULL COMMENT '是否有失败',
  `Remark` text COMMENT '操作说明',
  `Crdate` datetime NOT NULL COMMENT '操作时间',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='服务器刷新记录';

/*Data for the table `system_data_refresh_log` */

/*Table structure for table `system_month_card_log` */

DROP TABLE IF EXISTS `system_month_card_log`;

CREATE TABLE `system_month_card_log` (
  `ID` int(10) NOT NULL AUTO_INCREMENT COMMENT '自增id',
  `UserName` varchar(32) NOT NULL COMMENT '操作用户',
  `ServerGroupID` int(11) NOT NULL COMMENT '操作的服务器id',
  `PlayerNames` varchar(1024) NOT NULL COMMENT '玩家名称',
  `MonthCardType` tinyint(3) NOT NULL COMMENT '月卡类型(1,20月卡 2,50月卡)',
  `Crdate` datetime NOT NULL COMMENT '操作时间',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='发放月卡记录';

/*Data for the table `system_month_card_log` */

/*Table structure for table `system_operation_log` */

DROP TABLE IF EXISTS `system_operation_log`;

CREATE TABLE `system_operation_log` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '日志唯一标识',
  `OperationName` varchar(100) NOT NULL COMMENT '操作说明',
  `OperationMothod` varchar(50) NOT NULL COMMENT '操作方法',
  `OperationData` text NOT NULL COMMENT '操作数据内容',
  `Crdate` datetime NOT NULL COMMENT '操作时间',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='系统操作日志';

/*Data for the table `system_operation_log` */

/*Table structure for table `system_push_to_chat_log` */

DROP TABLE IF EXISTS `system_push_to_chat_log`;

CREATE TABLE `system_push_to_chat_log` (
  `ID` int(10) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `PushServers` varchar(512) DEFAULT NULL COMMENT '推送到的服务器',
  `PushMsg` varchar(512) DEFAULT NULL COMMENT '推送的消息',
  `PushNum` tinyint(10) DEFAULT NULL COMMENT '推送次数',
  `OpUser` varchar(25) DEFAULT NULL COMMENT '推送者',
  `StartDate` datetime DEFAULT NULL COMMENT '推送起始时间',
  `EndDate` datetime DEFAULT NULL COMMENT '推送结束时间',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8 COMMENT='后台推送信息到聊天服务器log';

/*Data for the table `system_push_to_chat_log` */

insert  into `system_push_to_chat_log`(`ID`,`PushServers`,`PushMsg`,`PushNum`,`OpUser`,`StartDate`,`EndDate`) values (19,'20018','test啊哈哈哈\r\n',1,'admin','2017-09-27 16:03:17','2017-09-27 17:03:17'),(20,'20018','test啊哈哈哈\r\n',1,'admin','2017-09-27 16:03:17','2017-09-27 17:03:17'),(21,'20018','testy哈哈哈\r\n',1,'admin','2017-09-27 16:05:32','2017-09-27 17:05:32');

/*Table structure for table `user` */

DROP TABLE IF EXISTS `user`;

CREATE TABLE `user` (
  `UserID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '用户唯一标识',
  `UserName` varchar(64) NOT NULL COMMENT '用户名称',
  `UserPwd` varchar(64) NOT NULL COMMENT '用户密码',
  `UserRole` int(10) NOT NULL COMMENT '用户角色',
  `IfSuper` tinyint(1) NOT NULL COMMENT '是否超级用户',
  `Status` int(10) unsigned NOT NULL COMMENT '状态(0-正常，-1-锁定)',
  `LastLoginIP` varchar(128) DEFAULT NULL COMMENT '上次登陆IP',
  `LastLoginTime` datetime NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '上次登陆时间',
  `Crdate` datetime DEFAULT NULL COMMENT '添加时间',
  PRIMARY KEY (`UserID`),
  UNIQUE KEY `UserName` (`UserName`)
) ENGINE=InnoDB AUTO_INCREMENT=279 DEFAULT CHARSET=utf8;

/*Data for the table `user` */

insert  into `user`(`UserID`,`UserName`,`UserPwd`,`UserRole`,`IfSuper`,`Status`,`LastLoginIP`,`LastLoginTime`,`Crdate`) values (1,'admin','E10ADC3949BA59ABBE56E057F20F883E',1,1,0,'::1','2018-01-02 10:53:15','2015-01-05 18:27:37'),(232,'x1x','1',1,1,1,NULL,'0001-01-01 00:00:00','2017-12-29 11:10:02');

/*Table structure for table `user_operation_log` */

DROP TABLE IF EXISTS `user_operation_log`;

CREATE TABLE `user_operation_log` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '日志唯一标识',
  `UserID` int(10) unsigned NOT NULL COMMENT '操作用户唯一标识',
  `UserName` varchar(32) NOT NULL COMMENT '操作用户名称',
  `OperationName` varchar(100) NOT NULL COMMENT '操作说明',
  `OperationMothod` varchar(50) NOT NULL COMMENT '操作方法',
  `OperationData` text NOT NULL COMMENT '操作数据内容',
  `Crdate` datetime NOT NULL COMMENT '操作时间',
  PRIMARY KEY (`ID`),
  KEY `IX_UserID` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=370 DEFAULT CHARSET=utf8 COMMENT='操作日志';

/*Data for the table `user_operation_log` */

insert  into `user_operation_log`(`ID`,`UserID`,`UserName`,`OperationName`,`OperationMothod`,`OperationData`,`Crdate`) values (363,1,'admin','系统登录','Home.Login','','2018-01-02 10:48:50'),(364,0,'未知用户','登出','Home.LoginOut','{}','2018-01-02 10:52:06'),(365,1,'admin','系统登录','Home.Login','','2018-01-02 10:52:17'),(366,0,'未知用户','登出','Home.LoginOut','{}','2018-01-02 10:52:22'),(367,1,'admin','系统登录','Home.Login','','2018-01-02 10:52:30'),(368,0,'未知用户','登出','Home.LoginOut','{}','2018-01-02 10:52:40'),(369,1,'admin','系统登录','Home.Login','','2018-01-02 10:53:15');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
