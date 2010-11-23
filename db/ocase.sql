SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";

CREATE TABLE IF NOT EXISTS `cases` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Year` int(11) NOT NULL,
  `Sequence` int(11) NOT NULL,
  `Title` varchar(255) NOT NULL,
  `CaseTypeId` int(11) NOT NULL,
  `CreationDate` datetime NOT NULL,
  `OrganizationId` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Year` (`Year`,`Sequence`),
  KEY `Title` (`Title`,`CaseTypeId`,`OrganizationId`,`UserId`),
  KEY `CaseTypeId` (`CaseTypeId`),
  KEY `UserId` (`UserId`),
  KEY `OrganizationId` (`OrganizationId`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251 AUTO_INCREMENT=1 ;


CREATE TABLE IF NOT EXISTS `casetypes` (
  `Id` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;


INSERT INTO `casetypes` (`Id`, `Name`) VALUES
(1, 'BGSAG'),
(2, 'EMSAG'),
(3, 'EJSAG'),
(4, 'PERSAG');


CREATE TABLE IF NOT EXISTS `customfields` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `FieldName` varchar(31) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `FieldName` (`FieldName`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251 AUTO_INCREMENT=1 ;


INSERT INTO `customfields` (`Id`, `FieldName`) VALUES
(1, 'FieldOne'),
(2, 'FieldTwo'),
(3, 'FieldThree'),
(4, 'FieldFour');


CREATE TABLE IF NOT EXISTS `customfieldvalues` (
  `FieldId` int(11) NOT NULL,
  `CaseId` int(11) NOT NULL,
  `FieldValue` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`FieldId`,`CaseId`),
  KEY `FieldValue` (`FieldValue`),
  KEY `CaseId` (`CaseId`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;


CREATE TABLE IF NOT EXISTS `organization` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251 AUTO_INCREMENT=1 ;


CREATE TABLE IF NOT EXISTS `users` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `CreationDate` date NOT NULL,
  `ExpireDate` date NULL,
  PRIMARY KEY (`Id`),
  KEY `Name` (`Name`,`CreationDate`,`ExpireDate`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251 AUTO_INCREMENT=1 ;

ALTER TABLE `cases`
  ADD CONSTRAINT FOREIGN KEY (`CaseTypeId`) REFERENCES `casetypes` (`Id`);
ALTER TABLE `cases`
  ADD CONSTRAINT FOREIGN KEY (`OrganizationId`) REFERENCES `organization` (`Id`);
ALTER TABLE `cases`
  ADD CONSTRAINT FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`);

ALTER TABLE `customfieldvalues`
  ADD CONSTRAINT FOREIGN KEY (`FieldId`) REFERENCES `customfields` (`Id`);
ALTER TABLE `customfieldvalues`
  ADD CONSTRAINT FOREIGN KEY (`CaseId`) REFERENCES `cases` (`Id`);
