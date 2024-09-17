namespace Template.Interface
{
    public interface IPdfService
    {
        byte[] GeneratePdfFromHtml(string htmlContent, string password = null);
    }
}
