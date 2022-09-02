using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Domain.Common
{
    public class BaseEntity
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the LastModificationTime.
        /// </summary>
        /// <value>The LastModificationTime.</value>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// Gets or sets the LastModificationTime.
        /// </summary>
        /// <value>The CreationTime.</value>
        public DateTime? CreationTime { get; set; }
    }
}
