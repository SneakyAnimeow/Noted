using System.ComponentModel.DataAnnotations;

namespace Noted.Extensions; 

/// <summary>
/// Extension methods for strings
/// </summary>
public static class StringExtension {
    /// <summary>
    /// Validates a string value
    /// </summary>
    /// <param name="value"> The string value to validate </param>
    /// <param name="name"> The name of the string value </param>
    /// <param name="minLength"> The minimum length of the string value </param>
    /// <param name="maxLength"> The maximum length of the string value </param>
    /// <exception cref="ValidationException"> Thrown when the string value is null, empty or whitespace or when the string value is too short or too long </exception>
    public static void Validate(this string value, string name, int minLength, int maxLength) {
        if (string.IsNullOrWhiteSpace(value)) throw new ValidationException($"{name} cannot be empty");
        if (value.Length < minLength) throw new ValidationException($"{name} must be at least {minLength} characters long");
        if (value.Length > maxLength) throw new ValidationException($"{name} cannot be longer than {maxLength} characters");
    }
}