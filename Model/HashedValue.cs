using System.Text;
using App.Helper.Converts;

namespace App.Model
{
    public class HashedValue
    {
        private string _originalValue;
        public string OriginalValue
        {
            get => _originalValue;
            set
            {
                _originalValue = value;
                Value = Converts.ConvertStringToHeshString(value);
            }
        }
        public string Value { get; private set; }

        private HashedValue() { }
        public HashedValue(string originalValue)
        {
            OriginalValue = originalValue;
        }
        public override string ToString()
        {
            return Value;
        }
    }
}
