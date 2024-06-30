using Autofac.Extras.NSubstitute;
using Restaurant.Admin.Validators;
using FluentValidation.TestHelper;
using Restaurant.Admin.Models;
using NSubstitute;
using FizzWare.NBuilder;
using Restaurant.Admin.Business.Contracts;

namespace Restaurant.Admin.Validator.Tests;

[TestClass]
public class RestaurantModelValidatorTests
{
    private RestaurantModelValidator _restaurantModelValidator;
    private IRestaurantService _restaurantService;
    private AutoSubstitute _mockContainer;

    [TestInitialize]
    public void SetUp()
    {
        _mockContainer = new AutoSubstitute();
        _restaurantModelValidator = _mockContainer.Resolve<RestaurantModelValidator>();
        _restaurantService = _mockContainer.Resolve<IRestaurantService>();
        MockRestaurants();
    }

    [TestMethod]
    public void ValidateName_WithEmptyName_ShouldFail()
    {
        var restaurant = new RestaurantModel();
        var result = _restaurantModelValidator.TestValidate(restaurant);
        result.ShouldHaveValidationErrorFor(_ => _.Name).WithErrorMessage(RestaurantModelValidator.NameRequired);
    }

    [TestMethod]
    public void ValidateName_WithNonEmptyName_ShouldPass()
    {
        var restaurant = new RestaurantModel() { Name = "TestName" };
        var result = _restaurantModelValidator.TestValidate(restaurant);
        result.ShouldNotHaveValidationErrorFor(_ => _.Name);
    }

    [TestMethod]
    public void ValidateName_WithEmptyLocation_ShouldFail()
    {
        var restaurant = new RestaurantModel();
        var result = _restaurantModelValidator.TestValidate(restaurant);
        result.ShouldHaveValidationErrorFor(_ => _.Location).WithErrorMessage(RestaurantModelValidator.LocationRequired);
    }

    [TestMethod]
    public void ValidateName_WithNonEmptyLocation_ShouldPass()
    {
        var restaurant = new RestaurantModel() { Location = "TestLocation" };
        var result = _restaurantModelValidator.TestValidate(restaurant);
        result.ShouldNotHaveValidationErrorFor(_ => _.Location);
    }

    private void MockRestaurants()
    {
        var restaurants = Builder<RestaurantModel>.CreateListOfSize(5).
            TheFirst(1)
                .With(_ => _.Name = "TestDuplicate")
            .Build().ToList();
        _restaurantService.GetAllRestaurants().Returns(restaurants);
    }

    [TestMethod]
    public void ValidateName_WithDuplicateName_ShouldFail()
    { 
        var restaurant = new RestaurantModel() { Name = "TestDuplicate", Location = "Test" };
        var result = _restaurantModelValidator.TestValidate(restaurant);
        result.ShouldHaveValidationErrorFor(_ => _).WithErrorMessage(RestaurantModelValidator.NameExists);
    }

    [TestMethod]
    public void ValidateName_WithUniqueName_ShouldPass()
    {
        var restaurant = new RestaurantModel() { Name = "TestUnique", Location = "Test" };
        var result = _restaurantModelValidator.TestValidate(restaurant);
        result.ShouldNotHaveValidationErrorFor(_ => _);
    }
}
