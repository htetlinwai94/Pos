using System;
using System.Collections.Generic;
namespace mysqlefcore
{
  public class Product
  {
    public string PCode{ get; set; }
    public string Name { get; set; }
    public string Photo { get; set; }
    public decimal Price { get; set; }   
    public DateTime  CreatedDate { get; set; }
    public virtual Group Group { get; set; }
    public virtual User User { get; set; }
  }
  public class Group
  {
    public string GCode{ get; set; }
    public string GName { get; set; }
    public virtual ICollection<Product> products { get; set; }
   
  }
  public class User
  {
    public string UserID{ get; set; }
    public string Name { get; set; }
    public string Photo { get; set; }
    public string Email { get; set; }   
    public string password { get; set; }
    public virtual ICollection<Product> products { get; set; }
  }
  public class Inventory
  {
    public int ID { get; set; }
    public string Quantity { get; set; }

    public virtual Product Product { get; set; }
  }
}