using System.Threading.Tasks;

namespace BS_Core_WepApp.Services
{
    public interface IJavaScriptService
    {
        Task<dynamic> FileList();
        Task<int> AddNumbers(int x, int y);
        Task<string> Hello(string name);
        Task<string> Goodbye(string name);
      
    }
}
