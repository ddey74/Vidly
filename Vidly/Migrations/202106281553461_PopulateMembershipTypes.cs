namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMembershipTypes : DbMigration
    {
        public override void Up()
        {
            //here we can write sql query to put data into sql
            //we are pushing data of membership type because it will be going to 
            //consistent data over the qa dev and other environment
            Sql("Insert into MembershipTypes(Id,SignupFee,DurationInMonth,DiscountRate) values (1,0,0,0)");//one time use member
            Sql("Insert into MembershipTypes(Id,SignupFee,DurationInMonth,DiscountRate) values (2,30,1,10)");//weekly suscribed member
            Sql("Insert into MembershipTypes(Id,SignupFee,DurationInMonth,DiscountRate) values (3,90,3,15)");//monthly suscribed member
            Sql("Insert into MembershipTypes(Id,SignupFee,DurationInMonth,DiscountRate) values (4,300,12,20)");//yearly suscribed member
        }
        
        public override void Down()
        {
        }
    }
}
