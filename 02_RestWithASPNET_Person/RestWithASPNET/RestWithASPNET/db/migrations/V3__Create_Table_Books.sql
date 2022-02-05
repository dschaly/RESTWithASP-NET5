CREATE TABLE `rest_aspnet`.`books` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Title` VARCHAR(80) NOT NULL,
  `Author` VARCHAR(80),
  `Price` DECIMAL(65,2) NOT NULL,
  `LaunchDate` DATETIME(6) NOT NULL,
  PRIMARY KEY (`Id`));