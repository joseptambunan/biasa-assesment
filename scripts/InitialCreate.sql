CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `tbl_user` (
    `id` int NOT NULL AUTO_INCREMENT,
    `username` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `password` varchar(500) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_tbl_user` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4;

CREATE UNIQUE INDEX `IX_tbl_user_username` ON `tbl_user` (`username`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20261014053248_InitialCreate', '8.0.21');

COMMIT;

