using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Classes
{
	public static class LayoutUpdate
	{
		public static void TaskPageLandscapeEditLayout()
		{
			if(TaskPage.TaskPageInstance.ChangeLayoutButtonState.IsVisible == false)
			{
				TaskPage.TaskPageInstance.ChangeLayoutButtonState.IsVisible = true;

				TaskPage.TaskPageInstance.ContentsRow.Height = new GridLength(4, GridUnitType.Star);
				TaskPage.TaskPageInstance.ButtonsRow0.Height = new GridLength(1, GridUnitType.Star);
				TaskPage.TaskPageInstance.ButtonsRow1.Height = new GridLength(1, GridUnitType.Star);

			}
			else if(TaskPage.TaskPageInstance.ChangeLayoutButtonState.IsVisible == true)
			{
				TaskPage.TaskPageInstance.ChangeLayoutButtonState.IsVisible = false;
				TaskPage.TaskPageInstance.ContentsRow.Height = new GridLength(7, GridUnitType.Star);
				TaskPage.TaskPageInstance.ButtonsRow0.Height = new GridLength(0, GridUnitType.Star);
				TaskPage.TaskPageInstance.ButtonsRow1.Height = new GridLength(0, GridUnitType.Star);
			}
		}
		public static void TaskPagePortraitEditLayout()
		{
			if (TaskPage.TaskPageInstance.ChangeLayoutButtonState.IsVisible == false)
			{
				TaskPage.TaskPageInstance.ChangeLayoutButtonState.IsVisible = true;
				TaskPage.TaskPageInstance.ContentsRow.Height = new GridLength(15, GridUnitType.Star);
				TaskPage.TaskPageInstance.ButtonsRow0.Height = new GridLength(1, GridUnitType.Star);
				TaskPage.TaskPageInstance.ButtonsRow1.Height = new GridLength(1, GridUnitType.Star);
			}
			else if(TaskPage.TaskPageInstance.ChangeLayoutButtonState.IsVisible == true)
			{
				TaskPage.TaskPageInstance.ChangeLayoutButtonState.IsVisible = false;
				TaskPage.TaskPageInstance.ContentsRow.Height = new GridLength(17, GridUnitType.Star);
				TaskPage.TaskPageInstance.ButtonsRow0.Height = new GridLength(0, GridUnitType.Star);
				TaskPage.TaskPageInstance.ButtonsRow1.Height = new GridLength(0, GridUnitType.Star);
			}
		}
		public static void TaskPageLandscapeLayout()
		{
			if (TaskPage.TaskPageInstance.ChangeLayoutButtonState.IsVisible == true)
			{
				TaskPage.TaskPageInstance.ContentsRow.Height = new GridLength(4, GridUnitType.Star);
				TaskPage.TaskPageInstance.ButtonsRow0.Height = new GridLength(1, GridUnitType.Star);
				TaskPage.TaskPageInstance.ButtonsRow1.Height = new GridLength(1, GridUnitType.Star);

			}
			else if (TaskPage.TaskPageInstance.ChangeLayoutButtonState.IsVisible == false)
			{
				TaskPage.TaskPageInstance.ContentsRow.Height = new GridLength(6, GridUnitType.Star);
				TaskPage.TaskPageInstance.ButtonsRow0.Height = new GridLength(0, GridUnitType.Star);
				TaskPage.TaskPageInstance.ButtonsRow1.Height = new GridLength(0, GridUnitType.Star);
			}
		}

		public static void TaskPagePortraitLayout()
		{
			if (TaskPage.TaskPageInstance.ChangeLayoutButtonState.IsVisible == true)
			{
				TaskPage.TaskPageInstance.ContentsRow.Height = new GridLength(15, GridUnitType.Star);
				TaskPage.TaskPageInstance.ButtonsRow0.Height = new GridLength(1, GridUnitType.Star);
				TaskPage.TaskPageInstance.ButtonsRow1.Height = new GridLength(1, GridUnitType.Star);
			}
			else if (TaskPage.TaskPageInstance.ChangeLayoutButtonState.IsVisible == false)
			{
				TaskPage.TaskPageInstance.ContentsRow.Height = new GridLength(17, GridUnitType.Star);
				TaskPage.TaskPageInstance.ButtonsRow0.Height = new GridLength(0, GridUnitType.Star);
				TaskPage.TaskPageInstance.ButtonsRow1.Height = new GridLength(0, GridUnitType.Star);
			}
		}
		
		public static void TaskPageTwoColumnsLandscapeEditLayout()
		{
			if (TaskPageTwoColumns.TaskPageTwoColumnsInstance.ChangeLayoutButtonState.IsVisible == false)
			{
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ChangeLayoutButtonState.IsVisible = true;

				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ContentsRow.Height = new GridLength(4, GridUnitType.Star);
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ButtonsRow0.Height = new GridLength(1, GridUnitType.Star);
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ButtonsRow1.Height = new GridLength(1, GridUnitType.Star);

			}
			else if (TaskPageTwoColumns.TaskPageTwoColumnsInstance.ChangeLayoutButtonState.IsVisible == true)
			{
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ChangeLayoutButtonState.IsVisible = false;
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ContentsRow.Height = new GridLength(6, GridUnitType.Star);
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ButtonsRow0.Height = new GridLength(0, GridUnitType.Star);
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ButtonsRow1.Height = new GridLength(0, GridUnitType.Star);
			}
		}

		public static void TaskPageTwoColumnsPortraitEditLayout()
		{
			if (TaskPageTwoColumns.TaskPageTwoColumnsInstance.ChangeLayoutButtonState.IsVisible == false)
			{
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ChangeLayoutButtonState.IsVisible = true;
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ContentsRow.Height = new GridLength(15, GridUnitType.Star);
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ButtonsRow0.Height = new GridLength(1, GridUnitType.Star);
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ButtonsRow1.Height = new GridLength(1, GridUnitType.Star);
			}
			else if (TaskPageTwoColumns.TaskPageTwoColumnsInstance.ChangeLayoutButtonState.IsVisible == true)
			{
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ChangeLayoutButtonState.IsVisible = false;
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ContentsRow.Height = new GridLength(17, GridUnitType.Star);
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ButtonsRow0.Height = new GridLength(0, GridUnitType.Star);
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ButtonsRow1.Height = new GridLength(0, GridUnitType.Star);
			}
		}
		public static void TaskPageTwoColumnsLandscapeLayout()
		{
			if (TaskPageTwoColumns.TaskPageTwoColumnsInstance.ChangeLayoutButtonState.IsVisible == true)
			{
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ContentsRow.Height = new GridLength(4, GridUnitType.Star);
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ButtonsRow0.Height = new GridLength(1, GridUnitType.Star);
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ButtonsRow1.Height = new GridLength(1, GridUnitType.Star);

			}
			else if (TaskPage.TaskPageInstance.ChangeLayoutButtonState.IsVisible == false)
			{
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ContentsRow.Height = new GridLength(6, GridUnitType.Star);
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ButtonsRow0.Height = new GridLength(0, GridUnitType.Star);
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ButtonsRow1.Height = new GridLength(0, GridUnitType.Star);
			}
		}

		public static void TaskPageTwoColumnsPortraitLayout()
		{
			if (TaskPageTwoColumns.TaskPageTwoColumnsInstance.ChangeLayoutButtonState.IsVisible == true)
			{
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ContentsRow.Height = new GridLength(15, GridUnitType.Star);
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ButtonsRow0.Height = new GridLength(1, GridUnitType.Star);
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ButtonsRow1.Height = new GridLength(1, GridUnitType.Star);
			}
			else if (TaskPageTwoColumns.TaskPageTwoColumnsInstance.ChangeLayoutButtonState.IsVisible == false)
			{
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ContentsRow.Height = new GridLength(17, GridUnitType.Star);
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ButtonsRow0.Height = new GridLength(0, GridUnitType.Star);
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ButtonsRow1.Height = new GridLength(0, GridUnitType.Star);
			}
		}

		public static void MainPageLandscapeLayout()
		{
			MainPage.MainPageInstance!.ContentsRow.Height = new GridLength(5, GridUnitType.Star);
			MainPage.MainPageInstance.ButtonsRow0.Height = new GridLength(1, GridUnitType.Star);
		}

		public static void MainPagePortraitLayout()
		{
			MainPage.MainPageInstance!.ContentsRow.Height = new GridLength(13, GridUnitType.Star);
			MainPage.MainPageInstance.ButtonsRow0.Height = new GridLength(1, GridUnitType.Star);
		}

		public static void SettingsLandscapeLayout()
		{
			Settings.SettingsInstance!.ContentsRow.Height = new GridLength(1, GridUnitType.Star);
		}

		public static void SettingsPortraitLayout()
		{
			Settings.SettingsInstance!.ContentsRow.Height = new GridLength(10, GridUnitType.Star);
		}
	}
	
	
}
