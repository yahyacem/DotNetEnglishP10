using Mediscreen.PatientAPI.Controllers;
using Mediscreen.PatientAPI.Repositories;
using Mediscreen.PatientAPI.Services;
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

namespace Mediscreen.PatientAPI.Tests.Unit
{
    public class PatientAPITests : BaseTest
    {
        public PatientAPITests() : base() { }
        [Fact]
        public async void Test_GetAsync_ShouldReturn_ListPatient()
        {
            // Arrange
            // Seed data
            List<Patient> patients = SeedData.GetPatients(10);

            // Mock patients repository
            Mock<IPatientsRepository> mockPatientsRepository = new();
            mockPatientsRepository.Setup(x => x.GetAsync()).ReturnsAsync(patients);

            IPatientsService patientsService = new PatientsService(mockPatientsRepository.Object);
            PatientsController patientsController = new(patientsService);

            // Act
            var result = await patientsController.Get();

            // Assert
            Assert.IsType<List<Patient>>(result);
            Assert.Equal(patients, result);
        }
        [Fact]
        public async void Test_GetAsync_ShouldReturn_Patient()
        {
            // Arrange
            // Seed data
            Patient patient = SeedData.GetPatient();

            // Mock patients repository
            Mock<IPatientsRepository> mockPatientsRepository = new();
            mockPatientsRepository.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(patient);

            IPatientsService patientsService = new PatientsService(mockPatientsRepository.Object);
            PatientsController patientsController = new(patientsService);

            // Act
            var result = await patientsController.Get(patient.Id!);

            // Assert
            Assert.IsType<Patient>(result.Value);
            Assert.Equal(patient, result.Value);
        }
        [Fact]
        public async void Test_GetAsync_ShouldReturn_NotFound()
        {
            // Arrange
            // Seed data
            Patient patient = SeedData.GetPatient();

            // Mock patients repository
            Mock<IPatientsRepository> mockPatientsRepository = new();
            mockPatientsRepository.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync((Patient)null!);

            IPatientsService patientsService = new PatientsService(mockPatientsRepository.Object);
            PatientsController patientsController = new(patientsService);

            // Act
            var result = await patientsController.Get(patient.Id!);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public async void Test_CreateAsync_ShouldReturn_CreatedAtAction()
        {
            // Arrange
            // Seed data
            Patient patient = SeedData.GetPatient();

            // Mock patients repository
            Mock<IPatientsRepository> mockPatientsRepository = new();

            // Instantiate service and controller
            IPatientsService patientsService = new PatientsService(mockPatientsRepository.Object);
            PatientsController patientsController = new(patientsService);

            // Act
            var result = await patientsController.Post(patient);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(patient, ((CreatedAtActionResult)result).Value);
        }
        [Fact]
        public async void Test_CreateAsync_ShouldReturn_BadRequest()
        {
            // Arrange
            // Seed data
            Patient patient = new();

            // Mock patients repository
            Mock<IPatientsRepository> mockPatientsRepository = new();

            // Instantiate service and controller
            IPatientsService patientsService = new PatientsService(mockPatientsRepository.Object);
            PatientsController patientsController = new(patientsService);
            patientsController.ModelState.AddModelError("Error", "Error");

            // Act
            var result = await patientsController.Post(patient);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void Test_UpdateAsync_ShouldReturn_NoContent()
        {
            // Arrange
            // Seed data
            Patient patient = SeedData.GetPatient();

            // Mock patients repository
            Mock<IPatientsRepository> mockPatientsRepository = new();
            mockPatientsRepository.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(patient);

            // Instantiate service and controller
            IPatientsService patientsService = new PatientsService(mockPatientsRepository.Object);
            PatientsController patientsController = new(patientsService);

            // Act
            var result = await patientsController.Update(patient.Id!, patient);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async void Test_UpdateAsync_ShouldReturn_BadRequest()
        {
            // Arrange
            // Seed data
            Patient patient = new();

            // Mock patients repository
            Mock<IPatientsRepository> mockPatientsRepository = new();
            mockPatientsRepository.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(patient);

            // Instantiate service and controller
            IPatientsService patientsService = new PatientsService(mockPatientsRepository.Object);
            PatientsController patientsController = new(patientsService);
            patientsController.ModelState.AddModelError("Error", "Error");

            // Act
            var result = await patientsController.Update(patient.Id!, patient);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void Test_UpdateAsync_ShouldReturn_NotFound()
        {
            // Arrange
            // Seed data
            Patient patient = new();

            // Mock patients repository
            Mock<IPatientsRepository> mockPatientsRepository = new();
            mockPatientsRepository.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync((Patient)null!);

            // Instantiate service and controller
            IPatientsService patientsService = new PatientsService(mockPatientsRepository.Object);
            PatientsController patientsController = new(patientsService);

            // Act
            var result = await patientsController.Update(patient.Id!, patient);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async void Test_DeleteAsync_ShouldReturn_NoContent()
        {
            // Arrange
            // Seed data
            Patient patient = SeedData.GetPatient();

            // Mock patients repository
            Mock<IPatientsRepository> mockPatientsRepository = new();
            mockPatientsRepository.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(patient);

            // Instantiate service and controller
            IPatientsService patientsService = new PatientsService(mockPatientsRepository.Object);
            PatientsController patientsController = new(patientsService);

            // Act
            var result = await patientsController.Delete(patient.Id!);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async void Test_DeleteAsync_ShouldReturn_NotFound()
        {
            // Arrange
            // Seed data
            Patient patient = SeedData.GetPatient();

            // Mock patients repository
            Mock<IPatientsRepository> mockPatientsRepository = new();
            mockPatientsRepository.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync((Patient)null!);

            // Instantiate service and controller
            IPatientsService patientsService = new PatientsService(mockPatientsRepository.Object);
            PatientsController patientsController = new(patientsService);

            // Act
            var result = await patientsController.Delete(patient.Id!);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}