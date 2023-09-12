namespace QueryStress.App.Arguments
{
    public static class ArgumentsExtensions
    {
        public static string ExtractStringArgumentOrThrow(this ArgumentsSection arguments, string name)
        {
            if (arguments.Arguments == null || !arguments.Arguments.TryGetValue(name, out var val))
                throw new ArgumentException($"There is no argument with named '{name}' in {arguments.Type}");
            return val;
        }

        public static int ExtractIntArgumentOrThrow(this ArgumentsSection arguments, string name)
        {
            var val = ExtractStringArgumentOrThrow(arguments, name);
            if (!int.TryParse(val, out var result))
                throw new ArgumentException(
                    $"The value presented as an argument named '{name}' is not a valid integer. The value is '{val}'");
            return result;
        }

        public static TimeSpan ExtractTimeSpanArgumentOrThrow(this ArgumentsSection arguments, string name)
        {
            var val = ExtractStringArgumentOrThrow(arguments, name);
            if (!TimeSpan.TryParse(val, out var result))
                throw new ArgumentException(
                    $"The value presented as an argument named '{name}' is not a valid TimeSpan. The value is '{val}'");
            return result;
        }
    }
}
