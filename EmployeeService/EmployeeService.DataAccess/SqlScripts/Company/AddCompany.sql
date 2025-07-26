INSERT INTO "Companies" ("Name")
VALUES (@Name)
RETURNING "Id";