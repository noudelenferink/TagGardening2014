




-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 06/10/2014 16:00:00
-- Generated from EDMX file: d:\nick\documents\visual studio 2013\Projects\TagGardening2014\TagGardening2014\Model\Model.edmx
-- Target version: 3.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `deb76315_tg`;
CREATE DATABASE `deb76315_tg`;
USE `deb76315_tg`;

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE `TagSet`(
	`TagId` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`TagValue` longtext NOT NULL, 
	`Position` longtext NOT NULL, 
	`MediaItemId` int NOT NULL);

ALTER TABLE `TagSet` ADD PRIMARY KEY (TagId);




CREATE TABLE `MediaItemSet`(
	`MediaItemId` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`MediaTypeId` int NOT NULL, 
	`MediaItemStatusId` int NOT NULL);

ALTER TABLE `MediaItemSet` ADD PRIMARY KEY (MediaItemId);




CREATE TABLE `MediaItemMediaTagSet`(
	`MediaItemMediaTagId` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`MediaTagId` int NOT NULL, 
	`Position` longtext NOT NULL, 
	`MediaItemId` int NOT NULL);

ALTER TABLE `MediaItemMediaTagSet` ADD PRIMARY KEY (MediaItemMediaTagId);




CREATE TABLE `MediaTagSet`(
	`MediaTagId` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`MediaTagTypeId` int NOT NULL, 
	`MediaTagValue2` longtext NOT NULL);

ALTER TABLE `MediaTagSet` ADD PRIMARY KEY (MediaTagId);




CREATE TABLE `MediaTagTypeSet`(
	`MediaTagTypeId` int NOT NULL AUTO_INCREMENT UNIQUE);

ALTER TABLE `MediaTagTypeSet` ADD PRIMARY KEY (MediaTagTypeId);




CREATE TABLE `MediaTypeSet`(
	`MediaTypeId` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Description` longtext NOT NULL);

ALTER TABLE `MediaTypeSet` ADD PRIMARY KEY (MediaTypeId);




CREATE TABLE `MediaItemStatusSet`(
	`MediaItemStatusId` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Description` longtext NOT NULL);

ALTER TABLE `MediaItemStatusSet` ADD PRIMARY KEY (MediaItemStatusId);






-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `MediaTagId` in table 'MediaItemMediaTagSet'

ALTER TABLE `MediaItemMediaTagSet`
ADD CONSTRAINT `FK_MediaTagMediaItemMediaTag`
    FOREIGN KEY (`MediaTagId`)
    REFERENCES `MediaTagSet`
        (`MediaTagId`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MediaTagMediaItemMediaTag'

CREATE INDEX `IX_FK_MediaTagMediaItemMediaTag` 
    ON `MediaItemMediaTagSet`
    (`MediaTagId`);

-- Creating foreign key on `MediaTagTypeId` in table 'MediaTagSet'

ALTER TABLE `MediaTagSet`
ADD CONSTRAINT `FK_MediaTagTypeMediaTag`
    FOREIGN KEY (`MediaTagTypeId`)
    REFERENCES `MediaTagTypeSet`
        (`MediaTagTypeId`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MediaTagTypeMediaTag'

CREATE INDEX `IX_FK_MediaTagTypeMediaTag` 
    ON `MediaTagSet`
    (`MediaTagTypeId`);

-- Creating foreign key on `MediaTypeId` in table 'MediaItemSet'

ALTER TABLE `MediaItemSet`
ADD CONSTRAINT `FK_MediaType_MediaItem`
    FOREIGN KEY (`MediaTypeId`)
    REFERENCES `MediaTypeSet`
        (`MediaTypeId`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MediaType_MediaItem'

CREATE INDEX `IX_FK_MediaType_MediaItem` 
    ON `MediaItemSet`
    (`MediaTypeId`);

-- Creating foreign key on `MediaItemStatusId` in table 'MediaItemSet'

ALTER TABLE `MediaItemSet`
ADD CONSTRAINT `FK_MediaItemStatus_MediaItem`
    FOREIGN KEY (`MediaItemStatusId`)
    REFERENCES `MediaItemStatusSet`
        (`MediaItemStatusId`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MediaItemStatus_MediaItem'

CREATE INDEX `IX_FK_MediaItemStatus_MediaItem` 
    ON `MediaItemSet`
    (`MediaItemStatusId`);

-- Creating foreign key on `MediaItemId` in table 'TagSet'

ALTER TABLE `TagSet`
ADD CONSTRAINT `FK_TagMediaItem`
    FOREIGN KEY (`MediaItemId`)
    REFERENCES `MediaItemSet`
        (`MediaItemId`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TagMediaItem'

CREATE INDEX `IX_FK_TagMediaItem` 
    ON `TagSet`
    (`MediaItemId`);

-- Creating foreign key on `MediaItemId` in table 'MediaItemMediaTagSet'

ALTER TABLE `MediaItemMediaTagSet`
ADD CONSTRAINT `FK_MediaItemMediaTag_MediaItem`
    FOREIGN KEY (`MediaItemId`)
    REFERENCES `MediaItemSet`
        (`MediaItemId`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MediaItemMediaTag_MediaItem'

CREATE INDEX `IX_FK_MediaItemMediaTag_MediaItem` 
    ON `MediaItemMediaTagSet`
    (`MediaItemId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
