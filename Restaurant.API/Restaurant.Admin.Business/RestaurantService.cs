using Restaurant.Admin.Business.Contracts;
using Restaurant.Admin.Models;

namespace Restaurant.Admin.Business;

public class RestaurantService: IRestaurantService
{
    private static List<RestaurantModel> _restaurants = new();


    public List<RestaurantModel> GetAllRestaurants()
    {
        return _restaurants;
    }

    public RestaurantModel GetRestaurantById(Guid id)
    {
        var restaurant = _restaurants.FirstOrDefault(_ => _.Id.Equals(id));
        if(restaurant == null)
        {
            throw new Exception($"Restaurant with Id: {id} does not exists");
        }
        return restaurant;
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
        var existingRestaurant = GetRestaurantById(restaurant.Id.Value);
        _restaurants.Remove(existingRestaurant);
        _restaurants.Add(restaurant);
        return restaurant;
    }

    public Guid DeleteRestaurant(Guid id)
    {
        var restaurant = GetRestaurantById(id);
        _restaurants.Remove(restaurant);
        return id;
    }
}

