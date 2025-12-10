CREATE DATABASE  IF NOT EXISTS `misa.qtsx.workshift` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `misa.qtsx.workshift`;
-- MySQL dump 10.13  Distrib 8.0.44, for Win64 (x86_64)
--
-- Host: localhost    Database: misa.qtsx.workshift
-- ------------------------------------------------------
-- Server version	8.0.44

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `shift`
--

DROP TABLE IF EXISTS `shift`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shift` (
  `shift_id` char(36) NOT NULL COMMENT 'id ca làm việc',
  `shift_code` varchar(100) NOT NULL COMMENT 'Mã ca làm việc',
  `shift_name` varchar(100) NOT NULL COMMENT 'Tên ca làm việc',
  `begin_shift_time` time NOT NULL COMMENT 'Giời vào ca',
  `end_shift_time` time NOT NULL COMMENT 'Giờ tan ca',
  `begin_break_time` time DEFAULT NULL COMMENT 'Giờ bắt đầu nghỉ',
  `end_break_time` time DEFAULT NULL COMMENT 'Gio ket thuc nghỉ',
  `working_time` double NOT NULL COMMENT 'Số giờ làm việc',
  `breaking_time` double DEFAULT NULL COMMENT 'Số giờ nghỉ',
  `inactive` tinyint(1) NOT NULL COMMENT 'false : ngưng sử dụng , true : sử dụng',
  `created_by` varchar(100) NOT NULL COMMENT 'người tạo',
  `created_date` datetime NOT NULL COMMENT 'Thời gian tạo',
  `modified_by` varchar(100) NOT NULL COMMENT 'Người sửa',
  `modified_date` datetime NOT NULL COMMENT 'Thời gian sửa',
  PRIMARY KEY (`shift_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `shift`
--

