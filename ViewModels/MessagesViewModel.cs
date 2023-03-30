using Firebase.Database;
using Firebase.Database.Query;
using Firebase_RTD.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Firebase_RTD.ViewModels
{
    internal class MessagesViewModel : BaseViewModel
    {

        FirebaseClient firebase = new FirebaseClient("https://ceq-dev-66f92-default-rtdb.firebaseio.com/");

        private List<Message> _messages=new List<Message>();


        public List<Message> Messages { get { return _messages; } set { _messages = value; OnPropertyChanged(); } }


        public Command CreateObserverCommand => new Command(CreateObserver);


        public MessagesViewModel()
        {
            
        }

        private async void CreateObserver()
        {
            firebase.Child("Messages").AsObservable<Message>().Subscribe(data =>
             {
                 if (data.EventType == Firebase.Database.Streaming.FirebaseEventType.Delete)
                 {
                     deleteItem(data.Object);
                 }
                 else if(data.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate)
                 {
                     if (Messages.Any(x => x.MessageId == data.Object.MessageId))
                     {
                         updateItem(data.Object);
                     }
                     else
                     {
                         addItem(data.Object);
                     }
                 }
                 Debug.WriteLine(JsonConvert.SerializeObject(data.Object));
             });
        }

        private void addItem(Message item)
        {
            try
            {
                var newList = new List<Message>();
                newList.AddRange(Messages);
                newList.Add(new Message
                {
                    MessageId = item.MessageId,
                    MessageContent = item.MessageContent
                });
                Messages = newList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR:" + ex.Message);
            }
        }

        private void deleteItem(Message item)
        {
            try
            {
                var newList = new List<Message>();
                var itemToDelete = Messages.FirstOrDefault(x => x.MessageId == item.MessageId);
                Messages.Remove(itemToDelete);
                newList.AddRange(Messages);
                Messages = newList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR:" + ex.Message);
            }
        }

        private void updateItem(Message item)
        {
            try
            {
                var newList = new List<Message>();
                var itemToUpdate = Messages.FirstOrDefault(x => x.MessageId == item.MessageId);
                itemToUpdate.MessageContent = item.MessageContent;
                newList.AddRange(Messages);
                Messages = newList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR:" + ex.Message);
            }
        }


        public async Task<List<Message>> GetAllMessages()
        {
            Messages= (await firebase
              .Child("Messages")
              .OnceAsync<Message>()).Select(item => new Message
              {
                  MessageContent = item.Object.MessageContent,
                  MessageId = item.Object.MessageId
              }).ToList();

            return Messages;
        }

        public async Task AddMessage(int messageId, string messageContent)
        {
            await firebase
              .Child("Messages")
              .PostAsync(new Message() { MessageId = messageId, MessageContent = messageContent });

            //try
            //{
            //    var newList = Messages;
            //    newList.Add(new Message
            //    {
            //        MessageId = messageId,
            //        MessageContent = messageContent
            //    });
            //    Messages = null;
            //    Messages = newList;

            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("ERROR:" + ex.Message);
            //}
        }

        public async Task<Message> GetMessage(int messageId)
        {
            var allPersons = await GetAllMessages();
            await firebase
              .Child("Messages")
              .OnceAsync<Message>();
            return allPersons.Where(a => a.MessageId == messageId).FirstOrDefault();
        }

        public async Task UpdateMessage(int messageId, string name)
        {
            var toUpdatePerson = (await firebase
              .Child("Messages")
              .OnceAsync<Message>()).Where(a => a.Object.MessageId == messageId).FirstOrDefault();

            await firebase
              .Child("Messages")
              .Child(toUpdatePerson.Key)
              .PutAsync(new Message() { MessageId = messageId, MessageContent = name });
        }

        public async Task DeleteMessage(int messageId)
        {
            var toDeletePerson = (await firebase
              .Child("Messages")
              .OnceAsync<Message>()).Where(a => a.Object.MessageId == messageId).FirstOrDefault();
            await firebase.Child("Messages").Child(toDeletePerson.Key).DeleteAsync();

        }





    }
}
