using BusinessLayer.Implementations;
using DataLayer.Entities;

namespace BusinessLayer
{
    public class DataManager
    {
        public EFRepository<Post> PostRepository { get; private set; }

        public DataManager(EFRepository<Post> postRepository) 
        {
            PostRepository = postRepository;
        }

    }
}
