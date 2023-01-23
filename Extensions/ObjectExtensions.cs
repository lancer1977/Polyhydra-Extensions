using System;
using System.Linq;
using System.Threading.Tasks;

namespace PolyhydraGames.Extensions
{

    public static class ObjectExtensions
    {
        public static T Clone<T>(this object source)
        {
            var returnObject = Activator.CreateInstance<T>();
            returnObject.CopySharedProperties(source);
            return returnObject;
        }
        public static async Task RunAsync<T>(this T item, Action<T> action) where T : class
        {
            if (item != null)
            {
                await Task.Run(() => action(item));
            }
        }

        /// <summary> copy base class instance's property values to this object. </summary>
        public static void CopySharedProperties(this object target, object source)
        {
            if (target == null) throw new NullReferenceException(nameof(target));
            if (source == null) return;
            var targetProperties = target.GetType().GetProperties().Where(i => i.CanWrite).ToArray();
            var sourceProperties = source.GetType().GetProperties();
            foreach (var propertyInfo in sourceProperties)
            {
                var prop = targetProperties.FirstOrDefault(i => i.Name == propertyInfo.Name);

                if (prop != null && prop.PropertyType == propertyInfo.PropertyType)
                {
                    var value = propertyInfo.GetValue(source, null);
                    if (null != value) prop.SetValue(target, value, null);
                }

            }
        }

    }
}