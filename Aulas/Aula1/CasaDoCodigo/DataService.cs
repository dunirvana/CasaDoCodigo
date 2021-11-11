using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo
{
    public class DataService : IDataService
    {
        private readonly ApplicationContext contexto;
        private readonly IProdutoRepository produtoRepository;

        public DataService(ApplicationContext contexto,
            IProdutoRepository produtoRepository)
        {
            this.contexto = contexto;
            this.produtoRepository = produtoRepository;
        }

        public void InicializaDB()
        {
            contexto.Database.EnsureCreated();

            List<Livro> livros = GetLivros();
            if (livros == null)
                return;

            produtoRepository.SaveProdutos(livros);

        }

        private static List<Livro> GetLivros()
        {
            var filePath = "livros.json";
            if (!File.Exists(filePath))
                return null;

            var json = File.ReadAllText(filePath);
            var livros = JsonConvert.DeserializeObject<List<Livro>>(json);
            
            return livros;
        }
    }


}
