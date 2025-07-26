UPDATE "Passports"
SET
    "Type" = CASE WHEN @Type is NULL then "Type" ELSE @Type END,
    "Number" = CASE WHEN @Number is NULL then "Number" ELSE @Number END
WHERE "Id" = @Id;