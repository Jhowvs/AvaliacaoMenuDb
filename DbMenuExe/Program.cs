using Dapper;
using System.Data.SqlClient;

namespace DbMenuExe
{
    internal class Program
    {
        public class Genero
        {
            public int Id { get; set; }
            public string? FGenero { get; set; }
        }
        static void Main(string[] args)
        {

            int opcao;
            ExibeMenu();
            Console.Write("Escolha a sua opção: ");
            opcao = int.Parse(Console.ReadLine());



            while (opcao != 5)
            {
                if (opcao == 1)
                {
                    IncluirGenero();
                }
                if (opcao == 2)
                {
                    ListarGenero();
                }
                if (opcao == 3)
                {
                    AlterarGenero();
                }
                if (opcao == 4)
                {
                    ExcluirGenero();
                }
                Console.Write("Escolha opção:  ");
                opcao = int.Parse(Console.ReadLine());
            }



        }

        static string conexao = @"Data Source=(localdb)\MSSQLLocalDB; 
                               Initial Catalog= DbMusica;
                               Integrated Security=True";

        static void ExibeMenu()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("1 - Incluir: ");
            Console.WriteLine("2 - Listar: ");
            Console.WriteLine("3 - Alterar: ");
            Console.WriteLine("4 - Excluir: ");
            Console.WriteLine("5 - Sair: ");
            Console.WriteLine(new string('-', 50));

        }

        static void IncluirGenero()
        {
            string resposta;
            do

            {
                Console.Write("Informe o Genero: ");
                string genero = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(genero))
                {
                    Console.WriteLine("Campo em branco, não foi possivel cadastrar o Genêro da musica");
                    break;
                }

                using (var conn = new SqlConnection(conexao))
                {

                    var registros = conn
                        .Execute("Insert into TBGenero (FGenero) values (@FGenero)",
                        new { FGenero = genero });
                    Console.WriteLine("Registros inseridos: " + registros);
                    Console.Write("Continuar a Inserir? (S/N)");

                }
                resposta = Console.ReadLine();
                Console.WriteLine(resposta);
            } while (resposta == "s");
            ExibeMenu();


        }


        static void ListarGenero()
        {
            using (var conn = new SqlConnection(conexao))
            {
                var Generos = conn.Query<Genero>("Select * from TBGenero");
                foreach (var item in Generos)
                {
                    Console.WriteLine("Id: " + item.Id);
                    Console.WriteLine("Genero: " + item.FGenero);
                    Console.WriteLine("------------------------------------");
                }
                Console.WriteLine("Pressione um tecla para continuar: ");
                Console.ReadKey();
                Console.Clear();

            }

            ExibeMenu();

        }
        static void ExcluirGenero()
        {
            Console.Write("Informe o Código: ");
            int codigo = int.Parse(Console.ReadLine());
            using (var conn = new SqlConnection(conexao))
            {
                var registros = conn.Execute("Delete from TBGenero where Id=@Id",
                    new { Id = codigo });
                Console.WriteLine("Registro Removido: " + registros);
            }

            Console.WriteLine("Pressione uma tecla para continuar");
            Console.ReadKey();
            Console.Clear();
            ExibeMenu();

        }

        static void AlterarGenero()
        {
            //update nome da tabela SET FGenero=@FGenero where Id=@id;
            Console.Write("Informe o Código: ");
            int codigo = int.Parse(Console.ReadLine());

            Console.WriteLine("Informe um novo Genêro");
            string genero = Console.ReadLine();

            using (var conn = new SqlConnection(conexao))
            {
                var registros = conn.Execute("Update TBGenero set FGenero=@FGenero where Id=@id",
                    new { Id = codigo, FGenero = genero });
                Console.WriteLine("registros Alterados: " + registros);

            }

        }
    }
}