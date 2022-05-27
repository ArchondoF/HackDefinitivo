using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class Program
    {
        static async Task Main(string[] args)
        {

            Buscador hola = new Buscador();
            List<Articulo> Articulos = await hola.BuscadorFuncionAsync("Autos");
            Console.ReadLine();
            
            
        }
        
           
        
    }

}