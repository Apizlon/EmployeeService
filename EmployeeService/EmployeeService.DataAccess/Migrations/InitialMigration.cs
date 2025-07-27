using FluentMigrator;

namespace EmployeeService.DataAccess.Migrations;

/// <summary>
/// Создание таблиц с сотрудниками, компаниями, отделами, паспортами.
/// </summary>
[Migration(202507261630)]
public class InitialMigration : Migration
{
    /// <inheritdoc/>
    public override void Up()
    {
        Create.Table("Companies")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(255).NotNullable();

        Create.Table("Departments")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("CompanyId").AsInt32().NotNullable()
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Phone").AsString(50).NotNullable();

        Create.Table("Passports")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Type").AsString(50).NotNullable()
            .WithColumn("Number").AsString(50).NotNullable();

        Create.Table("Employees")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Surname").AsString(255).NotNullable()
            .WithColumn("Phone").AsString(50).NotNullable()
            .WithColumn("CompanyId").AsInt32().NotNullable()
            .WithColumn("PassportId").AsInt32().NotNullable()
            .WithColumn("DepartmentId").AsInt32().NotNullable();

        Create.ForeignKey("FK_Departments_Companies_CompanyId")
            .FromTable("Departments").ForeignColumn("CompanyId")
            .ToTable("Companies").PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.Cascade);

        Create.ForeignKey("FK_Employees_Companies_CompanyId")
            .FromTable("Employees").ForeignColumn("CompanyId")
            .ToTable("Companies").PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.Cascade);

        Create.ForeignKey("FK_Employees_Passports_PassportId")
            .FromTable("Employees").ForeignColumn("PassportId")
            .ToTable("Passports").PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.Cascade);

        Create.ForeignKey("FK_Employees_Departments_DepartmentId")
            .FromTable("Employees").ForeignColumn("DepartmentId")
            .ToTable("Departments").PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.Cascade);
    }

    /// <inheritdoc/>
    public override void Down()
    {
        Delete.Table("Employees");
        Delete.Table("Passports");
        Delete.Table("Departments");
        Delete.Table("Companies");
    }
}