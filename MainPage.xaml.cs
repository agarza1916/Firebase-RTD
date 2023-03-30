using Firebase_RTD.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Firebase_RTD
{
    public partial class MainPage : ContentPage
    {
        MessagesViewModel VM;

        public MainPage()
        {
            InitializeComponent();
            VM = BindingContext as MessagesViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            VM.CreateObserverCommand.Execute(null);
        }

        private async void btnUpdate_Clicked(object sender, EventArgs e)
        {
            await VM.UpdateMessage(Convert.ToInt32(txtId.Text), txtName.Text);
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            await VM.DeleteMessage(Convert.ToInt32(txtId.Text));
        }

        private async void btnRetrive_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            await VM.AddMessage(Convert.ToInt32(txtId.Text), txtName.Text);
        }
    }
}
