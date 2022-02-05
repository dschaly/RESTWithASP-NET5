CREATE TABLE `rest_aspnet`.`person` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `FirstName` VARCHAR(80) NOT NULL,
  `LastName` VARCHAR(80) NOT NULL,
  `Address` VARCHAR(100) NOT NULL,
  `Gender` VARCHAR(6) NOT NULL,
  PRIMARY KEY (`Id`));