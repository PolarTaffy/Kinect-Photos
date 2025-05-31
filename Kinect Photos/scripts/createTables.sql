CREATE TABLE IF NOT EXISTS users (
	userID int UNIQUE NOT NULL,
	userName varchar(25) NOT NULL, -- varchar --> string max length 25
	pfpPath varchar(200) DEFAULT 'res/user.png' --the default doesn't seem to be working here
);

CREATE TABLE IF NOT EXISTS folderPaths (
	folderPath varchar(200) NOT NULL,
	userID int NOT NULL -- manages who owns the folder
);