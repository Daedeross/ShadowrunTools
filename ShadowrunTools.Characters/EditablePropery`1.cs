namespace ShadowrunTools.Characters
{
    public class EditablePropery<TValue>
    {
        private TValue _value;
        private TValue _tempValue;
        private bool _isEditing;

        public TValue Value
        {
            get => _isEditing ? _tempValue : _value;
            set
            {
                if (_isEditing)
                {
                    _tempValue = value;
                }
                else
                {
                    _value = value;
                }
            }
        }
    }
}
