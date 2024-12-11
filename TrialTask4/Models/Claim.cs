using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TrialTask4.Models;

public class Claim
{
    public int ClaimId { get; set; }
    
    [ValidateNever]
    public DateTime ClaimDate { get; set; }
    
    [ValidateNever]
    public string? UserId { get; set; }
    public string Name { get; set; }
    
    
    [ValidateNever]
    public string? Description { get; set; }
}