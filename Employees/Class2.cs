using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Employees
{
    class RepItem
    {
        private String itemName;
        private String itemValue;
        public RepItem(String itN, String iV)
        {
            itemName = itN;
            itemValue = iV;
        }
        public String getItemName()
        {
            return itemName;
        }
        public String getItemValue()
        {
            return itemValue;
        }
    }
}
