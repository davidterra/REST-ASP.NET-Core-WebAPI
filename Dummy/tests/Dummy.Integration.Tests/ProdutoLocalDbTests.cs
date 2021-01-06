using Dummy.Api.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Dummy.Integration.Tests
{
    public class ProdutoLocalDbTests
        : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _serverFixture;

        public ProdutoLocalDbTests(TestServerFixture serverFixture) => _serverFixture = serverFixture;

        [Fact]
        public async Task Deve_cadastrar_um_produto()
        {
            // Arrange

            const string Request = "api/v1/produtos";

            string accessToken = await Auth.GetAccessTokenAsync(_serverFixture.Client);

            _serverFixture.Client.DefaultRequestHeaders.Clear();
            _serverFixture.Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);


            Guid fornecedorId = await ObterUltimoFornecedorIdAsync();

            // Act

            var produto = new ProdutoViewModel
            {
                FornecedorId = fornecedorId,
                Nome = "Bugman",
                Descricao = "Bixo papão",
                Imagem = "Bugman.png",
                ImagemUpload = ImagemBase64,
                Valor = 99.9m,
                Ativo = true,
            };

            var postResponse = await _serverFixture.Client.PostAsync(Request, produto);

            var jsonFromPostResponse = await postResponse.Content.ReadAsStringAsync();

            // Assert
            postResponse.EnsureSuccessStatusCode();
            Assert.Contains("\"success\":true", jsonFromPostResponse);
        }

        private async Task<Guid> ObterUltimoFornecedorIdAsync()
        {

            const string Request = "api/v1/fornecedores";

            var jsonFromGetResponse = await _serverFixture.Client.GetStringAsync(Request);

            var fornecedores = JsonConvert.DeserializeObject<IEnumerable<FornecedorViewModel>>(jsonFromGetResponse);

            return fornecedores.Last().Id;

        }

        string ImagemBase64 => "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";
    }
}
