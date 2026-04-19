using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelosDiscretosyContinuos
{
    public class BinomialModel
    {
        public(Binomial binomial, double[] valoresX, double[] probabilidades,
                double probabilidadRango, double acumuladoFinal)
        CalcularProbabilidad(int n, double p, int xInicio, int xFinal)
        {
            var binomial = new Binomial(p, n);

            int tamaño = xFinal - xInicio + 1;
            double[] valoresX = new double[tamaño];
            double[] probabilidades = new double[tamaño];

            double acumuladoTabla = 0;
            double probabilidadRango = 0;

            int index = 0;

            for (int i = xInicio; i <= xFinal; i++)
            {
                double prob = binomial.Probability(i);

                acumuladoTabla += prob;
                probabilidadRango += prob;

                valoresX[index] = i;
                probabilidades[index] = prob;

                index++;
            }

            return (binomial, valoresX, probabilidades, probabilidadRango, acumuladoTabla);
        }

        //public double CalcularCurtosis(int n, double p)
        //{
        //    double q = 1 - p;
        //    return (1 - 6 * p * q) / (n * p * q);
        //}

        public double CalcularCurtosis(int n, double p)
        {
            double q = 1 - p;
            double resta = q - p;
            double raiz = Math.Sqrt(n * p * q);
            double curtosis = resta / raiz;

            return curtosis;
        }

        //public double CalcularSesgo(int n, double p)
        //{
        //    double q = 1 - p;
        //    return (q - p) / Math.Sqrt(n * p * q);
        //}


        public double CalcularSesgo(int n, double p)
        {

            double q = 1 - p;
            return 3 + (1 - 6 * p * q) / Math.Sqrt(n * p * q);
        }
        public double CalcularCorreccionPoblacionFinita(int n, int N)
        {
            return Math.Sqrt((double)(N - n) / (N - 1));
        }
    }
}
