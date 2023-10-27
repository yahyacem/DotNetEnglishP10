using Mediscreen.HistoryAPI.Repositories;
using Mediscreen.Shared.Entities;
using Mediscreen.Tests.Helpers;
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
            Mock<IHistoryRepository> mockhistoryRepository = new();
            mockhistoryRepository.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(notes);

            IPatientsService patientsService = new PatientsService(mockPatientsRepository.Object);
            PatientsController patientsController = new(patientsService);

            // Act
            var result = await patientsController.Get();

            // Assert
            Assert.IsType<List<Patient>>(result);
            Assert.Equal(patients, result);
        }
        [Fact]
        public async void Test_GetAsync_ShouldReturn_NotFound()
        {

        }
        [Fact]
        public async void Test_CreateAsync_ShouldReturn_CreatedAtAction()
        {

        }
        [Fact]
        public async void Test_CreateAsync_ShouldReturn_BadRequest()
        {

        }
        [Fact]
        public async void Test_CreateAsync_ShouldReturn_NotFound()
        {

        }
    }
}
