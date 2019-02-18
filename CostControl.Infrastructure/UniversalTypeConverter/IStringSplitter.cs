namespace CostControl.Infrastructure.UniversalTypeConverter
{
    /// <summary>
    /// Defines a splitter for converting a string represenation of a list of values.
    /// </summary>
    public interface IStringSplitter
    {
        /// <summary>
        /// Splits the given string represenation of a list of values.
        /// </summary>
        /// <param name="valueList">String represenation of the list to split.</param>
        /// <returns>A list of the splitted values.</returns>
        string[] Split(string valueList);
    }
}