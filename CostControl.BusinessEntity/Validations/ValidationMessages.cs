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

        public static string Range(string name, int min, int max)
        {
            return $"مقدار {name} باید بیشتر از {min.ToString()} و کمتر از {max.ToString()} باشند!";
        }

        public static string PastDate(string name)
        {
            return $"{name} اجباریست و نمیتواند در گذشته باشد!";
        }

        public static string WrongSequence(string firstName, string secondName)
        {
            return $"{secondName} نمیتواند قبل از {firstName} باشد!";
        }
    }
}