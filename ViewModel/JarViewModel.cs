using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace kinetic.ViewModel
{
    public class Results<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

    }
    public class JarTransactionViewModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public List<DataView> Data { get; set; }
    }
    public class DataView
    {
        public int Id { get; set; }
        public int JarId { get; set; }
        public double Amount { get; set; }
        public double Volume { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Action { get; set; }
    } 
}
