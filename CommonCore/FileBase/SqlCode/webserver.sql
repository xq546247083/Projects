/*
SQLyog Ultimate v12.09 (64 bit)
MySQL - 5.7.12-log : Database - webserver
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`webserver` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `webserver`;

/*Table structure for table `sys_menu` */

DROP TABLE IF EXISTS `sys_menu`;

CREATE TABLE `sys_menu` (
  `MenuID` int(10) NOT NULL COMMENT '菜单标识',
  `ParentMenuID` int(10) NOT NULL COMMENT '上级ID',
  `MenuName` varchar(36) NOT NULL COMMENT '菜单名称',
  `MenuUrl` varchar(256) DEFAULT NULL COMMENT '菜单地址',
  `SortOrder` int(10) NOT NULL COMMENT '排序号',
  `MenuIcon` varchar(36) DEFAULT NULL COMMENT '菜单图标路径（未用到）',
  `BigMenuIcon` varchar(36) DEFAULT NULL COMMENT '常用菜单图标（未用到）',
  `ShortCut` varchar(36) DEFAULT NULL COMMENT '快捷键（未用到）',
  `IsShow` bit(1) NOT NULL DEFAULT b'1' COMMENT '是否显示',
  PRIMARY KEY (`MenuID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `sys_menu` */

insert  into `sys_menu`(`MenuID`,`ParentMenuID`,`MenuName`,`MenuUrl`,`SortOrder`,`MenuIcon`,`BigMenuIcon`,`ShortCut`,`IsShow`) values (1100,0,'菜单管理','',1,'',NULL,NULL,''),(1200,0,'博客','',3,'chrome',NULL,NULL,''),(1201,1200,'博客主页','/Main/WebPage/Blog/BlogSelfMain.html',1,NULL,NULL,NULL,''),(1202,1200,'浏览','/Main/WebPage/Blog/BlogMain.html',2,NULL,NULL,NULL,'');

/*Table structure for table `sys_role` */

DROP TABLE IF EXISTS `sys_role`;

CREATE TABLE `sys_role` (
  `RoleID` int(10) NOT NULL COMMENT '主键',
  `RoleName` varchar(36) NOT NULL COMMENT '角色名称',
  `MenuIDS` varchar(1024) NOT NULL COMMENT '菜单id（用,隔开）',
  `IsDefault` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否默认角色',
  `IsSupper` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否是超级管理员角色',
  `Notes` varchar(256) NOT NULL COMMENT '描述',
  PRIMARY KEY (`RoleID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `sys_role` */

insert  into `sys_role`(`RoleID`,`RoleName`,`MenuIDS`,`IsDefault`,`IsSupper`,`Notes`) values (1,'超级管理员','1100,1200,1201,1202','\0','','超级管理员');

/*Table structure for table `sys_user` */

DROP TABLE IF EXISTS `sys_user`;

CREATE TABLE `sys_user` (
  `UserID` char(36) NOT NULL COMMENT '主键',
  `UserName` varchar(128) NOT NULL COMMENT '登录ID',
  `FullName` varchar(128) NOT NULL COMMENT '用户真实姓名',
  `Password` varchar(512) NOT NULL COMMENT '登陆密码',
  `PwdExpiredTime` datetime DEFAULT NULL COMMENT '密码过期时间',
  `Sex` bit(1) DEFAULT b'1' COMMENT '性别 1男0女',
  `Phone` varchar(36) DEFAULT NULL COMMENT '工作电话',
  `Email` varchar(36) DEFAULT NULL COMMENT '电子邮箱',
  `Status` int(10) DEFAULT NULL COMMENT '状态 1 启用 2禁用 3已删',
  `LoginCount` int(20) DEFAULT '0' COMMENT '登录次数',
  `LastLoginTime` datetime DEFAULT NULL COMMENT '最后登录时间',
  `LastLoginIP` varchar(36) DEFAULT NULL COMMENT '公司ID',
  `RoleIDs` varchar(512) DEFAULT NULL COMMENT '角色ID（可以多个）',
  `CreateTime` datetime DEFAULT NULL COMMENT '创建日期',
  PRIMARY KEY (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `sys_user` */

insert  into `sys_user`(`UserID`,`UserName`,`FullName`,`Password`,`PwdExpiredTime`,`Sex`,`Phone`,`Email`,`Status`,`LoginCount`,`LastLoginTime`,`LastLoginIP`,`RoleIDs`,`CreateTime`) values ('26d3ee41-332d-4bef-8ef1-5ec154d15acb','aaa','aaa','nN+VhKyLxdn/rKO3V5qbSrvznFlGC1FWvz9prZRKlY7hIvgPkKmvYTwUg4k5Qu1MyiCPJ3ASQBahvTM+ACMLQg##=2','0001-01-01 00:00:00','\0','','15828353445@163.com',1,0,'0001-01-01 00:00:00',NULL,'1','2017-08-16 17:53:26'),('32059fdf-639b-4b81-a362-7254ac99ad61','aaaa','aaaa','W33J3sTLU/ieWQgXnhe1B60DAXC0zCdwxrozCVWodQuj/V9Zp7MU2EXCi+aiXUA7yiCPJ3ASQBahvTM+ACMLQg##=2','0001-01-01 00:00:00','\0','','2545625776@qq.com',1,0,'0001-01-01 00:00:00',NULL,'1','2017-08-16 17:47:54'),('555ec8ca-a5e6-4163-b862-d889fbbbccfb','xiaoqiang','xiaoqiang','PJIlyB4C718+mExM8RFsrs0yNo61tWIgMfyyJIC89C1dmOrtpn9ZBeapruQ80CsfyiCPJ3ASQBahvTM+ACMLQg##=2','2017-09-08 19:00:29','\0','','546247083@qq.com',1,185,'2017-09-06 18:59:12',NULL,'1','2017-08-15 13:10:53');

/*Table structure for table `sys_userinfo` */

DROP TABLE IF EXISTS `sys_userinfo`;

CREATE TABLE `sys_userinfo` (
  `UserInfoID` char(36) NOT NULL COMMENT '用户ID',
  `CompanyID` varchar(50) DEFAULT NULL COMMENT '公司ID（未用到）',
  `Memo` varchar(150) DEFAULT NULL COMMENT '用户备注',
  `AreaID` char(36) DEFAULT NULL COMMENT '区域ID（未用到）',
  PRIMARY KEY (`UserInfoID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `sys_userinfo` */

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
