using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace BankApp.Models;

public class Transactions
{
    public int Id { get; set; }
    
    public int SourceId { get; set; }

    [DisplayName("Source account number")]
    public virtual Account? Source { get; set; }
    
    public int RecipientId { get; set; }

    [DisplayName("Recipient account number")]
    public virtual Account? Recipient { get; set; }
    
    [DisplayName("Amount")]
    public float Value { get; set; }
}