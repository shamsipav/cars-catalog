using CarsCatalogAPI.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;

namespace CarsCatalogAPI.Tests
{
    public class UnitTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UnitTest(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Theory]
        [InlineData("/api/Cars")]
        public async Task GetCarsList_Returns_OK(string url)
        {
            var response = await _client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/Stats")]
        public async Task GetStats_Returns_OK(string url)
        {
            var response = await _client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/Cars", 1)]
        public async Task GetOneCar_Returns_OK(string url, int id)
        {
            var response = await _client.GetAsync(url + '/' + id);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/Cars")]
        public async Task PostCar_Returns_OK(string url)
        {
            Car car = new Car { LicensePlate = "ÀB999C96", Brand = "Brand", Color = "Color", ReleaseYear = DateTime.Now };
            var stringContent = new StringContent(JsonConvert.SerializeObject(car), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, stringContent);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        //[Theory]
        //[InlineData("/api/Cars", 1)]
        //public async Task DeleteCar_Returns_OK(string url, int id)
        //{
        //    var response = await _client.DeleteAsync(url + '/' + id);
        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //}
    }
}