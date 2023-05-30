using Microsoft.AspNetCore.Identity;

namespace BankApp.Models;

public class Account
{
    public int Id { get; set; }
    
    public string UserId { get; set; }
    
    public virtual IdentityUser? User { get; set; }

    public float Balance { get; set; }
    
    public string Number { get; set; }
    
    public void AddBalance(float value)
    {
        Balance += value;
    }

    public void SubBalance(float value)
    {
        Balance -= value;
    }
}