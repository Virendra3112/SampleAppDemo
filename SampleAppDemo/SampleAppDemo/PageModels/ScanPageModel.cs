using FreshMvvm;
using SampleAppDemo.Models;
using SampleAppDemo.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace SampleAppDemo.PageModels
{
    public class ScanPageModel : BasePageModel
    {
        private ObservableCollection<ScanModel> _scanList;
        public ObservableCollection<ScanModel> ScanList
        {
            get
            {
                return _scanList;
            }
            set
            {
                _scanList = value;
                RaisePropertyChanged();
            }
        }
        private string _scanId;
        public string ScanId
        {
            get
            {
                return _scanId;
            }
            set
            {
                _scanId = value;
                RaisePropertyChanged();
            }
        }
        public ICommand ScanCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private SQLiteService _sQLiteService = FreshIOC.Container.Resolve<SQLiteService>();

        public ScanPageModel()
        {
            ScanCommand = new Command(OnScan);
            DeleteCommand = new Command(OnDelete);
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            GetData();

            //ScanList = new ObservableCollection<ScanModel>();

            //ScanList.Add(new ScanModel { IsChecked = false, Name = "First" });
            //ScanList.Add(new ScanModel { IsChecked = false, Name = "Second" });
            //ScanList.Add(new ScanModel { IsChecked = false, Name = "third" });
        }

        public async Task GetData()
        {
            try
            {
                //todo: get data from db
                var _list = await _sQLiteService.GetAllItems();

                ScanList = new ObservableCollection<ScanModel>(_list);

            }
            catch (Exception ex)
            {

            }
        }

        public async Task AddData(ScanModel model)
        {
            await _sQLiteService.InsertData(model);
        }

        private async void OnScan(object obj)
        {
            try
            {
                var scanPage = new ZXingScannerPage();

                await App.Current.MainPage.Navigation.PushAsync(scanPage);

                scanPage.OnScanResult += (result) =>
                {

                    scanPage.IsScanning = false;
                    Device.BeginInvokeOnMainThread(() =>
                    {

                        var test = "Scanned Barcode "
                            + result.Text
                            + " , "
                            + result.BarcodeFormat
                            + " ,"
                            + result.ResultPoints[0].ToString();

                        Console.WriteLine(test);

                        if (!string.IsNullOrEmpty(result.Text))
                        {
                            //add data to DB
                            _ = AddData(new ScanModel { IsChecked = false, Name = result.Text });

                            App.Current.MainPage.Navigation.PopAsync();

                        }

                    });
                };
            }
            catch (Exception ex)
            {

            }
        }


        private async void OnDelete(object obj)
        {
            try
            {
                var items = ScanList.Where(s => s.IsChecked).ToList();

                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        //delete selected items
                        await _sQLiteService.DeleteData(item);
                    }

                    //update the list
                    ScanList.Clear();

                    GetData();
                }
                else
                {
                    //please select items to delete
                    await CoreMethods.DisplayAlert("Alert", "Please select items to delete", "OK");

                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
