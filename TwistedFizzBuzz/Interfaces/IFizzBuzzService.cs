using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwistedFizzBuzz.Library.Interfaces
{
    public interface IFizzBuzzService
    {
        string GetFizzBuzzValue(int number);
        string GetFizzBuzzValue(int number, Dictionary<int, string> customTokens);
    }

}
