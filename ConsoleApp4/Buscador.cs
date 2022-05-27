using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class Buscador
    {

        public async Task<List<Articulo>> BuscadorFuncionAsync(string Busqueda)
        {

            string url = "https://listado.mercadolibre.com.uy/" + Busqueda + "#D[A: " + Busqueda + "]";
            string chrome = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            var browserFetcher = new BrowserFetcher();
            await  browserFetcher.DownloadAsync();

            var browser = await Puppeteer.LaunchAsync(
                  new LaunchOptions
                  {
                      Headless = true,
                      ExecutablePath = chrome,

                  }
              );

            var pagina = await browser.NewPageAsync();

            await pagina.GoToAsync(url);

            List<string> titulos = new List<string>();

            var Titulos = await pagina.EvaluateFunctionAsync("()=>{" +
                "const a = document.querySelectorAll('.ui-search-item__title');" +
                "const resultado = [];" +
                "for(let i = 0 ; i < a.length ; i++){" +
                "   resultado.push(a[i].innerHTML);}" +
                "return resultado;" +
                "}");


            var Precios = await pagina.EvaluateFunctionAsync("()=>{" +
                "const a =  document.querySelectorAll('.price-tag-fraction');" +
                "const resultado = [];" +
                "for(let i = 0 ; i < a.length ; i++){" +
                "   resultado.push(a[i].innerHTML);}" +
                "return resultado;" +
                "}");

            var Ubicacion = await pagina.EvaluateFunctionAsync("()=>{" +
                "const a =  document.querySelectorAll('.ui-search-item__location');" +
                "const resultado = [];" +
                "for(let i = 0 ; i < a.length ; i++){" +
                "   resultado.push(a[i].innerHTML);}" +
                "return resultado;" +
                "}");
            var ImagenSrc = await pagina.EvaluateFunctionAsync("()=>{" +
               "const a =  document.querySelectorAll('.ui-search-result-image__element');" +

               "const resultado = [];" +

               "for(let i = 0 ; i < a.length ; i++){" +
               "const b =  document.querySelectorAll('.ui-search-result-image__element')[i].src;" +
               "   resultado.push(b);}" +
               "return resultado;" +
               "}");
            var Link = await pagina.EvaluateFunctionAsync("()=>{" +
              "const a =  document.querySelectorAll('.ui-search-result--core');" +

              "const resultado = [];" +

              "for(let i = 0 ; i < a.length ; i++){" +
              "const b =  document.querySelectorAll('.ui-search-result--core')[i].firstChild.firstChild.href;" +
              "   resultado.push(b);}" +
              "return resultado;" +
              "}");

            List<Articulo> colArticulos = new List<Articulo>();
            for (int i = 0; i < Titulos.Count(); i++)
            {
                Articulo aux = new Articulo();

                aux.Titulo = Titulos[i].ToString();
                if (Ubicacion.Count() != 0)
                {
                    aux.Ubicacion = Ubicacion[i].ToString();
                }

                aux.Precio = Precios[i].ToString();
                aux.Imagen = ImagenSrc[i].ToString();
                //aux.Link = Link[i].ToString();
                colArticulos.Add(aux);
            }
            foreach (Articulo aux in colArticulos)
            {
                Console.WriteLine("Titulo: " + aux.Titulo + " " + "Ubicacion: " + " " + aux.Ubicacion + "Precio: " + " " + aux.Precio);
            }
           

            return colArticulos ;
        }

    }
}
