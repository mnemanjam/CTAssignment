namespace CT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CRMS", c => c.String());
            DropColumn("dbo.AspNetUsers", "CRM");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "CRM", c => c.String());
            DropColumn("dbo.AspNetUsers", "CRMS");
        }
    }
}
