namespace ebay.Models;

public class Country
{
    private object value1;
    private object value2;

    public Country()
    {
    }

    public Country(object value1, object value2)
    {
        this.value1 = value1;
        this.value2 = value2;
    }

    public int id { get; set; }
    public string? Name { get; set; }
    // public Dictionary<string, string>? CountryList { get; set; }

}
