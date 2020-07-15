using System.Text;

namespace Platform.Model.Runtime.Param
{
    public class SecretModel
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public SecretModel()
        {
        }

        public SecretModel(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append($"{nameof(Name)}: {Name} ");
            builder.Append($"{nameof(Value)}: {Value} ");

            return builder.ToString();
        }
    }
}
