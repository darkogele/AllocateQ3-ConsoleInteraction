using AllocateQ3_ConsoleInteraction.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AllocateQ3_ConsoleInteraction.Repositories
{
    public abstract class BaseRepository<T> where T : BaseEntity
    {
        public BaseRepository(string filename)
        {
            // Never call virtual functions in constructor, the derived class might not be initialized yet
            // Path = $"..\\..\\..\\..\\AllocateQ3-ConsoleInteraction.Repositories\\{GetFileName()}";
            
            // fewer slashes with verbatim string
            Path = $@"..\..\..\..\AllocateQ3-ConsoleInteraction.Repositories\{filename}";

            if (!File.Exists(Path))
            {
                File.WriteAllText(Path, "[]");
            }

            // Do not load the file in constructor, it should be done on demand, this way you don't load the file if you don't need it
            // One way to do this is to wrap data with lazy loading property,
            // additionally storing data in memory and never reading the file again means if another process changes the file you will not see the changes 
            // instead read the data from file every time you need it, and change create/saveChanges to use change tracking
            // TLDR: don't create your own database system use SQLite with ef core
            var entities = File.ReadAllText(Path);
            
            var deserialized = JsonConvert.DeserializeObject<List<T>>(entities);

            Data = deserialized;
        }

        protected List<T> Data { get; set; }

        protected string Path { get; set; }

        //protected abstract string GetFileName();

        public void SaveChanges()
        {
            var serialized = JsonConvert.SerializeObject(Data);
            File.WriteAllText(Path, serialized);
        }
        
        // Use async methods instead of sync ones
        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            var serialized = JsonConvert.SerializeObject(Data);
            await File.WriteAllTextAsync(Path, serialized, ct);
        }

        public void Create(T entity)
        {
            entity.Id = GenerateId();
            Data.Add(entity);
        }

        public void Delete(T entity)
        {
            Data.Remove(entity);
        }

        public List<T> GetAll()
        {
            return Data;
        }

        public T GetById(int id)
        {
            return Data.FirstOrDefault(x => x.Id == id);
        }

        // We could save series in the file and then return the last one to generate the next one
        // After that increment the last one and save it back, like SQL Identity
        private int GenerateId()
        {
            var newId = 0;

            if (Data.Count > 0)
            {
                newId = Data.Max(x => x.Id);
            }

            return newId + 1;
        }
    }
}