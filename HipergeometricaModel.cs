using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelosDiscretosyContinuos
{
    internal class HipergeometricaModel
    {
        private readonly int _N; 
        private readonly int _K; 
        private readonly int _n; 

        private readonly Hypergeometric _hiper;

        public HipergeometricaModel(int N, int K, int n)
        {
            _N = N;
            _K = K;
            _n = n;

            _hiper = new Hypergeometric(N, K, n);
        }

      

       
        public double Media()
        {
            return (double)(_n * _K) / _N;
        }

     
        public double DesviacionEstandar()
        {
            double p = (double)_K / _N;
            double q = 1 - p;

            return Math.Sqrt(_n * p * q) *
                   Math.Sqrt((double)(_N - _n) / (_N - 1));
        }


        //public double Sesgo()
        //{
        //    double p = (double)_K / _N;
        //    double q = 1 - p;

        //    return (1 - 2 * p) / Math.Sqrt(_n * p * q);
        //}


        //public double Curtosis()
        //{
        //    double p = (double)_K / _N;
        //    double q = 1 - p;

        //    return (1 - 6 * p * q) / (_n * p * q);
        //}

        public double Curtosis()
        {
            double p = (double)_K / _N;
            double q = 1 - p;
            double resta = q - p;
            double raiz = Math.Sqrt(_n * p * q);
            double curtosis = resta / raiz;

            return curtosis;
        }

        public double Probabilidad(int x)
        {
            return _hiper.Probability(x);
        }


        public double Sesgo()
        {
            double p = (double)_K / _N;
            double q = 1 - p;
            return 3 + (1 - 6 * p * q) / Math.Sqrt(_n * p * q);
        }




        public List<(int x, double prob)> CalcularRango(int xInicio, int xFinal)
        {
            var lista = new List<(int, double)>();

            for (int i = xInicio; i <= xFinal; i++)
            {
                lista.Add((i, _hiper.Probability(i)));
            }

            return lista;
        }

    }
}
