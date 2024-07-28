using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Classes
{
	public static class ItemMethods
	{
		public static Classes.Item DefineItem()
		{
			Classes.Item? item = new Classes.Item();
			if(MainPage.TaskType == "2C")
			{
				switch (TaskPageTwoColumns.SelectedItemCollection)
				{
					case "ItemsToDo":
					item = TaskPageTwoColumns.TaskPageTwoColumnsInstance.ItemsToDo_CV.SelectedItem as Classes.Item;
					TaskPageTwoColumns.TaskPageTwoColumnsInstance.ItemsToDo_CV.SelectedItem = null;
					break;
					case "ItemsDone":
					item = TaskPageTwoColumns.TaskPageTwoColumnsInstance.ItemsDone_CV.SelectedItem as Classes.Item;
					TaskPageTwoColumns.TaskPageTwoColumnsInstance.ItemsDone_CV.SelectedItem = null;
					break;
					default:
					item = null;
					break;
				}
			}
			else if(MainPage.TaskType == "3C")
			{
				switch (TaskPage.SelectedItemCollection)
				{
					case "ItemsToDo":
					item = TaskPage.TaskPageInstance.ItemsToDo_CV.SelectedItem as Classes.Item;
					TaskPage.TaskPageInstance.ItemsToDo_CV.SelectedItem = null;
					break;
					case "ItemsDoing":
					item = TaskPage.TaskPageInstance.ItemsDoing_CV.SelectedItem as Classes.Item;
					TaskPage.TaskPageInstance.ItemsDoing_CV.SelectedItem = null;
					break;
					case "ItemsDone":
					item = TaskPage.TaskPageInstance.ItemsDone_CV.SelectedItem as Classes.Item;
					TaskPage.TaskPageInstance.ItemsDone_CV.SelectedItem = null;
					break;
					default:
					item = null;
					break;
				}
			}
			return item!;
		}

		public static void CheckTextOrImage(Classes.Item item)
		{
			if (item.isImageItem == true && item.isTextItem == true && MainPage.TaskType == "2C")
			{
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.OpenButton.IsEnabled = true;
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.EditButton.IsEnabled = true;
			}
			else if (item.isImageItem == true && MainPage.TaskType == "2C")
			{
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.OpenButton.IsEnabled = true;
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.EditButton.IsEnabled = false;
			}
			else if (MainPage.TaskType == "2C")
			{
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.OpenButton.IsEnabled = false;
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.EditButton.IsEnabled = true;
			}

			if (item.isImageItem == true && item.isTextItem == true && MainPage.TaskType == "3C")
			{
				TaskPage.TaskPageInstance.OpenButton.IsEnabled = true;
				TaskPage.TaskPageInstance.EditButton.IsEnabled = true;				
			}
			else if (item.isImageItem == true && MainPage.TaskType == "3C")
			{
				TaskPage.TaskPageInstance.OpenButton.IsEnabled = true;
				TaskPage.TaskPageInstance.EditButton.IsEnabled = false;
			}
			else if (MainPage.TaskType == "3C")
			{
				TaskPage.TaskPageInstance.OpenButton.IsEnabled = false;
				TaskPage.TaskPageInstance.EditButton.IsEnabled = true;
			}
		}

	}
}
