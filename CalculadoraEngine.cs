using System;

namespace CalculadoraAlgebra
{
    public class CalculadoraEngine
    {
        // Aritmética Básica
        public static double Soma(double n1, double n2) => n1 + n2;
        
        public static double Subtracao(double n1, double n2) => n1 - n2;
        
        public static double Multiplicacao(double n1, double n2) => n1 * n2;
        
        public static double Divisao(double n1, double n2)
        {
            if (n2 == 0) throw new ArgumentException("Divisão por zero!");
            return n1 / n2;
        }
        
        public static double Potencia(double n1, double n2) => Math.Pow(n1, n2);
        
        public static double RaizQuadrada(double n)
        {
            if (n < 0) throw new ArgumentException("Raiz de número negativo!");
            return Math.Sqrt(n);
        }
        
        public static double Modulo(double n1, double n2)
        {
            if (n2 == 0) throw new ArgumentException("Resto por zero!");
            return n1 % n2;
        }
        
        public static double PI() => Math.PI;

        // Trigonometria
        public static double Seno(double graus) => Math.Sin(Math.PI * graus / 180);
        
        public static double Cosseno(double graus) => Math.Cos(Math.PI * graus / 180);
        
        public static double Tangente(double graus)
        {
            if ((graus - 90) % 180 == 0)
                throw new ArgumentException("Tangente indefinida!");
            return Math.Tan(Math.PI * graus / 180);
        }
        
        public static double ArcoSeno(double valor)
        {
            if (valor < -1 || valor > 1)
                throw new ArgumentException("Fora do limite [-1, 1]");
            return Math.Asin(valor) * 180 / Math.PI;
        }
        
        public static double ArcoCosseno(double valor)
        {
            if (valor < -1 || valor > 1)
                throw new ArgumentException("Fora do limite [-1, 1]");
            return Math.Acos(valor) * 180 / Math.PI;
        }
        
        public static double ArcoTangente(double valor)
        {
            return Math.Atan(valor) * 180 / Math.PI;
        }

        // Hiperbólicas e Logaritmos
        public static double Sinh(double valor) => Math.Sinh(valor);
        
        public static double Cosh(double valor) => Math.Cosh(valor);
        
        public static double Tanh(double valor) => Math.Tanh(valor);
        
        public static double LogBase10(double valor)
        {
            if (valor <= 0) throw new ArgumentException("Log de número <= 0!");
            return Math.Log10(valor);
        }
        
        public static double LogNatural(double valor)
        {
            if (valor <= 0) throw new ArgumentException("Ln de número <= 0!");
            return Math.Log(valor);
        }
        
        public static double ExpX(double valor) => Math.Exp(valor);

        // Funções Especiais
        public static long Fatorial(int n)
        {
            if (n < 0) throw new ArgumentException("Fatorial apenas para >= 0");
            long resultado = 1;
            for (int i = 2; i <= n; i++)
                resultado *= i;
            return resultado;
        }
        
        public static long Permutacao(int n, int r)
        {
            if (r > n) throw new ArgumentException("r não pode ser maior que n");
            return Fatorial(n) / Fatorial(n - r);
        }
        
        public static long Combinacao(int n, int r)
        {
            if (r > n) throw new ArgumentException("r não pode ser maior que n");
            return Fatorial(n) / (Fatorial(r) * Fatorial(n - r));
        }

        // Conversões Polar <-> Retangular
        public static (double r, double theta) RetangularParaPolar(double x, double y)
        {
            double r = Math.Sqrt(x * x + y * y);
            double theta = Math.Atan2(y, x) * 180 / Math.PI;
            return (r, theta);
        }
        
        public static (double x, double y) PolarParaRetangular(double r, double theta)
        {
            double thetaRad = theta * Math.PI / 180;
            double x = r * Math.Cos(thetaRad);
            double y = r * Math.Sin(thetaRad);
            return (x, y);
        }

        // Equação da Reta
        public class ResultadoReta
        {
            public double CoeficienteAngular { get; set; }
            public double CoeficienteLinear { get; set; }
            public double Distancia { get; set; }
            public bool EhVertical { get; set; }
            public double X { get; set; }
        }

        public static ResultadoReta RetaPorDoisPontos(double x1, double y1, double x2, double y2)
        {
            if (x1 == x2)
            {
                return new ResultadoReta
                {
                    EhVertical = true,
                    X = x1,
                    Distancia = Math.Abs(y2 - y1)
                };
            }

            double m = (y2 - y1) / (x2 - x1);
            double n = y1 - m * x1;
            double distancia = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));

            return new ResultadoReta
            {
                CoeficienteAngular = m,
                CoeficienteLinear = n,
                Distancia = distancia,
                EhVertical = false
            };
        }

        public static ResultadoReta RetaPorPontoECoeficiente(double x1, double y1, double m)
        {
            double n = y1 - m * x1;
            return new ResultadoReta
            {
                CoeficienteAngular = m,
                CoeficienteLinear = n,
                EhVertical = false
            };
        }

        // Circunferência
        public class ResultadoCircunferencia
        {
            public double Centro_X { get; set; }
            public double Centro_Y { get; set; }
            public double Raio { get; set; }
            public double Area { get; set; }
            public double Perimetro { get; set; }
            public bool EhDegenerada { get; set; }
            public bool EhComplexo { get; set; }
        }

        public static ResultadoCircunferencia CircunferenciaPorCentroRaio(double xc, double yc, double r)
        {
            if (r <= 0) throw new ArgumentException("Raio deve ser > 0");

            double area = Math.PI * (r * r);
            double perimetro = 2 * Math.PI * r;

            return new ResultadoCircunferencia
            {
                Centro_X = xc,
                Centro_Y = yc,
                Raio = r,
                Area = area,
                Perimetro = perimetro,
                EhDegenerada = false,
                EhComplexo = false
            };
        }

        public static ResultadoCircunferencia AnalisarEquacaoGeral(double A, double B, double C)
        {
            double xc = -A / 2;
            double yc = -B / 2;
            double rQuadrado = (xc * xc) + (yc * yc) - C;

            if (rQuadrado < 0)
            {
                return new ResultadoCircunferencia { EhComplexo = true };
            }

            if (rQuadrado == 0)
            {
                return new ResultadoCircunferencia
                {
                    Centro_X = xc,
                    Centro_Y = yc,
                    Raio = 0,
                    EhDegenerada = true
                };
            }

            double r = Math.Sqrt(rQuadrado);
            double area = Math.PI * rQuadrado;
            double perimetro = 2 * Math.PI * r;

            return new ResultadoCircunferencia
            {
                Centro_X = xc,
                Centro_Y = yc,
                Raio = r,
                Area = area,
                Perimetro = perimetro,
                EhDegenerada = false,
                EhComplexo = false
            };
        }

        public static object FormatarResultado(double valor)
        {
            if (double.IsNaN(valor) || double.IsInfinity(valor))
                return "Erro: Resultado inválido";
            
            if (valor == Math.Floor(valor))
                return (long)valor;
            
            return Math.Round(valor, 6);
        }
    }
}
