namespace CryptoSystems.Models;

public class LaboratoryWork
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ControllerName { get; set; } = null!;

    public string ActionName { get; set; } = null!;
}
