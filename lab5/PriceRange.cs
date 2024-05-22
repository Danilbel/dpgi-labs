using System.ComponentModel;

namespace lab5;

public class PriceRange : INotifyPropertyChanged, IDataErrorInfo
{
    private decimal? _minPrice;
    private decimal? _maxPrice;

    public decimal? MinPrice
    {
        get => _minPrice;
        set
        {
            if (_minPrice == value) return;
            _minPrice = value;
            OnPropertyChanged(nameof(MinPrice));
        }
    }

    public decimal? MaxPrice
    {
        get => _maxPrice;
        set
        {
            if (_maxPrice == value) return;
            _maxPrice = value;
            OnPropertyChanged(nameof(MaxPrice));
        }
    }

    public string Error => null;

    public string this[string columnName]
    {
        get
        {
            string result = null;
            if (columnName is nameof(MinPrice) or nameof(MaxPrice))
            {
                if (MinPrice.HasValue && MaxPrice.HasValue)
                {
                    if (MinPrice > MaxPrice)
                    {
                        result = "Min Price cannot be greater than Max Price.";
                    }
                }
                if (!MinPrice.HasValue && !MaxPrice.HasValue)
                {
                    result = "Prices should be numeric.";
                }
            }
            return result;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}