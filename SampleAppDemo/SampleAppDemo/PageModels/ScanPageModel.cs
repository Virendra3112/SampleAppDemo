﻿using FreshMvvm;
using SampleAppDemo.Models;
using SampleAppDemo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
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
            set
            {
                _scanId = value;
                RaisePropertyChanged();
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
            GetData();
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
            }
            catch (Exception ex)
            {

            }
        }

        public async Task AddData()
        {
            await _sQLiteService.CreateItem(new ScanModel());
        }

    }
}
