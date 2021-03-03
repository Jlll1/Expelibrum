namespace Expelibrum.Services
{
    public interface IFileMoverService
    {
        void Move(string source, string[] destination);
    }
}