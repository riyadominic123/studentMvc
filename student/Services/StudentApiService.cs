using student.Model;

namespace student.Services
{
    public class StudentApiService
    {
        private readonly HttpClient _httpClient;
        public StudentApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7175/");
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Student>>("api/student");
        }
        public async Task<List<Class>> GetClassesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Class>>("api/class");
        }

        public async Task AddStudentAsync(Student student)
        {
            await _httpClient.PostAsJsonAsync("api/student", student);
        }
    }
}
