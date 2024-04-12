using System.Linq;

namespace RPG.Character
{
    public class BlendTrigger
    {
        string mTrigger;
        string[] mConditions;

        public BlendTrigger(string trigger, string[] conditions)
        {
            mTrigger = trigger;
            mConditions = conditions;
        }
        public string CheckORConditionsAndGetTrigger(string trigger)
        {
            string result;
            if (!mConditions.Any(x => x.Equals(trigger)))
            {
                result = trigger;
            }
            else
            {
                result = mTrigger;
            }
            return result;
        }
    }
}

