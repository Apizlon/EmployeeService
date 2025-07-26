SELECT "Id","Name","Surname","Phone","CompanyId","PassportId","DepartmentId"
FROM "Employees"
WHERE "CompanyId" = @CompanyId;