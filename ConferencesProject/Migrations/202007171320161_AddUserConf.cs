namespace ConferencesProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserConf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserConfs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        IdInAspNetUsers = c.String(),
                        Conference_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conferences", t => t.Conference_Id)
                .Index(t => t.Conference_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserConfs", "Conference_Id", "dbo.Conferences");
            DropIndex("dbo.UserConfs", new[] { "Conference_Id" });
            DropTable("dbo.UserConfs");
        }
    }
}
