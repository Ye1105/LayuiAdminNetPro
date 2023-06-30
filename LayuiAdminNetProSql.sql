-- MySQL dump 10.13  Distrib 8.0.21, for Win64 (x86_64)
--
-- Host: localhost    Database: layuiadmin
-- ------------------------------------------------------
-- Server version	8.0.21

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
-- Table structure for table `admin_account`
--

DROP TABLE IF EXISTS `admin_account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `admin_account` (
  `UId` varchar(45) NOT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `Phone` varchar(45) DEFAULT NULL,
  `Password` varchar(45) DEFAULT NULL,
  `JobStatus` tinyint DEFAULT '0',
  `LastLoginIp` varchar(45) DEFAULT NULL,
  `Status` tinyint DEFAULT '0',
  `LastLoginTime` datetime DEFAULT NULL,
  `Created` timestamp NULL DEFAULT NULL,
  `Sex` tinyint DEFAULT '0',
  PRIMARY KEY (`UId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `admin_account`
--

LOCK TABLES `admin_account` WRITE;
/*!40000 ALTER TABLE `admin_account` DISABLE KEYS */;
INSERT INTO `admin_account` VALUES ('0d68d9a9-c250-4ca6-92bd-fa548b6d27fe','孔平','18195921476','d0970714757783e6cf17b26fb8e2298f',0,NULL,0,NULL,'2023-06-02 09:18:16',2),('11433cc1-098a-4463-8e6f-8f85942e7521','小白百','16666666666','d0970714757783e6cf17b26fb8e2298f',2,'::1',1,'2023-06-30 16:13:09','2023-01-01 17:33:21',1),('11433cc1-098a-4463-8e6f-8f85942e7525','叶笺','18888888888','d0970714757783e6cf17b26fb8e2298f',0,'127.0.0.1',1,'2023-06-30 16:14:06','2023-01-04 17:33:21',0),('11528199-753c-4ba9-9006-87c0dc241a30','冉冉','15838707161','d0970714757783e6cf17b26fb8e2298f',3,NULL,1,NULL,'2023-06-07 01:45:37',1),('350d91e7-0a30-41d3-8ade-39e07e6c81f8','贤心','15868707168','d0970714757783e6cf17b26fb8e2298f',2,NULL,1,NULL,'2023-06-05 06:50:21',0),('414f0820-96ba-447f-8f98-752ba8feddaf','Shirley Thomas','18117816481','d0970714757783e6cf17b26fb8e2298f',2,NULL,1,NULL,'2023-06-06 08:15:13',1),('6f0422fa-b040-4968-a220-27b922cf014a','姜然','15868707163','d0970714757783e6cf17b26fb8e2298f',3,NULL,1,NULL,'2023-01-16 05:21:22',0),('7c18ad95-73e8-401a-beae-b5c22d27ac45','大白','15868707183','d0970714757783e6cf17b26fb8e2298f',0,NULL,1,NULL,'2023-06-05 08:50:23',0),('9190329c-5e32-42c0-ac27-d2c32a82a139','小瑶','15868505967','d0970714757783e6cf17b26fb8e2298f',0,'::1',1,'2023-01-16 14:33:50','2023-01-13 09:03:41',1),('c6a33fb0-a998-4e1e-a270-8cf5a33baf6f','秋缄默','15432549673','d0970714757783e6cf17b26fb8e2298f',1,NULL,1,NULL,'2023-06-15 07:10:16',0),('f8a0868e-0b46-4034-b8e5-63c11780ae5a','小红','15868707169','d0970714757783e6cf17b26fb8e2298f',3,NULL,1,NULL,'2023-01-13 08:54:26',1);
/*!40000 ALTER TABLE `admin_account` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `admin_account_role`
--

DROP TABLE IF EXISTS `admin_account_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `admin_account_role` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UId` varchar(45) DEFAULT NULL,
  `RId` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `admin_account_role`
--

LOCK TABLES `admin_account_role` WRITE;
/*!40000 ALTER TABLE `admin_account_role` DISABLE KEYS */;
INSERT INTO `admin_account_role` VALUES (7,'11433cc1-098a-4463-8e6f-8f85942e7521','b9ca9242-efcc-4241-b6e8-c47bf3b1b4f0'),(8,'11433cc1-098a-4463-8e6f-8f85942e7521','e4d7b4eb-13fd-460c-8354-6b334f3209c7'),(10,'c6a33fb0-a998-4e1e-a270-8cf5a33baf6f','99aeb8ab-e6f6-4104-a2c3-bd27cd826fb1'),(11,'c6a33fb0-a998-4e1e-a270-8cf5a33baf6f','1a3b6758-fadf-4233-9f06-32bb35534b31'),(12,'11433cc1-098a-4463-8e6f-8f85942e7525','bad885fb-3b07-4464-a18f-e9f4735bfa14');
/*!40000 ALTER TABLE `admin_account_role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `admin_log_tencent_sms`
--

DROP TABLE IF EXISTS `admin_log_tencent_sms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `admin_log_tencent_sms` (
  `RequestId` varchar(45) NOT NULL,
  `SendStatusSet` json DEFAULT NULL,
  `Sms` varchar(45) DEFAULT NULL,
  `Created` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`RequestId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `admin_log_tencent_sms`
--

LOCK TABLES `admin_log_tencent_sms` WRITE;
/*!40000 ALTER TABLE `admin_log_tencent_sms` DISABLE KEYS */;
/*!40000 ALTER TABLE `admin_log_tencent_sms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `admin_role_info`
--

DROP TABLE IF EXISTS `admin_role_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `admin_role_info` (
  `RId` varchar(45) NOT NULL,
  `Name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`RId`),
  UNIQUE KEY `_UniqueName_` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `admin_role_info`
--

LOCK TABLES `admin_role_info` WRITE;
/*!40000 ALTER TABLE `admin_role_info` DISABLE KEYS */;
INSERT INTO `admin_role_info` VALUES ('bad885fb-3b07-4464-a18f-e9f4735bfa14','客服'),('b9ca9242-efcc-4241-b6e8-c47bf3b1b4f0','总监'),('99aeb8ab-e6f6-4104-a2c3-bd27cd826fb1','测试'),('e4d7b4eb-13fd-460c-8354-6b334f3209c7','管理员'),('1a3b6758-fadf-4233-9f06-32bb35534b31','运维');
/*!40000 ALTER TABLE `admin_role_info` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `admin_role_permission`
--

DROP TABLE IF EXISTS `admin_role_permission`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `admin_role_permission` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CId` tinyint DEFAULT NULL,
  `RId` varchar(45) DEFAULT NULL,
  `MId` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=1858 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `admin_role_permission`
--

LOCK TABLES `admin_role_permission` WRITE;
/*!40000 ALTER TABLE `admin_role_permission` DISABLE KEYS */;
INSERT INTO `admin_role_permission` VALUES (1816,1,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','081c623d-9ca5-47ea-80a3-363b42f90b36'),(1817,1,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','86150648-1bea-4000-abf6-d1e6d80aecbc'),(1818,0,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','86150648-1bea-4000-abf6-d1e6d80aecbc'),(1819,2,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','86150648-1bea-4000-abf6-d1e6d80aecbc'),(1820,3,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','86150648-1bea-4000-abf6-d1e6d80aecbc'),(1821,1,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','ebad17b5-91ef-4f4c-be67-dd9f65edb276'),(1822,0,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','ebad17b5-91ef-4f4c-be67-dd9f65edb276'),(1823,2,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','ebad17b5-91ef-4f4c-be67-dd9f65edb276'),(1824,3,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','ebad17b5-91ef-4f4c-be67-dd9f65edb276'),(1825,1,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','6be04da1-2d15-426a-8a01-d624166f2b6a'),(1826,1,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','2fb8b71c-81a8-418a-8cf1-277f98656092'),(1827,0,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','2fb8b71c-81a8-418a-8cf1-277f98656092'),(1828,2,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','2fb8b71c-81a8-418a-8cf1-277f98656092'),(1829,3,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','2fb8b71c-81a8-418a-8cf1-277f98656092'),(1830,1,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','587a6466-191f-479e-a77e-dc751759d971'),(1831,0,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','587a6466-191f-479e-a77e-dc751759d971'),(1832,2,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','587a6466-191f-479e-a77e-dc751759d971'),(1833,3,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','587a6466-191f-479e-a77e-dc751759d971'),(1834,1,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','7425b6cf-0348-437e-8b0f-9e68fc2e8002'),(1835,0,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','7425b6cf-0348-437e-8b0f-9e68fc2e8002'),(1836,1,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','8261c605-f848-44d3-82ac-e2a8bbc750da'),(1837,0,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','8261c605-f848-44d3-82ac-e2a8bbc750da'),(1838,2,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','8261c605-f848-44d3-82ac-e2a8bbc750da'),(1839,3,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','8261c605-f848-44d3-82ac-e2a8bbc750da'),(1840,1,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','8e5ec295-c37d-403d-8523-731aefbdd95a'),(1841,0,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','8e5ec295-c37d-403d-8523-731aefbdd95a'),(1842,2,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','8e5ec295-c37d-403d-8523-731aefbdd95a'),(1843,3,'e4d7b4eb-13fd-460c-8354-6b334f3209c7','8e5ec295-c37d-403d-8523-731aefbdd95a'),(1849,1,'bad885fb-3b07-4464-a18f-e9f4735bfa14','081c623d-9ca5-47ea-80a3-363b42f90b36'),(1850,1,'bad885fb-3b07-4464-a18f-e9f4735bfa14','86150648-1bea-4000-abf6-d1e6d80aecbc'),(1851,0,'bad885fb-3b07-4464-a18f-e9f4735bfa14','86150648-1bea-4000-abf6-d1e6d80aecbc'),(1852,2,'bad885fb-3b07-4464-a18f-e9f4735bfa14','86150648-1bea-4000-abf6-d1e6d80aecbc'),(1853,3,'bad885fb-3b07-4464-a18f-e9f4735bfa14','86150648-1bea-4000-abf6-d1e6d80aecbc'),(1854,1,'bad885fb-3b07-4464-a18f-e9f4735bfa14','ebad17b5-91ef-4f4c-be67-dd9f65edb276'),(1855,0,'bad885fb-3b07-4464-a18f-e9f4735bfa14','ebad17b5-91ef-4f4c-be67-dd9f65edb276'),(1856,2,'bad885fb-3b07-4464-a18f-e9f4735bfa14','ebad17b5-91ef-4f4c-be67-dd9f65edb276'),(1857,3,'bad885fb-3b07-4464-a18f-e9f4735bfa14','ebad17b5-91ef-4f4c-be67-dd9f65edb276');
/*!40000 ALTER TABLE `admin_role_permission` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `admin_route`
--

DROP TABLE IF EXISTS `admin_route`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `admin_route` (
  `Id` varchar(45) NOT NULL,
  `PId` varchar(45) DEFAULT NULL,
  `Route` varchar(45) DEFAULT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `Order` int DEFAULT NULL,
  `Menu` tinyint DEFAULT NULL,
  `Status` tinyint DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `admin_route`
--

LOCK TABLES `admin_route` WRITE;
/*!40000 ALTER TABLE `admin_route` DISABLE KEYS */;
INSERT INTO `admin_route` VALUES ('081c623d-9ca5-47ea-80a3-363b42f90b36','00000000-0000-0000-0000-000000000000','/','主页',0,1,1),('2fb8b71c-81a8-418a-8cf1-277f98656092','6be04da1-2d15-426a-8a01-d624166f2b6a','/accounts','用户管理',1,1,1),('587a6466-191f-479e-a77e-dc751759d971','6be04da1-2d15-426a-8a01-d624166f2b6a','/routes','路由管理',3,1,1),('6be04da1-2d15-426a-8a01-d624166f2b6a','00000000-0000-0000-0000-000000000000','/','设置',1,1,1),('7425b6cf-0348-437e-8b0f-9e68fc2e8002','6be04da1-2d15-426a-8a01-d624166f2b6a','/roles','角色分配管理',4,0,1),('8261c605-f848-44d3-82ac-e2a8bbc750da','6be04da1-2d15-426a-8a01-d624166f2b6a','/rolepermissions','权限管理',0,0,1),('86150648-1bea-4000-abf6-d1e6d80aecbc','081c623d-9ca5-47ea-80a3-363b42f90b36','/welcome','主页信息',0,1,1),('8e5ec295-c37d-403d-8523-731aefbdd95a','6be04da1-2d15-426a-8a01-d624166f2b6a','/roleinfos','角色管理',2,1,1),('ebad17b5-91ef-4f4c-be67-dd9f65edb276','081c623d-9ca5-47ea-80a3-363b42f90b36','/person','个人信息',2,0,1);
/*!40000 ALTER TABLE `admin_route` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `admin_system_log`
--

DROP TABLE IF EXISTS `admin_system_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `admin_system_log` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UId` varchar(45) DEFAULT NULL,
  `Ip` varchar(45) DEFAULT NULL,
  `Route` text,
  `Description` text,
  `Created` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=114 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `admin_system_log`
--

LOCK TABLES `admin_system_log` WRITE;
/*!40000 ALTER TABLE `admin_system_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `admin_system_log` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-06-30 16:56:52
