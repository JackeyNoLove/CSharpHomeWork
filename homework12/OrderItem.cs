using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Homework12.Models {
  public class OrderItem {
    private double _amount;
    private double _price;

    public string OrderId { get; set; }
    [Required]
    public string Name { get; set; }

    [Required]
    public double Price {
      get => _price;
      set {
        if (value > 0) {
          _price = value;
        }
        else {
          throw new InvalidDataException("Price can't be negative");
        }
      }
    }

    [Required]
    public double Amount {
      get => _amount;
      set {
        if (value > 0) {
          _amount = value;
        }
        else {
          throw new InvalidDataException("Amount can't be negative");
        }
      }
    }
  }
}