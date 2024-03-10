using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TaskManager.Databases
{
	public class TaskDatabase
	{
		private SQLiteAsyncConnection _db;
		public TaskDatabase(string dbPath)
		{
			_db = new SQLiteAsyncConnection(dbPath, Constants.Flags);
			_db.CreateTableAsync<Classes.Task>();
		}

		public string GetDatabasePath()
		{
			return  _db.DatabasePath;
		}

		public async Task AddTask(Classes.Task task)
		{
			await _db.InsertAsync(task);
		}

		public Task<List<Classes.Task>> GetTasks()
		{
			return _db.Table<Classes.Task>().ToListAsync();
		}

		public async Task DeleteTask(Classes.Task task)
		{
			await _db.DeleteAsync(task);
		}

		public async Task UpdateItem(Classes.Task task)
		{
			await _db.UpdateAsync(task);
		}

		public async Task CloseAllConnections()
		{
			await _db.CloseAsync();
		}
		public async Task ClearDatabase()
		{
			await _db.DeleteAllAsync<Classes.Task>();
		}
	}
	
}