LOCK TABLES `shift` WRITE;
/*!40000 ALTER TABLE `shift` DISABLE KEYS */;
INSERT INTO `shift` VALUES ('003be2a1-82b0-411c-92a2-4fd6409b6422','Ancs 1','Hoàng Anh 11a3','08:00:00','17:01:00','09:30:00','14:30:00',4,5,1,'admin','2025-12-04 10:08:46','admin','2025-12-10 02:47:23'),('12aabe79-33b3-4bca-9f40-f4176e294360','21313','32424','00:00:00','07:00:00','02:00:00','03:00:00',6,1,1,'Phí Hoàng Anh','2025-12-09 08:17:48','Phí Hoàng Anh','2025-12-10 03:20:49'),('1b77293c-d513-45d5-9308-ce57804ed439','Ancs4','Hoàng Em123','00:00:00','01:00:00','00:00:00','00:00:00',0,0,1,'Phí Hoàng Anh','2025-12-09 03:02:32','Phí Hoàng Anh','2025-12-09 03:04:56'),('1df144fa-ed25-4d08-a862-8a0d7b557834','T1 vô địch','Hoàng Anh 11a3','12:00:00','17:00:00','00:00:00','00:00:00',5,0,1,'Phí Hoàng Anh','2025-12-09 14:54:49','Phí Hoàng Anh','2025-12-09 14:55:31'),('223292d3-c317-4e40-b4dc-cdc937abbea9','Ancs2','Hoàng Anh','08:00:00','17:00:00','12:00:00','13:00:00',8,1,1,'admin','2025-12-04 10:08:46','admin','2025-12-04 10:08:46'),('2257978c-d832-435b-a8f4-8d5787b706a4','3213213','312312312','00:00:00','04:30:00','02:00:00','02:30:00',4,0,1,'Phí Hoàng Anh','2025-12-09 02:34:55','Phí Hoàng Anh','2025-12-09 13:39:44'),('2d3b47db-50cf-4fc7-b4be-cd25658799c9','123123123','312312','00:00:00','00:30:00','00:00:00','00:00:00',1,0,1,'Phí Hoàng Anh','2025-12-09 14:15:55','Phí Hoàng Anh','2025-12-09 14:16:06'),('4d3d623a-31d3-48b8-bed7-bbb6290b36c8','112','Hoàng Anh 11a3','08:00:00','17:01:00','09:30:00','13:00:00',5.5,0,1,'Phí Hoàng Anh','2025-12-09 14:54:49','Phí Hoàng Anh','2025-12-10 02:05:38'),('54d3f44b-4769-420f-b761-5ef805298ae7','11a2','Hoàng Anh 11a3','08:00:00','17:01:00','09:30:00','13:00:00',5.5,0,1,'Phí Hoàng Anh','2025-12-09 14:52:40','Phí Hoàng Anh','2025-12-09 14:52:42'),('5942f71d-2d1f-40c9-b383-cca472e0e261','443324','Hoàng Anh 11a3','08:00:00','17:01:00','09:30:00','14:30:00',4,5,1,'Phí Hoàng Anh','2025-12-10 04:03:57','Phí Hoàng Anh','2025-12-10 04:04:00'),('5bb8c131-1491-4988-8961-e5979416f579','Css01','Ca tối','17:00:00','23:00:00','00:00:00','00:00:00',0,0,1,'Phí Hoàng Anh','2025-12-09 12:25:40','Phí Hoàng Anh','2025-12-09 12:29:29'),('640faab2-0751-4cab-9d5e-498b8eabfeba','adsadsad','đasa','00:00:00','00:30:00','00:00:00','00:00:00',0,0,1,'Phí Hoàng Anh','2025-12-09 02:43:33','Phí Hoàng Anh','2025-12-09 02:44:02'),('674b2816-7394-49c2-bc23-96c37c9c53f1','Caccc1','Ca trưa','00:12:00','03:04:00','00:00:00','00:00:00',0,0,0,'Phí Hoàng Anh','2025-12-09 12:22:25','Phí Hoàng Anh','2025-12-09 12:23:43'),('6d04ed5c-282f-4f2d-be93-effbf9e4f5cf','Geng vô địch','Hoàng Anh 11a3','12:00:00','17:00:00','13:00:00','13:30:00',4.5,0,1,'Phí Hoàng Anh','2025-12-09 14:56:05','Phí Hoàng Anh','2025-12-09 14:56:31'),('780924d1-c152-42f4-9e45-2ed3e8329a15','231321','312312','01:00:00','02:00:00','00:00:00','00:00:00',0,0,0,'Phí Hoàng Anh','2025-12-09 03:25:33','Phí Hoàng Anh','2025-12-09 03:26:33'),('7b96d950-e06a-4ca1-9fff-961c0134f3a7','11a3','Hoàng Anh 11a3','08:00:00','17:01:00','09:30:00','13:00:00',5.5,0,1,'Phí Hoàng Anh','2025-12-09 14:52:31','Phí Hoàng Anh','2025-12-09 14:52:38'),('7e1b4ca3-7bb4-40ac-b2a3-5e0f00434514','3213123','123123','00:00:00','01:00:00','00:00:00','00:01:00',0,0,0,'Phí Hoàng Anh','2025-12-09 03:25:33','Phí Hoàng Anh','2025-12-09 03:25:54'),('87033a00-111c-470b-bfc7-97ab3968df59','123213','Ca sáng','07:00:00','12:30:00','00:00:00','00:00:00',0,0,1,'Phí Hoàng Anh','2025-12-09 02:58:55','Phí Hoàng Anh','2025-12-09 03:01:58'),('8c4da541-d0be-11f0-9955-581122801490','HC_01','Hành Chính Sáng','08:00:00','17:00:00','12:00:00','13:00:00',8,1,1,'admin','2025-12-04 10:08:46','admin','2025-12-04 10:08:46'),('8c4db53b-d0be-11f0-9955-581122801490','DEM_A','Ca Đêm A (Công nhân)','22:00:00','06:00:00','02:00:00','02:30:00',7.5,0.5,1,'manager','2025-12-04 10:08:46','manager','2025-12-04 10:08:46'),('8c4db5b4-d0be-11f0-9955-581122801490','PART_S','Part-time Sáng','08:00:00','12:00:00',NULL,NULL,4,0,1,'hr','2025-12-04 10:08:46','hr','2025-12-04 10:08:46'),('8c4db63b-d0be-11f0-9955-581122801490','PART_C','Part-time Chiều','13:00:00','17:00:00',NULL,NULL,4,0,0,'hr','2025-12-04 10:08:46','hr','2025-12-04 10:08:46'),('8c4db6b3-d0be-11f0-9955-581122801490','PART_T','Part-time Tối','18:00:00','22:00:00',NULL,NULL,4,0,1,'hr','2025-12-04 10:08:46','hr','2025-12-04 10:08:46'),('8c4dbc13-d0be-11f0-9955-581122801490','BV_01','Bảo Vệ Ca 1','06:00:00','18:00:00','11:00:00','12:00:00',11,1,1,'security','2025-12-04 10:08:46','security','2025-12-04 10:08:46'),('8c4dbcac-d0be-11f0-9955-581122801490','BV_02','Bảo Vệ Ca 2','18:00:00','06:00:00','23:00:00','00:00:00',11,1,1,'security','2025-12-04 10:08:46','security','2025-12-04 10:08:46'),('8c4dbd36-d0be-11f0-9955-581122801490','QC_01','Ca QC Sáng','07:00:00','16:00:00','11:00:00','12:00:00',8,1,1,'qa_lead','2025-12-04 10:08:46','qa_lead','2025-12-04 10:08:46'),('8c4dbdcc-d0be-11f0-9955-581122801490','IT_SUP','IT Support','08:30:00','17:30:00','12:00:00','13:00:00',8,1,1,'it_manager','2025-12-04 10:08:46','it_manager','2025-12-04 10:08:46'),('8c4dbe4e-d0be-11f0-9955-581122801490','CLEAN_1','Vệ Sinh Sáng','05:00:00','09:00:00',NULL,NULL,4,0,1,'admin','2025-12-04 10:08:46','admin','2025-12-04 10:08:46'),('8c4dbeda-d0be-11f0-9955-581122801490','CLEAN_2','Vệ Sinh Tối','17:00:00','21:00:00',NULL,NULL,4,0,1,'admin','2025-12-04 10:08:46','admin','2025-12-04 10:08:46'),('8c4dc00e-d0be-11f0-9955-581122801490','OT_EVN','Tăng ca tối (2h)','17:00:00','19:00:00',NULL,NULL,2,0,1,'manager','2025-12-04 10:08:46','manager','2025-12-04 10:08:46'),('8c4dc0fd-d0be-11f0-9955-581122801490','TEST_01','Ca Thử Nghiệm','08:00:00','08:30:00',NULL,NULL,0.5,0,0,'dev','2025-12-04 10:08:46','dev','2025-12-04 10:08:46'),('ba10cf23-3649-4f7b-b2c1-d4c64551e92e','Ca sáng','Ca sáng','07:00:00','12:30:00','10:00:00','10:30:00',5,0.5,1,'Phí Hoàng Anh','2025-12-10 03:28:56','Phí Hoàng Anh','2025-12-10 03:30:17'),('fb7fb8d8-99a0-45da-950a-2a1456ad92cf','3123123','Hoàng Anh 11a3','08:00:00','17:01:00','09:30:00','14:30:00',4,5,1,'Phí Hoàng Anh','2025-12-10 04:04:06','Phí Hoàng Anh','2025-12-10 04:04:07');
/*!40000 ALTER TABLE `shift` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-12-10 11:17:41
