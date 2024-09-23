using ClienteAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClienteAPI.Dtos;

namespace ClienteAPI.Repositorio
{
    public class Salvamento
    {
        private static string salvarArquivo = @"Arquivos/cliente.txt";

        // Método para garantir que o diretório existe
        private static void GarantirDiretorio()
        {
            var diretorio = Path.GetDirectoryName(salvarArquivo);
            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }
        }

        // Método para salvar um cliente no arquivo
        public static Cliente Salvar(ClienteDTO clienteD)
        {
            // Garantir que o diretório onde o arquivo será salvo existe
            GarantirDiretorio();

            // Obter todos os clientes existentes do arquivo
            var clientes = Percorrer().ToList();

            // Determinar o próximo ID disponível
            int maiorId = clientes.Any() ? clientes.Max(c => c.Id) : 0;
            int novoId = maiorId + 1;

            // Criar o novo cliente com base nos dados do ClienteDTO
            Cliente cliente = new Cliente
            {
                Id = novoId,
                Nome = clienteD.Nome,
                DataN = clienteD.DataN,
                Estado = clienteD.Estado,
                Cidade = clienteD.Cidade,
                Cpf = clienteD.Cpf,
                Endereco = clienteD.Endereco,
                Rg = clienteD.Rg,
                Sexo = clienteD.Sexo,
                Email = clienteD.Email,
                Telefone = clienteD.Telefone
            };

            // Adicionar o novo cliente à lista
            clientes.Add(cliente);

            // Gravar a lista atualizada de clientes no arquivo
            GravarClientesNoArquivo(clientes);

            // Retornar o cliente recém-adicionado
            return cliente;
        }

        // Método para atualizar um cliente existente no arquivo
        public static Cliente Atualizar(ClienteDTO clienteAtualizado, int id)
        {
            GarantirDiretorio();

            var clientes = Percorrer().ToList();
            var clienteExistente = clientes.FirstOrDefault(c => c.Id == id);
            if( clienteExistente == null )
            {
                return null;
            }
                Cliente cliente = new Cliente();
                cliente.Id = id;
                cliente.Nome = clienteAtualizado.Nome;
                cliente.DataN = clienteAtualizado.DataN;
                cliente.Sexo = clienteAtualizado.Sexo;
                cliente.Rg = clienteAtualizado.Rg;
                cliente.Cpf = clienteAtualizado.Cpf;
                cliente.Endereco = clienteAtualizado.Endereco;
                cliente.Cidade = clienteAtualizado.Cidade;
                cliente.Estado = clienteAtualizado.Estado;
                cliente.Telefone = clienteAtualizado.Telefone;
                cliente.Email = clienteAtualizado.Email;
                clientes.Remove(clienteExistente);
                clientes.Add(cliente);
                GravarClientesNoArquivo(clientes);
                return cliente;
         
        }

        // Método para deletar um cliente do arquivo
        public static bool Deletar(int id)
        {
            GarantirDiretorio();

            var clientes = Percorrer().ToList();
            var clienteParaRemover = clientes.FirstOrDefault(c => c.Id == id);

            if (clienteParaRemover != null)
            {
                clientes.Remove(clienteParaRemover);
                GravarClientesNoArquivo(clientes);
                return true;
            }

            return false;
        }

        // Método para procurar um cliente pelo ID
        public static Cliente ProcurarPorId(int id)
        {
            var clientes = Percorrer();
            return clientes.FirstOrDefault(c => c.Id == id);
        }

        // Método para ler todos os clientes do arquivo
        public static IEnumerable<Cliente> Percorrer()
        {
            var clientes = new List<Cliente>();
            if (File.Exists(salvarArquivo))
            {
                var linhas = File.ReadAllLines(salvarArquivo);

                foreach (var linha in linhas)
                {
                    var arrayClientes = linha.Split('|');
                    if (arrayClientes.Length == 11) // Certifique-se de que há 11 campos
                    {
                        var cliente = new Cliente()
                        {
                            Id = Convert.ToInt32(arrayClientes[0]),
                            Nome = arrayClientes[1],
                            DataN = arrayClientes[2],
                            Sexo = arrayClientes[3],
                            Rg = arrayClientes[4],
                            Cpf = arrayClientes[5],
                            Endereco = arrayClientes[6],
                            Cidade = arrayClientes[7],
                            Estado = arrayClientes[8],
                            Telefone = arrayClientes[9],
                            Email = arrayClientes[10]
                        };
                        clientes.Add(cliente);
                    }
                }
            }
            return clientes;
        }

        // Método para gravar a lista de clientes no arquivo
        private static void GravarClientesNoArquivo(IEnumerable<Cliente> clientes)
        {
            GarantirDiretorio();
            var linhas = clientes.Select(cliente =>
                $"{cliente.Id} | {cliente.Nome} | {cliente.DataN} | {cliente.Sexo} | {cliente.Rg} | {cliente.Cpf} | {cliente.Endereco} | {cliente.Cidade} | {cliente.Estado} | {cliente.Telefone} | {cliente.Email}");
            File.WriteAllLines(salvarArquivo, linhas);
        }
    }
}
