using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.Business.Business
{
    public interface IBusiness
    {
        int AddNumbers(int firstNumber, int secondNumber);
        string WriteToDatabase();
    }
}
