namespace GAT_TaskkResolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false, maxLength: 450),
                        FirstName = c.String(nullable: false),
                        MidName = c.String(),
                        LastName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Number, unique: true);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(nullable: false, maxLength: 450),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.StudentSubjects",
                c => new
                    {
                        Student_ID = c.Int(nullable: false),
                        Subject_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_ID, t.Subject_ID })
                .ForeignKey("dbo.Students", t => t.Student_ID, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.Subject_ID, cascadeDelete: true)
                .Index(t => t.Student_ID)
                .Index(t => t.Subject_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentSubjects", "Subject_ID", "dbo.Subjects");
            DropForeignKey("dbo.StudentSubjects", "Student_ID", "dbo.Students");
            DropIndex("dbo.StudentSubjects", new[] { "Subject_ID" });
            DropIndex("dbo.StudentSubjects", new[] { "Student_ID" });
            DropIndex("dbo.Subjects", new[] { "Code" });
            DropIndex("dbo.Students", new[] { "Number" });
            DropTable("dbo.StudentSubjects");
            DropTable("dbo.Subjects");
            DropTable("dbo.Students");
        }
    }
}
