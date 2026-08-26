using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Stores
{
    public class TagsStore
    {
        public event Action<Tag> TagAdded;
        public event Action<Tag> TagUpdated;
        public event Action<int> TagDeleted;

        // 🔍 ДИАГНОСТИЧЕСКИЙ МЕТОД: количество подписчиков
        public int GetTagAddedSubscriberCount()
        {
            return TagAdded?.GetInvocationList()?.Length ?? 0;
        }

        // 🔍 ДИАГНОСТИЧЕСКИЙ МЕТОД: список подписчиков
        public string GetTagAddedSubscribersInfo()
        {
            var handlers = TagAdded?.GetInvocationList();
            if (handlers == null || handlers.Length == 0)
                return "Нет подписчиков";

            var info = new List<string>();
            foreach (var handler in handlers)
            {
                var target = handler.Target;
                var method = handler.Method;

                string targetInfo = target != null
                    ? $"{target.GetType().Name} (HashCode: {target.GetHashCode()})"
                    : "Статический метод";

                info.Add($"{targetInfo}.{method.Name}");
            }
            return string.Join("; ", info);
        }
        public async Task Add(Tag tag)
        {
            Debug.WriteLine($"🏪 TagsStore.Add вызван для тега '{tag.Name}' (ID: {tag.Id})");
            Debug.WriteLine($"📌 Количество подписчиков до вызова: {GetTagAddedSubscriberCount()}");
            Debug.WriteLine($"📌 Список подписчиков: {GetTagAddedSubscribersInfo()}");
            //Debug.WriteLine($"📌 StackTrace: {Environment.StackTrace}");
            TagAdded?.Invoke(tag);
        }
        public async Task Update(Tag tag)
        {
            TagUpdated?.Invoke(tag);
        }

        public async Task Delete(int id)
        {
            TagDeleted?.Invoke(id);
        }
    }
}
