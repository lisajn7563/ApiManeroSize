namespace ApiManeroSize.Models;

public class SizeRegistration
{
    public string id { get; set; } = Guid.NewGuid().ToString();
    public string SizeTitle { get; set; } = null!;
}
