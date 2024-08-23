using Aula2407.Data;
using Aula2407.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aula2407.Controllers
{
    public class ProdutoController : Controller
    {

        private readonly AulaContext _context;
        public ProdutoController(AulaContext context)
        {
            _context = context;
        }

        //BUSCAR PRODUTO
        public async Task<IActionResult> BuscarProdutos(int pagina = 1)
        {
            var QtdeTProduto = 3;

            var items = await _context.Produtos.OrderBy(c => c.Id).ToListAsync();
            //var pagedItems = items.Skip((pagina - 1) * QtdeTClientes).Take(QtdeTClientes).ToList();

            ViewBag.QtdePaginas = (int)Math.Ceiling((double)items.Count() / QtdeTProduto);
            ViewBag.PaginaAtual = pagina;
            ViewBag.QtdeTProduto = QtdeTProduto;

            return View(items);

            //return View(await _context.Produtos.ToListAsync());
        }


        //CADASTRO PRODUTO
        public async Task<IActionResult> CadastroProduto(int? id)
        {
            if (id == null)
            {
                return View();
            }
            else
            {
                return View(await _context.Produtos.FindAsync(id));
            }
        }

        public async Task<IActionResult> DetalhesProduto(int id)
        {
            return View(await _context.Produtos.FindAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastroProduto(
             [Bind("Id,Nome,Fabricante,Codigo,Quantidade,ValordeCompra,ValordeVenda,Descricao")]
            Produto produto)

        {
            if (ModelState.IsValid)
            {
                if (produto.Id != 0)
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                    TempData["msg"] = "2";
                }
                else
                {
                    _context.Add(produto);
                    await _context.SaveChangesAsync();
                    TempData["msg"] = "1";
                }
            }
            return RedirectToAction("BuscarProdutos");
        }

        public async Task<IActionResult> DeletarProduto(int Id)
        {
            if (Id != 0)
            {
                var produto = await _context.Produtos.FindAsync(Id);
                if (produto != null)
                {
                    _context.Remove<Produto>(produto);
                    await _context.SaveChangesAsync();
                    TempData["msg"] = "3";
                    return RedirectToAction("BuscarProdutos");
                }
            }
            return RedirectToAction("BuscarProdutos");
        }
    }
}
