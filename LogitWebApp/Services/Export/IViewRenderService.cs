using System.Threading.Tasks;

namespace LogitWebApp.Services.Export
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}
