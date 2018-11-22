namespace Goggles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemdescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "description", c => c.String(maxLength: 1024));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "description");
        }
    }
}
