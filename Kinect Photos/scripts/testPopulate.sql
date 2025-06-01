INSERT INTO Users (userName) VALUES ('Jessica') ON CONFLICT(userId) DO NOTHING;
INSERT INTO Users (userName) VALUES ('Maylei') ON CONFLICT(userId) DO NOTHING;