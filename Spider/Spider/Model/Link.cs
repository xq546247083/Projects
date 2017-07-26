using System;

namespace Spider.Model
{
    /// <summary>
    /// 链接model
    /// </summary>
    public class Link
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Uri
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// Count
        /// </summary>
        public Int32 Count { get; set; }
    }
}
