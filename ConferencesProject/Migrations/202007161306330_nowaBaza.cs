namespace ConferencesProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nowaBaza : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EventDate = c.DateTime(nullable: false),
                        Location_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .Index(t => t.Location_Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        City = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Talks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Abstract = c.String(),
                        Conference_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conferences", t => t.Conference_Id)
                .Index(t => t.Conference_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Talks", "Conference_Id", "dbo.Conferences");
            DropForeignKey("dbo.Conferences", "Location_Id", "dbo.Locations");
            DropIndex("dbo.Talks", new[] { "Conference_Id" });
            DropIndex("dbo.Conferences", new[] { "Location_Id" });
            DropTable("dbo.Talks");
            DropTable("dbo.Locations");
            DropTable("dbo.Conferences");
        }
    }
}
