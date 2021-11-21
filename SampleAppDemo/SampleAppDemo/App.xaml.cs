using FreshMvvm;
using SampleAppDemo.PageModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleAppDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var tabbedNavigation = new FreshTabbedNavigationContainer();
            tabbedNavigation.AddTab<ScanPageModel>("Scan", null);
            tabbedNavigation.AddTab<ValidatePageModel>("Validate", null);
            tabbedNavigation.AddTab<SyncPageModel>("Sync", null);
            MainPage = tabbedNavigation;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
