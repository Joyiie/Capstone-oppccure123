using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capstonep2.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MiddleName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Abbreviation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Key = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Symptom = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PurposeOfVisit = table.Column<int>(type: "int", nullable: false),
                    PatientID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PatientID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MiddleName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RoleID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConsultationRecords",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PatientID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    AppointmentID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultationRecords", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConsultationRecords_Appointments_AppointmentID",
                        column: x => x.AppointmentID,
                        principalTable: "Appointments",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ConsultationRecords_Patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    RoleID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Findings",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Tags = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConsultationRecordID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Findings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Findings_ConsultationRecords_ConsultationRecordID",
                        column: x => x.ConsultationRecordID,
                        principalTable: "ConsultationRecords",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Tags = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConsultationRecordID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Prescriptions_ConsultationRecords_ConsultationRecordID",
                        column: x => x.ConsultationRecordID,
                        principalTable: "ConsultationRecords",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "ID", "Address", "BirthDate", "FirstName", "Gender", "LastName", "MiddleName" },
                values: new object[,]
                {
                    { new Guid("2b792220-f333-49ec-abe2-3a6fc4edb0c2"), "Luakan,Dinalupihan, Bataan", new DateTime(2023, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Luisa Katrina", 0, "Reyes", "Pangilinan" },
                    { new Guid("5a7e7bc3-8816-41df-b44d-eeb60ae99b5b"), "Luakan,Dinalupihan, Bataan", new DateTime(2023, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Clarissa Joy", 0, "Flores", "Gozon" },
                    { new Guid("8664a4bd-0ec6-4aaa-83e6-7d2bd0315b5a"), "Bacong,Hermosa, Bataan", new DateTime(2023, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Raniel", 1, "David", "Mallari" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "ID", "Abbreviation", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("2afa881f-e519-4e67-a841-e4a2630e8a2a"), "Pt", "One who receives medical treatment", "patient" },
                    { new Guid("54f00f70-72b8-4d34-a953-83321ff6b101"), "Adm", "One who manages the system", "admin" }
                });

            migrationBuilder.InsertData(
                table: "UserLogins",
                columns: new[] { "ID", "Key", "Type", "UserID", "Value" },
                values: new object[,]
                {
                    { new Guid("0e78547c-271e-40c3-9f14-405d43809ef1"), "Password", "General", new Guid("0352c124-f290-4f99-b1a5-e235cafcd836"), "$2a$11$v1LumSNEu6f5ERlPtxmRvuSKH.1/c37.j.3GkKoYiGrKVjOkcZL9q" },
                    { new Guid("126a7056-d4a8-4000-a83d-229429fb11f1"), "Password", "General", new Guid("7e5e4f74-9902-43da-9974-4b2a08814398"), "$2a$11$2G0dkr0duHWPexQGHfjJy.T3UBup1An1bkQe/5fCmGQQlmdox2hy2" },
                    { new Guid("1b6dda12-4bc5-442c-9216-c8d35af454c0"), "LoginRetries", "General", new Guid("7e5e4f74-9902-43da-9974-4b2a08814398"), "0" },
                    { new Guid("2b22c96f-0ad8-4db8-8616-70b43539b46a"), "IsActive", "General", new Guid("00acfb7f-6c90-459a-b53f-bf73ddac85b4"), "true" },
                    { new Guid("4d98783e-7cc4-4740-a5d9-90ded2993007"), "LoginRetries", "General", new Guid("00acfb7f-6c90-459a-b53f-bf73ddac85b4"), "0" },
                    { new Guid("73ac8929-b02c-4ae3-a66b-d40fa5b15342"), "Password", "General", new Guid("1bd5f519-b891-4491-9a7c-a86cd0c2a15e"), "$2a$11$OOXiZp8/hY/DA0yEHOyRx.UwNiapq1gOd8.7x9WApnSiKAIR8HsHS" },
                    { new Guid("971e29d7-647d-4f5c-8175-e6e8d4afb98e"), "Password", "General", new Guid("00acfb7f-6c90-459a-b53f-bf73ddac85b4"), "$2a$11$5Da.9Ti8xzbgstxSmlD5OevwBsMA0okGIONUeip/KzCD5dDmVYLU2" },
                    { new Guid("99263b5e-d451-4cff-96ab-5a8fd3d65d5e"), "Password", "General", new Guid("d7dbd16f-1c71-4415-a147-22a2b428bf95"), "$2a$11$OWi3rsNJgsSqH1fAg7Y4Vu4RLlHnezaCsVGbSaFefEAj1wE4/01Bq" },
                    { new Guid("a7e58274-f631-483c-b230-b1710a465c52"), "LoginRetries", "General", new Guid("0352c124-f290-4f99-b1a5-e235cafcd836"), "0" },
                    { new Guid("c9d9f3a1-2414-403d-9da4-1997bea84b06"), "IsActive", "General", new Guid("0352c124-f290-4f99-b1a5-e235cafcd836"), "true" },
                    { new Guid("cb443dc2-1af1-45c9-ac33-835e1b25588d"), "IsActive", "General", new Guid("d7dbd16f-1c71-4415-a147-22a2b428bf95"), "true" },
                    { new Guid("d447bf09-db77-424b-b86c-8066991e98b5"), "LoginRetries", "General", new Guid("d7dbd16f-1c71-4415-a147-22a2b428bf95"), "0" },
                    { new Guid("daf12fa2-f3be-413c-9f68-c3e6db449bc1"), "IsActive", "General", new Guid("7e5e4f74-9902-43da-9974-4b2a08814398"), "true" },
                    { new Guid("df57a9e8-adc2-47c2-943f-2f5cd82a6a77"), "IsActive", "General", new Guid("1bd5f519-b891-4491-9a7c-a86cd0c2a15e"), "true" },
                    { new Guid("eab55f58-c210-4212-aa70-f9e3b34cb5fb"), "LoginRetries", "General", new Guid("1bd5f519-b891-4491-9a7c-a86cd0c2a15e"), "0" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "ID", "EndTime", "PatientID", "PurposeOfVisit", "StartTime", "Status", "Symptom" },
                values: new object[,]
                {
                    { new Guid("20f20659-d4e3-466e-b2df-e6a6b1f62fab"), new DateTime(2022, 2, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8664a4bd-0ec6-4aaa-83e6-7d2bd0315b5a"), 0, new DateTime(2022, 2, 12, 11, 30, 0, 0, DateTimeKind.Unspecified), 4, "Red Eyes" },
                    { new Guid("2222ed0f-aaea-45f3-8a72-f0ee3ed23a22"), new DateTime(2022, 2, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8664a4bd-0ec6-4aaa-83e6-7d2bd0315b5a"), 0, new DateTime(2022, 2, 12, 11, 30, 0, 0, DateTimeKind.Unspecified), 3, "Swelling" },
                    { new Guid("3ce371f9-dc79-4623-b84f-0b2fe7c99962"), new DateTime(2023, 2, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5a7e7bc3-8816-41df-b44d-eeb60ae99b5b"), 1, new DateTime(2023, 2, 12, 11, 30, 0, 0, DateTimeKind.Unspecified), 4, "Light Sensitivity" },
                    { new Guid("7297d64f-7912-4e46-a663-e543af0102fb"), new DateTime(2023, 2, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5a7e7bc3-8816-41df-b44d-eeb60ae99b5b"), 1, new DateTime(2023, 2, 12, 11, 30, 0, 0, DateTimeKind.Unspecified), 1, "Light Sensitivity" },
                    { new Guid("861c579e-bb80-4dea-b8f2-4b189cb6a362"), new DateTime(2022, 2, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8664a4bd-0ec6-4aaa-83e6-7d2bd0315b5a"), 0, new DateTime(2022, 2, 12, 11, 30, 0, 0, DateTimeKind.Unspecified), 1, "Headache" },
                    { new Guid("a0d287bc-73e8-41b6-88f1-e7385ea7da7d"), new DateTime(2023, 2, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5a7e7bc3-8816-41df-b44d-eeb60ae99b5b"), 1, new DateTime(2023, 2, 12, 11, 30, 0, 0, DateTimeKind.Unspecified), 0, "Light Sensitivity" },
                    { new Guid("c7d431a6-579b-4841-8629-2bbcb79a5e15"), new DateTime(2023, 2, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8664a4bd-0ec6-4aaa-83e6-7d2bd0315b5a"), 2, new DateTime(2023, 2, 12, 11, 30, 0, 0, DateTimeKind.Unspecified), 0, "Light Sensitivity" },
                    { new Guid("e822435f-5110-465f-a276-c89ee9a5dc54"), new DateTime(2023, 2, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5a7e7bc3-8816-41df-b44d-eeb60ae99b5b"), 1, new DateTime(2023, 2, 12, 11, 30, 0, 0, DateTimeKind.Unspecified), 3, "Light Sensitivity" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Address", "BirthDate", "Email", "FirstName", "Gender", "LastName", "MiddleName", "PatientID", "RoleID" },
                values: new object[,]
                {
                    { new Guid("00acfb7f-6c90-459a-b53f-bf73ddac85b4"), "Dinalupihan, Orani , Bataan", new DateTime(2002, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Janedavid@yahoo.com", "Jane", 0, "David", "Adan", null, new Guid("54f00f70-72b8-4d34-a953-83321ff6b101") },
                    { new Guid("0352c124-f290-4f99-b1a5-e235cafcd836"), "Dinalupihan, Orani, Bataan", new DateTime(2001, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "luisa@yahoo.com", "Luisa Katrina", 0, "Pangilinan", "Reyes", new Guid("2b792220-f333-49ec-abe2-3a6fc4edb0c2"), new Guid("2afa881f-e519-4e67-a841-e4a2630e8a2a") },
                    { new Guid("1bd5f519-b891-4491-9a7c-a86cd0c2a15e"), "Dinalupihan, Orani , Bataan", new DateTime(2002, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@yahoo.com", "admin", 0, "admin", "admin", null, new Guid("54f00f70-72b8-4d34-a953-83321ff6b101") },
                    { new Guid("7e5e4f74-9902-43da-9974-4b2a08814398"), "Dinalupihan, Orani, Bataan", new DateTime(2001, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "renieldavid@yahoo.com", "Reniel", 1, "Mallari", "David", new Guid("8664a4bd-0ec6-4aaa-83e6-7d2bd0315b5a"), new Guid("2afa881f-e519-4e67-a841-e4a2630e8a2a") },
                    { new Guid("d7dbd16f-1c71-4415-a147-22a2b428bf95"), "Dinalupihan, Orani, Bataan", new DateTime(2001, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "joy@yahoo.com", "Clarissa Joy", 1, "Gozon", "Flores", new Guid("5a7e7bc3-8816-41df-b44d-eeb60ae99b5b"), new Guid("2afa881f-e519-4e67-a841-e4a2630e8a2a") }
                });

            migrationBuilder.InsertData(
                table: "ConsultationRecords",
                columns: new[] { "ID", "AppointmentID", "PatientID" },
                values: new object[,]
                {
                    { new Guid("0c096359-c9ef-4f37-9c37-47b7bf247746"), new Guid("c7d431a6-579b-4841-8629-2bbcb79a5e15"), new Guid("8664a4bd-0ec6-4aaa-83e6-7d2bd0315b5a") },
                    { new Guid("73ec06ac-56aa-453a-a29d-447691d51cd9"), new Guid("20f20659-d4e3-466e-b2df-e6a6b1f62fab"), new Guid("8664a4bd-0ec6-4aaa-83e6-7d2bd0315b5a") },
                    { new Guid("a11b823f-8eff-4d89-abdc-8efa8f28291c"), new Guid("3ce371f9-dc79-4623-b84f-0b2fe7c99962"), new Guid("5a7e7bc3-8816-41df-b44d-eeb60ae99b5b") }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "RoleID", "UserID" },
                values: new object[,]
                {
                    { new Guid("1cf66b07-5c66-49f7-a6c5-31efb6722a8d"), new Guid("2afa881f-e519-4e67-a841-e4a2630e8a2a"), new Guid("7e5e4f74-9902-43da-9974-4b2a08814398") },
                    { new Guid("4010084c-fc04-45ea-aabe-f0eca2df5415"), new Guid("54f00f70-72b8-4d34-a953-83321ff6b101"), new Guid("1bd5f519-b891-4491-9a7c-a86cd0c2a15e") },
                    { new Guid("5b50acd4-e03c-400e-b454-2dee03beddaf"), new Guid("2afa881f-e519-4e67-a841-e4a2630e8a2a"), new Guid("0352c124-f290-4f99-b1a5-e235cafcd836") },
                    { new Guid("868052ff-25a8-4a01-b9e6-2cf098f6be68"), new Guid("2afa881f-e519-4e67-a841-e4a2630e8a2a"), new Guid("d7dbd16f-1c71-4415-a147-22a2b428bf95") },
                    { new Guid("cacbe4f7-35d0-4ea3-8572-488a17963f21"), new Guid("54f00f70-72b8-4d34-a953-83321ff6b101"), new Guid("00acfb7f-6c90-459a-b53f-bf73ddac85b4") }
                });

            migrationBuilder.InsertData(
                table: "Findings",
                columns: new[] { "ID", "ConsultationRecordID", "Description", "Tags" },
                values: new object[,]
                {
                    { new Guid("236eab6b-d279-4f85-827e-01b007225139"), new Guid("73ec06ac-56aa-453a-a29d-447691d51cd9"), "findings test 1", "testtable123" },
                    { new Guid("26277f24-a7dc-4275-b25b-7cf24143ffd1"), new Guid("0c096359-c9ef-4f37-9c37-47b7bf247746"), "sore eyes", "123" },
                    { new Guid("5320c688-933d-4089-a208-f3caf956ea9f"), new Guid("a11b823f-8eff-4d89-abdc-8efa8f28291c"), "test2", "test2" }
                });

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "ID", "ConsultationRecordID", "Description", "Tags" },
                values: new object[,]
                {
                    { new Guid("71b69472-6846-41e3-9f40-82a4c5072ce0"), new Guid("73ec06ac-56aa-453a-a29d-447691d51cd9"), "prescriptions test 2", "tabletest123" },
                    { new Guid("911303f9-8184-428a-962b-aa7e129b4d64"), new Guid("a11b823f-8eff-4d89-abdc-8efa8f28291c"), " test 2", "test2" },
                    { new Guid("fcfd65e4-9a02-4df2-99df-cc5db15169ae"), new Guid("0c096359-c9ef-4f37-9c37-47b7bf247746"), "biogesic", "123" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientID",
                table: "Appointments",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationRecords_AppointmentID",
                table: "ConsultationRecords",
                column: "AppointmentID");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationRecords_PatientID",
                table: "ConsultationRecords",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Findings_ConsultationRecordID",
                table: "Findings",
                column: "ConsultationRecordID");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_ConsultationRecordID",
                table: "Prescriptions",
                column: "ConsultationRecordID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleID",
                table: "UserRoles",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserID",
                table: "UserRoles",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PatientID",
                table: "Users",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Findings");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "ConsultationRecords");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
