using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Classes
{
	internal class TasksMethods
	{
		public static async void GetTasks()
		{
			List<Classes.Task> tasks = await MainPage.Database.GetTasks();
			foreach(Classes.Task task in tasks)
			{
				MainPage.Tasks.Add(task);
			}
		}
	}
}
