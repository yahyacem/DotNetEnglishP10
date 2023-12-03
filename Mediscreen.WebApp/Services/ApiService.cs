using AutoMapper;
using Mediscreen.Shared.Entities;
using Mediscreen.Shared.Models;
using Mediscreen.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Mediscreen.Shared.Services;
using Microsoft.Identity.Client;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.Identity.Web.Resource;
using Azure.Core;
using Microsoft.Extensions.Options;

namespace Mediscreen.WebApp.Services
{
    public class ApiService : IApiService
    {
        private readonly IMapper _mapper;
        private IDownstreamApi _downstreamApi;
        private readonly ConstantsService _constantsService;
        public ApiService(IMapper mapper, IDownstreamApi downstreamApi, ConstantsService constantsService)
        {
            _mapper = mapper;
            _downstreamApi = downstreamApi;
            _constantsService = constantsService;
        }
        public async Task<List<PatientViewModel>> GetPatientsAsync()
        {
            List<Patient> patientEntities = new();

            var response = await _downstreamApi.CallApiForAppAsync("GatewayAPI", options => 
            { 
                options.RelativePath = $"api/patients";
                options.Scopes = new[] { _constantsService.Scopes["PatientAPI"] };
            });
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
            PatientViewModel patientViewModel = null!;

            var patientResponse = await _downstreamApi.CallApiForAppAsync("GatewayAPI", options => 
            {
                options.RelativePath = $"api/patients/{id}";
                options.Scopes = new[] { _constantsService.Scopes["PatientAPI"] };
            });
            if (patientResponse.IsSuccessStatusCode)
            {
                // Deserialize result
                string apiResponse = await patientResponse.Content.ReadAsStringAsync();
                Patient patientEntity = JsonConvert.DeserializeObject<Patient>(apiResponse)!;

                // Map entity to view model
                patientViewModel = _mapper.Map<PatientViewModel>(patientEntity);
            }

            if (patientViewModel == null)
                return patientViewModel;

            var historyResponse = await _downstreamApi.CallApiForAppAsync("GatewayAPI", options => 
            { 
                options.RelativePath = $"api/patients/{id}/history";
                options.Scopes = new[] { _constantsService.Scopes["HistoryAPI"] };
            });
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
            Patient patientEntity = _mapper.Map<Patient>(patientViewModel);
            patientEntity.Id = null;

            var patientJson = JsonConvert.SerializeObject(patientEntity);
            var requestContent = new StringContent(patientJson, Encoding.UTF8, "application/json");

            var response = await _downstreamApi.CallApiForAppAsync("GatewayAPI", options =>
            {
                options.RelativePath = "api/patients";
                options.HttpMethod = "POST";
                options.Scopes = new[] { _constantsService.Scopes["PatientAPI"] };
            }, requestContent);
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
            AssessmentModel assessmentModel = new();

            var assessmentResponse = await _downstreamApi.CallApiForAppAsync("GatewayAPI", options => 
            { 
                options.RelativePath = $"api/patients/{id}/assessment";
                options.Scopes = new[] { _constantsService.Scopes["AssessmentAPI"] };
            });
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
            Note noteEntity = _mapper.Map<Note>(newNote);
            noteEntity.Id = null;

            var noteJson = JsonConvert.SerializeObject(newNote);
            var requestContent = new StringContent(noteJson, Encoding.UTF8, "application/json");

            var response = await _downstreamApi.CallApiForAppAsync("GatewayAPI", options =>
            {
                options.RelativePath = $"api/patients/{newNote.PatientId}/history";
                options.HttpMethod = "POST";
                options.Scopes = new[] { _constantsService.Scopes["HistoryAPI"] };
            }, requestContent);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> EditPatientAsync(PatientViewModel patientViewModel)
        {
            Patient patientEntity = _mapper.Map<Patient>(patientViewModel);

            var patientJson = JsonConvert.SerializeObject(patientEntity);
            var requestContent = new StringContent(patientJson, Encoding.UTF8, "application/json");

            var response = await _downstreamApi.CallApiForAppAsync("GatewayAPI", options =>
            {
                options.RelativePath = $"api/patients/{patientEntity.Id}";
                options.HttpMethod = "PUT";
                options.Scopes = new[] { _constantsService.Scopes["PatientAPI"] };
            }, requestContent);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
        public async Task<bool> DeletePatientAsync(string id)
        {
            var response = await _downstreamApi.CallApiForAppAsync("GatewayAPI", options =>
            {
                options.RelativePath = $"api/patients/{id}";
                options.HttpMethod = "DELETE";
                options.Scopes = new[] { _constantsService.Scopes["PatientAPI"] };
            });
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}