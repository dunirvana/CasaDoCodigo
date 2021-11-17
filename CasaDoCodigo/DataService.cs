using CasaDoCodigo.Data;
using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CasaDoCodigo
{
    public class DataService : IDataService
    {
        public async Task InicializaDBAsync(IServiceProvider provider)
        {
            await CriarBaseCasaDoCodigoAsync(provider);

            await CriarBaseIdentityAsync(provider);
        }

        private async Task CriarBaseCasaDoCodigoAsync(IServiceProvider provider)
        {
            var contexto = provider.GetService<ApplicationContext>();

            await contexto.Database.MigrateAsync();

            if (await contexto.Set<Produto>().AnyAsync())
            {
                return;
            }

            List<Livro> livros = await GetLivrosAsync();

            var produtoRepository = provider.GetService<IProdutoRepository>();
            await produtoRepository.SaveProdutosAsync(livros);
        }

        private async Task CriarBaseIdentityAsync(IServiceProvider provider)
        {
            var contexto = provider.GetService<ApplicationDbContext>();

            await contexto.Database.MigrateAsync();
        }

        private async Task<List<Livro>> GetLivrosAsync()
        {
            var json = await File.ReadAllTextAsync("livros.json");
            return JsonConvert.DeserializeObject<List<Livro>>(json);
        }
    }
}
