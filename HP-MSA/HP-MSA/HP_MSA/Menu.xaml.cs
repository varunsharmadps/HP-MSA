﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HP_MSA
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Menu : ContentPage
	{
        private ContentPage lastPage { get; set; }
        private string companyName;
		public Menu (ContentPage lastPage, string companyName)
		{
            this.lastPage = lastPage;
            InitializeComponent ();
            this.companyName = companyName;
		}

        private void goBack(Object o, EventArgs e)
        {
            App.Current.MainPage = lastPage;
        }

        private void logOut(Object o, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }

        private void healthAlerts(Object o, EventArgs e)
        {
            App.Current.MainPage = new HealthAlerts(this,companyName);
        }
	}
}