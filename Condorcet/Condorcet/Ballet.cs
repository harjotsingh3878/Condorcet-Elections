/*
 * COMMENT Ballet.cs
 * COMMENT Class containing properties to store data
 * COMMENT
 * COMMENT Version 1.0
 * COMMENT   2015.03.13: Created
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condorcet
{
    class Ballet
    {
        List<string[]> ballet = new List<string[]>();

        public List<string[]> Ballets
        {
            get { return ballet; }
            set { ballet = value; }
        }
    }
}
