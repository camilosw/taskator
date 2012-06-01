-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.1.50-community - MySQL Community Server (GPL)
-- Server OS:                    Win32
-- HeidiSQL version:             7.0.0.4053
-- Date/time:                    2012-05-31 21:10:17
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET FOREIGN_KEY_CHECKS=0 */;

-- Dumping database structure for tareas
CREATE DATABASE IF NOT EXISTS `tareas` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `tareas`;


-- Dumping structure for table tareas.cliente
CREATE TABLE IF NOT EXISTS `cliente` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) NOT NULL,
  `activo` int(10) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table tareas.estado
CREATE TABLE IF NOT EXISTS `estado` (
  `id` int(10) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `tarea_activa` int(11) NOT NULL,
  `color` char(7) NOT NULL,
  `orden` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table tareas.log_trabajo_tareas
CREATE TABLE IF NOT EXISTS `log_trabajo_tareas` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `id_tarea` int(10) NOT NULL,
  `id_persona` int(10) NOT NULL,
  `fecha_inicio` datetime NOT NULL,
  `fecha_fin` datetime NOT NULL,
  `minutos` float NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_log_trabajo_tareas_tarea` (`id_tarea`),
  KEY `FK_log_trabajo_tareas_usuario` (`id_persona`),
  CONSTRAINT `FK_log_trabajo_tareas_tarea` FOREIGN KEY (`id_tarea`) REFERENCES `tarea` (`id`),
  CONSTRAINT `FK_log_trabajo_tareas_usuario` FOREIGN KEY (`id_persona`) REFERENCES `usuario` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table tareas.proyecto
CREATE TABLE IF NOT EXISTS `proyecto` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `id_cliente` int(11) DEFAULT NULL,
  `nombre` varchar(50) NOT NULL,
  `codigo_presupuesto` varchar(20) NOT NULL,
  `orden` int(20) NOT NULL,
  `activo` int(11) NOT NULL DEFAULT '1',
  `creado` datetime NOT NULL,
  `actualizado` datetime NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_proyecto_cliente` (`id_cliente`),
  CONSTRAINT `FK_proyecto_cliente` FOREIGN KEY (`id_cliente`) REFERENCES `cliente` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table tareas.tarea
CREATE TABLE IF NOT EXISTS `tarea` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `id_proyecto` int(10) NOT NULL,
  `id_estado` int(10) NOT NULL,
  `id_asignado` int(11) DEFAULT NULL,
  `id_usuario_crea` int(11) DEFAULT NULL,
  `descripcion` text NOT NULL,
  `fecha_entrega` date DEFAULT NULL,
  `tiempo_estimado` int(11) DEFAULT NULL,
  `orden` int(20) NOT NULL,
  `creada` datetime NOT NULL,
  `actualizada` datetime NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_tarea_proyecto` (`id_proyecto`),
  KEY `FK_tarea_estado` (`id_estado`),
  KEY `FK_tarea_usuario` (`id_asignado`),
  KEY `FK_tarea_usuario_2` (`id_usuario_crea`),
  CONSTRAINT `FK_tarea_estado` FOREIGN KEY (`id_estado`) REFERENCES `estado` (`id`),
  CONSTRAINT `FK_tarea_proyecto` FOREIGN KEY (`id_proyecto`) REFERENCES `proyecto` (`id`),
  CONSTRAINT `FK_tarea_usuario` FOREIGN KEY (`id_asignado`) REFERENCES `usuario` (`id`),
  CONSTRAINT `FK_tarea_usuario_2` FOREIGN KEY (`id_usuario_crea`) REFERENCES `usuario` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table tareas.usuario
CREATE TABLE IF NOT EXISTS `usuario` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `nombre_usuario` varchar(50) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) DEFAULT NULL,
  `activo` int(11) DEFAULT NULL,
  `administrador` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.
/*!40014 SET FOREIGN_KEY_CHECKS=1 */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
