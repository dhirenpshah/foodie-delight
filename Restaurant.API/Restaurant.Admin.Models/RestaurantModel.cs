namespace Restaurant.Admin.Models;

public class RestaurantModel
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
}