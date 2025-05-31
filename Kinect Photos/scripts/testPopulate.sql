INSERT INTO Users (userID, userName) VALUES ('dummyID', 'Jessica') ON CONFLICT(userID) DO NOTHING;
INSERT INTO Users (userID, userName) VALUES ('dummyID2', 'Maylei') ON CONFLICT(userID) DO NOTHING;