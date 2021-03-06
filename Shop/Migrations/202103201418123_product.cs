namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    /// <summary>
    /// Миграция
    /// </summary>
    public partial class product : DbMigration
    {
        /// <summary>
        /// Создание
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                        Code = c.String(maxLength: 15),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        /// <summary>
        /// Удаление
        /// </summary>
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
