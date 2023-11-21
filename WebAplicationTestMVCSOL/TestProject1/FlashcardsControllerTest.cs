using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebAplicationTestMVC.Controllers;
using WebAplicationTestMVC.Interface;
using WebAplicationTestMVC.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTestMVCTests
{
    [TestClass]
    public class FlashcardsControllerTests
    {
        private Mock<IFlashcardService> _mockFlashcardService;
        private Mock<IStudySetService> _mockStudySetService;
        private FlashcardsController _controller;

        [TestInitialize]
        public void SetUp()
        {
            _mockFlashcardService = new Mock<IFlashcardService>();
            _mockStudySetService = new Mock<IStudySetService>();
            _controller = new FlashcardsController(_mockFlashcardService.Object, _mockStudySetService.Object);
        }

        [TestMethod]
        public void RandomizedAndSystemCheck_ReturnsViewWithFlashcards()
        {
            var setName = "TestSet";
            var time = 5;
            var flashcards = new List<Flashcard>
            {
                new Flashcard("1", "What is 2+2?", "4", setName),
                new Flashcard("2", "What is the capital of France?", "Paris", setName)
            };

            var flashcardDTOs = new List<FlashcardDTO>
            {
                new FlashcardDTO("1", "What is 2+2?", "4", setName),
                new FlashcardDTO("2", "What is the capital of France?", "Paris", setName)
            };

            _mockFlashcardService.Setup(s => s.GetAllFlashcardsBySetName(setName)).Returns(flashcards);
            _mockFlashcardService.Setup(s => s.FlashcardsToDTOs(flashcards)).Returns(flashcardDTOs);
           
            var result = _controller.RandomizedAndSystemCheck(setName, time) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<FlashcardDTO>));
            var model = result.Model as List<FlashcardDTO>;
            Assert.AreEqual(flashcardDTOs.Count, model.Count);
            for (int i = 0; i < flashcardDTOs.Count; i++)
            {
                Assert.AreEqual(flashcardDTOs[i].Id, model[i].Id);
                Assert.AreEqual(flashcardDTOs[i].Question, model[i].Question);
                Assert.AreEqual(flashcardDTOs[i].Answer, model[i].Answer);
                Assert.AreEqual(flashcardDTOs[i].SetName, model[i].SetName);
            }
        }

        
    }
}
