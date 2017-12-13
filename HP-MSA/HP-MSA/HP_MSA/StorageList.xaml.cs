﻿using System; using System.Collections.Generic; using System.Linq; using System.Text; using System.Text.RegularExpressions; using System.IO; using System.Net; using System.Windows.Input; using Xamarin.Forms; using Xamarin.Forms.Xaml;  namespace HP_MSA {     [XamlCompilation(XamlCompilationOptions.Compile)]     public partial class StorageList : ContentPage     {         List<Unit> items = new List<Unit>();         public StorageList(string companyName)         {             InitializeComponent();             HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri("http://54.173.140.216:8080"));             var postData = "query=SELECT * FROM msa.data WHERE companyName = '" + companyName + "'";             var data = Encoding.ASCII.GetBytes(postData);             request.Method = "POST";             request.ContentType = "application/x-www-form-urlencoded";             request.ContentLength = data.Length;             using (var stream = request.GetRequestStream())             {                 stream.Write(data, 0, data.Length);             }             var response = (HttpWebResponse)request.GetResponse();             var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();             List<String> raw_data = new List<String>();             Regex rgx = new Regex(@"\[.*?\]");             Regex rgx2 = new Regex("[^a-zA-Z0-9\\.\\(\\)\\-\\:\\, -]");             string temp = "";             foreach (Match match in rgx.Matches(responseString))             {                 temp = rgx2.Replace(match.Value, "");                 raw_data.Add(temp);             }             for (int i = 0; i < raw_data.Count; i++){                 string[] unitInfo = raw_data[i].Split(',');                 items.Add(new Unit() { systemName = unitInfo[1], serialNumber = unitInfo[2], productFamily = unitInfo[3],                      model = unitInfo[4], osVersion = unitInfo[5], cpgCount = unitInfo[6], updated = unitInfo[11],                      nodeCount = unitInfo[12], diskCount = unitInfo[14], vvCount = unitInfo[19] });             }             Storage.ItemsSource = items;         }          private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)         {             Storage.BeginRefresh();              if (string.IsNullOrWhiteSpace(e.NewTextValue))                 Storage.ItemsSource = items;             else                 Storage.ItemsSource = items.Where(i => i.systemName.Contains(e.NewTextValue));              Storage.EndRefresh();         }          private string _searchedText;         public string SearchedText         {             get { return _searchedText; }             set { _searchedText = value; OnPropertyChanged(); }         }          private List<Unit> _modList;          public List<Unit> ModList         {             get             {                 return _modList;             }             set             {                 _modList = value;                 OnPropertyChanged();             }         }          void OnItemSelected(object sender, SelectedItemChangedEventArgs e)         {             Navigation.PushAsync(new DiskInfo((Unit) e.SelectedItem));         }     }      public class Unit     {         public String systemName { get; set; }         public String serialNumber { get; set; }         public String productFamily { get; set; }         public String model { get; set; }         public String osVersion { get; set; }         public String cpgCount { get; set; }         public String updated { get; set; }         public String nodeCount { get; set; }         public String diskCount { get; set; }         public String vvCount { get; set; }      } }; 