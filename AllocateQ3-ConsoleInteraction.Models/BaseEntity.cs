using System;
using System.Collections.Generic;
using System.Text; // Remove unused usings

namespace AllocateQ3_ConsoleInteraction.Models
{
    // public class BaseEntity // Make it abstract cuz it should not be instantiated directly
    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }
}
