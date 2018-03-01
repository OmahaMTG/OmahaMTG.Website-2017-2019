namespace OmahaMtg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLastLoginAndCreateDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LastLogin", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "CreateDate", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CreateDate");
            DropColumn("dbo.AspNetUsers", "LastLogin");
        }
    }
}
