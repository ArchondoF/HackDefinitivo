using ConsoleApp4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace InternalServices.Controllers
{
    public class BuscadorController : ApiController
    {
        [HttpGet]
        [ActionName("Buscar")]
        public async Task<IHttpActionResult> BuscarAsync(string busqueda)
        {
            try
            {
                Buscador buscador = new Buscador();

                List<Articulo> colArticulos = await buscador.BuscadorFuncionAsync(busqueda);
                
                    if (colArticulos == null)
                        return NotFound();

                    return Ok(colArticulos);


                
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
    }
}
