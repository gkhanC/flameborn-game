using System;
using Sirenix.OdinInspector;

namespace flameborn.Core.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class BoxGroupAttribute : PropertyGroupAttribute
    {
        public string Label { get; private set; }
        public bool ShowLabel { get; private set; }
        public bool CenterLabel { get; private set; }

        public BoxGroupAttribute(string group, bool showLabel = true, bool centerLabel = false, float order = 0)
            : base(group, order)
        {
            this.Label = group;
            this.ShowLabel = showLabel;
            this.CenterLabel = centerLabel;
        }

        protected override void CombineValuesWith(PropertyGroupAttribute other)
        {
           
            var attr = other as BoxGroupAttribute;
          
            if (this.Label == null)
            {
                this.Label = attr.Label;
            }
            
            this.ShowLabel |= attr.ShowLabel;
            this.CenterLabel |= attr.CenterLabel;
        }
    }
}