using BusinessLogic.ParametrObjects;

namespace BusinessLogic.Statics
{
    static class TitleFieldsOperations
    {
        #region public methods
        public static int CountNonNullFields(TitleFields title)
        {
            return CalcAffiliation(title.Name) + CalcAffiliation(title.Tag) + CalcAffiliation(title.Kind);
        }

        public static bool IsEmpty(TitleFields title)
        {
            return CountNonNullFields(title) == 0;
        }

        public static bool IsFull(TitleFields title)
        {
            return CountNonNullFields(title) == 3;
        }
        #endregion

        private static int CalcAffiliation(string input)
        {
            return string.IsNullOrEmpty(input) ? 0 : 1;
        }
    }
}
