namespace lab5.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Article { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int? UnitOfMeasureId { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual UnitsOfMeasure? UnitOfMeasure { get; set; }
}
