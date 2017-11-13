CREATE DATABASE  IF NOT EXISTS `electionsystem` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `electionsystem`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: electionsystem
-- ------------------------------------------------------
-- Server version	5.7.18-log

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
-- Table structure for table `tb_candidato`
--

DROP TABLE IF EXISTS `tb_candidato`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_candidato` (
  `ID_CANDIDATO` int(11) NOT NULL,
  `NOME` varchar(45) NOT NULL,
  `FOTO` varchar(400) NOT NULL,
  `ID_TIPO_CANDIDATO` int(11) NOT NULL,
  `QUANTIDADE_VOTOS` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID_CANDIDATO`),
  KEY `ID_TIPO_CANDIDATO_TIPO_CANDIDATO_idx` (`ID_TIPO_CANDIDATO`),
  CONSTRAINT `ID_TIPO_CANDIDATO_TIPO_CANDIDATO` FOREIGN KEY (`ID_TIPO_CANDIDATO`) REFERENCES `tb_tipo_candidato` (`ID_TIPO_CANDIDATO`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_candidato`
--

LOCK TABLES `tb_candidato` WRITE;
/*!40000 ALTER TABLE `tb_candidato` DISABLE KEYS */;
INSERT INTO `tb_candidato` VALUES (1,'Lula','C:\\fotos_candidatos\\1_Lula.jpeg',2,0),(2,'Dilma','C:\\fotos_candidatos\\2_Dilma.jpeg',2,0),(3,'Aecio','C:\\fotos_candidatos\\3_Aecio.jpeg',2,0),(4,'Davi Alcolumbre','C:\\fotos_candidatos\\4_Davi Alcolumbre.jpeg',1,0),(5,'Ronaldo Caiado','C:\\fotos_candidatos\\5_Ronaldo Caiado.jpeg',1,0),(6,'Serra','C:\\fotos_candidatos\\6_Serra.jpeg',3,0),(7,'Alckmin','C:\\fotos_candidatos\\7_Alckmin.jpeg',3,0),(8,'Nulo - Presidente','C:\\fotos_candidatos\\8_Nulo - Presidente.jpeg',2,0),(9,'Nulo - Senador','C:\\fotos_candidatos\\9_Nulo - Senador.jpeg',1,0),(10,'Nulo - Governador','C:\\fotos_candidatos\\10_Nulo - Governador.jpeg',3,0),(11,'Branco - Governador','C:\\fotos_candidatos\\11_Branco - Governador.jpeg',3,0),(12,'Branco - Senador','C:\\fotos_candidatos\\12_Branco - Senador.jpeg',1,0),(13,'Branco - Presidente','C:\\fotos_candidatos\\13_Branco - Presidente.jpeg',2,0);
/*!40000 ALTER TABLE `tb_candidato` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_eleitor`
--

DROP TABLE IF EXISTS `tb_eleitor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_eleitor` (
  `ID_ELEITOR` int(11) NOT NULL,
  `CPF` varchar(45) NOT NULL,
  `NOME` varchar(45) NOT NULL,
  `FOTO` varchar(45) NOT NULL,
  `VOTOU` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID_ELEITOR`),
  UNIQUE KEY `CPF_UNIQUE` (`CPF`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_eleitor`
--

LOCK TABLES `tb_eleitor` WRITE;
/*!40000 ALTER TABLE `tb_eleitor` DISABLE KEYS */;
INSERT INTO `tb_eleitor` VALUES (1,'223412351','Gustavo Viana','no_photo',0),(2,'229281351','Joao Pedro','no_photo',0),(3,'212541231','Pedro Augusto','no_photo',0),(4,'212532212','Caio Augusto','no_photo',0);
/*!40000 ALTER TABLE `tb_eleitor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_tipo_candidato`
--

DROP TABLE IF EXISTS `tb_tipo_candidato`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_tipo_candidato` (
  `ID_TIPO_CANDIDATO` int(11) NOT NULL,
  `DESCRICAO` varchar(45) NOT NULL,
  PRIMARY KEY (`ID_TIPO_CANDIDATO`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_tipo_candidato`
--

LOCK TABLES `tb_tipo_candidato` WRITE;
/*!40000 ALTER TABLE `tb_tipo_candidato` DISABLE KEYS */;
INSERT INTO `tb_tipo_candidato` VALUES (1,'Senador'),(2,'Presidente'),(3,'Governador');
/*!40000 ALTER TABLE `tb_tipo_candidato` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_uev`
--

DROP TABLE IF EXISTS `tb_uev`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tb_uev` (
  `ID_UEV` int(11) NOT NULL,
  `INICIO` datetime NOT NULL,
  `FIM` datetime NOT NULL,
  PRIMARY KEY (`ID_UEV`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_uev`
--

LOCK TABLES `tb_uev` WRITE;
/*!40000 ALTER TABLE `tb_uev` DISABLE KEYS */;
INSERT INTO `tb_uev` VALUES (1,'2017-11-06 06:00:00','2017-11-06 17:00:00');
/*!40000 ALTER TABLE `tb_uev` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'electionsystem'
--

--
-- Dumping routines for database 'electionsystem'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-11-11 11:32:04
