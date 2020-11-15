namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'12ae04b1-8f78-47ce-8eb4-a8cb0ca5ed7f', N'guest@vidly.com', 0, N'ANlkQG68Avqd1dP6C4fMkt6l7rByMJUg/o1l4Ti6G8vmK1uW3oP3g/fXQSZNLIA8pw==', N'e6170c52-5455-43fb-a4f6-7bb56b64f04d', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7c039f30-7a94-4850-9c2e-5d5a0717e1a9', N'admin@vidly.com', 0, N'AEW2RleW+/Wh5C1UYvRwYXYoJeq7aglhz2Dn42V9CUSv2zfPFD+eu8FL9J4shGDSpQ==', N'ae8fe505-0902-4fc1-86c9-79df1b01f39e', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'175436ff-c035-4198-9bc4-169cd7d3dc6c', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7c039f30-7a94-4850-9c2e-5d5a0717e1a9', N'175436ff-c035-4198-9bc4-169cd7d3dc6c')
");
        }
        
        public override void Down()
        {
        }
    }
}
