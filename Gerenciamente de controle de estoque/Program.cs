using MySql.Data.MySqlClient;
using System.Configuration;
namespace Gerenciamente_de_controle_de_estoque
{
    public class Program
    {
        static void Main(string[] args)
        {
            int opcao = 0;
            
            string entradaquanti = "";
            int quantidadeteste;
            do
            {
                Produto produto1 = new Produto();
                Console.WriteLine("|=====================|");
                Console.WriteLine("|         MENU        |");
                Console.WriteLine("|=====================|");
                Console.WriteLine("| 1 - Cadastrar       |");
                Console.WriteLine("| 2 - Listar Produtos |");
                Console.WriteLine("| 3 - Editar Produtos |");
                Console.WriteLine("| 4 - Exluir Produtos |");
                Console.WriteLine("| 5 - Sair            |");
                Console.WriteLine("|=====================|");
                Console.WriteLine();

                Console.Write("Escolha uma das opções acima ? (1 ao 5) :");
                opcao = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (opcao)
                {
                    case 1:
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("|======================|");
                            Console.WriteLine("| Cadastro de produtos |");
                            Console.WriteLine("|======================|");
                            Console.WriteLine();

                            Console.Write("Digite o nome do produto :");
                            produto1.nome = Console.ReadLine();
                            Console.WriteLine();

                            produto1.quantidade = lerinteiro("Digite a quantidade do produto :");


                            produto1.valor = lervalor("Digite o valor do produto :");
                            Console.WriteLine();

                            produto1.data_venc = lerdata("Digite a data de vencimento (dd/mm/yyyy) :");
                            Console.WriteLine();


                            Inserindo(produto1);

                            

                        }

                        break;
                }


            } while (opcao != 5);

        }
        
        static decimal lervalor(string mensagem)
        {
            decimal testevalor;
            string entradavalor;
            while (true)
            {
                Console.Write(mensagem);
                entradavalor = Console.ReadLine().Trim();

                entradavalor = entradavalor.Replace(',', '.');

                if (decimal.TryParse(entradavalor, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out testevalor) && (testevalor >= 0))
                {
                    
                   return testevalor;
                }
                else
                {
                    Console.WriteLine("Por favor, Digite um valor válido ");
                    Console.WriteLine("Aperte qualquer tecla para continuar !");
                    Console.ReadKey();
                    Console.Clear();
                }

            }

        }

        static int lerinteiro(string mensagem)
        {
            string entradaquanti = "";
            int quantidadeteste;
            while (true)
            {
                Console.Write(mensagem);
                entradaquanti = Console.ReadLine();
                Console.WriteLine();

                if (int.TryParse(entradaquanti, out quantidadeteste) && (quantidadeteste >= 0))
                {
                    return quantidadeteste;
                    

                }
                else
                {
                    Console.WriteLine("Por favor, Digite um número inteiro !");
                    Console.WriteLine("Aperte qualquer tecla para continuar !");
                    Console.ReadKey();
                    Console.Clear();
                }

            }
        }
        static DateTime lerdata(string mensagem)
        {
            string entradadata;
            while (true)
            {
                Console.Write(mensagem);
                entradadata = Console.ReadLine();

                if ((entradadata.Length == 8) && (entradadata.All(char.IsDigit)))
                {
                    entradadata = entradadata.Insert(2, "/").Insert(5, "/");
                    return DateTime.Parse(entradadata);

                }
                else
                {
                    Console.WriteLine("Data inválida. Use o formato dd/MM/yyyy ou digite 8 dígitos (ex: 25082025).");
                    Console.WriteLine("Aperte qualquer tecla para continuar !");
                    Console.ReadKey();
                    Console.Clear();
                }
            }    
        }

        static void Inserindo(Produto produto)
        {
            string conexao = "server=localhost;user=root;password=SuaSenha.;database=Nomedobanco";

            try
            {
                using (MySqlConnection banco = new MySqlConnection(conexao))
                {
                    banco.Open();

                    string tabela = "INSERT INTO Produto (Nome, quantidade, valor ,data_vencimento) VALUES (@nome, @qtd, @valor, @data)";

                    MySqlCommand cmd = new MySqlCommand(tabela, banco);

                    cmd.Parameters.AddWithValue("@nome", produto.nome);
                    cmd.Parameters.AddWithValue("@qtd", produto.quantidade);
                    cmd.Parameters.AddWithValue("@valor", produto.valor);
                    cmd.Parameters.AddWithValue("@data", produto.data_venc);
                    
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Produto inserido com sucesso!");
                    Console.WriteLine();
                    Console.WriteLine("Aperte qualquer tecla para continuar !");
                    Console.ReadKey();


                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
            }   
        }







    }
}
