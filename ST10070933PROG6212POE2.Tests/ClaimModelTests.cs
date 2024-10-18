using Xunit;
using ST10070933PROG6212POE2.Models;
using System.ComponentModel.DataAnnotations;

public class ClaimModelTests
{
    [Fact]
    public void ClaimViewModel_ValidData_ShouldBeValid()
    {
        // Arrange
        var model = new ClaimViewModel
        {
            LecturerId = 1,
            HoursWorked = 10,
            HourlyRate = 20,
            Notes = "Valid claim"
        };

        // Assert
        var isValid = Validator.TryValidateObject(model, new ValidationContext(model), null, true);
        Assert.True(isValid);
    }

    [Fact]
    public void ClaimViewModel_InvalidHours_ShouldReturnValidationError()
    {
        // Arrange
        var model = new ClaimViewModel { HoursWorked = -5 }; // Invalid hours

        // Assert
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(model, new ValidationContext(model), validationResults, true);
        Assert.False(isValid);
        Assert.Contains(validationResults, r => r.ErrorMessage.Contains("Hours must be between 0.1 and 1000"));
    }
}