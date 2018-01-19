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
  `ID` INT(10) NOT NULL AUTO_INCREMENT COMMENT '角色Id',
  `RolesName` VARCHAR(256) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL COMMENT '角色名称',
  `Remark` VARCHAR(256) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL COMMENT '描述',
  `Page` VARCHAR(2048) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL COMMENT '角色具有的权限',
  PRIMARY KEY (`ID`)
) ENGINE=INNODB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8;

/*Data for the table `role` */

INSERT  INTO `role`(`ID`,`RolesName`,`Remark`,`Page`) VALUES (1,'管理员','老大','1,1.2,2,2.1,2.2,2.3,2.4,2.5,2.6,2.7,2.9,2.10,2.11,3,3.1,3.2,3.3,4,4.1,4.3,4.4,5,5.1,5.2,5.5,6'),(3,'合作方运营','合作方运营的高档权限','2,2.1,2.2,2.3,2.4,2.5,2.6,2.7,2.8,2.9,2.10,2.11,2.12,2.13,2.14,2.15,2.16,2.17,2.18,2.19,2.20,2.21,2.22,2.23,2.24,2.25,2.26,2.27,2.28');

/*Table structure for table `user` */

DROP TABLE IF EXISTS `user`;

CREATE TABLE `user` (
  `UserID` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '用户唯一标识',
  `UserName` VARCHAR(64) NOT NULL COMMENT '用户名称',
  `UserPwd` VARCHAR(64) NOT NULL COMMENT '用户密码',
  `UserRole` INT(10) NOT NULL COMMENT '用户角色',
  `IfSuper` TINYINT(1) NOT NULL COMMENT '是否超级用户',
  `Status` INT(10) UNSIGNED NOT NULL COMMENT '状态(0-正常，-1-锁定)',
  `LastLoginIP` VARCHAR(128) DEFAULT NULL COMMENT '上次登陆IP',
  `LastLoginTime` DATETIME NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '上次登陆时间',
  `Crdate` DATETIME DEFAULT NULL COMMENT '添加时间',
  PRIMARY KEY (`UserID`),
  UNIQUE KEY `UserName` (`UserName`)
) ENGINE=INNODB AUTO_INCREMENT=283 DEFAULT CHARSET=utf8;

/*Data for the table `user` */

INSERT  INTO `user`(`UserID`,`UserName`,`UserPwd`,`UserRole`,`IfSuper`,`Status`,`LastLoginIP`,`LastLoginTime`,`Crdate`) VALUES (1,'admin','E10ADC3949BA59ABBE56E057F20F883E',1,1,0,'::1','2018-01-12 16:35:15','2015-01-05 18:27:37');

/*Table structure for table `user_operation_log` */

DROP TABLE IF EXISTS `user_operation_log`;

CREATE TABLE `user_operation_log` (
  `ID` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '日志唯一标识',
  `UserID` INT(10) UNSIGNED NOT NULL COMMENT '操作用户唯一标识',
  `UserName` VARCHAR(32) NOT NULL COMMENT '操作用户名称',
  `OperationName` VARCHAR(100) NOT NULL COMMENT '操作说明',
  `OperationMothod` VARCHAR(50) NOT NULL COMMENT '操作方法',
  `OperationData` TEXT NOT NULL COMMENT '操作数据内容',
  `Crdate` datetime NOT NULL COMMENT '操作时间',
  PRIMARY KEY (`ID`),
  KEY `IX_UserID` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=1019 DEFAULT CHARSET=utf8 COMMENT='操作日志';

/*Data for the table `user_operation_log` */

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
