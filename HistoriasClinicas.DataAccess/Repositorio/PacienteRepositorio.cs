using HistoriasClinicas.DataAccess.Repositorio.Interfaces;
using HistoriasClinicas.Models.Modelos;

namespace HistoriasClinicas.DataAccess.Repositorio
{
    public class PacienteRepositorio : Repositorio<Paciente>, IPacienteRepositorio
    {
        private readonly ApplicationDbContext _db;


        public PacienteRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        public void Actualizar(Paciente paciente)
        {
            var pacienteDb = _db.Pacientes.FirstOrDefault(p => p.Id == paciente.Id);
            if (pacienteDb != null)
            {
                pacienteDb.Nombres = paciente.Nombres;
                pacienteDb.Apellidos = paciente.Apellidos;
                pacienteDb.Direccion = paciente.Direccion;
                pacienteDb.Telefono = paciente.Telefono;
                pacienteDb.EpsId = paciente.EpsId;
                _db.SaveChanges();
            }
        }
    }
}
