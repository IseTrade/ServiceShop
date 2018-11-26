namespace ServiceShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRatingDatatoemployeemodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "RatingData", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "RatingData");
        }
    }
}
