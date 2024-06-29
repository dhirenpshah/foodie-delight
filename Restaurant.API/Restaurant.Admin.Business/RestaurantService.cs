using Restaurant.Admin.Models;

namespace Restaurant.Admin.Business;

public class RestaurantService
{
    private static List<RestaurantModel> _restaurants;

    public List<RestaurantModel> GetAllRestaurants()
    {
        return _restaurants;
    }

    public RestaurantModel GetRestaurantById(Guid id)
    {
        return _restaurants.FirstOrDefault(_ => _.Id.Equals(id));
    }

    public RestaurantModel SaveRestaurant(RestaurantModel restaurant)
    {
        if (restaurant.Id.HasValue)
        {
            return UpdateRestaurant(restaurant);
        }
        return AddRestaurant(restaurant);
    }

    private RestaurantModel AddRestaurant(RestaurantModel restaurant)
    {
        restaurant.Id = Guid.NewGuid();
        _restaurants.Add(restaurant);
        return restaurant;
    }

    private RestaurantModel UpdateRestaurant(RestaurantModel restaurant)
    {
        var index = _restaurants.IndexOf(restaurant);
        _restaurants[index] = restaurant;
        return restaurant;
    }

    public object? DeleteRestaurant(Guid id)
    {
        var restaurant = GetRestaurantById(id);
        _restaurants.Remove(restaurant);
        return restaurant;
    }
}

