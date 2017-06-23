namespace PAUP_FS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rads", "Path", c => c.String(maxLength: 160));
            AlterColumn("dbo.Rads", "Korisnik", c => c.String(maxLength: 160));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rads", "Korisnik", c => c.String(nullable: false, maxLength: 160));
            AlterColumn("dbo.Rads", "Path", c => c.String(nullable: false, maxLength: 160));
        }
    }
}
