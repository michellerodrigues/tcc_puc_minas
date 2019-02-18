using TriagemService.DataContext;
using TriagemService.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TriagemService.Data
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
                    var operador1 = new Operador { NomeOperador="Michelle Rodrigues",Email="mica_operador@mailinator.com"};
                    var operador2 = new Operador { NomeOperador="Cleiton Rodrigues",Email="cleiton_operador@mailinator.com"};
                    var operador3 = new Operador { NomeOperador="Cleber Rodrigues",Email="cleber_operador@mailinator.com"};
                                        
                    context.Operadores.Add(operador1);
                    context.Operadores.Add(operador2);
                    context.Operadores.Add(operador3);

                    context.SaveChanges();
                }
                
            }
        }
    }
}