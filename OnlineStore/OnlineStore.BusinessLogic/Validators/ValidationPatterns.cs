namespace OnlineStore.BusinessLogic.Validators
{
    public static class Patterns
    {
        public const string PasswordPattern =
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{5,16}$";

        public const string NamePattern = @"^[a-zA-Z]+$";
    }
}
