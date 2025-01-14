using System;
using System.Linq;

namespace Euromilhoes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            // Define o título da janela do console
            Console.Title = "Euromilhões";

            // Exibe uma mensagem de boas-vindas
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("===================================");
            Console.WriteLine("      Bem-vindo ao Euromilhões     ");
            Console.WriteLine("===================================");
            Console.ResetColor();

            // Pede ao utilizador para inserir o número de jogos que deseja jogar
            int numJogos = PedirNumero(1, 6, "Quantos jogos deseja jogar (entre 1 e 6)? ");
            int[][] todasAsApostasNumeros = new int[numJogos][];
            int[][] todasAsApostasEstrelas = new int[numJogos][];

            Random random = new Random();

            // Loop para fazer todas as apostas
            for (int j = 0; j < numJogos; j++)
            {
                // Exibe o número do jogo atual
                Console.WriteLine($"\n{new string('=', 10)} Jogo {j + 1} {new string('=', 10)}");

                // Arrays para armazenar os números e estrelas apostados pelo jogador
                int[] numerosAposta = new int[5];
                int[] estrelasAposta = new int[2];

                // Pede ao jogador para inserir os seus números e estrelas
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Introduza os seus números (1 a 50):");
                Console.ResetColor();
                for (int i = 0; i < numerosAposta.Length; i++)
                {
                    // Pede ao jogador para inserir cada número, garantindo que esteja dentro do intervalo e não seja repetido
                    numerosAposta[i] = PedirNumero(1, 50, $"Número {i + 1}: ", numerosAposta);
                }
                todasAsApostasNumeros[j] = numerosAposta;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Introduza as suas estrelas (1 a 12):");
                Console.ResetColor();
                for (int i = 0; i < estrelasAposta.Length; i++)
                {
                    // Pede ao jogador para inserir cada estrela, garantindo que esteja dentro do intervalo e não seja repetida
                    estrelasAposta[i] = PedirNumero(1, 12, $"Estrela {i + 1}: ", estrelasAposta);
                }
                todasAsApostasEstrelas[j] = estrelasAposta;
            }

            // Sorteia os números e estrelas da chave
            int[] chaveNumeros = new int[5];
            int[] chaveEstrelas = new int[2];

            for (int i = 0; i < chaveNumeros.Length; i++)
            {
                // Gera números aleatórios para a chave, dentro dos limites especificados
                chaveNumeros[i] = random.Next(1, 51); // números de 1 a 50
            }

            for (int i = 0; i < chaveEstrelas.Length; i++)
            {
                // Gera estrelas aleatórias para a chave, dentro dos limites especificados
                chaveEstrelas[i] = random.Next(1, 13); // estrelas de 1 a 12
            }

            // Determina os prêmios
            double PrimeiroPremio = random.Next(5000000, 15000001);
            double SegundoPremio = PrimeiroPremio / 2;
            double TerceiroPremio = SegundoPremio / 4;
            double QuartoPremio = TerceiroPremio / 8;
            double QuintoPremio = QuartoPremio / 16;
            double SextoPremio = QuintoPremio / 32;

            // Exibe os valores dos prêmios
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nValor do 1º Prémio: {PrimeiroPremio:C2}");
            Console.WriteLine($"Valor do 2º Prémio: {SegundoPremio:C2}");
            Console.WriteLine($"Valor do 3º Prémio: {TerceiroPremio:C2}");
            Console.WriteLine($"Valor do 4º Prémio: {QuartoPremio:C2}");
            Console.WriteLine($"Valor do 5º Prémio: {QuintoPremio:C2}");
            Console.WriteLine($"Valor do 6º Prémio: {SextoPremio:C2}");
            Console.ResetColor();

            // Exibe a chave correta
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nA chave certa é números {string.Join(", ", chaveNumeros)} e estrelas {string.Join(", ", chaveEstrelas)}");
            Console.ResetColor();

            // Verifica os acertos e determina o prêmio para todas as apostas
            for (int j = 0; j < numJogos; j++)
            {
                int numerosCertos = VerificarAcertos(todasAsApostasNumeros[j], chaveNumeros);
                int estrelasCertas = VerificarAcertos(todasAsApostasEstrelas[j], chaveEstrelas);

                // Exibe o resultado de cada jogo
                Console.WriteLine($"\n{new string('=', 10)} Resultado do Jogo {j + 1} {new string('=', 10)}");
                DeterminarPremio(numerosCertos, estrelasCertas, chaveNumeros, chaveEstrelas, PrimeiroPremio, SegundoPremio,
                                TerceiroPremio, QuartoPremio, QuintoPremio, SextoPremio);
            }

            Console.ReadLine();
        }

        // Função para pedir um número ao utilizador dentro de um intervalo especificado e verificar se já foi escolhido
        static int PedirNumero(int min, int max, string prompt, int[] numerosEscolhidos = null)
        {
            int numero;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out numero))
                {
                    if (numero >= min && numero <= max && (numerosEscolhidos == null || !numerosEscolhidos.Contains(numero)))
                    {
                        break; // O número está dentro do intervalo especificado e não foi escolhido ainda
                    }
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Por favor, introduza um número entre {min} e {max} que ainda não foi escolhido.");
                Console.ResetColor();
            }
            return numero;
        }

        // Função para verificar acertos
        static int VerificarAcertos(int[] apostas, int[] chave)
        {
            int acertos = 0;
            foreach (int aposta in apostas)
            {
                if (Array.IndexOf(chave, aposta) != -1)
                {
                    acertos++;
                }
            }
            return acertos;
        }

        // Função para determinar o prêmio
        static void DeterminarPremio(int numerosCertos, int estrelasCertas, int[] chaveNumeros, int[] chaveEstrelas,
            double PrimeiroPremio, double SegundoPremio, double TerceiroPremio, double QuartoPremio, double QuintoPremio, double SextoPremio)
        {
            if (numerosCertos == 5 && estrelasCertas == 2)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Parabéns! Ganhou o 1º Prémio!\t Valor do prémio {PrimeiroPremio:C2}");
            }
            else if (numerosCertos == 5 && estrelasCertas == 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Ganhou o 2º Prémio!\t Valor do prémio {SegundoPremio:C2}");
            }
            else if (numerosCertos == 5 && estrelasCertas == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Ganhou o 3º Prémio!\t Valor do prémio {TerceiroPremio:C2}");
            }
            else if (numerosCertos == 4 && estrelasCertas == 2)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Ganhou o 4º Prémio!\t Valor do prémio {QuartoPremio:C2}");
            }
            else if (numerosCertos == 4 && estrelasCertas == 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Ganhou o 5º Prémio!\t Valor do prémio {QuintoPremio:C2}");
            }
            else if (numerosCertos == 4 && estrelasCertas == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Ganhou o 6º Prémio!\t Valor do prémio {SextoPremio:C2}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Não ganhou nenhum prémio. Melhor sorte para a próxima!");
            }
            Console.ResetColor();
        }
    }
}
