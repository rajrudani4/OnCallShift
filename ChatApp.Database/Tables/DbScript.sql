-- add status
IF NOT EXISTS(SELECT * FROM Status)
BEGIN
INSERT INTO Status (Content) values ('available'), ('dnd'), ('away'), ('busy'), ('rightback'), ('offline');
END

