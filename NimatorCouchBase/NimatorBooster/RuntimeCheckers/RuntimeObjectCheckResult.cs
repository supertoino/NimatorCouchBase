using Nimator;

namespace NimatorCouchBase.NimatorBooster.RuntimeCheckers
{
    public class RuntimeObjectCheckResult : IRuntimeObjectCheckResult
    {
        public RuntimeObjectCheckResult(NotificationLevel pLevel, string pCheckName, string pLValidation, bool pLValidationResult)
        {
            CheckName = pCheckName;
            LValidationResult = pLValidationResult;
            LValidation = pLValidation;
            Level = pLevel;
        }

        /// <summary>
        ///     Joins <see cref="P:Nimator.ICheckResult.Level" />, <see cref="P:Nimator.ICheckResult.CheckName" />, and any other
        ///     details, in  a readable fashion.
        /// </summary>
        public virtual string RenderPlainText()
        {
            return $"{Level} in {CheckName}: L Validation({LValidation}) returned {LValidationResult}";
        }

        public bool LValidationResult { get; }

        public string LValidation { get; }

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