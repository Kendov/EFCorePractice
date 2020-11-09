using System;
using CursoEFCore.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //AddEntry();
            //AddMultiplesEntrys();
            //QueryData();
            //RegisterPedido();
            //EagerQuery();
            //UpdateEntity();
            DeleteEntity();
        }


        private static void DeleteEntity()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.Find(3);
            //var cliente = new Cliente{ Id = 4 }; // delete without find
            
            //db.Clientes.Remove(cliente);
            db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();
        }

        private static void UpdateEntity()
        {
            using var db = new Data.ApplicationContext();

            //var cliente = db.Clientes.Find(1);
            //cliente.Nome = "cliente updated again";

            var cliente = new Cliente
            {
                Id = 1
            };
            var clienteDTO = new 
            {
                Nome = "cliente updated",
                Telefone = "61985465858"
            };

            // update entry without find
            db.Attach(cliente);
            db.Entry(cliente).CurrentValues.SetValues(clienteDTO);
            

            //db.Clientes.Update(cliente); // this will update all entity fields
            db.SaveChanges();
        }

        // Eager loading is the process whereby a query for one type
        // of entity also loads related entities as part of the query
        // https://www.entityframeworktutorial.net/eager-loading-in-entity-framework.aspx
        private static void EagerQuery()
        {
            using var db = new Data.ApplicationContext();

            var pedidos = db
                .Pedidos
                .Include(x => x.Itens)
                    .ThenInclude(x => x.Produto)
                .ToList();

            Console.WriteLine(pedidos.Count);
        }


        private static void RegisterPedido()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "test observation",
                Status = ValueObjects.StatusPedido.Analise,
                TipoFrete = ValueObjects.TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10
                    }
                }
            };
            db.Pedidos.Add(pedido);
            db.SaveChanges();
        }
        private static void QueryData()
        {
            using var db = new Data.ApplicationContext();
            //var linqQuerySyntax = (from c in db.Clientes where c.Id > 0 select c).ToList();
            var linqMethodSyntax = db.Clientes.AsNoTracking().Where(x => x.Id > 0).ToList();

            foreach (var cliete in linqMethodSyntax)
            {
                Console.WriteLine($"Cliente id: {cliete.Id}");
                
                // "Find" will search the memory first for tracked entities
                db.Clientes.Find(cliete.Id);
            }
        }
        private static void AddEntry()
        {
            var produto = new Produto
            {
                Descricao = "product test",
                CodigoBarras = "1231567893232",
                Valor = 20m,
                TipoProduto = ValueObjects.TipoProduto.MercadoriaParaRevenda,
                Ativo = true

            };

            using var db = new Data.ApplicationContext();
            db.Produtos.Add(produto);
            // db.Set<Produto>().Add(produto);
            // db.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            //db.Add(produto);
            var records = db.SaveChanges();
            Console.WriteLine($"Total register(s): {records}");
        }

        private static void AddMultiplesEntrys()
        {
            var produto = new Produto
            {
                Descricao = "product test2",
                CodigoBarras = "1231567893000",
                Valor = 100m,
                TipoProduto = ValueObjects.TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "cliente1",
                CEP = "99999900",
                Cidade = "Curitiba",
                Estado = "PR",
                Telefone = "61995811111"
            };

            using var db = new Data.ApplicationContext();
            db.AddRange(produto, cliente);

            var records = db.SaveChanges();
            Console.WriteLine($"Total records: {records}");
        }
    }
}
