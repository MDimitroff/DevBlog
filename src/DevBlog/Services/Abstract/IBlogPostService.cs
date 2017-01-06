using DevBlog.Models;

namespace DevBlog.Services.Abstract
{
    public interface IBlogPostService
    {
        void Save(PostModel model);
    }
}
