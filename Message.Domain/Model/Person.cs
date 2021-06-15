using System;

namespace Message.Consumer.Model
{
    public class Person
    {
        public Guid Id
        {
            get => Id;
            protected set => value = Guid.NewGuid();
        }
        public string Name { get; set; }
        public string LastName { get; set; }
        public ArchitetureEnum ArchitetureType { get; set; }
        public DateTime CreatedAt {
            get => CreatedAt;
            protected set => value = DateTime.Now;
        }
        public DateTime UpdatedAt { get; set; }

    }

    public enum ArchitetureEnum
    {
        Sequential = 1,
        DatabaseState = 2
    }
}
