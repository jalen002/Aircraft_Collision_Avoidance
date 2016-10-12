-- MySQL dump 10.13  Distrib 5.6.27, for debian-linux-gnu (x86_64)
--
-- Host: localhost    Database: gps_data
-- ------------------------------------------------------
-- Server version	5.6.27-0ubuntu0.14.04.1

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
-- Table structure for table `collision_data`
--

DROP TABLE IF EXISTS `collision_data`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `collision_data` (
  `id` bigint(20) DEFAULT NULL,
  `latitude_1` decimal(8,5) DEFAULT NULL,
  `longitude_1` decimal(8,5) DEFAULT NULL,
  `latitude_2` decimal(8,5) DEFAULT NULL,
  `longitude_2` decimal(8,5) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `collision_data`
--

LOCK TABLES `collision_data` WRITE;
/*!40000 ALTER TABLE `collision_data` DISABLE KEYS */;
INSERT INTO `collision_data` VALUES (1446956471,39.85851,104.67534,40.86151,105.67234),(1446956643,39.85851,104.67534,40.86151,105.67234),(1446956645,39.97808,104.36131,40.98409,105.35532),(1446956645,40.09802,104.04823,41.10704,105.03926),(1446956647,40.21831,103.73608,41.23037,104.72414),(1446956648,40.33897,103.42488,41.35406,104.40997),(1446956649,40.45998,103.11460,41.47812,104.09674),(1446956650,40.58136,102.80526,41.60255,103.78445),(1446956652,40.70311,102.49684,41.72736,103.47309),(1446956654,40.82522,102.18935,41.85254,103.16267),(1446956655,40.94769,101.88278,41.97810,102.85319),(1446956657,41.07054,101.57714,42.10404,102.54463),(1446956658,41.19375,101.27240,42.23035,102.23699),(1446956660,41.31733,100.96859,42.35704,101.93028),(1446956661,41.44128,100.66568,42.48411,101.62449),(1446956663,41.56560,100.36368,42.61156,101.31962),(1446956663,41.69030,100.06259,42.73940,101.01566),(1446956664,41.81537,99.76240,42.86761,100.71261),(1446956666,41.94082,99.46312,42.99622,100.41047),(1446956668,42.06664,99.16473,43.12521,100.10924),(1446956668,42.19284,98.86723,43.25458,99.80891),(1446956669,42.31942,98.57063,43.38435,99.50949),(1446956670,42.44638,98.27492,43.51450,99.21096),(1446956670,42.57372,97.98010,43.64504,98.91333),(1446956671,42.70144,97.68616,43.77598,98.61659),(1446956672,42.82954,97.39310,43.90731,98.32074),(1446956673,42.95803,97.10092,44.03903,98.02577),(1446956674,43.08690,96.80961,44.17114,97.73170),(1446956675,43.21617,96.51919,44.30366,97.43850),(1446956676,43.34581,96.22963,44.43657,97.14619),(1446956678,43.47585,95.94094,44.56988,96.85475);
/*!40000 ALTER TABLE `collision_data` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2015-11-07 21:29:01