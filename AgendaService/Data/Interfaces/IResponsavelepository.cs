using System.Collections.Generic;
using AgendaService.Data.Interfaces;
using AgendaService.Data.Models;

public interface IResponsavelRepository: IRepository<Responsavel>
{
    IEnumerable<Responsavel> FindResponsavelByEmail(string email);
}
