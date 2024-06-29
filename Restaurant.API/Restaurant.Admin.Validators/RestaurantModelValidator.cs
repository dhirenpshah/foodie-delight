using FluentValidation;
using Restaurant.Admin.Business;
using Restaurant.Admin.Models;

namespace Restaurant.Admin.Validators
{
    public class RestaurantModelValidator : AbstractValidator<RestaurantModel>
    {
        private readonly RestaurantService _restaurantService;

        public const string NameRequired = "Name is required";
        public const string LocationRequired = "Location is required";
        public const string NameExists = "Another Restaurant with same name already exists.";

        public RestaurantModelValidator(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;

            RuleFor(_ => _.Name).NotEmpty().WithMessage(NameRequired);
            RuleFor(_ => _.Location).NotEmpty().WithMessage(LocationRequired);
            RuleFor(_ => _.Name).Must(_ => IsNameDuplicate(_))
                .When(_ => !String.IsNullOrEmpty(_.Name))
                .WithMessage(NameExists);
        }

        private bool IsNameDuplicate(string name)
        {
            return _restaurantService.GetAllRestaurants()
                .Any(_ => _.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
