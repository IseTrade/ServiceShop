namespace ServiceShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRateTemptoemployeemodeltoholdtemporaryRatevaluebeforeconcattoRatingData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "RateTemp", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "RateTemp");
        }
    }
}
