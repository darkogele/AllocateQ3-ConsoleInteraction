using AllocateQ3_ConsoleInteraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; // Remove unused usings

namespace AllocateQ3_ConsoleInteraction.Repositories
{
    public class AnimalsRepository : BaseRepository<Animal>
    {
        public AnimalsRepository() : base("Animals.txt")
        {
        }

        // protected override string GetFileName()
        // {
        //     return "Animals.txt";
        // }
    }


}
