using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    /// <summary>
    /// Clase con las propiedades para la salida de la información solicitada
    /// </summary>
    public class Solution
    {
        public long[,,] tree;
        public long[,,] nums;
        private int dimensions = 0;

        public Solution(int dimensions)
        {
            if (dimensions == 0) return;
            this.dimensions = dimensions;
            tree = new long[dimensions + 1, dimensions +1, dimensions + 1];
            nums = new long[dimensions, dimensions, dimensions];
        }
    }
}
