using System;
using System.Threading.Tasks;
using Dummy.Business.Intefaces;
using Dummy.Business.Models;
using Dummy.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Dummy.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(DummyDbContext context) : base(context) { }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await Db.Enderecos.AsNoTracking()
                .FirstOrDefaultAsync(f => f.FornecedorId == fornecedorId);
        }
    }
}