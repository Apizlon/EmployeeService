UPDATE "Departments"
SET
    "CompanyId" = CASE WHEN @CompanyId = 0 then "CompanyId" ELSE @CompanyId END,
    "Name" = CASE WHEN @Name is NULL then "Name" ELSE @Name END,
    "Phone" = CASE WHEN @Phone is NULL then "Phone" ELSE @Phone END
WHERE "Id" = @Id;