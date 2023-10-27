using AutoMapper;
using Mediscreen.Shared.Entities;
using Mediscreen.Shared.Models;
using Mediscreen.WebApp.Models;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Mediscreen.WebApp.Services
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        private readonly string _apiUrl;
        public ApiService(IHttpClientFactory httpClientFactory, IMapper mapper, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
            _apiUrl = configuration["ApiGateway"]!;
        }
        public async Task<List<PatientViewModel>> GetPatientsAsync()
        {
            // Create new HTTP client
            var _httpClient = _httpClientFactory.CreateClient();

            List<Patient> patientEntities = new();

            var response = await _httpClient.GetAsync($"{_apiUrl}/api/patients");
            if (response.IsSuccessStatusCode)
            {
                // Deserialize result
                string apiResponse = await response.Content.ReadAsStringAsync();
                patientEntities = JsonConvert.DeserializeObject<List<Patient>>(apiResponse)!;
            }

            // Map entities to view model
            List<PatientViewModel> patientViewModels = new();
            foreach (Patient patient in patientEntities)
            {
                var p = _mapper.Map<PatientViewModel>(patient);
                patientViewModels.Add(p);
            }

            return patientViewModels;
        }
        public async Task<PatientViewModel?> GetPatientAsync(string id)
        {
            // Create new HTTP client
            var _httpClient = _httpClientFactory.CreateClient();

            Patient patientEntity = new();
            PatientViewModel patientViewModel = null!;

            var patientResponse = await _httpClient.GetAsync($"{_apiUrl}/api/patients/{id}");
            if (patientResponse.IsSuccessStatusCode)
            {
                // Deserialize result
                string apiResponse = await patientResponse.Content.ReadAsStringAsync();
                patientEntity = JsonConvert.DeserializeObject<Patient>(apiResponse)!;

                // Map entity to view model
                patientViewModel = _mapper.Map<PatientViewModel>(patientEntity);
            }

            if (patientViewModel == null)
                return patientViewModel;

            List<Note> historyEntity = new();
            var historyResponse = await _httpClient.GetAsync($"{_apiUrl}/api/history/{id}");
            if (historyResponse.IsSuccessStatusCode)
            {
                // Deserialize result
                string apiResponse = await historyResponse.Content.ReadAsStringAsync();
                patientViewModel.Notes = JsonConvert.DeserializeObject<List<Note>>(apiResponse)!;
            }

            return patientViewModel;
        }
        public async Task<PatientViewModel?> CreatePatientAsync(PatientViewModel patientViewModel)
        {
            // Create new HTTP client
            var _httpClient = _httpClientFactory.CreateClient();

            Patient patientEntity = _mapper.Map<Patient>(patientViewModel);
            patientEntity.Id = null;

            var patientJson = JsonConvert.SerializeObject(patientEntity);
            var requestContent = new StringContent(patientJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_apiUrl}/api/patients", requestContent);
            if (response.IsSuccessStatusCode)
            {
                // Deserialize result
                string apiResponse = await response.Content.ReadAsStringAsync();
                patientEntity = JsonConvert.DeserializeObject<Patient>(apiResponse)!;
            }

            // Map entity to view model
            var patientCreatedViewModel = _mapper.Map<PatientViewModel>(patientEntity);

            return patientCreatedViewModel;
        }
        public async Task<AssessmentModel?> GetAssessmentAsync(string id)
        {
            // Create new HTTP client
            var _httpClient = _httpClientFactory.CreateClient();

            AssessmentModel assessmentModel = new();

            var assessmentResponse = await _httpClient.GetAsync($"{_apiUrl}/api/assessment/{id}");
            if (assessmentResponse.IsSuccessStatusCode)
            {
                // Deserialize result
                string apiResponse = await assessmentResponse.Content.ReadAsStringAsync();
                assessmentModel = JsonConvert.DeserializeObject<AssessmentModel>(apiResponse)!;
            }

            return assessmentModel;
        }
        public async Task<bool> CreateNoteAsync(NoteViewModel newNote)
        {
            // Create new HTTP client
            var _httpClient = _httpClientFactory.CreateClient();

            Note noteEntity = _mapper.Map<Note>(newNote);
            noteEntity.Id = null;

            var noteJson = JsonConvert.SerializeObject(newNote);
            var requestContent = new StringContent(noteJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_apiUrl}/api/history", requestContent);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> EditPatientAsync(PatientViewModel patientViewModel)
        {
            // Create new HTTP client
            var _httpClient = _httpClientFactory.CreateClient();

            Patient patientEntity = _mapper.Map<Patient>(patientViewModel);

            var patientJson = JsonConvert.SerializeObject(patientEntity);
            var requestContent = new StringContent(patientJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_apiUrl}/api/patients/{patientEntity.Id}", requestContent);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
        public async Task<bool> DeletePatientAsync(string id)
        {
            // Create new HTTP client
            var _httpClient = _httpClientFactory.CreateClient();

            var response = await _httpClient.DeleteAsync($"{_apiUrl}/api/patients/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            
            return false;
        }
    }
}