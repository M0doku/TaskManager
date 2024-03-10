using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace TaskManager.Databases
{
	public class ItemsDatabase
	{
		private SQLiteAsyncConnection _db;
		public ItemsDatabase(string dbPath)
		{
			_db = new SQLiteAsyncConnection(dbPath, Constants.Flags);
			_db.CreateTableAsync<Classes.Item>();
		}
		public string GetDatabasePath()
		{
			return _db.DatabasePath;
		}

		public async Task AddItem(Classes.Item item)
		{
			await _db.InsertAsync(item);
		}

		public Task<List<Classes.Item>> GetItems()
		{
			return _db.Table<Classes.Item>().ToListAsync();
		}

		public async Task CloseAllConnections()
		{
			await _db.CloseAsync();
		}

		public async Task DeleteItem(Classes.Item item)
		{
			await _db.DeleteAsync(item);
		}

		public async Task ClearDatabase()
		{
			await _db.DeleteAllAsync<Classes.Item>();
		}
		public async Task UpdateItem(Classes.Item item)
		{
			await _db.UpdateAsync(item);
		}
	}
}
