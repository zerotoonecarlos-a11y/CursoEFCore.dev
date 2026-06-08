using CursoEFCore.Domain;
using CursoEFCore.Data;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using CursoEFCore.ValueObjects;

Console.WriteLine("Hello, World!");



static void ExcluirDados()
{
    using var db = new ApplicationContext();
    var cliente = db.Clientes.Find(1);
    if (cliente != null)
    {
        //db.Clientes.Remove(cliente);
        db.Entry(cliente).State = EntityState.Deleted;
        db.Remove(cliente);
        db.SaveChanges();
    }
}



static void AtualizarDados()
{
    using var db = new ApplicationContext();
    var cliente = db.Clientes.Find(1);
    cliente.Nome = "Cliente 1 - Atualizado";

    db.Entry(cliente).State = EntityState.Modified;

    var cliente01 = new Cliente
    {
        Id = 1
    };

    var clienteDesconectado = new
    {
        Nome = "Cliente Desconectado",
        Telefone = "11999999999",
    };
    db.Attach(cliente01);

    db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);

    // Ao usar esse metodo o Entity Framework Core entende que o cliente já existe e irá atualizar o registro, mas a linha abaixo não e uma boa pratica
    // Pois assim ele ira atualizar propriedades que não foram modificadas, e isso pode causar problemas de concorrencia, pois outros usuarios
    // podem ter modificado o mesmo registro e ao atualizar ele irá sobrescrever as modificações dos outros usuarios, por isso é recomendado usar
    // o metodo Update apenas quando o cliente for um objeto novo, ou seja, quando ele não tiver um Id definido, caso contrario é recomendado usar
    // o metodo Attach para informar ao Entity Framework Core que o cliente já existe e que ele deve ser atualizado apenas as propriedades que foram modificadas.
    // não e necessaria a linha abaixo.
    //db.Clientes.Update(cliente);

    db.SaveChanges();
}
static void ConsultarDados()
{
    using var db = new ApplicationContext();
    //var consultaPorSintaxe = (from c in db.Clientes
    //                          where c.Id > 0
    //                          select c).ToList();
    var consultaPorMetodo = db.Clientes.AsNoTracking().Where(p => p.Id > 0).ToList();

    foreach (var cliente in consultaPorMetodo)
    {
        Console.WriteLine($"Consulta Cliente: {cliente.Id}");
        //db.Clientes.Find(cliente.Id);
        db.Clientes.FirstOrDefault(c => c.Id == cliente.Id);
    }
}

static void ConsultarPedidoCarregamentoAdiantado()
{
    using var db = new ApplicationContext();
    var pedidos = db
        .Pedidos
        .Include(p => p.Itens)
            .ThenInclude(p => p.Produto)
            .ToList();
    Console.WriteLine(pedidos.Count);
}



static void CadastrarPedido()
{
    using var db = new ApplicationContext();
    var cliente = db.Clientes.FirstOrDefault(c => c.Id == 1);
    var produto = db.Produtos.FirstOrDefault(p => p.Id == 1);
    if (cliente != null && produto != null)
    {
        var pedido = new Pedido
        {
            ClienteId = cliente.Id,
            IniciadoEM = DateTime.Now,
            FinalizadoEM = DateTime.Now.AddDays(7),
            Observacao = "Pedido Teste",
            Status = StatusPedido.Analise,
            Itens = new List<PedidoItem>
            {
                new PedidoItem
                {
                    ProdutoId = produto.Id,
                    Quantidade = 2,
                    Valor = produto.Valor,
                    Desconto = 0
                }
            }
        };
        db.Pedidos.Add(pedido);
        db.SaveChanges();

        static void InserirDadosEmMassa()
        {
            var cliente = new Cliente
            {
                Nome = "Cliente 1",
                Telefone = "11999999999",
                CEP = "12345678",
                Estado = "SP",
                Cidade = "São Paulo"
            };
            var produtos = new Produto
            {
                Descricao = "Produto 1",
                Valor = 10,
                CodigoBarras = "123456789",
            };

            using var db = new ApplicationContext();
            db.AddRange(cliente, produtos);
            db.SaveChanges();
        }

        static void InserirDados()
        {
            var cliente = new Cliente
            {
                Nome = "Cliente 1",
                Telefone = "11999999999",
                CEP = "12345678",
                Estado = "SP",
                Cidade = "São Paulo"
            };

            using var db = new ApplicationContext();
            //db.Clientes.Add(cliente);
            //db.Set<Cliente>().Add(cliente);
            db.Add(cliente);
            //db.SaveChanges();
        }
    }
}