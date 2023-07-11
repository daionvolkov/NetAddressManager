using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetAddressManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentManufacturer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentManufacturer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostalAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Building = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoreSwitch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IPGateway = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPMask = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MACAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ManufacturerId = table.Column<int>(type: "int", nullable: true),
                    EquipmentManufacturerId = table.Column<int>(type: "int", nullable: true),
                    PostalAddressId1 = table.Column<int>(type: "int", nullable: true),
                    SwitchPortId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoreSwitch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoreSwitch_EquipmentManufacturer_EquipmentManufacturerId",
                        column: x => x.EquipmentManufacturerId,
                        principalTable: "EquipmentManufacturer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CoreSwitch_PostalAddress_PostalAddressId1",
                        column: x => x.PostalAddressId1,
                        principalTable: "PostalAddress",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AggregationSwitch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoreSwitchId = table.Column<int>(type: "int", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPMask = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MACAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ManufacturerId = table.Column<int>(type: "int", nullable: true),
                    EquipmentManufacturerId = table.Column<int>(type: "int", nullable: true),
                    PostalAddressId1 = table.Column<int>(type: "int", nullable: true),
                    SwitchPortId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AggregationSwitch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AggregationSwitch_CoreSwitch_CoreSwitchId",
                        column: x => x.CoreSwitchId,
                        principalTable: "CoreSwitch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AggregationSwitch_EquipmentManufacturer_EquipmentManufacturerId",
                        column: x => x.EquipmentManufacturerId,
                        principalTable: "EquipmentManufacturer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AggregationSwitch_PostalAddress_PostalAddressId1",
                        column: x => x.PostalAddressId1,
                        principalTable: "PostalAddress",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccessSwitch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AggregationSwitchId = table.Column<int>(type: "int", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPMask = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MACAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ManufacturerId = table.Column<int>(type: "int", nullable: true),
                    EquipmentManufacturerId = table.Column<int>(type: "int", nullable: true),
                    PostalAddressId1 = table.Column<int>(type: "int", nullable: true),
                    SwitchPortId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessSwitch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessSwitch_AggregationSwitch_AggregationSwitchId",
                        column: x => x.AggregationSwitchId,
                        principalTable: "AggregationSwitch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccessSwitch_EquipmentManufacturer_EquipmentManufacturerId",
                        column: x => x.EquipmentManufacturerId,
                        principalTable: "EquipmentManufacturer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccessSwitch_PostalAddress_PostalAddressId1",
                        column: x => x.PostalAddressId1,
                        principalTable: "PostalAddress",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SwitchPort",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortNumber = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    AccessSwitchId = table.Column<int>(type: "int", nullable: true),
                    AggregationSwitchId = table.Column<int>(type: "int", nullable: true),
                    CoreSwitchId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SwitchPort", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SwitchPort_AccessSwitch_AccessSwitchId",
                        column: x => x.AccessSwitchId,
                        principalTable: "AccessSwitch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SwitchPort_AggregationSwitch_AggregationSwitchId",
                        column: x => x.AggregationSwitchId,
                        principalTable: "AggregationSwitch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SwitchPort_CoreSwitch_CoreSwitchId",
                        column: x => x.CoreSwitchId,
                        principalTable: "CoreSwitch",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessSwitch_AggregationSwitchId",
                table: "AccessSwitch",
                column: "AggregationSwitchId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessSwitch_EquipmentManufacturerId",
                table: "AccessSwitch",
                column: "EquipmentManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessSwitch_PostalAddressId1",
                table: "AccessSwitch",
                column: "PostalAddressId1");

            migrationBuilder.CreateIndex(
                name: "IX_AggregationSwitch_CoreSwitchId",
                table: "AggregationSwitch",
                column: "CoreSwitchId");

            migrationBuilder.CreateIndex(
                name: "IX_AggregationSwitch_EquipmentManufacturerId",
                table: "AggregationSwitch",
                column: "EquipmentManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_AggregationSwitch_PostalAddressId1",
                table: "AggregationSwitch",
                column: "PostalAddressId1");

            migrationBuilder.CreateIndex(
                name: "IX_CoreSwitch_EquipmentManufacturerId",
                table: "CoreSwitch",
                column: "EquipmentManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_CoreSwitch_PostalAddressId1",
                table: "CoreSwitch",
                column: "PostalAddressId1");

            migrationBuilder.CreateIndex(
                name: "IX_SwitchPort_AccessSwitchId",
                table: "SwitchPort",
                column: "AccessSwitchId");

            migrationBuilder.CreateIndex(
                name: "IX_SwitchPort_AggregationSwitchId",
                table: "SwitchPort",
                column: "AggregationSwitchId");

            migrationBuilder.CreateIndex(
                name: "IX_SwitchPort_CoreSwitchId",
                table: "SwitchPort",
                column: "CoreSwitchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SwitchPort");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "AccessSwitch");

            migrationBuilder.DropTable(
                name: "AggregationSwitch");

            migrationBuilder.DropTable(
                name: "CoreSwitch");

            migrationBuilder.DropTable(
                name: "EquipmentManufacturer");

            migrationBuilder.DropTable(
                name: "PostalAddress");
        }
    }
}
