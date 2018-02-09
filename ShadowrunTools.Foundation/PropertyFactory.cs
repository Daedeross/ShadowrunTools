namespace ShadowrunTools.Foundation
{
    using System.Collections.Generic;
    using System.Linq;
    
    public static class PropertyFactory
    {
        public static IPropertyList CreateFromObject(object obj, bool inherit = false)
        {
            var type = obj.GetType();
            var props = type.GetProperties();

            IPropertyList dict = (IPropertyList)new Dictionary<string, IProperty>(props.Length);

            foreach (var prop in props)
            {
                if (prop.GetCustomAttributes(typeof(DisplayAttribute), inherit).FirstOrDefault() is DisplayAttribute displayAttribute)
                {
                    string label;
                    if (string.IsNullOrWhiteSpace(displayAttribute.Label))
                    {
                        label = TextUtilities.SplitPascalCase(prop.Name);
                    }
                    else
                    {
                        label = displayAttribute.Label;
                    }
                    dict.Add(prop.Name, new Property(label, prop.GetValue(obj), prop.PropertyType, displayAttribute.Editable));
                }
            }

            return dict;
        }
    }
}
