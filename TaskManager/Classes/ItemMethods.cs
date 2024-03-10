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
			return item!;
		}

		public static void CheckTextOrImage(Classes.Item item)
		{

			if (item.isImageItem == true && item.isTextItem == true)
			{
				TaskPage.TaskPageInstance.OpenButton.IsEnabled = true;
				TaskPage.TaskPageInstance.EditButton.IsEnabled = true;				
			}
			else if (item.isImageItem == true)
			{
				TaskPage.TaskPageInstance.OpenButton.IsEnabled = true;
				TaskPage.TaskPageInstance.EditButton.IsEnabled = false;
			}
			else
			{
				TaskPage.TaskPageInstance.OpenButton.IsEnabled = false;
				TaskPage.TaskPageInstance.EditButton.IsEnabled = true;
			}
		}

	}
}
