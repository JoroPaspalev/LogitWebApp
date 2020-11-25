namespace WHMS.Services.Common
{
    public interface IHtmlToPdfConverter
    {
        byte[] Convert(string basePath, string htmlCode, string formatType = "A4", string orientationType = "landscape");
    }
}
