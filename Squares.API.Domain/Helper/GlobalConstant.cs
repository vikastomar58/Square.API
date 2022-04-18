namespace Squares.API.Domain.Helper
{
    public static class GlobalConstant
    {
        public static readonly string SuccessCode = "S01";

        public static readonly string ErrorCode = "E01";

        public static readonly string UserExistsMessage = "User already exists.";

        public static readonly string UserNotExistsMessage = "User does not exists.";

        public static readonly string UserCreatedMessage = "User added successfully.";

        public static readonly string CoordinateCreatedMessage = "Points added successfully.";

        public static readonly string CoordinateDeletedMessage = "Points deleted successfully.";

        public static readonly string CoordinateUploadedMessage = "Points uploaded successfully.";

        public static readonly string FileExtensionErrorMessage = "Please upload json file only.";

        public static readonly string CoordinateNotFoundMessage = "Point not found.";
    }
}
