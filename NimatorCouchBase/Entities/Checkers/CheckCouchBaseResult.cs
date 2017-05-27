using System.Collections.Generic;
using Nimator;

namespace NimatorCouchBase.Entities.Checkers
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

    public interface ICheckRule
    {
        List<INotifier> Notifiers { get; }
        ICheckResult ResultToCheck { get; }
    }

    //TODO CheckRules(Result, INotifier[]) -> Given an Business Rule I'll then Notifier this. I'll do a parser for the rules. One level objects. <,>,!=,=,>=,<=
    //TODO Do this notifier https://stackoverflow.com/questions/32260/sending-email-in-net-through-gmail
    //TODO Do a test drive with their console app. 
    //TODO Refactor code   
}