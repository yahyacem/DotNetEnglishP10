using Mediscreen.HistoryAPI.Controllers;
using Mediscreen.HistoryAPI.Repositories;
using Mediscreen.HistoryAPI.Services;
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

namespace Mediscreen.Tests.API
{
    public class HistoryAPITests : BaseTest
    {
        [Fact]
        public async void Test_GetAsync_ShouldReturn_ListNote()
        {
            // Arrange
            // Seed data
            List<Note> notes = SeedData.GetNotes(10);

            // Mock patients repository
            Mock<IHistoryRepository> mockHistoryRepository = new();
            mockHistoryRepository.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(notes);

            Mock<IPatientsRepository> mockPatientsRepository = new();

            IHistoryService historyService = new HistoryService(mockHistoryRepository.Object);
            IPatientsService patientsService = new PatientsService(mockPatientsRepository.Object);
            HistoryController historyController = new(historyService, patientsService);

            // Act
            var result = await historyController.Get("");

            // Assert
            Assert.IsType<List<Note>>(result.Value);
            Assert.Equal(notes, result.Value);
        }
        [Fact]
        public async void Test_GetAsync_ShouldReturn_NotFound()
        {
            // Arrange
            
            // Mock patients repository
            Mock<IHistoryRepository> mockHistoryRepository = new();
            mockHistoryRepository.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync((List<Note>)null!);

            Mock<IPatientsRepository> mockPatientsRepository = new();

            IHistoryService historyService = new HistoryService(mockHistoryRepository.Object);
            IPatientsService patientsService = new PatientsService(mockPatientsRepository.Object);
            HistoryController historyController = new(historyService, patientsService);

            // Act
            var result = await historyController.Get("");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public async void Test_CreateAsync_ShouldReturn_CreatedAtAction()
        {
            // Arrange
            // Seed data
            Note note = SeedData.GetNote();

            // Mock patients repository
            Mock<IHistoryRepository> mockHistoryRepository = new();

            Mock<IPatientsRepository> mockPatientsRepository = new();
            mockPatientsRepository.Setup(x => x.PatientExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            IHistoryService historyService = new HistoryService(mockHistoryRepository.Object);
            IPatientsService patientsService = new PatientsService(mockPatientsRepository.Object);
            HistoryController historyController = new(historyService, patientsService);

            // Act
            var result = await historyController.Post(note);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(note, ((CreatedAtActionResult)result).Value);
        }
        [Fact]
        public async void Test_CreateAsync_ShouldReturn_BadRequest()
        {
            // Arrange
            // Seed data
            Note note = SeedData.GetNote();

            // Mock patients repository
            Mock<IHistoryRepository> mockHistoryRepository = new();

            Mock<IPatientsRepository> mockPatientsRepository = new();

            IHistoryService historyService = new HistoryService(mockHistoryRepository.Object);
            IPatientsService patientsService = new PatientsService(mockPatientsRepository.Object);
            HistoryController historyController = new(historyService, patientsService);
            historyController.ModelState.AddModelError("Error", "Error");

            // Act
            var result = await historyController.Post(note);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void Test_CreateAsync_ShouldReturn_NotFound()
        {
            // Arrange
            // Seed data
            Note note = SeedData.GetNote();

            // Mock patients repository
            Mock<IHistoryRepository> mockHistoryRepository = new();

            Mock<IPatientsRepository> mockPatientsRepository = new();
            mockPatientsRepository.Setup(x => x.PatientExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

            IHistoryService historyService = new HistoryService(mockHistoryRepository.Object);
            IPatientsService patientsService = new PatientsService(mockPatientsRepository.Object);
            HistoryController historyController = new(historyService, patientsService);

            // Act
            var result = await historyController.Post(note);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}