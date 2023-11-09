using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustChores.MobileApp.Models
{
    public class AsyncBindable<TData>
    {

        public event EventHandler<EventArgs> OnBegin;

        SemaphoreSlim fetchLocker = new(1);
        TaskCompletionSource<TData> taskCompletionSource;

        public async Task<TData> FetchAsync()
        {
            await fetchLocker.WaitAsync();
            taskCompletionSource = new();
            OnBegin?.Invoke(this, EventArgs.Empty);

            var res = await taskCompletionSource.Task;
            fetchLocker.Release();
            return res;
        }

        public void NotifyResponse(TData data)
        {
            taskCompletionSource?.TrySetResult(data);
        }

    }
}
