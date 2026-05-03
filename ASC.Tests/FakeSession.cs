using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ASC.Tests
{
    public class FakeSession : ISession
    {
        private readonly Dictionary<string, byte[]> _sessionStorage = new Dictionary<string, byte[]>();

        public string Id => "test-session-id";

        public bool IsAvailable => true;

        public IEnumerable<string> Keys => _sessionStorage.Keys;

        public void Clear()
        {
            _sessionStorage.Clear();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task LoadAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public void Remove(string key)
        {
            if (_sessionStorage.ContainsKey(key))
            {
                _sessionStorage.Remove(key);
            }
        }

        public void Set(string key, byte[] value)
        {
            _sessionStorage[key] = value;
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            return _sessionStorage.TryGetValue(key, out value);
        }
    }
}
