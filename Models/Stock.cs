using System;

namespace Site_Cadastro.Models;

public class Stock
{
    public int Id {get; set;}
    public string Product{ get; set; } = string.Empty;

    public double Price { get; set; }
    
    public int Quantities {get; set;}

    public int UserId { get; set; } 

    public User User { get; set; } = null!;
}