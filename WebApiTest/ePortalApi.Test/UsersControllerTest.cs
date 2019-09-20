using ApiConnection.ConnectionFactory;
using ApiConnection.Entities;
using ApiConnection.Interfaces;
using ApiConnection.Repositories;
using ePortalApi.Controllers;
using ePortalApi.Interfaces;
using ePortalApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ePortalApi.Test
{
    public class UsersControllerTest  
    {
        UsersController _controller;
        UserServiceTest _service;
  
        public UsersControllerTest()
        {
            _service = new UserServiceTest();
            _controller = new UsersController(_service);
        }
        
        [Fact]
        public void getAll()
        {

           // Act
            var okResult = _controller.GetListUsers();
            // Assert
            Assert.IsType<OkObjectResult> (okResult.Result);

        }

        [Fact]
        public void getById()
        {
            // Act
            var okResult = _controller.GetUser(2);
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);

        }

        //[Fact]
        //public void Delete()
        //{
        //    // Act
        //    var okResult = _controller.Delete(2);
        //    // Assert
        //    Assert.IsType<OkObjectResult>(okResult.Result);

        //}

        [Fact]
        public void Create()
        {
            // Act
            var okResult = _controller.Post(new User()
            {
                Id = 0,
                Name = "Pedro Perez",
                LastName = "Perez",
                Address = "Cantabria"
            });
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);

        }
        [Fact]
        public void Update()
        {
            // Act
            var okResult = _controller.Put(new User()
            {
                Id = 2,
                Name = "Pedro Perez",
                LastName = "Perez",
                Address = "Cantabria"
            });
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);

        }

    }
}
