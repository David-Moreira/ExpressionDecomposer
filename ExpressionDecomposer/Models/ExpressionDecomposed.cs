using System.Collections.Generic;

namespace ExpressionDecomposer.Models
{
    public class ExpressionDecomposed
    {
        public IEnumerable<string> OrderedPropertyNames { get; set; }
        public string PropertyName { get; set; }
        public string Operator { get; set; }

        /// <summary>
        /// Retrieves the accessor path
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            return string.Join(".", OrderedPropertyNames);
        }
    }
}