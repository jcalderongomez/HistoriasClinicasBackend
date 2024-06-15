using AutoMapper;
using HistoriasClinicas.DataAccess.Repositorio.IRepositorio;
using HistoriasClinicas.Models.Modelos;
using HistoriasClinicas.Models.Modelos.Dto;
using Microsoft.EntityFrameworkCore;

namespace HistoriasClinicas.DataAccess.Repositorio
{
    public class EpsRepositorio : Repositorio<Eps>, IEpsRepositorio
    {
        private readonly ApplicationDbContext _db;

        public EpsRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Actualizar(Eps eps)
        {
            var epsDb = _db.Epss.FirstOrDefault(e => e.Id == eps.Id);
            if (epsDb != null)
            {
                epsDb.NombreEPS = eps.NombreEPS;
                _db.SaveChanges();

            }

        }
    }
}