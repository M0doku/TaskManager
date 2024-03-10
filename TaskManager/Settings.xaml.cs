using LocalizationResourceManager.Maui;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace TaskManager;

public partial class Settings : ContentPage
{
	private readonly ILocalizationResourceManager resourceManager;
	public static ILocalizationResourceManager resourceManagerInstance;
	public static Settings SettingsInstance = new(resourceManagerInstance);

	public  Settings(ILocalizationResourceManager resourceManager)
	{
		InitializeComponent();
		SettingsInstance = this;
		this.resourceManager = resourceManager;
		resourceManagerInstance = resourceManager;
		PickerLanguages.SelectedItem = AppResources.SettingsPickerLanguageSelectedItem;
		DeviceDisplay.MainDisplayInfoChanged += UpdateLayout;
		StartLayout();
	}

	public void StartLayout()
	{
		AppShell.Orientation = DeviceDisplay.MainDisplayInfo.Orientation.ToString();
		if (AppShell.Orientation == "Landscape")
		{
			Classes.LayoutUpdate.SettingsLandscapeLayout();
		}
		else if (AppShell.Orientation == "Portrait")
		{
			Classes.LayoutUpdate.SettingsPortraitLayout();
		}
	}

	private void UpdateLayout(object? sender, DisplayInfoChangedEventArgs e)
	{
		AppShell.Orientation = DeviceDisplay.MainDisplayInfo.Orientation.ToString();
		if (AppShell.Orientation == "Landscape")
		{
			Classes.LayoutUpdate.SettingsLandscapeLayout();
		}
		else if (AppShell.Orientation == "Portrait")
		{
			Classes.LayoutUpdate.SettingsPortraitLayout();
		}
	}

	private async void RemoveAllTasksButton_Clicked(object sender, EventArgs e)
	{
		string[] files = Directory.GetFiles(FileSystem.AppDataDirectory + "/Tasks/");
		foreach(string file in files)
		{
			var db = new Databases.ItemsDatabase(file);
			await db.ClearDatabase();
			File.Delete(file);
			
		}
		await MainPage.Database.ClearDatabase();
	}

	private void Settings_Appearing(object sender, EventArgs e)
	{
		AppShell.AppShellInstance.AppShellTitle.Text = AppResources.ShellSettingsMenuLabel;
	}

	private void SaveSettings_Clicked(object sender, EventArgs e)
	{
		if(PickerLanguages.SelectedItem.ToString() == "Русский")
		{
			resourceManager.CurrentCulture = new System.Globalization.CultureInfo("ru");
			AppShell.AppShellInstance.AppShellTitle.Text = AppResources.ShellSettingsMenuLabel;
		}
		else if(PickerLanguages.SelectedItem.ToString() == "English")
		{
			resourceManager.CurrentCulture = new System.Globalization.CultureInfo("en");
			AppShell.AppShellInstance.AppShellTitle.Text = AppResources.ShellSettingsMenuLabel;
		}
	}
}