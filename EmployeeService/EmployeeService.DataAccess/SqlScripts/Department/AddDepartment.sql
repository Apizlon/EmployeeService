INSERT INTO "Departments" ("CompanyId","Name","Phone")
VALUES (@CompanyId,@Name,@Phone)
RETURNING "Id";