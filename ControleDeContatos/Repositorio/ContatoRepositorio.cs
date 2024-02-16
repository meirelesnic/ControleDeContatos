using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;

        public ContatoRepositorio(BancoContext bancoContext) 
        {
            _bancoContext = bancoContext;
        }

        public ContatoModel Adicionar(ContatoModel contato)
        {
            _bancoContext.Contato.Add(contato);
            _bancoContext.SaveChanges();

            return contato;
        }

        public List<ContatoModel> BuscarTodos()
        {
            return _bancoContext.Contato.ToList();
        }

        public ContatoModel ListarPorId(int id)
        {
            return _bancoContext.Contato.FirstOrDefault(x => x.Id == id);
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDB = ListarPorId(contato.Id);

            if (contatoDB == null) throw new Exception("Houve um erro na atualização do contato");

            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;

            _bancoContext.Contato.Update(contatoDB);
            _bancoContext.SaveChanges();

            return contatoDB;
        }

        public bool Apagar(int id)
        {
            ContatoModel contadoDB = ListarPorId(id);

            if (contadoDB == null) throw new Exception("Houve um erro na exclusão do contato!");

            _bancoContext.Contato.Remove(contadoDB);
            _bancoContext.SaveChanges(false);

            return true;
        }
    }
}
