namespace ConferencesProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserConfUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserConfs", "Conference_Id", "dbo.Conferences");
            DropIndex("dbo.UserConfs", new[] { "Conference_Id" });
            CreateTable(
                "dbo.UserConfConferences",
                c => new
                    {
                        UserConf_Id = c.Int(nullable: false),
                        Conference_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserConf_Id, t.Conference_Id })
                .ForeignKey("dbo.UserConfs", t => t.UserConf_Id, cascadeDelete: true)
                .ForeignKey("dbo.Conferences", t => t.Conference_Id, cascadeDelete: true)
                .Index(t => t.UserConf_Id)
                .Index(t => t.Conference_Id);
            
            DropColumn("dbo.UserConfs", "Conference_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserConfs", "Conference_Id", c => c.Int());
            DropForeignKey("dbo.UserConfConferences", "Conference_Id", "dbo.Conferences");
            DropForeignKey("dbo.UserConfConferences", "UserConf_Id", "dbo.UserConfs");
            DropIndex("dbo.UserConfConferences", new[] { "Conference_Id" });
            DropIndex("dbo.UserConfConferences", new[] { "UserConf_Id" });
            DropTable("dbo.UserConfConferences");
            CreateIndex("dbo.UserConfs", "Conference_Id");
            AddForeignKey("dbo.UserConfs", "Conference_Id", "dbo.Conferences", "Id");
        }
    }
}
