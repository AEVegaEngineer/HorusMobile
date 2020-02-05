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
            Title = "Notificaciones";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                Debug.WriteLine("\n\n**************EJECUTANDO SUBSCRIPCIÓN*************\n\n");
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });

            App.Current.getNotifications();
        }
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                //var items = App.Current.getNotifications();
                var items = await DataStore.GetItemsAsync(true);

                if (items == null || !items.Any())
                {
                    /*VERIFICAR SI NO EXISTEN NOTIFICACIONES Y MOSTRAR UN MENSAJE QUE LO EXPLIQUE*/
                    Debug.WriteLine("No hay notificaciones");
                }
                else
                {
                    foreach (var item in items)
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