using Android.App;
using Android.Runtime;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;

namespace TaskManager
{
	[Application]
	public class MainApplication : MauiApplication
	{
		public MainApplication(IntPtr handle, JniHandleOwnership ownership)
			: base(handle, ownership)
		{
		}

		protected override MauiApp CreateMauiApp() {
			Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
			{
				h.PlatformView.BackgroundTintList =
					Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
			});
			Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
			{
				h.PlatformView.BackgroundTintList =
					Android.Content.Res.ColorStateList.ValueOf(Colors.LavenderBlush.ToAndroid());
				h.PlatformView.SetPadding(0, 2, 0, 0);
			});
			return MauiProgram.CreateMauiApp();
		}
	}
}
