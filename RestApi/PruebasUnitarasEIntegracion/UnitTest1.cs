using NUnit.Framework;
using Logica;

namespace PruebasUnitarasEIntegracion
{
    public class Tests
    {
        private static int M = 4;
        private static int N = 5;

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
      
            int[,] mat = {{1, 2, 3, 4, 6},
                      {5, 3, 8, 1, 2},
                      {4, 6, 7, 5, 5},
                      {2, 4, 8, 9, 4}};

            int[,] aux = new int[M, N];
            
            var update = Logica.Negocio.preProcess(mat, aux, M, N);


            int tli = 2, tlj = 2, rbi = 3, rbj = 4;
            var result = Logica.Negocio.sumQuery(aux, tli, tlj, rbi, rbj);
            Assert.AreEqual(38, result, "El proceso de sumatoria de las submatrices no es el esperado");

            tli = 0; tlj = 0; rbi = 1; rbj = 1;
            var result2 = Logica.Negocio.sumQuery(aux, tli, tlj, rbi, rbj);
            Assert.AreEqual(11, result2, "El proceso de sumatoria de las submatrices no es el esperado");

            tli = 1; tlj = 2; rbi = 3; rbj = 3;
            var result3 = Logica.Negocio.sumQuery(aux, tli, tlj, rbi, rbj);
            Assert.AreEqual(38, result3, "El proceso de sumatoria de las submatrices no es el esperado");
            
            int testcases = 5;
            for (int i = 0; i < testcases; i++)
            {
                int numOperations = 1;
                for (int j = 0; j < numOperations; j++)
                {
                    ///Caso UPDATE 2 2 2 4 makes the cell (2,2,2) = 4
                    //QUERY 1 1 1 3 3 3.As(2, 2, 2) is updated to 4 and the rest are all 0.The answer to this query is 4.
                    var updateResult = Negocio.update(2, 2, 2, 4, 4);
                    Assert.AreEqual(4, updateResult, "Se ha generado un error en la actualizacion");
                    var sumResult= Negocio.query(1, 1, 1, 3, 3, 3);
                    Assert.AreEqual(4, sumResult, "Se ha generado un error en la sumatoria");
                    ///UPDATE 1 1 1 23. updates the cell (1,1,1) to 23. QUERY 2 2 2 4 4 4. Only the cell (1,1,1) and (2,2,2) are non-zero and (1,1,1) is not between (2,2,2) and (4,4,4). So, the answer is 4
                    var updateResult2 = Negocio.update(1, 1, 1, 23, 23);
                    Assert.AreEqual(23, updateResult2, "Se ha generado un error en la actualizacion");
                    var sumResult2 = Negocio.query(1, 1, 1, 3, 3, 3);
                    Assert.AreEqual(27, (sumResult2+4), "Se ha generado un error en la sumatoria");
                }
            }

        }
    }
}