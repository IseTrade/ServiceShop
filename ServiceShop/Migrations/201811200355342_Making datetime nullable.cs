namespace ServiceShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Makingdatetimenullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Services", "WorkOrderDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Services", "WorkOrderDate", c => c.DateTime(nullable: false));
        }
    }
}
