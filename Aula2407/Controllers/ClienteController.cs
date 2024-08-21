using Aula2407.Data;
using Aula2407.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aula2407.Controllers
{
    public class ClienteController : Controller
    {
        private readonly AulaContext _context;
        public ClienteController(AulaContext context)
        {
            _context = context;
        }

        //BUSCAR CLIENTE
        public async Task<IActionResult> BuscarClientes(int pagina = 1)
        {
            var QtdeTClientes = 2;
            
            var items = await _context.Clientes.ToListAsync();
            var pagedItems = items.Skip((pagina - 1) * QtdeTClientes).Take(QtdeTClientes).ToList();

            ViewBag.QtdePaginas = (int)Math.Ceiling((double)items.Count() / QtdeTClientes);
            ViewBag.PaginaAtual = pagina;

            return View(pagedItems);

            //return View(await _context.Clientes.ToListAsync());
        }

        //CADASTRO CLIENTE
        public async Task<IActionResult> CadastroCliente(int? id)
        {
            if (id == null)
            {
                return View();
            }
            else
            {
                return View(await _context.Clientes.FindAsync(id));
            }
        }

        //DETALHE CLIENTE
        public async Task<IActionResult> DetalheClientes(int id)
        {
            return View(await _context.Clientes.FindAsync(id));
        }


        ////ALTERAR CLIENTE
        //public async Task<IActionResult> AlterarClientes(int id)
        //{
        //    return View(await _context.Clientes.FindAsync(id));
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastroCliente(
            [Bind("Id,Nome,CPF,RG,Usuario,Senha,CEP,UF,Cidade,Bairro,Rua,Numero,Complemento")]
            Cliente cliente)

        {
            if (ModelState.IsValid)
            {
                if (cliente.Id != 0)
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                    TempData["msg"] = "2";
                }
                else
                {
                    _context.Add(cliente);
                    await _context.SaveChangesAsync();
                    TempData["msg"] = "1";
                }
            }
            return RedirectToAction("BuscarClientes");
        }


        public async Task<IActionResult> DeletarCliente(int Id)
        {
            if (Id != 0)
            {
                var cliente = await _context.Clientes.FindAsync(Id);
                if (cliente != null)
                {
                    _context.Remove<Cliente>(cliente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("BuscarClientes");
                }
            }
            return RedirectToAction("BuscarClientes");
        }
    }
}
