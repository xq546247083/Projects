/*
SQLyog Ultimate v12.09 (64 bit)
MySQL - 5.7.12-log : Database - gameserver_player
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
/*Table structure for table `m_serverconfig` */

DROP TABLE IF EXISTS `m_serverconfig`;

CREATE TABLE `m_serverconfig` (
  `Id` char(36) NOT NULL COMMENT '主键',
  `ServerId` char(10) NOT NULL COMMENT '服务器id',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `m_serverconfig` */

insert  into `m_serverconfig`(`Id`,`ServerId`) values ('8866874d-6514-11e7-986e-408d5c748641','3213123123');
insert  into `m_serverconfig`(`Id`,`ServerId`) values ('886c43dd-6514-11e7-986e-408d5c748641','131');

/*Table structure for table `p_player` */

DROP TABLE IF EXISTS `p_player`;

CREATE TABLE `p_player` (
  `Id` char(36) NOT NULL COMMENT '用户id',
  `UserId` char(40) NOT NULL COMMENT '用户登录id',
  `UserName` char(40) NOT NULL COMMENT '用户名',
  `UserPwd` char(40) NOT NULL COMMENT '用户密码',
  `Gend` bit(1) NOT NULL COMMENT '性别',
  `IsOnline` bit(1) NOT NULL COMMENT '是否在线',
  `OnlieTime` datetime NOT NULL COMMENT '上线时间',
  `RegisterTime` datetime NOT NULL COMMENT '注册时间',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `p_player` */

/*Table structure for table `test` */

DROP TABLE IF EXISTS `test`;

CREATE TABLE `test` (
  `Id1` char(10) NOT NULL,
  `Id2` char(10) NOT NULL,
  `Name` char(10) NOT NULL,
  `Info` char(10) NOT NULL,
  PRIMARY KEY (`Id1`,`Id2`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `test` */

insert  into `test`(`Id1`,`Id2`,`Name`,`Info`) values ('2321','dasd','dasdtest','dasda');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
