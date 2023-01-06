using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core.Models;

public class Basket
{
    public string? Id { get; set; }

    public Basket(string? id)
    {
        Id = id;
    }

    public Basket()
    {
    }

    public List<BasketItem> Items { get; set; } = new List<BasketItem>();
}