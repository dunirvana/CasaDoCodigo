using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models
{
    [DataContract]
    public class BaseModel
    {
        [DataMember]
        public int Id { get; protected set; }
    }

    public class Produto : BaseModel
    {
        public Produto()
        {

        }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Codigo { get; private set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; private set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public decimal Preco { get; private set; }

        public Produto(string codigo, string nome, decimal preco)
        {
            this.Codigo = codigo;
            this.Nome = nome;
            this.Preco = preco;
        }
    }

    public class Cadastro : BaseModel
    {
        public Cadastro()
        {
        }

        public virtual Pedido Pedido { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(5, ErrorMessage = "O tamanho mínimo para o nome é de 5 caracteres")]
        [MaxLength(50, ErrorMessage = "O tamanho máximo para o nome é de 50 caracteres")]
        public string Nome { get; set; } = "";
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Email { get; set; } = "";
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Telefone { get; set; } = "";
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Endereco { get; set; } = "";
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Complemento { get; set; } = "";
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Bairro { get; set; } = "";
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Municipio { get; set; } = "";
        [Required(ErrorMessage = "Campo obrigatório")]
        public string UF { get; set; } = "";
        [Required(ErrorMessage = "Campo obrigatório")]
        public string CEP { get; set; } = "";
    }

    [DataContract]
    public class ItemPedido : BaseModel
    {   
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataMember]
        public Pedido Pedido { get; private set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataMember]
        public Produto Produto { get; private set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataMember]
        public int Quantidade { get; private set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataMember]
        public decimal PrecoUnitario { get; private set; }
        [DataMember]
        public decimal Subtotal => Quantidade * PrecoUnitario;

        public ItemPedido()
        {

        }

        public ItemPedido(Pedido pedido, Produto produto, int quantidade, decimal precoUnitario)
        {
            Pedido = pedido;
            Produto = produto;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }

        internal void AtualizaQuantidade(int quantidade)
        {
            Quantidade = quantidade;
        }
    }

    public class Pedido : BaseModel
    {
        public Pedido()
        {
            Cadastro = new Cadastro();
        }

        public Pedido(Cadastro cadastro)
        {
            Cadastro = cadastro;
        }

        public List<ItemPedido> Itens { get; private set; } = new List<ItemPedido>();
        [Required(ErrorMessage = "Campo obrigatório")]
        public virtual Cadastro Cadastro { get; private set; }
    }
}
