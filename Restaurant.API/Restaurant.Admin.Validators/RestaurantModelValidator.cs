using FluentValidation;
using Restaurant.Admin.Business;
using Restaurant.Admin.Business.Contracts;
using Restaurant.Admin.Models;

namespace Restaurant.Admin.Validators
{
    public class RestaurantModelValidator : AbstractValidator<RestaurantModel>, IValidator<RestaurantModel>
    {
        private readonly IRestaurantService _restaurantService;

        public const string NameRequired = "Name is required";
        public const string LocationRequired = "Location is required";
        public const string NameExists = "Another Restaurant with same name already exists.";

        public RestaurantModelValidator(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;

            RuleFor(_ => _.Name).NotEmpty().WithMessage(NameRequired);
            RuleFor(_ => _.Location).NotEmpty().WithMessage(LocationRequired);
            RuleFor(_ => _).Must(_ => !IsNameDuplicate(_))
                .When(_ => !String.IsNullOrEmpty(_.Name))
                .WithMessage(NameExists);
        }

        private bool IsNameDuplicate(RestaurantModel restaurant)
        {
            var restaurants = _restaurantService.GetAllRestaurants();
            return _restaurantService.GetAllRestaurants()
                .Any(_ => _.Name.Equals(restaurant.Name, StringComparison.InvariantCultureIgnoreCase)
                && !_.Id.Equals(restaurant.Id));
        }
    }
}
