namespace VoltesTechnology.Repositories.Abstract
{
    public interface IFileService
    {
        public Tuple<int, string> SaveImage(IFormFile imagefile);
        public bool DeleteImage(string imageFileName);
    }
}
