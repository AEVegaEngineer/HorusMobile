using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HorusMobile.Services
{
    public interface INotification<T>
    {
        Task<T> GetNotifAsync(string id);
        Task<IEnumerable<T>> GetNotifsAsync(bool forceRefresh = false);
    }
}
