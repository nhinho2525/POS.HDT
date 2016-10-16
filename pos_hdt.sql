/*
SQLyog Ultimate v11.11 (64 bit)
MySQL - 5.6.16 : Database - pos_hdt
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`pos_hdt` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_unicode_ci */;

USE `pos_hdt`;

/*Table structure for table `ingredient` */

DROP TABLE IF EXISTS `ingredient`;

CREATE TABLE `ingredient` (
  `IngredientId` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `IngredientName` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IngredientMeasure` int(11) DEFAULT NULL,
  `IngredientExchange` decimal(20,0) DEFAULT NULL,
  `IngredientUnit` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`IngredientId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `ingredient` */

/*Table structure for table `input` */

DROP TABLE IF EXISTS `input`;

CREATE TABLE `input` (
  `InputId` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `Sophieu` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SophieuDate` date DEFAULT NULL,
  `RecieptId` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `RecieptDate` datetime DEFAULT NULL,
  `SupplierId` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `StoreId` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Paid` tinyint(1) DEFAULT NULL,
  `InputDiscount` decimal(5,2) DEFAULT NULL,
  `InputNote` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `InputVAT` int(11) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`InputId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `input` */

/*Table structure for table `inputdetail` */

DROP TABLE IF EXISTS `inputdetail`;

CREATE TABLE `inputdetail` (
  `InputId` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `IngredientId` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `IngredientExpDate` date DEFAULT NULL,
  `IngredientQty` decimal(20,0) DEFAULT NULL,
  `IngredientPrice` decimal(20,0) DEFAULT NULL,
  `IngredientVat` decimal(20,0) DEFAULT NULL,
  `IngredientDiscount` decimal(20,0) DEFAULT NULL,
  PRIMARY KEY (`InputId`,`IngredientId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `inputdetail` */

/*Table structure for table `measure` */

DROP TABLE IF EXISTS `measure`;

CREATE TABLE `measure` (
  `MeasureId` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `MeasureNote` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`MeasureId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `measure` */

/*Table structure for table `measuredetail` */

DROP TABLE IF EXISTS `measuredetail`;

CREATE TABLE `measuredetail` (
  `MeasureDetailId` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `IngredientId` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MeasureQty` decimal(20,0) DEFAULT NULL,
  PRIMARY KEY (`MeasureDetailId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `measuredetail` */

/*Table structure for table `order` */

DROP TABLE IF EXISTS `order`;

CREATE TABLE `order` (
  `OrderId` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `TableId` int(11) DEFAULT NULL,
  `OrderNote` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OrderStatus` tinyint(1) DEFAULT NULL,
  `OrderTax` int(11) DEFAULT NULL,
  `OrderMoney` decimal(20,0) DEFAULT NULL,
  `OrderDiscount` decimal(20,0) DEFAULT NULL,
  `OrderPercentDiscount` int(11) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`OrderId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `order` */

/*Table structure for table `orderdetail` */

DROP TABLE IF EXISTS `orderdetail`;

CREATE TABLE `orderdetail` (
  `OrderId` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `ProductId` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `OrderDetailQty` int(11) DEFAULT NULL,
  `OrderDetailPrice` decimal(20,0) DEFAULT NULL,
  `OrderDetailIsPrint` tinyint(1) DEFAULT NULL,
  `OrderDetailStatus` tinyint(1) DEFAULT NULL,
  `OrderDetailMoney` decimal(20,0) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`OrderId`,`ProductId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `orderdetail` */

/*Table structure for table `output` */

DROP TABLE IF EXISTS `output`;

CREATE TABLE `output` (
  `OutputId` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `Sophieu` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SophieuDate` date DEFAULT NULL,
  `OutputReason` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `StoreId` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OutputType` tinyint(1) DEFAULT NULL,
  `RecieptId` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OutputNote` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CreatedBy` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`OutputId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `output` */

/*Table structure for table `outputdetail` */

DROP TABLE IF EXISTS `outputdetail`;

CREATE TABLE `outputdetail` (
  `OutputId` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `IngredientId` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `IngredientQty` decimal(20,0) DEFAULT NULL,
  `IngredientPrice` decimal(20,0) DEFAULT NULL,
  `IngredientVat` decimal(20,0) DEFAULT NULL,
  PRIMARY KEY (`OutputId`,`IngredientId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `outputdetail` */

/*Table structure for table `product` */

DROP TABLE IF EXISTS `product`;

CREATE TABLE `product` (
  `ProductId` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `ProductName` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProductGroupId` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`ProductId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `product` */

/*Table structure for table `productgroup` */

DROP TABLE IF EXISTS `productgroup`;

CREATE TABLE `productgroup` (
  `ProductGroupId` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `ProductGroupName` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`ProductGroupId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `productgroup` */

/*Table structure for table `receipt` */

DROP TABLE IF EXISTS `receipt`;

CREATE TABLE `receipt` (
  `ReceiptId` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `TableId` int(11) DEFAULT NULL,
  `ReceiptNote` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ReceiptStatus` tinyint(1) DEFAULT NULL,
  `ReceiptTax` int(11) DEFAULT NULL,
  `ReceiptMoney` decimal(20,0) DEFAULT NULL,
  `ReceiptDiscount` decimal(20,0) DEFAULT NULL,
  `ReceiptPercentDiscount` int(11) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`ReceiptId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `receipt` */

/*Table structure for table `receiptdetail` */

DROP TABLE IF EXISTS `receiptdetail`;

CREATE TABLE `receiptdetail` (
  `ReceiptId` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `ProductId` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `ReceiptDetailQty` int(11) DEFAULT NULL,
  `ReceiptDetailPrice` decimal(20,0) DEFAULT NULL,
  `ReceiptDetailIsPrint` tinyint(1) DEFAULT NULL,
  `ReceiptDetailStatus` tinyint(1) DEFAULT NULL,
  `ReceiptDetailMoney` decimal(20,0) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`ReceiptId`,`ProductId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `receiptdetail` */

/*Table structure for table `role` */

DROP TABLE IF EXISTS `role`;

CREATE TABLE `role` (
  `RoleId` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `RoleName` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`RoleId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `role` */

/*Table structure for table `supplier` */

DROP TABLE IF EXISTS `supplier`;

CREATE TABLE `supplier` (
  `SupplierId` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `SupplierName` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SuppplierAddress` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SupplierPhone` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SupplierEmail` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SupplierWebsite` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`SupplierId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `supplier` */

/*Table structure for table `table` */

DROP TABLE IF EXISTS `table`;

CREATE TABLE `table` (
  `TableID` int(11) NOT NULL AUTO_INCREMENT,
  `TableName` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`TableID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `table` */

/*Table structure for table `unit` */

DROP TABLE IF EXISTS `unit`;

CREATE TABLE `unit` (
  `UnitId` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `UnitName` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UnitQtyExchange` decimal(20,0) DEFAULT NULL,
  `UnitExchange` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`UnitId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `unit` */

/*Table structure for table `user` */

DROP TABLE IF EXISTS `user`;

CREATE TABLE `user` (
  `UserId` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `Pwd` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LastLogin` varchar(16) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CreatedBy` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `ModifiedBy` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  `RoleId` varchar(128) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `user` */

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
