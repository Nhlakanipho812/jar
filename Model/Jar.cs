using System;

namespace kinetic.Model
{
    public class Jar
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public double Volume { get; set; }
    }
    
    //this class will have all the transactions 
    public class JarTransaction
    {
        public int Id { get; set; }
        public int JarId { get; set; }
        public double Amount { get; set; }
        public double Volume { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Action { get; set; }
    }
}