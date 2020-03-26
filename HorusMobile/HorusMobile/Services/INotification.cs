using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace HorusMobile.Services
{
    [Preserve(AllMembers = true)]
    public interface INotification<T>
    {
        Task<T> GetNotifAsync(string id);
        Task<IEnumerable<T>> GetNotifsAsync(bool forceRefresh = false);
    }
}
