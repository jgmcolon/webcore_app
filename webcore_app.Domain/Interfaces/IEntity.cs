using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webcore_app.Core.Interfaces
{
    public interface IEntity
    {
        object Id { get; }
        DateTime Created_Date { get; set; }
        DateTime? Modified_Date { get; set; }
        long Created_By { get; set; }
        long Modified_By { get; set; }
        string Version { get; set; }
    }

    public interface IEntity<T> : IEntity
    {
        new T Id { get; set; }
    }
}
