/**********************************************

 Copyright (c) 2017 Raj Bandi
 Licensed under Apache License 2.0. See LICENSE file in the project root for full license information.
 
 Name: AppDelegate.cs 
 Project: ElasticSharp (https://www.github.com/rajbandi/elasticsharp)
 
***********************************************/
using Foundation;
using UIKit;

namespace ElasticSharp.Mobile.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
