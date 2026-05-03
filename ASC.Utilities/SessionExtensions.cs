using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using System.Text;
using System.Text.Json;

namespace ASC.Utilities
{
    public static class SessionExtensions
    {
        /// <summary>
        /// Set object in session
        /// </summary>
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            var jsonString = JsonSerializer.Serialize(value);
            session.Set(key, Encoding.UTF8.GetBytes(jsonString));
        }

        /// <summary>
        /// Get object from session
        /// </summary>
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            if (session.TryGetValue(key, out byte[] value))
            {
                var jsonString = Encoding.UTF8.GetString(value);
                return JsonSerializer.Deserialize<T>(jsonString);
            }
            return default(T);
        }

        /// <summary>
        /// Set string in session
        /// </summary>
        public static void SetStringValue(this ISession session, string key, string value)
        {
            session.Set(key, Encoding.UTF8.GetBytes(value));
        }

        /// <summary>
        /// Get string from session
        /// </summary>
        public static string GetStringValue(this ISession session, string key)
        {
            if (session.TryGetValue(key, out byte[] value))
            {
                return Encoding.UTF8.GetString(value);
            }
            return null;
        }
    }
}
