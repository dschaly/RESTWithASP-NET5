ALTER TABLE `person`
	ADD COLUMN `Enabled` BIT(1) NOT NULL DEFAULT b'1' AFTER `Gender`;