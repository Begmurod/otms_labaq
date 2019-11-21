using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Class
    {
        public static double H { get; set; } //Высота слоя
        public static double Tm0 { get; set; }//Начальная температура материала
        public static double Tg0 { get; set; } //Начальная температура газа, 
        public static double Ug { get; set; } //Скорость газа на свободное сечение шахты
        public static double crteplo { get; set; } //Средняя теплоемкость газа
        public static double rasxod { get; set; } //Расход материалов 
        public static double teploM { get; set; } //Теплоемкость материалов
        public static double Vkof { get; set; } //Объемный коэффициент теплоотдачи
        public static double d { get; set; } //Диаметр аппарата

        public static double  m { get; set; }
        public static double y0 { get; set; }
        public static double e1 { get; set; }
        public static double e2 { get; set; }
        public static double e3 { get; set; }
        public static double d1 { get; set; }
        public static double d2 { get; set; }

        public static double[] T;

        public static double[] t1;
        public static double[] t2;
        /// <summary>
        /// Отношение теплоемкостей потоков
        /// </summary>
        /// <param name="teploM">Теплоемкость материалов</param>
        /// <param name="rasxod">Расход материалов</param>
        /// <param name="Ug">Скорость газа на свободное сечение шахты</param>
        /// <param name="crteplo">Средняя теплоемкость газа</param>
        /// <param name="d">Диаметр аппарата</param>
        /// <returns></returns>
        public static double M ( double teploM, double rasxod, double Ug, double crteplo, double d)
        {
            return Math.Round((teploM * Math.Round(rasxod, 2)) / (Math.PI*Math.Pow(d/2, 2)*Ug*crteplo), 2);
        }
        /// <summary>
        /// Полная относительная высота слоя 
        /// </summary>
        /// <param name="Vkof">Объемный коэффициент теплоотдачи</param>
        /// <param name="H">Высота слоя</param>
        /// <param name="Ug">Скорость газа на свободное сечение шахты</param>
        /// <param name="crteplo">Средняя теплоемкость газа</param>
        /// <returns></returns>
        public static double Y0(  double Vkof, double H, double Ug, double crteplo)
        {
            return Math.Round((Vkof*H)/(Ug*crteplo*1000), 2);
        }

        /// <summary>
        /// Полная относительная высота слоя 
        /// </summary>
        /// <param name="m"></param>
        /// <param name="y0"></param>
        /// <returns></returns>

        public static double E1(double m, double y0)
        {
            return Math.Round((1-m*Math.Exp(((m-1)*y0)/m)), 2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Vkof">Объемный коэффициент теплоотдачи</param>
        /// <param name="Ug">Скорость газа на свободное сечение шахты</param>
        /// <param name="x"></param>
        /// <param name="crteplo">Средняя теплоемкость газа</param>
        /// <returns></returns>
        public static double Y(double Vkof, double Ug, double x, double crteplo)
        {
            return Math.Round((Vkof*x/1000)/(Ug*crteplo),3);
        }

        public static double E2(double m, double y)
        {
            return Math.Round(1-Math.Exp((m-1)*y/m),2);
        }

         public static double E3(double M, double Y)
        {
            return Math.Round(1 - M*Math.Exp(((M-1)*Y) / M), 3);
        }
         public static double D1(double Y0, double E2,double M)
        {
            return Math.Round((E2) / (1 - M * Math.Exp(((M - 1) * Y0) / M)), 3);
        }
         public static double D2(double Y0, double E3,double M)
        {
            return Math.Round((E3) / (1 - M * Math.Exp(((M - 1) * Y0) / M)), 3);
        }
        public static double T1(double tm0, double tg0, double e1)
        {
            return Math.Round(tm0 + (tg0 - tm0) * e1, 3);
        }
        public static double T2(double tm0, double tg0, double e2)
        {
            return Math.Round(tm0 + (tg0 - tm0) * e2, 3);
        }
    }
}
