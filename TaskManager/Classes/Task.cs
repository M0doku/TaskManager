using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TaskManager.Classes
{
    [Table("Tasks")]
    public class Task
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }
		[Column("taskname")]
		public string TaskName { get; set; } = string.Empty;
        [Column("databasepath")]
        public string DatabasePath { get; set; } = string.Empty;

        public bool TaskPageIsCreated { get; set; } = false;
        public Task(string name, string database)
        {
            TaskName = name;
            DatabasePath = database;
        }
        public Task() { }
    }
}
