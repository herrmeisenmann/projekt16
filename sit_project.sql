-- phpMyAdmin SQL Dump
-- version 4.3.11
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Erstellungszeit: 08. Okt 2016 um 19:25
-- Server-Version: 5.6.24
-- PHP-Version: 5.6.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Datenbank: `sit_project`
--
CREATE DATABASE IF NOT EXISTS `sit_project` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `sit_project`;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `beruf`
--

DROP TABLE IF EXISTS `beruf`;
CREATE TABLE IF NOT EXISTS `beruf` (
  `beruf_id` int(11) NOT NULL,
  `bezeichnung` text CHARACTER SET latin1 NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `beruf`
--

INSERT INTO `beruf` (`beruf_id`, `bezeichnung`) VALUES
(1, 'FIAE'),
(2, 'FISI');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `chatlog`
--

DROP TABLE IF EXISTS `chatlog`;
CREATE TABLE IF NOT EXISTS `chatlog` (
  `chatlog_id` int(11) NOT NULL,
  `username` varchar(45) NOT NULL,
  `message` varchar(45) NOT NULL,
  `date` datetime NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `chatlog`
--

INSERT INTO `chatlog` (`chatlog_id`, `username`, `message`, `date`) VALUES
(10, 'Jonas', '', '2016-10-07 10:51:06'),
(11, 'Jonas', 'Hallo', '2016-10-07 10:51:12');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `class`
--

DROP TABLE IF EXISTS `class`;
CREATE TABLE IF NOT EXISTS `class` (
  `class_id` int(11) NOT NULL,
  `bezeichnung` varchar(45) NOT NULL,
  `stundenplan_id` int(11) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `class`
--

INSERT INTO `class` (`class_id`, `bezeichnung`, `stundenplan_id`) VALUES
(1, 'IT4a', 1),
(2, 'IT4a', 1),
(3, 'IT5a', 2),
(4, 'Trinkverein Wilhelmsburg', 3);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `termine`
--

DROP TABLE IF EXISTS `termine`;
CREATE TABLE IF NOT EXISTS `termine` (
  `termine_id` int(11) NOT NULL,
  `bezeichnung` text CHARACTER SET latin1 NOT NULL,
  `Fach` text CHARACTER SET latin1 NOT NULL,
  `datum` date NOT NULL,
  `kommentar` longtext CHARACTER SET latin1,
  `user_id` int(11) NOT NULL,
  `Note` int(11) DEFAULT NULL
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `termine`
--

INSERT INTO `termine` (`termine_id`, `bezeichnung`, `Fach`, `datum`, `kommentar`, `user_id`, `Note`) VALUES
(1, 'Klausur', 'AE', '2004-10-20', NULL, 8, 1),
(2, 'Klausur', 'FE', '2004-10-20', NULL, 8, 1),
(3, 'Klausur', 'AE', '2004-10-20', NULL, 8, 2),
(4, 'Klausur', 'FE', '2004-10-20', NULL, 8, 1),
(5, 'Klausur', 'AE', '2004-10-20', NULL, 8, 2),
(6, 'Klausur', 'FE', '2004-10-20', NULL, 8, 1),
(7, 'Klausur', 'AE', '2004-10-20', NULL, 8, 2),
(8, 'Klausur', 'WuG', '2004-10-20', NULL, 8, 2),
(9, 'Klausur', 'AE', '2004-10-20', NULL, 8, 3),
(10, 'Hallo', 'Fach', '2016-10-05', 'Kommentar', 9, 1),
(11, 'Hallo', 'Fach', '2016-10-05', 'Kommentar', 9, 1),
(12, 'Hallo2', 'Fach', '2016-10-05', 'Kommentar', 9, 5);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `user`
--

DROP TABLE IF EXISTS `user`;
CREATE TABLE IF NOT EXISTS `user` (
  `user_id` int(11) NOT NULL,
  `username` varchar(45) NOT NULL,
  `first_name` text NOT NULL,
  `last_name` text NOT NULL,
  `password` varchar(128) CHARACTER SET latin1 NOT NULL,
  `beruf_id` int(11) NOT NULL,
  `class_id` int(11) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `user`
--

INSERT INTO `user` (`user_id`, `username`, `first_name`, `last_name`, `password`, `beruf_id`, `class_id`) VALUES
(8, 'NiGri', 'Niklas', 'Grieger', 'start123', 1, 1),
(9, 'JoKra', 'Jonas', 'Krahl', '123yxc', 1, 1);

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `beruf`
--
ALTER TABLE `beruf`
  ADD PRIMARY KEY (`beruf_id`);

--
-- Indizes für die Tabelle `chatlog`
--
ALTER TABLE `chatlog`
  ADD PRIMARY KEY (`chatlog_id`);

--
-- Indizes für die Tabelle `class`
--
ALTER TABLE `class`
  ADD PRIMARY KEY (`class_id`), ADD UNIQUE KEY `class_id_UNIQUE` (`class_id`);

--
-- Indizes für die Tabelle `termine`
--
ALTER TABLE `termine`
  ADD PRIMARY KEY (`termine_id`), ADD KEY `user_fk` (`user_id`);

--
-- Indizes für die Tabelle `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`user_id`), ADD UNIQUE KEY `username_UNIQUE` (`username`), ADD KEY `fk_class` (`class_id`), ADD KEY `fk_beruf` (`beruf_id`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `beruf`
--
ALTER TABLE `beruf`
  MODIFY `beruf_id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT für Tabelle `chatlog`
--
ALTER TABLE `chatlog`
  MODIFY `chatlog_id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=12;
--
-- AUTO_INCREMENT für Tabelle `class`
--
ALTER TABLE `class`
  MODIFY `class_id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT für Tabelle `termine`
--
ALTER TABLE `termine`
  MODIFY `termine_id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=13;
--
-- AUTO_INCREMENT für Tabelle `user`
--
ALTER TABLE `user`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=10;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
