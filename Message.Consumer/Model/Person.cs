using System;

namespace Message.Consumer.Model
{
    public class Person
    {
        public Guid id { get; protected set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
