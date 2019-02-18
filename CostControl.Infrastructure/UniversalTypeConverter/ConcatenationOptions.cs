using System;

namespace CostControl.Infrastructure.UniversalTypeConverter
{
    /// <summary>
    /// Defines Options for a string concatenation.
    /// </summary>
    [Flags]
    public enum ConcatenationOptions
    {
        /// <summary>
        /// No options are used.
        /// </summary>
        None = 0,

        /// <summary>
        /// Null values are ignored on concatenation.
        /// </summary>
        IgnoreNull = 1,

        /// <summary>
        /// Empty values are ignored on concatenation.
        /// </summary>
        IgnoreEmpty = 2,

        /// <summary>
        /// The default value for concatenations. Same as <see cref="None">ConcatenationOptions.None</see>.
        /// </summary>
        Default = None
    }
}