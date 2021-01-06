using Dummy.Api.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Dummy.Integration.Tests
{
    public class FornecedorLocalDbTest
        : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _serverFixture;

        public FornecedorLocalDbTest(TestServerFixture serverFixture) => _serverFixture = serverFixture;

        [Fact]
        public async Task Deve_adicionar_um_fornecedor()
        {
            // Arrange

            const string Request = "api/v1/fornecedores";

            var accessToken = await Auth.GetAccessTokenAsync(_serverFixture.Client);

            _serverFixture.Client.DefaultRequestHeaders.Clear();
            _serverFixture.Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var fornecedor = new FornecedorViewModel
            {
                Nome = "Namco",
                Documento = ObterCnpj().Replace(Environment.NewLine,""),
                TipoFornecedor = 2,
                Endereco = new EnderecoViewModel
                {
                    Logradouro = "Main Land",
                    Numero = "64",
                    Bairro = "Newtown",
                    Cep = "00000001",
                    Cidade = "Black Board",
                    Estado = "BB"
                },
                Ativo = true
            };

            // Act

            var postResponse = await _serverFixture.Client.PostAsync(Request, fornecedor);

            var jsonFromPostResponse = await postResponse.Content.ReadAsStringAsync();


            // Assert
            postResponse.EnsureSuccessStatusCode();
            Assert.Contains("\"success\":true", jsonFromPostResponse);
            

        }

        private string ObterCnpj()
        {
            var ng = new NumberGenerator();

            return ng.CnpjSemMascara(1);
        }


    }
}
