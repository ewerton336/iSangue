-- isangue_banco.cedente_local definition

CREATE TABLE `cedente_local` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `nm_cedente_local` varchar(200) DEFAULT NULL,
  `nm_responsavel_cedente` varchar(200) DEFAULT NULL,
  `nr_documento_proprietario` varchar(15) DEFAULT NULL,
  `nr_telefone` int(11) DEFAULT NULL,
  `nr_telefone_2` int(11) DEFAULT NULL,
  `nm_endereco` varchar(200) DEFAULT NULL,
  `nm_cidade` varchar(50) DEFAULT NULL,
  `sg_estado` varchar(2) DEFAULT NULL,
  `USUARIO_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `cd_usuario` (`USUARIO_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;


-- isangue_banco.usuario definition

CREATE TABLE `usuario` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `EMAIL` varchar(100) NOT NULL,
  `SENHA` varchar(50) NOT NULL,
  `TIPO_USUARIO` varchar(100) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=68 DEFAULT CHARSET=utf8;


-- isangue_banco.entidade_coletora definition

CREATE TABLE `entidade_coletora` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `NOME` varchar(50) NOT NULL,
  `ENDERECO_COMERCIAL` varchar(100) NOT NULL,
  `TELEFONE` bigint(20) NOT NULL,
  `NOME_RESPONSAVEL` varchar(70) NOT NULL,
  `USUARIO_ID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `entidade_coletora_FK` (`USUARIO_ID`),
  CONSTRAINT `entidade_coletora_FK` FOREIGN KEY (`USUARIO_ID`) REFERENCES `usuario` (`ID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;


-- isangue_banco.local_evento definition

CREATE TABLE `local_evento` (
  `cd_local_evento` int(11) NOT NULL,
  `cd_cedente_local` int(11) NOT NULL,
  `nm_local_evento` varchar(200) DEFAULT NULL,
  `qt_tamanho_area` int(11) DEFAULT NULL,
  `qt_banheiro` int(11) DEFAULT NULL,
  `qt_estacionamento` int(11) DEFAULT NULL,
  `ie_local_aprovado_sim_nao` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`cd_local_evento`),
  KEY `local_evento_FK` (`cd_cedente_local`),
  CONSTRAINT `local_evento_FK` FOREIGN KEY (`cd_cedente_local`) REFERENCES `cedente_local` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


-- isangue_banco.calendario_evento definition

CREATE TABLE `calendario_evento` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nm_evento` varchar(200) DEFAULT NULL,
  `dt_evento` datetime DEFAULT NULL,
  `qt_interessado` int(11) DEFAULT NULL,
  `cd_entidade_coletora_fk` int(11) DEFAULT NULL,
  `cd_cedente_local_fk` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `cd_entidade_coletora_fk` (`cd_entidade_coletora_fk`),
  KEY `calendario_evento_FK` (`cd_cedente_local_fk`),
  CONSTRAINT `calendario_evento_FK` FOREIGN KEY (`cd_cedente_local_fk`) REFERENCES `cedente_local` (`ID`),
  CONSTRAINT `calendario_evento_ibfk_2` FOREIGN KEY (`cd_entidade_coletora_fk`) REFERENCES `entidade_coletora` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;


-- isangue_banco.doador definition

CREATE TABLE `doador` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `NOME` varchar(100) DEFAULT NULL,
  `SOBRENOME` varchar(100) DEFAULT NULL,
  `ENDERECO` varchar(100) DEFAULT NULL,
  `NUMERO_RESIDENCIA` varchar(100) DEFAULT NULL,
  `COMPLEMENTO` varchar(100) DEFAULT NULL,
  `CIDADE_RESIDENCIA` varchar(100) DEFAULT NULL,
  `ESTADO_RESIDENCIA` varchar(100) DEFAULT NULL,
  `DT_NASCIMENTO` varchar(100) DEFAULT NULL,
  `TELEFONE` bigint(20) DEFAULT NULL,
  `CIDADE_DOACAO` varchar(100) DEFAULT NULL,
  `TIPO_SANGUINEO` varchar(100) DEFAULT NULL,
  `USUARIO_ID` int(11) DEFAULT NULL,
  `EVENTO_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `doador_FK` (`USUARIO_ID`),
  KEY `doador_FK_1` (`EVENTO_ID`),
  CONSTRAINT `doador_FK` FOREIGN KEY (`USUARIO_ID`) REFERENCES `usuario` (`ID`) ON DELETE CASCADE,
  CONSTRAINT `doador_FK_1` FOREIGN KEY (`EVENTO_ID`) REFERENCES `calendario_evento` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8;