using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BankApp.Models;

public class TransactionsDTO
{
    public int Id { get; set; }
    [DisplayName("Source account number")]
    public int SourceId { get; set; }

    [DisplayName("Source account number")]
    public virtual Account? Source { get; set; }
    
    [DisplayName("Recipient account number")]
    public string RecipientNumber { get; set; }
    
    [Range(0, float.MaxValue)]
    
    [DisplayName("Amount")]
    public float Value { get; set; }
}