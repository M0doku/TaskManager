using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Databases;

namespace TaskManager.Classes
{
	internal class ItemsAddingMethods
	{
		public static async Task<ObservableCollection<Item>> GetItemsToDo()
		{
			await System.Threading.Tasks.Task.Run(GetItemsToDo_Method);
			return TaskPage.ItemsToDo;
		}
		public static async Task<ObservableCollection<Item>> GetItemsDoing()
		{
			await System.Threading.Tasks.Task.Run(GetItemsDoing_Method);
			return TaskPage.ItemsDoing;
		}
		public static async Task<ObservableCollection<Item>> GetItemsDone()
		{
			await System.Threading.Tasks.Task.Run(GetItemsDone_Method);
			return TaskPage.ItemsDone;
		}
		public static async void GetItemsToDo_Method()
		{
			//ItemsDatabase? database = new ItemsDatabase(AppShell.AppDatabasePath);
			var db = new ItemsDatabase(MainPage.TaskDbPath);		
			List<Item> items = await System.Threading.Tasks.Task.Run(db.GetItems);
			if (items.Count > 0)
				{
					items = items.OrderBy(p => p.Position).ToList();
					foreach (Classes.Item item in items)
					{
						if (item.CurrentCollection == "ItemsToDo")
						{
							if (item.isTextItem == true && item.isImageItem == true)
							{
								TaskPage.ItemsToDo.Add(new Classes.Item() { Id = item.Id, Text = item.Text, isTextItem = true, isImageItem = true, ImageSource = item.ImageSource, CurrentCollection = item.CurrentCollection, IsNotEditable = item.IsNotEditable, Position = item.Position });
							}
							else if (item.isTextItem == true && item.isImageItem == false)
							{
								TaskPage.ItemsToDo.Add(new Classes.Item() { Id = item.Id, Text = item.Text, isTextItem = true, isImageItem = false, CurrentCollection = item.CurrentCollection, Position = item.Position });
							}
							else if (item.isImageItem == true && item.isTextItem == false)
							{
								TaskPage.ItemsToDo.Add(new Classes.Item() { Id = item.Id, isImageItem = true, isTextItem = false, ImageSource = item.ImageSource, CurrentCollection = item.CurrentCollection, Position = item.Position });
							}
						}
					}
				}
			
		}

		public static async void GetItemsDoing_Method()
		{
			//ItemsDatabase? database;
			var db = new ItemsDatabase(MainPage.TaskDbPath);
			List<Item> items = await System.Threading.Tasks.Task.Run(db.GetItems);
			if (items.Count > 0)
				{
					items = items.OrderBy(p => p.Position).ToList();
					foreach (Classes.Item item in items)
					{
						if (item.CurrentCollection == "ItemsDoing")
						{
							if (item.isTextItem == true && item.isImageItem == true)
							{
								TaskPage.ItemsDoing.Add(new Classes.Item() { Id = item.Id, Text = item.Text, isTextItem = true, isImageItem = true, ImageSource = item.ImageSource, CurrentCollection = item.CurrentCollection, IsNotEditable = item.IsNotEditable, Position = item.Position });
							}
							else if (item.isTextItem == true)
							{
								TaskPage.ItemsDoing.Add(new Classes.Item() { Id = item.Id, Text = item.Text, isTextItem = true, isImageItem = false, CurrentCollection = item.CurrentCollection, Position = item.Position });
							}
							else if (item.isImageItem == true)
							{
								TaskPage.ItemsDoing.Add(new Classes.Item() { Id = item.Id, isImageItem = true, isTextItem = false, ImageSource = item.ImageSource, CurrentCollection = item.CurrentCollection, Position = item.Position });
							}
						}
					}
				}
			
		}

		public static async void GetItemsDone_Method()
		{
			//ItemsDatabase? database;
			var db = new ItemsDatabase(MainPage.TaskDbPath);
			List<Item> items = await System.Threading.Tasks.Task.Run(db.GetItems);
			if (items.Count > 0 )
				{
					items = items.OrderBy(p => p.Position).ToList();
					foreach (Classes.Item item in items)
					{
						if (item.CurrentCollection == "ItemsDone")
						{
							if (item.isTextItem == true && item.isImageItem == true)
							{
								TaskPage.ItemsDone.Add(new Classes.Item() { Id = item.Id, Text = item.Text, isTextItem = true, isImageItem = true, ImageSource = item.ImageSource, CurrentCollection = item.CurrentCollection, IsNotEditable = item.IsNotEditable, Position = item.Position });
							}
							else if (item.isTextItem == true)
							{
								TaskPage.ItemsDone.Add(new Classes.Item() { Id = item.Id, Text = item.Text, isTextItem = true, isImageItem = false, CurrentCollection = item.CurrentCollection, Position = item.Position });
							}
							else if (item.isImageItem == true)
							{
								TaskPage.ItemsDone.Add(new Classes.Item() { Id = item.Id, isImageItem = true, isTextItem = false, ImageSource = item.ImageSource, CurrentCollection = item.CurrentCollection, Position = item.Position });
							}
						}
					}
				}
			
		}
	}
}
