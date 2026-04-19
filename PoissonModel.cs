using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelosDiscretosyContinuos
{
    public class PoissonModel
    {

        private readonly double _lambda;
        private readonly Poisson _poisson;

        public PoissonModel(double lambda)
        {
            if (lambda <= 0)
                throw new ArgumentException("Lambda debe ser mayor que 0");

            _lambda = lambda;
            _poisson = new Poisson(lambda);
        }

        
        public double Media => _poisson.Mean;
        public double Varianza => _poisson.Variance;
        public double DesviacionEstandar => _poisson.StdDev;
    
        public double Probabilidad(int x)
        {
            if (x < 0) return 0;
            return _poisson.Probability(x);
        }

       
        public double ProbabilidadRango(int xInicio, int xFinal)
        {
            double prob = 0;
            for (int x = xInicio; x <= xFinal; x++)
            {
                prob += Probabilidad(x);
            }
            return prob;
        }

  
        public List<(int x, double prob, double acumulado)> CalcularRango(int xInicio, int xFinal)
        {
            var lista = new List<(int, double, double)>();
            double acumulado = 0;

            //  x puede ir hasta infinito
            // Pero con λ moderada, a partir de λ + 5√λ es casi 0
            int xMaximo = (int)Math.Ceiling(_lambda + 5 * Math.Sqrt(_lambda));

            for (int x = xInicio; x <= xFinal; x++)
            {
                if (x > xMaximo) break;

                double prob = _poisson.Probability(x);
                acumulado += prob;

                lista.Add((x, prob, acumulado));
            }

            return lista;
        }

        public double CalcularSesgo(int n, double p)
        {

            double q = 1 - p;
            return 3 + (1 - 6 * p * q) / Math.Sqrt(n * p * q);
        }


        public double CalcularCurtosis(int n, double p)
        {
            double q = 1 - p;
            double resta = q - p;
            double raiz = Math.Sqrt(n * p * q);
            double curtosis = resta / raiz;

            return curtosis;
        }
    }
}
