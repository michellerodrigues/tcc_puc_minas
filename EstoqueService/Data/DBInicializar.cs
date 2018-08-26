using EstoqueService.DataContext;
using EstoqueService.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EstoqueService.Data
{
    public static class DBInicializar
    {
        public static void StartDataBase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDataContext>();
                if(context.Database.EnsureCreated())
                {
                    var revendedor1 = new Revendedor { Nome="Michelle Rodrigues 1 ",Email="mica-revenda1@mailinator.com"};
                    var revendedor2 = new Revendedor { Nome="Michelle Rodrigues 2",Email="mica-revenda2@mailinator.com"};
                    var revendedor3 = new Revendedor { Nome="ichelle Rodrigues 3",Email="mica-revenda3@mailinator.com"};
                    
                    
                    context.Revendedores.Add(revendedor1);
                    context.Revendedores.Add(revendedor2);
                    context.Revendedores.Add(revendedor3);


                    var produto1 = new Produto { Nome="Cupinicida 3MAX ", Valor=835, PesoCheio=450, PesoVazio=10,VolumeEmbalagem=0.3M}; 
                    var produto2 = new Produto { Nome="Herbicidas Hortalis", Valor=635, PesoCheio=450, PesoVazio=10,VolumeEmbalagem=0.3M}; 
                    var produto3 = new Produto { Nome="Inseticida Fatalis", Valor=395, PesoCheio=450, PesoVazio=10,VolumeEmbalagem=0.3M}; 

                  //  produto1.Revendedores.Add(revendedor1);
                  //  produto2.Revendedores.Add(revendedor1);
                  //  produto3.Revendedores.Add(revendedor1);

                  //  produto3.Revendedores.Add(revendedor2);
                  //  produto2.Revendedores.Add(revendedor2);


                 //   produto1.Revendedores.Add(revendedor3);
                  //  produto2.Revendedores.Add(revendedor3);


                    var fornecedor1 = new Fornecedor { Nome="Fornecedor 1 ",Email="mica-fornecedor1@mailinator.com"};
                    
                 //   produto2.Fornecedores.Add(fornecedor1);
                //    produto3.Fornecedores.Add(fornecedor1);

                    var fornecedor2 = new Fornecedor { Nome="Fornecedor 2",Email="mica-fornecedor2@mailinator.com"};
                    
                   // produto2.Fornecedores.Add(fornecedor2);
                    
                    var fornecedor3 = new Fornecedor { Nome="Fornecedor 3",Email="mica-fornecedor3@mailinator.com"};
                    
                 //   produto1.Fornecedores.Add(fornecedor3);
                  //  produto3.Fornecedores.Add(fornecedor3);

                    context.Fornecedores.Add(fornecedor1);
                    context.Fornecedores.Add(fornecedor2);
                    context.Fornecedores.Add(fornecedor3);

                    context.Produtos.Add(produto1);
                    context.Produtos.Add(produto2);
                    context.Produtos.Add(produto3);
                    
                     context.AddRange(
                        new FornecedorProduto { Fornecedor = fornecedor3, Produto = produto1 },
                        new FornecedorProduto { Fornecedor = fornecedor3, Produto  = produto3},
                        new FornecedorProduto { Fornecedor = fornecedor2, Produto = produto2 },
                        new FornecedorProduto { Fornecedor = fornecedor1, Produto = produto2 },
                        new FornecedorProduto { Fornecedor = fornecedor1, Produto = produto3 });
                    
                    context.AddRange(
                        new RevendedorProduto { Revendedor = revendedor1, Produto = produto1 },
                        new RevendedorProduto { Revendedor = revendedor1, Produto = produto2 },
                        new RevendedorProduto { Revendedor = revendedor1, Produto = produto3 },
                        new RevendedorProduto { Revendedor = revendedor2, Produto = produto2 },
                        new RevendedorProduto { Revendedor = revendedor2, Produto = produto3 },
                        new RevendedorProduto { Revendedor = revendedor3, Produto = produto2 },
                        new RevendedorProduto { Revendedor = revendedor3, Produto = produto1 });

                    var estoque = new Estoque{DataInclusao=DateTime.Now,DataVecimentoProduto=DateTime.Now.AddDays(15),Descartado=false,FornecidoPor = fornecedor1,IdProduto = produto1,QtdeDispUnidade=20,RevendidoPor = revendedor1,Lote="001",Serie="aab"};
                    var estoque1 = new Estoque{DataInclusao=DateTime.Now.AddHours(-1),DataVecimentoProduto=DateTime.Now.AddDays(15),Descartado=false,FornecidoPor = fornecedor3,IdProduto = produto1,QtdeDispUnidade=20,RevendidoPor = revendedor1,Lote="001",Serie="aab"};
                    var estoque2 = new Estoque{DataInclusao=DateTime.Now.AddHours(-2),DataVecimentoProduto=DateTime.Now.AddDays(45),Descartado=false,FornecidoPor = fornecedor2,IdProduto = produto2,QtdeDispUnidade=0,RevendidoPor = revendedor3,Lote="001",Serie="aab"};
                    var estoque3 = new Estoque{DataInclusao=DateTime.Now.AddHours(-3),DataVecimentoProduto=DateTime.Now.AddDays(5),Descartado=false,FornecidoPor = fornecedor2,IdProduto = produto3,QtdeDispUnidade=65,RevendidoPor = revendedor1,Lote="001",Serie="aab"};
                    var estoque4 = new Estoque{DataInclusao=DateTime.Now.AddHours(-4),DataVecimentoProduto=DateTime.Now.AddDays(13),Descartado=false,FornecidoPor = fornecedor1,IdProduto = produto1,QtdeDispUnidade=67,RevendidoPor = revendedor2,Lote="003",Serie="aax"};
                    var estoque5 = new Estoque{DataInclusao=DateTime.Now.AddHours(-5),DataVecimentoProduto=DateTime.Now.AddDays(22),Descartado=false,FornecidoPor = fornecedor3,IdProduto = produto3,QtdeDispUnidade=56,RevendidoPor = revendedor1,Lote="002",Serie="aac"};
                    
                    context.Estoques.Add(estoque);

                    context.Estoques.Add(estoque1);
                    context.Estoques.Add(estoque2);
                    context.Estoques.Add(estoque3);
                    context.Estoques.Add(estoque4);
                    context.Estoques.Add(estoque5);
                    
                    context.SaveChanges();
                }
                
            }
        }
    }
}