# Create DB
DROP DATABASE IF EXISTS `psycho_help_2`;
CREATE DATABASE `psycho_help_2`;

# Use default DB
USE `psycho_help_2`;

# Drop tables
DROP TABLE IF EXISTS `User`;
DROP TABLE IF EXISTS `UserRole`;

# Create tables
CREATE TABLE IF NOT EXISTS `UserRole`
(
    `Id` INT UNSIGNED UNIQUE AUTO_INCREMENT,
    `Title` VARCHAR(20) NOT NULL UNIQUE,
    PRIMARY KEY (`Id`)
);
CREATE TABLE IF NOT EXISTS `User`
(
    `IdUserRole` INT UNSIGNED NOT NULL,
    `Login` VARCHAR(30) UNIQUE NOT NULL,
    `HashPassword` BINARY(32) NOT NULL,
    `FirstName` VARCHAR(20) NOT NULL,
    `SecondName` VARCHAR(20) NOT NULL,
    `DateBirthday` DATE,
    `IsBlocked` BOOLEAN NOT NULL DEFAULT FALSE,
    PRIMARY KEY (`Login`),
    FOREIGN KEY (`IdUserRole`) REFERENCES UserRole(`Id`)
);

# Insert default data
INSERT `UserRole`(`Title`) VALUES ('User'), ('Psychologist'), ('CopyWriter'), ('Admin'), ('SuperAdmin');

# Setting up a user to access the database
DROP USER IF EXISTS 'user_for_psycho_help'@'%';
CREATE USER 'user_for_psycho_help'@'%' IDENTIFIED BY '';
GRANT SELECT, UPDATE, DELETE ON `psycho_help_2`.* TO 'user_for_psycho_help'@'%';
FLUSH PRIVILEGES;