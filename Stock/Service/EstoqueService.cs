using Stock.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Service
{
    public class EstoqueService : BaseService
    {
        public bool SalvarProduto(Produto produto)
        {
            bool salvo = false;

            string connectionString = base.connectionString;
            using (UnitOfWork uow = new UnitOfWork(connectionString))
            {
                #region "Produto"
                try
                {
                    List<Produto> pesquisaProduto = uow.ProdutoRepository.Pesquisar(new Produto() { Codigo = produto.Codigo });

                    if (pesquisaProduto.Count == 0)
                        uow.ProdutoRepository.Inserir(produto);
                    else
                        uow.ProdutoRepository.Atualizar(produto);
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    throw new ApplicationException(ex.Message, ex);
                }
                #endregion "Produto"

                #region "ProdutoSKU"
                try
                {
                    if (produto.Grades != null && produto.Grades.Count != 0)
                    {
                        foreach (ProdutoSKU sku in produto.Grades)
                        {
                            List<ProdutoSKU> pesquisaProdutoSKU = uow.ProdutoSKURepository.Pesquisar(new ProdutoSKU() { CodigoDeBarras = sku.CodigoDeBarras });

                            if (pesquisaProdutoSKU.Count == 0)
                                uow.ProdutoSKURepository.Inserir(sku);
                            //else
                            //    uow.ProdutoSKURepository.Atualizar(sku);
                        }
                    }
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    throw new ApplicationException(ex.Message, ex);
                }
                #endregion "ProdutoSKU"

                #region "ProdutoPreco"
                try
                {
                    if (produto.Precos != null && produto.Precos.Count != 0)
                    {
                        foreach (ProdutoPreco preco in produto.Precos)
                        {
                            List<ProdutoPreco> pesquisaProdutoPreco = uow.ProdutoPrecoRepository.Pesquisar(new ProdutoPreco()
                            {
                                CodigoBarras = preco.CodigoBarras,
                                CodTabelaPreco = preco.CodTabelaPreco
                            });

                            if (pesquisaProdutoPreco.Count == 0)
                                uow.ProdutoPrecoRepository.Inserir(preco);
                            else
                                uow.ProdutoPrecoRepository.Atualizar(preco);
                        }
                    }
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    throw new ApplicationException(ex.Message, ex);
                }
                #endregion "ProdutoPreco"

                #region "ProdutoEstoque"
                try
                {
                    List<ProdutoEstoque> pesquisaProduto = uow.ProdutoEstoqueRepository.Pesquisar(new ProdutoEstoque() { CodigoDeBarras = produto.Codigo });

                    if (pesquisaProduto.Count == 0)
                        uow.ProdutoEstoqueRepository.Inserir(produto.Estoque);
                    else
                        uow.ProdutoEstoqueRepository.Atualizar(produto.Estoque);

                    uow.Commit();

                    salvo = true;
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    throw new ApplicationException(ex.Message, ex);
                }
                #endregion "ProdutoEstoque"

            }

            return salvo;
        }

        public List<ProdutoTipo> ListarTiposDeProdutos(bool somenteAtivos = true)
        {
            List<ProdutoTipo> listaTipos = default(List<ProdutoTipo>);

            string connectionString = base.connectionString;
            using (UnitOfWork uow = new UnitOfWork(connectionString))
            {
                try
                {
                    listaTipos = uow.ProdutoTipoRepository.Pesquisar(new ProdutoTipo() { Ativo = (somenteAtivos) ? 'S' : Char.MinValue });

                    uow.Commit();
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    throw new ApplicationException(ex.Message, ex);
                }
            }

            return listaTipos;
        }
    }
}
