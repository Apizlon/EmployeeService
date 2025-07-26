UPDATE "Employees"
SET
    "Name" = CASE WHEN @Name is NULL then "Name" ELSE @Name END,
    "Surname" = CASE WHEN @Surname is NULL then "Surname" ELSE @Surname END,
    "Phone" = CASE WHEN @Phone is NULL then "Phone" ELSE @Phone END,
    "CompanyId" = CASE WHEN @CompanyId = 0 then "CompanyId" ELSE @CompanyId END,
    "DepartmentId" = CASE WHEN @DepartmentId = 0 then "CompanyId" ELSE @DepartmentId END,
    "PassportId" = CASE WHEN @PassportId = 0 then "PassportId" ELSE @PassportId END
WHERE "Id" = @Id;