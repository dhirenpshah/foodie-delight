using Restaurant.Admin.Models;

namespace Restaurant.Admin.Business;

public class RestaurantService
{
    private static List<RestaurantModel> _restaurants = GenerateMockRestaurants();

    private static List<RestaurantModel> GenerateMockRestaurants()
    {
        return new()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Description = "The first burger restaurant",
                Location = "Rajkot",
                Name = "Burger Singh"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Description = "The city's largest multi-cuisine Restaurant",
                Location = "Rajkot",
                Name = "Lemon Tree"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Description = "The city's largest multi-cuisine Restaurant",
                Location = "Rajkot",
                Name = "Ravi Palace"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Description = "The city's largest multi-cuisine Restaurant",
                Location = "Surat",
                Name = "Taj Hotel"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Description = "The city's largest multi-cuisine Restaurant",
                Location = "Surat",
                Name = "Flavours Restaurant"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Description = "The city's largest multi-cuisine Restaurant",
                Location = "Surat",
                Name = "The Imperial Palace"
            },
        };
    }


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

