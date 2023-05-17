using BusinessLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;



namespace TestTaskApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAppController : ControllerBase
    {

        private readonly DataManager dataManager;
        public TestAppController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        [HttpGet("GetPosts")]
        public async Task<IActionResult> GetAllPostsAsync() // Получение всех постов
        {
            await using var postRepository = dataManager.PostRepository;
            return Ok(await postRepository.GetAsync());
        }

        [HttpGet("GetOnePagePosts")]
        public async Task<IActionResult> GetOnePageUsersAsync(int pageNumber, int pageSize) //Get с ПАГИНАЦИЕЙ, возвращающий страницу с №pageNumber
        {

            await using var postRepository = dataManager.PostRepository;
            var users = await postRepository.GetAsync();

            if (users is not null)
            {
                users = users.Skip((pageNumber - 1) * pageSize).Take(pageSize);

                if (pageNumber > users.Count())
                {
                    return BadRequest("Required pages are more then users in DB");
                }

                return Ok(users);
            }

            return BadRequest();
        }

        [HttpPost("InsertPosts")]
        public async Task<IActionResult> InsertPostsAsync()
        {
            await using var postRepository = dataManager.PostRepository;
            var postList = new List<Post>();
            for (int i =0; i < 10000; i++)
            {
                postList.Add(new Post()
                {
                    Title = $"{i} Title",
                });
            }
            await postRepository.InsertRangeAsync(postList.ToArray());

            return Ok();
        }
    }
}
