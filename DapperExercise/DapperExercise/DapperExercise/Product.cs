using System;

namespace DapperExercise
{
    public class Product
    {
        public int  ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public Person UserOwner { get; set; }
        public DateTime CreateTime { get; set; }
        public int UserID { get; set; }
    }
}