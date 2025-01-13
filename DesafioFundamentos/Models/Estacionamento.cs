using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private Dictionary<string, DateTime> veiculos;

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
            veiculos = new Dictionary<string, DateTime>();
        }

        public void AdicionarVeiculo()
        {
            // TODO: Pedir para o usuário digitar uma placa (ReadLine) e adicionar na lista "veiculos"
            // *IMPLEMENTE AQUI*
            Console.Clear();
            Console.WriteLine("Digite a placa do veículo para estacionar ou pressione ESC para voltar o menu inicial:");
            bool flag = true;

            while (flag)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true); 

                if (keyInfo.Key == ConsoleKey.Escape) 
                {
                    Console.WriteLine("\nReturn to the main menu...");
                    Thread.Sleep(1000);
                    Console.Clear();
                    flag = false; 
                }
                else
                {
                    string input = Console.ReadLine();
                    bool plateValidate = CheckIsValidPlate(input);

                    if (plateValidate)
                    {
                        veiculos.Add(input.ToUpper(), DateTime.Now);
                        Console.WriteLine("Adding vehicle, please await...");
                        Thread.Sleep(1000);
                        Console.WriteLine("Vehicle add successfully!");
                        Thread.Sleep(1000);
                        Console.Clear();
                        flag = false;
                        break;
                    }
                    Console.WriteLine("Invalid plate. Please re-enter a valid plate(XXX1111 / XXX1X11) or press ESC to return main menu.");
                }
            }
            Console.WriteLine("Saiu do loop");
        }

        public void RemoverVeiculo()
        {
            Console.Clear();
            Console.WriteLine("Digite a placa do veículo para remover:");

            // Pedir para o usuário digitar a placa e armazenar na variável placa
            // *IMPLEMENTE AQUI*
            string placa = Console.ReadLine();

            // Verifica se o veículo existe
            //if (veiculos.Any(x => x.ToUpper() == placa.ToUpper()))
            if (veiculos.Any(x => x.Key.ToUpper() == placa.ToUpper()))
            {
                //Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                // TODO: Pedir para o usuário digitar a quantidade de horas que o veículo permaneceu estacionado,
                // TODO: Realizar o seguinte cálculo: "precoInicial + precoPorHora * horas" para a variável valorTotal                
                // *IMPLEMENTE AQUI*
                var horaEntrada = veiculos[placa.ToUpper()];
                int horas = (int)(DateTime.Now - horaEntrada).TotalHours;
                decimal valorTotal = precoInicial + precoPorHora * horas;
                veiculos.Remove(placa.ToUpper());
                // TODO: Remover a placa digitada da lista de veículos
                // *IMPLEMENTE AQUI*

                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}");
                Thread.Sleep(3000);
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
                Thread.Sleep(2000);
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                // TODO: Realizar um laço de repetição, exibindo os veículos estacionados
                // *IMPLEMENTE AQUI*
                Console.Clear();
                Console.WriteLine("Os veículos estacionados são:");
                Console.WriteLine();
                Console.WriteLine("***********");
                foreach (var veiculo in veiculos)
                {
                    Console.WriteLine($"* {veiculo.Key} *");
                }
                Console.WriteLine("***********");
                Console.WriteLine("Press any key to return main menu");
                string exit = "";
                ConsoleKeyInfo keyInfo;
                while (string.IsNullOrEmpty(exit))
                {
                    keyInfo = Console.ReadKey(intercept: true); 
                    exit = keyInfo.KeyChar.ToString();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Não há veículos estacionados.");
                Thread.Sleep(1000);
            }
        }

        public bool CheckIsValidPlate(string plate)
        {
            string pattern = @"^([A-Za-z]{3}\d[A-Za-z]\d{2})|([A-Za-z]{3}\-?\d{4})$";

            return Regex.IsMatch(plate, pattern);
        }
    }
}
