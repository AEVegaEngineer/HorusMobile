using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using HorusMobile.Models;
using HorusMobile.Views;
using System.Linq;
using Xamarin.Forms.Internals;

namespace HorusMobile.ViewModels
{
    [Preserve(AllMembers = true)]
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<Item> ReadItems { get; set; }
        public Command LoadItemsCommand { get; set; }

        [Preserve(AllMembers = true)]
        public ItemsViewModel()
        {
            Title = "Horus Mobile";
            Items = new ObservableCollection<Item>();
            ReadItems = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", (obj, item) =>
            {                
                //var newItem = item as Item;

                //Items.Add(newItem); 
                //await DataStore.AddItemAsync(newItem);
                //await DataStore.AddItemAsync(newItem);
            });

        }
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ReadItems.Clear();
                Items.Clear();
                Debug.WriteLine("\n\n**************EJECUTANDO GETNOTIFSASYNC*************\n\n");
                var objeto = await DataStoreNotifications.GetNotifsAsync(false);
                //var items = await DataStore.GetItemsAsync(true);

                if (objeto == null || !objeto.Any())
                {
                    /*VERIFICAR SI NO EXISTEN NOTIFICACIONES Y MOSTRAR UN MENSAJE QUE LO EXPLIQUE*/
                    Debug.WriteLine("No hay notificaciones");
                }
                else
                {
                    foreach (var item in objeto)
                    {
                        if (item.estado == 0)
                            Items.Add(item);
                        else
                            ReadItems.Add(item);
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