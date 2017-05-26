using Nimator;

namespace NimatorCouchBase
{
    public class CheckCouchBaseResult : ICheckResult
    {
        public CheckCouchBaseResult(NotificationLevel pLevel, string pCheckName)
        {
            CheckName = pCheckName;
            Level = pLevel;
        }

        /// <summary>
        ///     Joins <see cref="P:Nimator.ICheckResult.Level" />, <see cref="P:Nimator.ICheckResult.CheckName" />, and any other
        ///     details, in  a readable fashion.
        /// </summary>
        public string RenderPlainText()
        {
            return "";
        }

        /// <summary>
        ///     Repeats the name of the <see cref="T:Nimator.ICheck" />, possibly in a format altered to
        ///     work best for display purposes.
        /// </summary>
        public string CheckName { get; }

        /// <summary>
        ///     The <see cref="T:Nimator.NotificationLevel" /> for this result (e.g. "Okay", or "Error").
        /// </summary>
        public NotificationLevel Level { get; }
    }
}