using Restaurant.Admin.Models;

namespace Restaurant.Admin.Business.Contracts
{
	public interface IRestaurantService
	{
		List<RestaurantModel> GetAllRestaurants();
		RestaurantModel GetRestaurantById(Guid id);
		RestaurantModel SaveRestaurant(RestaurantModel restaurant);
        Guid DeleteRestaurant(Guid id);
    }
}

