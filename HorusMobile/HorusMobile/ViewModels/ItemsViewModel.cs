using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using HorusMobile.Models;
using HorusMobile.Views;
using System.Linq;

namespace HorusMobile.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        

        public ItemsViewModel()
        {
            Title = "Horus Mobile";
            //SubTitle = "Notificaciones";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {                
                var newItem = item as Item;
                Items.Add(newItem); 
                //await DataStore.AddItemAsync(newItem);
                await DataStore.AddItemAsync(newItem);
            });

            //App.Current.getNotifications();
        }
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                Debug.WriteLine("\n\n**************EJECUTANDO GETNOTIFSASYNC*************\n\n");
                var objeto = await DataStoreNotifications.GetNotifsAsync(false);
                var items = await DataStore.GetItemsAsync(true);

                if (objeto == null || !objeto.Any())
                {
                    /*VERIFICAR SI NO EXISTEN NOTIFICACIONES Y MOSTRAR UN MENSAJE QUE LO EXPLIQUE*/
                    Debug.WriteLine("No hay notificaciones");
                }
                else
                {
                    foreach (var item in objeto)
                    {
                        Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}