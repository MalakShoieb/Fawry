using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public interface IExpired
    {
        bool IsExpired();

    }
    public interface IShipped
    {
        string GetName();
        double GetWeight();

    }
}
