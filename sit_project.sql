-- MySQL dump 10.13  Distrib 5.7.12, for Win64 (x86_64)
--
-- Host: localhost    Database: sit_project
-- ------------------------------------------------------
-- Server version	5.7.14-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `beruf`
--

DROP TABLE IF EXISTS `beruf`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `beruf` (
  `beruf_id` int(11) NOT NULL AUTO_INCREMENT,
  `bezeichnung` text CHARACTER SET latin1 NOT NULL,
  PRIMARY KEY (`beruf_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `beruf`
--

LOCK TABLES `beruf` WRITE;
/*!40000 ALTER TABLE `beruf` DISABLE KEYS */;
INSERT INTO `beruf` VALUES (1,'FIAE'),(2,'FISI');
/*!40000 ALTER TABLE `beruf` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `class`
--

DROP TABLE IF EXISTS `class`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `class` (
  `class_id` int(11) NOT NULL AUTO_INCREMENT,
  `bezeichnung` varchar(45) NOT NULL,
  `stundenplan_id` int(11) NOT NULL,
  PRIMARY KEY (`class_id`),
  UNIQUE KEY `class_id_UNIQUE` (`class_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `class`
--

LOCK TABLES `class` WRITE;
/*!40000 ALTER TABLE `class` DISABLE KEYS */;
INSERT INTO `class` VALUES (1,'IT4a',1),(2,'IT4a',1),(3,'IT5a',2),(4,'Trinkverein Wilhelmsburg',3);
/*!40000 ALTER TABLE `class` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stundenplan`
--

DROP TABLE IF EXISTS `stundenplan`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `stundenplan` (
  `stundenplan_id` int(11) NOT NULL AUTO_INCREMENT,
  `stundenplan_blob` blob NOT NULL,
  PRIMARY KEY (`stundenplan_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stundenplan`
--

LOCK TABLES `stundenplan` WRITE;
/*!40000 ALTER TABLE `stundenplan` DISABLE KEYS */;
/*!40000 ALTER TABLE `stundenplan` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `termine`
--

DROP TABLE IF EXISTS `termine`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `termine` (
  `termine_id` int(11) NOT NULL AUTO_INCREMENT,
  `bezeichnung` text CHARACTER SET latin1 NOT NULL,
  `Fach` text CHARACTER SET latin1 NOT NULL,
  `datum` date NOT NULL,
  `kommentar` longtext CHARACTER SET latin1,
  `user_id` int(11) NOT NULL,
  `Note` int(11) DEFAULT NULL,
  PRIMARY KEY (`termine_id`),
  KEY `user_fk` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `termine`
--

LOCK TABLES `termine` WRITE;
/*!40000 ALTER TABLE `termine` DISABLE KEYS */;
INSERT INTO `termine` VALUES (1,'Klausur','AE','2004-10-20',NULL,8,1),(2,'Klausur','FE','2004-10-20',NULL,8,1),(3,'Klausur','AE','2004-10-20',NULL,8,2),(4,'Klausur','FE','2004-10-20',NULL,8,1),(5,'Klausur','AE','2004-10-20',NULL,8,2),(6,'Klausur','FE','2004-10-20',NULL,8,1),(7,'Klausur','AE','2004-10-20',NULL,8,2),(8,'Klausur','WuG','2004-10-20',NULL,8,2),(9,'Klausur','AE','2004-10-20',NULL,8,3),(10,'Hallo','Fach','2016-10-05','Kommentar',9,1),(11,'Hallo','Fach','2016-10-05','Kommentar',9,1),(12,'Hallo2','Fach','2016-10-05','Kommentar',9,5);
/*!40000 ALTER TABLE `termine` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(45) NOT NULL,
  `first_name` text NOT NULL,
  `last_name` text NOT NULL,
  `password` varchar(128) CHARACTER SET latin1 NOT NULL,
  `beruf_id` int(11) NOT NULL,
  `class_id` int(11) NOT NULL,
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `username_UNIQUE` (`username`),
  KEY `fk_class` (`class_id`),
  KEY `fk_beruf` (`beruf_id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (8,'NiGri','Niklas','Grieger','start123',1,1),(9,'JoKra','Jonas','Krahl','123yxc',1,1);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-10-06 12:18:24
