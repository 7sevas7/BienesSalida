using Microsoft.JSInterop;

namespace BienesSalida.Client
{
    public class LocalStorageService 
    {
        private readonly IJSRuntime _jsRuntime;
        private IJSObjectReference? _jsModule;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task InitializeAsync()
        {
            _jsModule = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/localStorage.js");
        }

        public async Task SetItemAsync(string key, string value)
        {
            if (_jsModule != null)
                await _jsModule.InvokeVoidAsync("setItem", key, value);
        }

        public async Task<string> GetItemAsync(string key)
        {
            return _jsModule != null
                ? await _jsModule.InvokeAsync<string>("getItem", key)
                : string.Empty;
        }

        public async Task RemoveItemAsync(string key)
        {
            if (_jsModule != null)
                await _jsModule.InvokeVoidAsync("removeItem", key);
        }
        private bool _disposed;

        /*
        public async ValueTask DisposeAsync()
        {
            if (!_disposed && _jsModule != null)
            {
                await _jsModule.DisposeAsync();
                _disposed = true;
            }

        }*/
    }


}
