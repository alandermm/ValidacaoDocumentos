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
        static Regex rgx = new Regex(@"^\d*$");
        static Regex rgxRG = new Regex(@"^\d+?(x|X|\d)+$");

        static string primeiroDigito, segundoDigito;
        static void Main(string[] args) {

            do {
                Console.WriteLine("Escola uma das opções abaixo\n"
                        + "1 - Validar CPF\n"
                        + "2 - Validar CNPJ\n"
                        + "3 - Validar Cartão de Crétido\n"
                        + "4 - Validar RG\n"
                        + "5 - Validar Título Eleitoral\n"
                        + "0 - Sair\n");
                Console.Write("Opção: ");
                int opt = 0;            
                do{
                    op2 = Console.ReadLine();
                    opt = Int16.Parse(op2);
                } while (opt < 0 || opt > 6);

                switch(op2){
                    case "0": Environment.Exit(0); break;
                    case "1": validarCPF(); break;
                    case "2": validarCNPJ(); break;
                    case "3": validarCredito(); break;
                    case "4": validarRG(); break;
                    case "5": validarTitulo(); break;
                }
            } while(op2 != "0");
        }

        private static void validarTitulo(){
            do{
                Console.Write("Digite o Título Eleitoral: ");
                doc = Console.ReadLine();
                doc = limparCaracteresDocumento(doc);
            } while(doc.Length != 10 || !rgx.IsMatch(doc));

            primeiroDigito = validarDigito(chaveTitulo, 5);
            //Console.WriteLine(primeiroDigito);
            if(primeiroDigito != doc.Substring(8,1)){
                Console.WriteLine("Título Eleitoral inválido!\n");
            } else {
                segundoDigito = validarDigito(chaveTitulo2, 6);
                int uf = Int16.Parse(doc.Substring(6, 2));
                if(doc.EndsWith(segundoDigito) && (uf > 0 && uf <= 28)){
                    Console.WriteLine("Titulo Eleitoral válido!\n");
                } else {
                    Console.WriteLine("Título Eleitoral inválido!\n");
                }
            }
        }

        private static void validarRG(){
            do{
                Console.Write("Digite o RG: ");
                doc = limparCaracteresDocumento(Console.ReadLine());
            } while (doc.Length != 9 || !rgxRG.IsMatch(doc));

            primeiroDigito = validarDigito(chaveRG, 4);
            if(doc.ToUpper().EndsWith(primeiroDigito)){
                Console.Clear();
                Console.WriteLine("RG válido!\n");
            } else{
                Console.Clear();
                Console.WriteLine("RG inválido!\n");
            }
        }

        private static void validarCredito(){
            do{
                Console.Write("Digite o número do Cartão de Crédito: ");
                doc = limparCaracteresDocumento(Console.ReadLine());
            } while (doc.Length != 16 || !rgx.IsMatch(doc));

            primeiroDigito = validarDigito(chaveCredito, 3);

            if(doc.EndsWith(primeiroDigito)){
                Console.WriteLine("Cartão válido!\n");
            } else {
                Console.WriteLine("Cartão inválido!\n");
            }
        }

        private static void validarCNPJ(){
            do{
                Console.Write("Digite seu CNPJ: ");
                doc = limparCaracteresDocumento(Console.ReadLine());
            } while (doc.Length != 14 || !rgx.IsMatch(doc));

            primeiroDigito = validarDigito(chaveCNPJ, 2);

            if(primeiroDigito != doc.Substring(12, 1)){
                Console.WriteLine("CNPJ inválido!\n");
            }else {
                segundoDigito = validarDigito(chaveCNPJ2, 2);
                if(doc.EndsWith(segundoDigito)){
                    Console.WriteLine("CNPJ válido!\n");
                } else {
                    Console.WriteLine("CNPJ inválido!\n");
                }
            }
        }

        private static string validarDigito(int[] chave, int tipoDoc){
            soma = 0;
            resto = 0;

            if(tipoDoc==6){
                tempdoc = doc.Substring(6,chave.Length);
            } else {
                tempdoc = doc.Substring(0, chave.Length);
            }

            for(int i = 0; i < chave.Length; i++){
                if(tipoDoc > 0 && tipoDoc <= 6 && tipoDoc != 3){
                    soma += ((int)Char.GetNumericValue(tempdoc[i]) * chave[i]);
                }else if(tipoDoc == 3){
                    if((int)Char.GetNumericValue(tempdoc[i]) * chave[i] > 9)        
                        soma += ((int)Char.GetNumericValue(tempdoc[i]) * chave[i] - 9);
                    } else {
                        soma += ((int)Char.GetNumericValue(tempdoc[i]) * chave[i]);
                    }
            }
            

            if(tipoDoc > 0 || tipoDoc <= 6 && tipoDoc != 3 ){
                resto = soma % 11;
                if(resto == 0 && (doc.Substring(6,2) == "01" || doc.Substring(6,2) == "02")){
                    return "1";
                } else if((resto < 10 && (tipoDoc == 5 || tipoDoc == 6)) || (resto < 10 && tipoDoc == 4)){
                    return resto.ToString();
                } else if(resto == 10 && (tipoDoc == 5 || tipoDoc == 6) || resto < 2){
                    return "0";
                } else if(resto == 10 && tipoDoc == 4){
                    return "X";
                } else {
                    return (11-resto).ToString();
                }
            } else {
                if(tipoDoc == 3){
                    int somatemp = soma;
                    while(somatemp % 10 != 0){
                        somatemp++;
                    }
                    return (somatemp - soma).ToString();
                } else {
                    return "0";
                }
            }
        }

        private static void validarCPF(){
            do{
                Console.Write("Digite o CPF: ");
                doc = limparCaracteresDocumento(Console.ReadLine());
            } while (doc.Length != 11 || !rgx.IsMatch(doc));

            primeiroDigito = validarDigito(chaveCPF, 1);

            if(primeiroDigito != doc.Substring(9, 1)){
                Console.WriteLine("CPF inválido!\n");
            } else {
                segundoDigito = validarDigito(chaveCPF2, 1);
                if(doc.EndsWith(segundoDigito)){
                    Console.WriteLine("CPF válido!\n");
                } else {
                    Console.WriteLine("CPF inválido!\n");
                }
            }
        }

        private static string limparCaracteresDocumento(string doc){
            return doc.Replace("/","").Replace("-","").Replace(".","");
        }
    }
}