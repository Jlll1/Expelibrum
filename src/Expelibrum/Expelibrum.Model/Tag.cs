using System;
using System.Collections.Generic;
using System.Text;

namespace Expelibrum.Model
{
    public struct Tag
    {
        public string TagName { get; }
        public string PropertyName { get; }

        public Tag(string tag, string propertyName)
        {
            TagName = tag;
            PropertyName = propertyName;
        }

        public override string ToString()
        {
            return TagName;
        }
    }
}
