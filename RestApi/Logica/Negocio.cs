using System;
using System.Collections.Generic;
using System.Text;

namespace Logica
{
    public class Negocio
    {
        private static Entidades.Solution solution = new Entidades.Solution(0);

        /// <summary>
        /// Método para actualizar la matriz 
        /// </summary>
        /// <param name="x">valor del eje x</param>
        /// <param name="y">valor del eje y</param>
        /// <param name="z">valor del eje z</param>
        /// <param name="value">valor de actulizacion</param>
        public static long update(int x, int y, int z, int value, int dimensions)
        {
            solution = new Entidades.Solution(dimensions);
            long delta = value - solution.nums[x, y, z];
            solution.nums[x, y, z] = value;
            for (int i = x + 1; i <= dimensions; i += i & (-i))
            {
                for (int j = y + 1; j <= dimensions; j += j & (-j))
                {
                    for (int k = z + 1; k <= dimensions; k += k & (-k))
                    {
                        solution.tree[i, j, k] += delta;
                    }
                }
            }
            return delta;
        }

        public static long query(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            long result = sum(x2 + 1, y2 + 1, z2 + 1)
                - sum(x1, y1, z1) 
                - sum(x1, y2 + 1, z2 + 1) 
                - sum(x2 + 1, y1, z2 + 1) 
                - sum(x2 + 1, y2 + 1, z1) 
                + sum(x1, y1, z2 + 1) 
                + sum(x1, y2 + 1, z1) 
                + sum(x2 + 1, y1, z1);
            return result;
        }

        public static long sum(int x, int y, int z)
        {
            long sum = 0;
            for (int i = x; i > 0; i -= i & (-i))
            {
                for (int j = y; j > 0; j -= j & (-j))
                {
                    for (int k = z; k > 0; k -= k & (-k))
                    {
                        sum += solution.tree[i,j,k];
                    }
                }
            }
            return sum;
        }

        /// <summary>
        /// Se genera una division de la matriz para 
        /// generar la sumatoria por submatrices
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="aux"></param>
        /// <param name="M"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        public static int preProcess(int[,] mat, int[,] aux, int M, int N)
        {
            // Copy first row of mat[][] to aux[][] 
            for (int i = 0; i < N; i++)
                aux[0, i] = mat[0, i];

            // Do column wise sum 
            for (int i = 1; i < M; i++)
                for (int j = 0; j < N; j++)
                    aux[i, j] = mat[i, j] + aux[i - 1, j];

            // Do row wise sum 
            for (int i = 0; i < M; i++)
                for (int j = 1; j < N; j++)
                    aux[i, j] += aux[i, j - 1];

            return 0;
        }


        /// <summary>
        /// Función que genera 
        /// </summary>
        /// <param name="aux"></param>
        /// <param name="tli"></param>
        /// <param name="tlj"></param>
        /// <param name="rbi"></param>
        /// <param name="rbj"></param>
        /// <returns></returns>
        public static int sumQuery(int[,] aux, int tli,
                       int tlj, int rbi, int rbj)
        {
            // result is now sum of elements  
            // between (0, 0) and (rbi, rbj) 
            int res = aux[rbi, rbj];

            // Remove elements between (0, 0)  
            // and (tli-1, rbj) 
            if (tli > 0)
                res = res - aux[tli - 1, rbj];

            // Remove elements between (0, 0)  
            // and (rbi, tlj-1) 
            if (tlj > 0)
                res = res - aux[rbi, tlj - 1];

            // Add aux[tli-1][tlj-1] as elements  
            // between (0, 0) and (tli-1, tlj-1)  
            // are subtracted twice 
            if (tli > 0 && tlj > 0)
                res = res + aux[tli - 1, tlj - 1];

            return res;
        }
    }
}