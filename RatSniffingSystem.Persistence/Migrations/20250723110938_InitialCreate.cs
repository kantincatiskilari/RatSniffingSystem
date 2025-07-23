using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RatSniffingSystem.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Odors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    OdorCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsHazardous = table.Column<bool>(type: "bit", nullable: false),
                    ExternalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ProjectTag = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Breed = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RatWeights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WeightInGrams = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatWeights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatWeights_Rats_RatId",
                        column: x => x.RatId,
                        principalTable: "Rats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    CageCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaterialType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaterialThawedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Rats_RatId",
                        column: x => x.RatId,
                        principalTable: "Rats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sessions_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BehaviorLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BehaviorType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeObserved = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BehaviorLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BehaviorLogs_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperimentalNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperimentalNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperimentalNotes_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodIntakeLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountInCc = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    TimeGiven = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodIntakeLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodIntakeLogs_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interventions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Substance = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Dose = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AppliedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InterventionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interventions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interventions_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewOdor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SessionToSuccess = table.Column<int>(type: "int", nullable: false),
                    WasSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferTests_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrialNumber = table.Column<int>(type: "int", nullable: false),
                    TargetOdor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstResponseTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstCorrectTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCorrectPositive = table.Column<bool>(type: "bit", nullable: false),
                    IsFalsePositive = table.Column<bool>(type: "bit", nullable: false),
                    IsCorrectNegative = table.Column<bool>(type: "bit", nullable: false),
                    IsFalseNegative = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trials_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrialOdors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OdorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsFalsePositive = table.Column<bool>(type: "bit", nullable: false),
                    OdorType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PositionIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrialOdors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrialOdors_Odors_OdorId",
                        column: x => x.OdorId,
                        principalTable: "Odors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrialOdors_Trials_TrialId",
                        column: x => x.TrialId,
                        principalTable: "Trials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BehaviorLogs_SessionId",
                table: "BehaviorLogs",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentalNotes_SessionId",
                table: "ExperimentalNotes",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodIntakeLogs_SessionId",
                table: "FoodIntakeLogs",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Interventions_SessionId",
                table: "Interventions",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_RatWeights_RatId",
                table: "RatWeights",
                column: "RatId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_RatId",
                table: "Sessions",
                column: "RatId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_TrainerId",
                table: "Sessions",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferTests_SessionId",
                table: "TransferTests",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrialOdors_OdorId",
                table: "TrialOdors",
                column: "OdorId");

            migrationBuilder.CreateIndex(
                name: "IX_TrialOdors_TrialId",
                table: "TrialOdors",
                column: "TrialId");

            migrationBuilder.CreateIndex(
                name: "IX_Trials_SessionId",
                table: "Trials",
                column: "SessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BehaviorLogs");

            migrationBuilder.DropTable(
                name: "ExperimentalNotes");

            migrationBuilder.DropTable(
                name: "FoodIntakeLogs");

            migrationBuilder.DropTable(
                name: "Interventions");

            migrationBuilder.DropTable(
                name: "RatWeights");

            migrationBuilder.DropTable(
                name: "TransferTests");

            migrationBuilder.DropTable(
                name: "TrialOdors");

            migrationBuilder.DropTable(
                name: "Odors");

            migrationBuilder.DropTable(
                name: "Trials");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Rats");

            migrationBuilder.DropTable(
                name: "Trainers");
        }
    }
}
