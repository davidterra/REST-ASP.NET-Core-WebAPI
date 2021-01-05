using System;
using System.Threading.Tasks;
using Dummy.Business.Models;

namespace Dummy.Business.Intefaces
{
    public interface IProdutoService : IDisposable
    {
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Remover(Guid id);
    }
}