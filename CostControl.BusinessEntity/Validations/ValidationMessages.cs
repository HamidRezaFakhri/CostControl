namespace CostControl.BusinessEntity.Validations
{
    public static class ValidationMessages
    {
        public static string CanNotBeEmpty(string name)
        {
            return $"{name} اجباریست!";
        }

        public static string StringLengthRange(string name, int min, int max)
        {
            return $"تعداد کاراکترها {name} باید بیشتر از {min.ToString()} و کمتر از {max.ToString()} باشند!";
        }
    }
}