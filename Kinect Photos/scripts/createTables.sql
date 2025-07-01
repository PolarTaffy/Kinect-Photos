-- USER/USER PROP MANAGEMENT --
CREATE TABLE IF NOT EXISTS users (
	userID INTEGER PRIMARY KEY AUTOINCREMENT,
	userName varchar(25) NOT NULL, -- varchar --> string max length 25
	pfpPath varchar(200) DEFAULT 'res/user.png' --the default doesn't seem to be working here
);

CREATE TABLE IF NOT EXISTS folderPaths (
	folderPath varchar(200) NOT NULL,
	userID int NOT NULL -- manages who owns the folder; a userID of -1 means that it's a universal folder.
);

--PHOTO/ALBUM MANAGEMENT -- 
CREATE TABLE IF NOT EXISTS photos ( --a collection of all the photo items on there
	photoID INTEGER PRIMARY KEY AUTOINCREMENT,
	filePath varchar(200) NOT NULL,
	metadata JSONB,
	photoDate DATETIME,
	uploaderID INTEGER
);

CREATE TABLE IF NOT EXISTS albums (
	albumID INTEGER PRIMARY KEY AUTOINCREMENT,
	albumName varchar(50) NOT NULL,
	userID int
	creationTime DATETIME DEFAULT CURRENT_TIMESTAMP,
	UNIQUE (userID, albumName)
);

CREATE TABLE IF NOT EXISTS albumPhotos ( -- stores which photos are in what album
	linkID INTEGER PRIMARY KEY AUTOINCREMENT, -- store this just in case?
	albumID INTEGER,
	photoID INTEGER,
	UNIQUE (albumID, photoID)
);