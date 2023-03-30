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
            //var allMessages = await VM.GetAllMessages();
            //lstPersons.ItemsSource = allMessages;
        }

        private async void btnUpdate_Clicked(object sender, EventArgs e)
        {
            //await VM.UpdateMessage(Convert.ToInt32(txtId.Text), txtName.Text);
            //txtId.Text = string.Empty;
            //txtName.Text = string.Empty;
            //await DisplayAlert("Success", "Person Updated Successfully", "OK");
            //var allPersons = await VM.GetAllMessages();
            //lstPersons.ItemsSource = allPersons;
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            //await VM.DeleteMessage(Convert.ToInt32(txtId.Text));
            //await DisplayAlert("Success", "Person Deleted Successfully", "OK");
            //var allPersons = await VM.GetAllMessages();
            //lstPersons.ItemsSource = allPersons;
        }

        private async void btnRetrive_Clicked(object sender, EventArgs e)
        {
            //var person = await VM.GetMessage(Convert.ToInt32(txtId.Text));
            //if (person != null)
            //{
            //    txtId.Text = person.MessageId.ToString();
            //    txtName.Text = person.MessageContent;
            //    await DisplayAlert("Success", "Person Retrive Successfully", "OK");

            //}
            //else
            //{
            //    await DisplayAlert("Success", "No Person Available", "OK");
            //}
        }

        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            await VM.AddMessage(Convert.ToInt32(txtId.Text), txtName.Text);
            //txtId.Text = string.Empty;
            //txtName.Text = string.Empty;
            //await DisplayAlert("Success", "Message Added Successfully", "OK");
            //var allPersons = await VM.GetAllMessages();
            //lstPersons.ItemsSource = allPersons;
        }
    }
}
