using MySql.Data.MySqlClient;
using System.Configuration;
namespace Gerenciamente_de_controle_de_estoque
{
    public class Program
    {
        static void Main(string[] args)
        {
            int opcao = 0; // Variavel para navegar no menu 
            
            do
            {
                Console.Clear();
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
                { // Começo do switch 
                    case 1: // Inicio caso 1 
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

                            Console.Write("Deseja Cadastrar mais  produtos ? (s/n) :");
                            string comfirProd = Console.ReadLine().ToUpper();

                            if (comfirProd == "S")
                            {
                                Console.WriteLine("Agurde...");
                                Thread.Sleep(1000);
                                
                            }
                            else if (comfirProd == "N")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Opção invalida. Por favor digite s ou n :");
                                Console.ReadKey();
                            }
                            

                            

                        }

                        break;// final caso 1 

                    case 2: // INICIO CASO 2 
                        Console.Clear();
                        string campo = "";
                        int opcaoBuscar = 0;
                        string valorParaBusc = "";
                        do
                        {
                            Console.WriteLine("|==================|");
                            Console.WriteLine("|  Buscar Produtos |");
                            Console.WriteLine("|==================|");
                            Console.WriteLine("| 1 - Buscar todos |");
                            Console.WriteLine("| 2 - ID           |");
                            Console.WriteLine("| 3 - Nome         |");
                            Console.WriteLine("| 4 - Vencimento   |");
                            Console.WriteLine("| 5 - Voltar       |");
                            Console.WriteLine("|==================|");
                            Console.WriteLine();

                            Console.Write("Escolha uma das opções acima (1 ao 4 ):");
                            opcaoBuscar = int.Parse(Console.ReadLine());

                            switch (opcaoBuscar)
                            {

                                case 1:
                                    
                                    valorParaBusc = "1";
                                    Console.Clear();
                                    BuscarProduto(opcaoBuscar,valorParaBusc);
                                    Console.ReadKey();
                                    

                                    break;

                                case 2:

                                    break;
                                default:
                                    break;
                            }




                        } while (opcaoBuscar != 5);
                        break; // FINAL CASO 2 
                }// Final do swtich 


            } while (opcao != 5);

        }
        
        static decimal lervalor(string mensagem) // Metodo para tratar o erro do valor 
        {
            decimal testevalor; 
            string entradavalor;
            while (true)
            {
                Console.Write(mensagem);    // Recebendo os parametros 
                entradavalor = Console.ReadLine().Trim(); // Tirando os espaços em brancos 

                entradavalor = entradavalor.Replace(',', '.'); // Trocando as vírgulas por pontos 

                if (decimal.TryParse(entradavalor, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out testevalor) && (testevalor >= 0)) // aceitando ponto e virgula com simbolo.
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

                if (int.TryParse(entradaquanti, out quantidadeteste) && (quantidadeteste >= 0)) // validando a quantidade se e numero e inteiro, e não pode ser menor que 0 
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
                Console.Write(mensagem); // pegando os dados do usuario 
                entradadata = Console.ReadLine();

                if ((entradadata.Length == 8) && (entradadata.All(char.IsDigit)))
                {
                    entradadata = entradadata.Insert(2, "/").Insert(5, "/"); // Colocando a barra 
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
            string conexao = "server=localhost;user=root;password=Suasenha.;database=nomedobanco";

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
                    
                    banco.Close();

                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
            }   
        }

        static void BuscarProduto(int opcao, string valor) 
        {
            string conexao = "server=localhost;user=root;password=suasenha@.;database=nomedobanco";
            try
            {
                using (MySqlConnection banco = new MySqlConnection(conexao))
                {
                    banco.Open();
                    string tabela = "";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = banco;

                    if (opcao == 1)
                    {
                        tabela = "SELECT * FROM Produto; ";
                    }


                    cmd.CommandText = tabela;

                    using (MySqlDataReader tela = cmd.ExecuteReader())
                    {
                        bool achou = false;
                        Console.WriteLine("|======================|");
                        Console.WriteLine("| Produtos encontrados |");
                        Console.WriteLine("|======================|");
                        

                        while (tela.Read())
                        {
                            achou = true;
                           

                            Console.WriteLine("ID :{0}", tela["id"]);
                            Console.WriteLine("Nome :{0}", tela["Nome"]);
                            Console.WriteLine("Quantidade :{0}", tela["Quantidade"]);
                            Console.WriteLine("Valor :{0}", tela["valor"]);
                            DateTime data = Convert.ToDateTime(tela["data_vencimento"]);
                            Console.WriteLine("Data de Vencimento : {0:dd/MM/yyyy}", data); // Formatando a data
                     
                            Console.WriteLine("\n");

                            Console.WriteLine();


                        }
                        
                        if (!achou)
                        {
                            Console.WriteLine("Nenhum produto encontrado com esse critério.");
                        }
                            
                            
                    }



                }
            }
            catch (Exception ex)  
            {
                Console.WriteLine("Erro! :{0}",ex.Message);
            }

        }







    }
}
