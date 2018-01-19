/*
SQLyog Ultimate v12.09 (64 bit)
MySQL - 5.5.27 : Database - Manage
*********************************************************************
*/


/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`Manage` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `Manage`;

/*Table structure for table `role` */

DROP TABLE IF EXISTS `role`;

CREATE TABLE `role` (
  `ID` int(10) NOT NULL AUTO_INCREMENT COMMENT '角色Id',
  `RolesName` varchar(256) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL COMMENT '角色名称',
  `Remark` varchar(256) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL COMMENT '描述',
  `Page` varchar(2048) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL COMMENT '角色具有的权限',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8;

/*Data for the table `role` */

insert  into `role`(`ID`,`RolesName`,`Remark`,`Page`) values (1,'管理员','老大','1,1.2,2,2.1,2.2,2.3,2.4,2.5,2.6,2.7,2.9,2.10,2.11,3,3.1,3.2,3.3,4,4.1,4.3,4.4,5,5.1,5.2,5.5,6'),(3,'合作方运营','合作方运营的高档权限','2,2.1,2.2,2.3,2.4,2.5,2.6,2.7,2.8,2.9,2.10,2.11,2.12,2.13,2.14,2.15,2.16,2.17,2.18,2.19,2.20,2.21,2.22,2.23,2.24,2.25,2.26,2.27,2.28');

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
) ENGINE=InnoDB AUTO_INCREMENT=140 DEFAULT CHARSET=utf8 COMMENT='服务器刷新记录';

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
) ENGINE=InnoDB AUTO_INCREMENT=283 DEFAULT CHARSET=utf8;

/*Data for the table `user` */

insert  into `user`(`UserID`,`UserName`,`UserPwd`,`UserRole`,`IfSuper`,`Status`,`LastLoginIP`,`LastLoginTime`,`Crdate`) values (1,'admin','E10ADC3949BA59ABBE56E057F20F883E',1,1,0,'::1','2018-01-12 16:35:15','2015-01-05 18:27:37');

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
) ENGINE=InnoDB AUTO_INCREMENT=1019 DEFAULT CHARSET=utf8 COMMENT='操作日志';

/*Data for the table `user_operation_log` */

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
