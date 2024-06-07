using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HistoriasClinicas.DataAccess.Repositorio.Interfaces
{
    public interface IRepositorio <T> where T:class
    {
        Task<IEnumerable<T>> ObtenerTodos(
            Expression<Func<T,bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string incluirPropiedades =null  //Medico, Especialidad
        );

        Task<T> ObtenerPrimero(
            Expression<Func<T, bool>> filtro = null,
            string incluirPropiedades = null
            );

        Task Agregar(T entidad);

        void Remover(T entidad);



    }
}
