using Autofac.Extras.NSubstitute;
using Restaurant.Admin.Models;
using FizzWare.NBuilder;
using FluentAssertions;

namespace Restaurant.Admin.Business.Tests;

[TestClass]
public class RestaurantServiceTests
{
    private RestaurantService _restaurantService;

    [TestInitialize]
    public void Setup()
    {
        _restaurantService = new AutoSubstitute().Resolve<RestaurantService>();
    }

    private RestaurantModel MockAndSaveSingleRestaurant()
    {
        var restaurant = Builder<RestaurantModel>.CreateNew().With(_ => _.Id = null).Build();
        return _restaurantService.SaveRestaurant(restaurant);
    }

    [TestMethod]
    public void GetAllRestaurants_WithoutArguments_ShoudReturnAllRestaurants()
    {
        var restaurant = MockAndSaveSingleRestaurant();
        var result = _restaurantService.GetAllRestaurants();

        result.Count.Should().Be(1);
        result.Should().Contain(restaurant);
    }

    [TestMethod]
    public void GetRestaurantById_WithId_ShoudReturnRestaurant()
    {
        var restaurant = MockAndSaveSingleRestaurant();
        var result = _restaurantService.GetRestaurantById(restaurant.Id.Value);

        result.Should().Be(restaurant);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void GetRestaurantById_WithNonExistingId_ShoudThrowException()
    {
        MockAndSaveSingleRestaurant();

        _restaurantService.GetRestaurantById(Guid.NewGuid());
    }

    [TestMethod]
    public void DeleteRestaurant_WithId_ShouldDeleteRestaurant()
    {
        var restaurant = MockAndSaveSingleRestaurant();
        var result = _restaurantService.DeleteRestaurant(restaurant.Id.Value);

        result.Should().Be(restaurant.Id.Value);
    }

    [TestMethod]
    public void SaveRestaurant_WithIdAsNull_ShouldAddRestaurant()
    {
        var restaurant = Builder<RestaurantModel>.CreateNew().With(_ => _.Id = null).Build();
        var result = _restaurantService.SaveRestaurant(restaurant);

        result.Id.Should().NotBeNull();
    }

    [TestMethod]
    public void SaveRestaurant_WithId_ShouldUpdateRestaurant()
    {
        var restaurant = MockAndSaveSingleRestaurant();
        restaurant.Name = "Updated Value";

        var result = _restaurantService.SaveRestaurant(restaurant);

        result.Id.Should().Be(restaurant.Id.Value);
        result.Name.Should().Be("Updated Value");
    }
}
