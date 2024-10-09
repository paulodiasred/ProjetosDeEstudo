using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projeto_app_transferencia_bancaria;
using Microsoft.Data.SqlClient;

namespace projeto_app_transferencia_bancaria
{
    class Program
    {
        static string connectionString = "Server=localhost\\sqlexpress;Initial Catalog=ContaBancariaDB;Integrated Security=True;TrustServerCertificate=True;";
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarContas();
                        break;
                    case "2":
                        InserirConta();
                        break;
                    case "3":
                        Transferir();
                        break;
                    case "4":
                        Sacar();
                        break;
                    case "5":
                        Depositar();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Obrigado por utilizar nosso serviços.");
            Console.ReadLine();
        }

        private static void ListarContas()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Contas";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TipoConta tipoConta = (TipoConta)reader.GetInt32(reader.GetOrdinal("Tipo"));
                            Console.WriteLine($"Conta: {reader["NumeroConta"]}, Titular: {reader["Titular"]}, Saldo: {reader["Saldo"]}, Tipo: {tipoConta}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao listar contas: " + ex.Message);
                }
            }
        }

        private static void InserirConta()
        {
            Console.WriteLine("Informe o número da conta:");
            string numeroConta = Console.ReadLine();

            Console.WriteLine("Informe o titular da conta:");
            string titular = Console.ReadLine();

            Console.WriteLine("Informe o saldo inicial:");
            decimal saldo = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Informe o tipo de conta (1 para Pessoa Física, 2 para Pessoa Jurídica):");
            int tipoConta;
            while (!int.TryParse(Console.ReadLine(), out tipoConta) || (tipoConta != 1 && tipoConta != 2))
            {
                Console.WriteLine("Por favor, insira um tipo válido (1 ou 2):");
            }
            TipoConta tipo = (TipoConta)tipoConta;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Contas (NumeroConta, Titular, Saldo, Tipo) VALUES (@NumeroConta, @Titular, @Saldo, @Tipo)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@NumeroConta", numeroConta);
                    cmd.Parameters.AddWithValue("@Titular", titular);
                    cmd.Parameters.AddWithValue("@Saldo", saldo);
                    cmd.Parameters.AddWithValue("@Tipo", (int)tipo);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Conta inserida com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao inserir conta: " + ex.Message);
                }
            }
        }

        private static void Transferir()
        {
            Console.WriteLine("Informe o número da conta de origem:");
            string contaOrigem = Console.ReadLine();

            Console.WriteLine("Informe o número da conta de destino:");
            string contaDestino = Console.ReadLine();

            Console.WriteLine("Informe o valor da transferência:");
            decimal valor = decimal.Parse(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"
                        UPDATE Contas SET Saldo = Saldo - @Valor WHERE NumeroConta = @ContaOrigem;
                        UPDATE Contas SET Saldo = Saldo + @Valor WHERE NumeroConta = @ContaDestino;
                    ";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Valor", valor);
                    cmd.Parameters.AddWithValue("@ContaOrigem", contaOrigem);
                    cmd.Parameters.AddWithValue("@ContaDestino", contaDestino);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Transferência realizada com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao realizar transferência: " + ex.Message);
                }
            }
        }

        private static void Sacar()
        {
            Console.WriteLine("Informe o número da conta:");
            string numeroConta = Console.ReadLine();

            Console.WriteLine("Informe o valor do saque:");
            decimal valor = decimal.Parse(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "UPDATE Contas SET Saldo = Saldo - @Valor WHERE NumeroConta = @NumeroConta";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Valor", valor);
                    cmd.Parameters.AddWithValue("@NumeroConta", numeroConta);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Saque realizado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao realizar saque: " + ex.Message);
                }
            }
        }

        private static void Depositar()
        {
            Console.WriteLine("Informe o número da conta:");
            string numeroConta = Console.ReadLine();

            Console.WriteLine("Informe o valor do depósito:");
            decimal valor = decimal.Parse(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "UPDATE Contas SET Saldo = Saldo + @Valor WHERE NumeroConta = @NumeroConta";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Valor", valor);
                    cmd.Parameters.AddWithValue("@NumeroConta", numeroConta);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Depósito realizado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao realizar depósito: " + ex.Message);
                }
            }
        }
        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("Red Bank a seu dispor!!!");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1- Listar contas");
            Console.WriteLine("2- Inserir nova conta");
            Console.WriteLine("3- Transferir");
            Console.WriteLine("4- Sacar");
            Console.WriteLine("5- Depositar");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine("");

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}