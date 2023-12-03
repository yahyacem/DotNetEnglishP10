using Mediscreen.AssessmentAPI.Controllers;
using Mediscreen.AssessmentAPI.Repositories;
using Mediscreen.AssessmentAPI.Services;
using Mediscreen.Shared.Entities;
using Mediscreen.Shared.Helpers;
using Mediscreen.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediscreen.AssessmentAPI.Tests.Unit
{
    public class AssessmentAPITests : BaseTest
    {
        public AssessmentAPITests() : base() { }
        [Fact]
        public async void Test_GetAsync_ShouldReturn_Assessment()
        {
            // Arrange
            List<string> terms = new() { "Test term 1", "Test term 2", "Test term 3" };
            var triggerTerms = SeedData.GetTriggers(terms);

            List<string> noteComments = new() { "A comment containing Test term 1", "A comment containing test TERM 2", "No term here", "Nada here either" };
            List<Note> notes = SeedData.GetNotes(noteComments);

            Patient patient = SeedData.GetPatient();

            // Mock repositories
            Mock<ITriggerTermsRepository> mockTriggerTermsRepository = new();
            mockTriggerTermsRepository.Setup(x => x.GetAsync()).ReturnsAsync(triggerTerms);

            Mock<IHistoryRepository> mockHistoryRepository = new();
            mockHistoryRepository.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(notes);

            Mock<IPatientsRepository> mockPatientsRepository = new();
            mockPatientsRepository.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(patient);

            IPatientsService patientsService = new PatientsService(mockPatientsRepository.Object);
            Mock<ITriggerTermsService> triggerTermsService = new();
            IAssessmentService assessmentService = new AssessmentService(mockTriggerTermsRepository.Object, mockHistoryRepository.Object);
            PatientsController patientsController = new(patientsService, assessmentService);

            // Act
            var result = await patientsController.Get(patient.Id!);

            // Assert
            Assert.IsType<AssessmentModel>(result.Value);
            Assert.Equal(2, result.Value!.TriggersDetected.Count);
        }
        [Fact]
        public async void Test_GetAsync_ShouldReturn_NotFound()
        {
            // Arrange
            // Mock repositories
            Mock<ITriggerTermsRepository> mockTriggerTermsRepository = new();
            mockTriggerTermsRepository.Setup(x => x.GetAsync()).ReturnsAsync(new List<TriggerTerm>());

            Mock<IHistoryRepository> mockHistoryRepository = new();
            mockHistoryRepository.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync((List<Note>)null!);

            Mock<IPatientsRepository> mockPatientsRepository = new();
            mockHistoryRepository.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new List<Note>());



            IPatientsService patientsService = new PatientsService(mockPatientsRepository.Object);
            Mock<ITriggerTermsService> triggerTermsService = new();
            IAssessmentService assessmentService = new AssessmentService(mockTriggerTermsRepository.Object, mockHistoryRepository.Object);
            PatientsController patientsController = new(patientsService, assessmentService);

            // Act
            var result = await patientsController.Get("");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}