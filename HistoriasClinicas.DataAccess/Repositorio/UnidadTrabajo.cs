﻿using HistoriasClinicas.DataAccess.Repositorio.IRepositorio;
using HistoriasClinicas.Models.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoriasClinicas.DataAccess.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private ApplicationDbContext _db;
        public IPacienteRepositorio Paciente { get; private set; }

        public IEpsRepositorio Eps { get; private set; }
        public IEspecialidadRepositorio Especialidad { get; private set; }
        public IMedicoRepositorio Medico { get; private set; }
        public IHistoriaClinicaRepositorio HistoriaClinica { get; private set; }
        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Eps = new EpsRepositorio(db);
            Especialidad = new EspecialidadRepositorio(db);
            Paciente = new PacienteRepositorio(db);
            Medico = new MedicoRepositorio(db);
            HistoriaClinica= new HistoriaClinicaRepositorio(db);
        }


        public void Dispose()
        {
            _db.Dispose();

        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}
