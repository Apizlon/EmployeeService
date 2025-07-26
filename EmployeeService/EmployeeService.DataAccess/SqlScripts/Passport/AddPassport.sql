INSERT INTO "Passports" ("Type","Number")
VALUES (@Type,@Number)
RETURNING "Id";