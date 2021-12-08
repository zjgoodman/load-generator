using System.Threading.Tasks;
using System.Collections.Generic;

namespace mParticle.LoadGenerator
{
    public class AsyncUtils
    {
        public static Task<List<T>> FlattenListOfTasks<T>(List<Task<T>> tasks)
        {
            return Task.Run<List<T>>(async () => {
                List<T> list = new List<T>();
                foreach (var task in tasks)
                {
                    T result = await task;
                    list.Add(result);
                }
                return list;
            });
        }
    }
}