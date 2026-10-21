namespace HeroVerseMatrix.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizarCasaMatrix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CasaMatrices", "Descripcion", c => c.String());
            AlterColumn("dbo.CasaMatrices", "Nombre", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CasaMatrices", "Nombre", c => c.String(nullable: false, maxLength: 80));
            DropColumn("dbo.CasaMatrices", "Descripcion");
        }
    }
}
