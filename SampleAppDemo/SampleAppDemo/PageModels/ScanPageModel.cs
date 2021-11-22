using FreshMvvm;
using SampleAppDemo.Models;
using SampleAppDemo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

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
        }
        public ICommand ScanCommand { get; set; }

        private SQLiteService _sQLiteService = FreshIOC.Container.Resolve<SQLiteService>();

        public ScanPageModel()
        {

        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            ScanList = new ObservableCollection<ScanModel>();

            ScanList.Add(new ScanModel { IsChecked = false, Name = "First" });
            ScanList.Add(new ScanModel { IsChecked = false, Name = "Second" });
            ScanList.Add(new ScanModel { IsChecked = false, Name = "third" });
            
            //todo: get data from db
            var _list = _sQLiteService.GetAllItems();
        }

    }
}
