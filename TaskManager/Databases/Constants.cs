using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Databases
{
	public class Constants
	{
		public const SQLite.SQLiteOpenFlags Flags =
	SQLite.SQLiteOpenFlags.ReadWrite |
	SQLite.SQLiteOpenFlags.Create |
	SQLite.SQLiteOpenFlags.SharedCache;
	}
}
