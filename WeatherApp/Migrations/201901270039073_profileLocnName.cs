namespace WeatherApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class profileLocnName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profiles", "LocationName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profiles", "LocationName");
        }
    }
}
