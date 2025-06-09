using Microsoft.JSInterop;

namespace BienesSalida.Client.Pages
{
    public class LocalStorageService : IAsyncDisposable
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

        public async ValueTask DisposeAsync()
        {
            if (_jsModule != null)
            {
                await _jsModule.DisposeAsync(); // 🔥 Libera el módulo JS cuando ya no se necesita
            }
        }
    }


}
