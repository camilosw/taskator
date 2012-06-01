-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.1.50-community - MySQL Community Server (GPL)
-- Server OS:                    Win32
-- HeidiSQL version:             7.0.0.4053
-- Date/time:                    2012-05-31 21:11:00
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET FOREIGN_KEY_CHECKS=0 */;
-- Dumping data for table tareas.estado: ~7 rows (approximately)
/*!40000 ALTER TABLE `estado` DISABLE KEYS */;
INSERT INTO `estado` (`id`, `nombre`, `tarea_activa`, `color`, `orden`) VALUES
	(1, 'Pendiente', 1, 'ffcc00', 1),
	(2, 'Falta información', 1, 'ff0000', 2),
	(3, 'En proceso', 1, '0094fb', 3),
	(4, 'Terminado', 1, '00ff00', 4),
	(5, 'Cerrado', 0, 'c0d2f0', 6),
	(6, 'Rechazado', 0, '6d7269', 7),
	(7, 'Corrección', 1, 'FF970F', 5);
/*!40000 ALTER TABLE `estado` ENABLE KEYS */;
/*!40014 SET FOREIGN_KEY_CHECKS=1 */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
