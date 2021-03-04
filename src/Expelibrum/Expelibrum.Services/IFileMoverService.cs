namespace Expelibrum.Services
{
    /// <summary>
    /// Defines an interface to move a file 
    /// </summary>
    public interface IFileMoverService
    {
        /// <summary>
        /// Moves the file from source to destination,
        /// while preserving the file extension
        /// </summary>
        /// <param name="source">Current location of the file</param>
        /// <param name="destination">Array that will be combined into single destination path</param>
        void Move(string source, string[] destination);
    }
}