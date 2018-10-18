namespace ManageDos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TodoList : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ToDoItems", "List_ID", "dbo.ToDoLists");
            AddColumn("dbo.ToDoItems", "ToDoList_ID", c => c.Int());
            AddColumn("dbo.ToDoLists", "Item_ID", c => c.Int());
            CreateIndex("dbo.ToDoItems", "ToDoList_ID");
            CreateIndex("dbo.ToDoLists", "Item_ID");
            AddForeignKey("dbo.ToDoLists", "Item_ID", "dbo.ToDoItems", "ID");
            AddForeignKey("dbo.ToDoItems", "ToDoList_ID", "dbo.ToDoLists", "ID");
            DropColumn("dbo.ToDoLists", "visible");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ToDoLists", "visible", c => c.Int(nullable: false));
            DropForeignKey("dbo.ToDoItems", "ToDoList_ID", "dbo.ToDoLists");
            DropForeignKey("dbo.ToDoLists", "Item_ID", "dbo.ToDoItems");
            DropIndex("dbo.ToDoLists", new[] { "Item_ID" });
            DropIndex("dbo.ToDoItems", new[] { "ToDoList_ID" });
            DropColumn("dbo.ToDoLists", "Item_ID");
            DropColumn("dbo.ToDoItems", "ToDoList_ID");
            AddForeignKey("dbo.ToDoItems", "List_ID", "dbo.ToDoLists", "ID");
        }
    }
}
