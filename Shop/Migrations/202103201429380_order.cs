namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    /// <summary>
    /// Миграция
    /// </summary>
    public partial class order : DbMigration
    {
        /// <summary>
        /// Создание
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserPhone = c.String(maxLength: 12),
                        UserComment = c.String(maxLength: 128),
                        Date = c.String(maxLength: 20),
                        UserId = c.String(maxLength: 128),
                        OrderedProducts = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        /// <summary>
        /// Удаление
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropTable("dbo.Orders");
        }
    }
}
