namespace OmahaMtg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBannerAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BannerAdds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RotationStart = c.DateTime(nullable: false),
                        RotationEnd = c.DateTime(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        FileName = c.String(nullable: false, maxLength: 100),
                        AddUrl = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BannerAdds");
        }
    }
}
