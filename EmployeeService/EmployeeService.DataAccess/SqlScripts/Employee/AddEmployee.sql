INSERT INTO "Employees" ("Name","Surname","Phone","CompanyId","PassportId","DepartmentId")
VALUES (@Name,@Surname,@Phone,@CompanyId,@PassportId,@DepartmentId)
RETURNING "Id";