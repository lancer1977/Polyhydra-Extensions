using System.Threading.Tasks;

namespace PolyhydraGames.Extensions
{
    public static class AsyncAwaitExtensions
    {
        public static void Await(this Task task)
        {
            task.GetAwaiter().GetResult();
        }
    }
}