using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Task_Tracker
{
    [Serializable]
    public sealed class SystemDatabase
    {
        public static readonly string FileName = "database.json";
        public uint LastInsertedId { get; set; } = 0;
        public ICollection<Task> Tasks { get; set; } = new List<Task>();

        public void Save()
        {
            File.WriteAllText(FileName, JsonSerializer.Serialize(this, new JsonSerializerOptions()
                {
                    WriteIndented = true
                }
            ));
        }

        public static SystemDatabase GetDatabase()
        {
            try
            {
                if (!File.Exists(SystemDatabase.FileName))
                    File.WriteAllText(SystemDatabase.FileName, string.Empty);

                var content = File.ReadAllText(SystemDatabase.FileName);
                // TODO melhorar essa chamada, carregar todas tasks em memória não é interessante
                var database = string.IsNullOrEmpty(content)
                    ? new SystemDatabase()
                    : JsonSerializer.Deserialize<SystemDatabase>(content);

                return database;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
