namespace HeroVerseMatrix.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class CrearHeroVerse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CasaMatrices",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Nombre = c.String(nullable: false, maxLength: 80),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Personajes",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Nombre = c.String(nullable: false, maxLength: 100),
                    Tipo = c.Int(nullable: false),
                    CasaMatrixId = c.Guid(nullable: false),
                    AlterEgo_Nombre = c.String(maxLength: 50),
                    AlterEgo_Apellido = c.String(maxLength: 50),
                    Imagen = c.Binary(),
                    ImagenContentType = c.String(maxLength: 100),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CasaMatrices", t => t.CasaMatrixId)
                .Index(t => t.CasaMatrixId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Personajes", "CasaMatrixId", "dbo.CasaMatrices");
            DropIndex("dbo.Personajes", new[] { "CasaMatrixId" });
            DropTable("dbo.Personajes");
            DropTable("dbo.CasaMatrices");
        }
    }
}

