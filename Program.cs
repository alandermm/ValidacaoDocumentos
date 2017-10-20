using System;
using System.Text.RegularExpressions;

namespace ValidacaoDocumentos
{
    class Program
    {
        static int[] chaveCPF = {10,9,8,7,6,5,4,3,2};
        static int[] chaveCPF2 = {11,10,9,8,7,6,5,4,3,2};
        static int[] chaveCNPJ = {5,4,3,2,9,8,7,6,5,4,3,2};
        static int[] chaveCNPJ2 = {6,5,4,3,2,9,8,7,6,5,4,3,2};
        static int[] chaveCredito;
        static int[] chaveRG;
        static int[] chaveTitulo;
        static int[] chaveTitulo2;
        static string doc, op2;
        static string tempdoc;
        static int soma = 0, resto = 0;
        //static Regex rgx = new Regex(@"^\d*$");
        //static Regex rgxRG = new Regex(@"^\d+?(x|X|\d+$");

        static string primeiroDigito, segundoDigito;
        static void Main(string[] args)
        {
            validarCNPJ();
        }

        /*private static void validarTitulo(){

        }

        private static void validarRG(){

        }

        private static void validarCredito(){

        }*/

        private static void validarCNPJ(){
            Console.Write("Digite o CNPJ: ");
            string cnpj = Console.ReadLine();

            tempdoc = cnpj.Substring(0, 12);

            for(int i = 0; i < 12; i++){
                //Console.Write(soma + " + " + tempdoc[i] + " * " + chaveCNPJ[i] + " = ");
                soma += (tempdoc[i] * chaveCNPJ[i]);
                Console.WriteLine(soma);
            }

            resto = soma % 11;

            if (resto < 2){
                primeiroDigito = "0";
            } else {
                primeiroDigito = resto.ToString();
            }

            soma = 0;

            tempdoc = tempdoc + primeiroDigito;

            for(int i = 0; i < 13; i++){
                soma += (tempdoc[i] * chaveCNPJ2[i]);
            }

            resto = soma % 11;

            if(resto < 2){
                segundoDigito = "0";
            } else {
                segundoDigito = resto.ToString();
            }

            if(cnpj.EndsWith(primeiroDigito + segundoDigito)){
                Console.WriteLine("O CNPJ é Válido!");
            } else {
                Console.WriteLine("O CNPJ é Inválido!");
            }
        }

        /*private static string ValidarDigito(int[] chave, int tipoDoc){
            return null;
        }*/

        private static void ValidarCPF(){

        }
    }
}
