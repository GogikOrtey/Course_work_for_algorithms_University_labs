using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_07
{
    public class MyDate
    {
        // Класс для АВЛ_2 и ХТ (2е окно)

        public int dd, mm, gg;

        public MyDate() { }
        public MyDate(int d, int m, int g)
        {
            dd = d;
            mm = m;
            gg = g;
        }

        public string RetStringForPrint()
        {
            string str = dd + "." + mm + "." + gg;
            return str;
        }

        public static bool operator >(MyDate c1, MyDate c2)
        {
            if (c1.gg > c2.gg) return true;
            else if (c1.gg == c2.gg)
            {
                if (c1.mm > c2.mm) return true;
                else if (c1.mm == c2.mm)
                {
                    if (c1.dd > c2.dd) return true;
                }
            }
            return false;
        }
        public static bool operator <(MyDate c1, MyDate c2)
        {
            if ((c1 == c2) || (c1 > c2)) return false;
            else return true;
        }

        public static bool operator ==(MyDate c1, MyDate c2)
        {
            if ((c1.gg == c2.gg) && (c1.mm == c2.mm) && (c1.dd == c2.dd)) return true;
            else return false;
        }
        public static bool operator !=(MyDate c1, MyDate c2)
        {
            if (c1 == c2) return false;
            else return true;
        }
    };
}
