namespace ConferencesProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class errorlogs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conferences", "Tets", c => c.String());
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ErrorLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        ControllerName = c.String(),
                        TargetedResult = c.String(),
                        SessionId = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Conferences", "Tets");
        }
    }
}
