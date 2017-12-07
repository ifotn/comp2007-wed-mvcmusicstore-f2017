using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// add ref to web project controllers
using MvcMusicStore_Wed_F2017.Controllers;
using System.Web.Mvc;
using Moq;
using MvcMusicStore_Wed_F2017.Models;
using System.Collections.Generic;
using System.Linq;

namespace MvcMusicStore_Wed_F2017.Tests.Controllers
{

    [TestClass]
    public class StoreManagerControllerTest
    {
        StoreManagerController controller;
        Mock<IStoreManagerRepository> mock;
        Mock<IArtistRepository> artistMock;
        Mock<IGenreRepository> genreMock;

        List<Album> albums;

        [TestInitialize]
        public void TestInitialize()
        {
            // arrange
            mock = new Mock<IStoreManagerRepository>();
            artistMock = new Mock<IArtistRepository>();
            genreMock = new Mock<IGenreRepository>();

            // mock data
            albums = new List<Album>
            {
                new Album { AlbumId = 1, Title = "Album 1", Price = 9, Artist = new Artist { ArtistId = 1, Name = "Artist 1" } },
                new Album { AlbumId = 2, Title = "Album 2", Price = 10, Artist = new Artist { ArtistId = 2, Name = "Artist 2" } },
                new Album { AlbumId = 3, Title = "Album 3", Price = 8, Artist = new Artist { ArtistId = 3, Name = "Artist 3" } }
            };

            // pass the mock data to the mock repo
            mock.Setup(m => m.Albums).Returns(albums.AsQueryable());

            // now instantiate controller and pass it the mock object
            controller = new StoreManagerController(mock.Object);
        }

        [TestMethod]
        public void Index()
        {
            // act
            var actual = (List<Album>)controller.Index().Model;
            
            // assert
            CollectionAssert.AreEqual(albums, actual);

        }

        [TestMethod]
        public void Details()
        {
            // arrange
            Random random = new Random();
            int randomNumber = random.Next(1, 6);

            Console.WriteLine(randomNumber);

            // act
            var actual = (Album)controller.Details(randomNumber).Model;

            // assert
            Assert.AreEqual(albums.ToList()[0], actual);
        }
    }

}
