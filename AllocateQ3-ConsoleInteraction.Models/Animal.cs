using System;
using System.Collections.Generic;
using System.Text; // Remove unused usings

namespace AllocateQ3_ConsoleInteraction.Models
{
    public class Animal : BaseEntity
    {
        public string Type { get; set; }
        public string Sound { get; set; }

        //public virtual void MakeSound() // Why virtual? it is not needed
        // Why does this entity know how to make sound? It should be in service layer or 
        // Rearrange the application to follow a Domain first approach
        public void MakeSound()
        {
            Console.WriteLine("Old MacDonald had a farm, E I E I O.");
            Console.WriteLine($"And on his farm he had a {Type}, E I E I O.");
            Console.WriteLine($"With a {Sound} {Sound} here and a {Sound} {Sound} there,");
            Console.WriteLine($"Here a {Sound}, there a {Sound}, everywhere a {Sound} {Sound}");
            Console.WriteLine("Old MacDonald had a farm, E I E I O.");
            Console.WriteLine("");
        }
    }
}