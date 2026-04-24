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
            _httpClient.DefaultRequestHeaders.Add("X-API-KEY", "MY_SECRET_KEY_123");
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Student>>("api/student");
        }
        public async Task<List<Class>> GetClassesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Class>>("api/class");
        }
        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Student>($"api/student/{id}");
        }
        public async Task UpdateStudentAsync(int id, Student student)
        {
            await _httpClient.PutAsJsonAsync($"api/student/{id}", student);
        }

        public async Task AddStudentAsync(Student student)
        {
            var response = await _httpClient.PostAsJsonAsync("api/student", student);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine("❌ API ERROR: " + error);
                Console.WriteLine("❌ FULL ERROR: " + error);
                throw new Exception(error);
            }
            else
            {
                Console.WriteLine("✅ Student Added Successfully");
            }
        }
        public async Task DeleteStudentAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/student/{id}");
        }
    }
}
