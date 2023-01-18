using Microsoft.EntityFrameworkCore;
using Radar_Api.Models;
using Radar_Api.Repositories.Context;
using Radar_Api.Repositorios.Interfaces;

namespace api.Repositorios.Entity;

public class CampanhaRepository : IServico<Campanha>
{
    private EntityContext contexto;

    public CampanhaRepository()
    {
        contexto = new EntityContext();
    }

    public async Task<List<Campanha>> TodosAsync()
    {
        return await contexto.Campanhas.ToListAsync();
    }

    public async Task IncluirAsync(Campanha campanha)
    {
        contexto.Campanhas.Add(campanha);
        await contexto.SaveChangesAsync();
    }

    public async Task<Campanha> AtualizarAsync(Campanha campanha)
    {
        contexto.Entry(campanha).State = EntityState.Modified;
        await contexto.SaveChangesAsync();

        return campanha;
    }

    public async Task ApagarAsync(Campanha campanha)
    {
        var obj = await contexto.Campanhas.FindAsync(campanha.Id);
        if (obj is null) throw new Exception("Campanha não encontrada.");
        contexto.Campanhas.Remove(obj);
        await contexto.SaveChangesAsync();
    }
    
}