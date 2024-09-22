namespace ConferenceRoomsReservation.Core.Shared;

public record Error(string Code, string Message);

public static class Errors
{
    public static class General
    {
        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $"for Id '{id}'";
            return new("record.not.found", $"Record not found for Id '{forId}'");
        }

        public static Error NotFound(string message)
        {
            return new("item not found", $"{message}");
        }

        public static Error ValueIsInvalid() =>
            new("value.is.invalid", "Value is invalid");

        public static Error ValueIsRequired() =>
            new("value.is.required", "Value is required");

        public static Error InvalidLength(string? name = null)
        {
            var label = name == null ? " " : " " + name + " ";
            return new("invalid.string.length", $"Invalid{label}length");
        }

        public static Error InvalidLengthValue(string? name = null)
        {
            var label = name == null ? " " : " " + name + " ";
            return new("invalid.decimal.length", $"Invalid{label}length");
        }

        public static Error UpdateSuccess() =>
            new("update.success", "Entity updated successfully");

        public static Error DeleteSuccess() =>
            new("delete.success", "Entity deleted successfully");
    }
}
