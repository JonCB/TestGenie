namespace TestGenie.Templates
{
    public class ItemSelectionTemplate<TValue> : IGeneratorTemplate<TValue>
    {
        private readonly TValue[] _values;

        public ItemSelectionTemplate(TValue[] values)
        {
            _values = values;
        }

        public void Build(BuildContext context, out TValue subject)
        {
            subject = default(TValue);
        }
    }
}