using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mediscreen.WebApp.Models;
using Mediscreen.WebApp.Services;
using Mediscreen.Shared.Entities;

namespace Mediscreen.WebApp.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IApiService _apiService;
        public PatientsController(IApiService apiService)
        {
            _apiService = apiService;
        }
        // GET: Patient
        public async Task<IActionResult> Index()
        {
            var result = await _apiService.GetPatientsAsync();
            return View(result);
        }

        // GET: Patient/Details/{id}
        public async Task<IActionResult> Details(string id)
        {
            var result = await _apiService.GetPatientAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            var assessment = await _apiService.GetAssessmentAsync(id);
            result.Assessment = assessment;

            return View(result);
        }

        // GET: Patient/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patient/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GivenName,FamillyName,DateOfBirth,Sex,HomeAddress,PhoneNumber")] PatientViewModel patientViewModel)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _apiService.CreatePatientAsync(patientViewModel);
            if (result == null)
                return View("Error");

            return Redirect($"/Patients/Details/{result!.Id}");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNote([Bind("NotesRecommendations, PatientId")] NoteViewModel newNote)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(newNote.NotesRecommendations))
                return Redirect($"/Patients/Details/{newNote.PatientId}");

            newNote.Id = null;
            var result = await _apiService.CreateNoteAsync(newNote);

            return result ? Redirect($"/Patients/Details/{newNote.PatientId}") : View("Error");
        }
        // GET: Patient/Edit/{id}
        public async Task<IActionResult> Edit(string id)
        {
            var result = await _apiService.GetPatientAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Patient/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,GivenName,FamillyName,DateOfBirth,Sex,HomeAddress,PhoneNumber")] PatientViewModel patientViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            patientViewModel.Id = id;
            var result = await _apiService.EditPatientAsync(patientViewModel);
            if (!result)
            {
                return View("Error");
            }

            return Redirect($"/Patients/Details/{id}");
        }

        // GET: Patient/Delete/{id}
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _apiService.GetPatientAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Patient/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var result = await _apiService.DeletePatientAsync(id);

            if (!result)
            {
                return View("Error");
            }

            return Redirect("/Patients");
        }

        private bool PatientExists(string id)
        {
            return false;
        }
    }
}
