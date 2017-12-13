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

        // GET: Details
        [TestMethod]
        public void DetailsValidId()
        {
            // act
            var actual = (Album)controller.Details(1).Model;

            // assert
            Assert.AreEqual(albums.ToList()[0], actual);
        }

        [TestMethod]
        public void DetailsInvalidId()
        {
            // act
            ViewResult actual = controller.Details(4);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DetailsInvalidNoId()
        {
            // act
            ViewResult actual = controller.Details(null);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        // GET: Create
        [TestMethod]
        public void CreateViewLoads()
        {
            // act
            ViewResult actual = controller.Create();

            // assert
            Assert.AreEqual("Create", actual.ViewName);
        }

        // POST: Create
        [TestMethod]
        public void CreateValid()
        {
            // arrange
            Album album = new Album
            {
                AlbumId = 4,
                Title = "Album 4",
                Price = 44
            };

            // act
            ViewResult actual = controller.Create(album);

            // assert
            Assert.AreEqual("Index", actual.ViewName);
        }

        [TestMethod]
        public void CreateInvalidAlbum()
        {
            // act
            ViewResult actual = controller.Create(null);

            // assert
            Assert.AreEqual("Create", actual.ViewName);
        }

        [TestMethod]
        public void CreateArtistsValid()
        {
            // act
            ViewResult actual = controller.Create(null);

            // assert
            Assert.IsNotNull(actual.ViewBag.ArtistId);
        }

        [TestMethod]
        public void CreateGenresValid()
        {
            // act
            ViewResult actual = controller.Create(null);

            // assert
            Assert.IsNotNull(actual.ViewBag.GenreId);
        }

        // GET: Edit
        [TestMethod]
        public void EditValidId()
        {
            // act
            var actual = (Album)controller.Edit(1).Model;

            // assert
            Assert.AreEqual(albums.ToList()[0], actual);
        }

        [TestMethod]
        public void EditInvalidId()
        {
            // act
            ViewResult actual = controller.Edit(4);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void EditInvalidNoId()
        {
            // arrange
            int? AlbumId = null;

            // act
            ViewResult actual = controller.Edit(AlbumId);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        // POST: Edit
        [TestMethod]
        public void EditValid()
        {
            // arrange
            Album album = new Album
            {
                AlbumId = 1,
                Title = "Album 1",
                Price = 6
            };

            // act
            ViewResult actual = controller.Edit(album);

            // assert
            Assert.AreEqual("Index", actual.ViewName);
        }

        [TestMethod]
        public void EditInvalidAlbum()
        {
            // arrange
            Album album = null;

            // act
            ViewResult actual = controller.Edit(album);

            // assert
            Assert.AreEqual("Edit", actual.ViewName);
        }

        [TestMethod]
        public void EditArtistsValid()
        {
            // arrange
            Album album = null;

            // act
            ViewResult actual = controller.Edit(album);

            // assert
            Assert.IsNotNull(actual.ViewBag.ArtistId);
        }

        [TestMethod]
        public void EditGenresValid()
        {
            // arrange
            Album album = null;

            // act
            ViewResult actual = controller.Edit(album);

            // assert
            Assert.IsNotNull(actual.ViewBag.GenreId);
        }

        // GET: Delete
        [TestMethod]
        public void DeleteValidId()
        {
            // act
            var actual = (Album)controller.Delete(1).Model;

            // assert
            Assert.AreEqual(albums.ToList()[0], actual);
        }

        [TestMethod]
        public void DeleteInvalidId()
        {
            // act
            ViewResult actual = controller.Delete(4);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteInvalidNoId()
        {
            // arrange
            int? id = null;

            // act
            ViewResult actual = controller.Delete(id);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        // POST: DeleteConfirmed
        [TestMethod]
        public void DeleteConfirmedValidId()
        {
            // act
            ViewResult actual = controller.DeleteConfirmed(1);

            // assert
            Assert.AreEqual("Index", actual.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedInvalidId()
        {
            // act
            ViewResult actual = controller.DeleteConfirmed(4);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedInvalidNoId()
        {
            // arrange
            int? id = null;

            // act
            ViewResult actual = controller.DeleteConfirmed(id);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }
    }

}
