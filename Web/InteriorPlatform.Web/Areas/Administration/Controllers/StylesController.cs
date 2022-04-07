namespace InteriorPlatform.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Common.Repositories;
    using InteriorPlatform.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class StylesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Style> dataRepository;

        public StylesController(IDeletableEntityRepository<Style> dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        // GET: Administration/Styles
        public async Task<IActionResult> Index()
        {
            return this.View(await this.dataRepository.AllWithDeleted().ToListAsync());
        }

        // GET: Administration/Styles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var style = await this.dataRepository
                .All()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (style == null)
            {
                return this.NotFound();
            }

            return this.View(style);
        }

        // GET: Administration/Styles/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Styles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Style style)
        {
            if (this.ModelState.IsValid)
            {
                await this.dataRepository.AddAsync(style);
                await this.dataRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(style);
        }

        // GET: Administration/Styles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var style = await this.dataRepository.All().FirstOrDefaultAsync(m => m.Id == id);
            if (style == null)
            {
                return this.NotFound();
            }

            return this.View(style);
        }

        // POST: Administration/Styles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Style style)
        {
            if (id != style.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.dataRepository.Update(style);
                    await this.dataRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.StyleExists(style.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(style);
        }

        // GET: Administration/Styles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var style = await this.dataRepository
                .All()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (style == null)
            {
                return this.NotFound();
            }

            return this.View(style);
        }

        // POST: Administration/Styles/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var style = await this.dataRepository.All().FirstOrDefaultAsync(m => m.Id == id);
            this.dataRepository.Delete(style);
            await this.dataRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool StyleExists(int id)
        {
            return this.dataRepository.All().Any(e => e.Id == id);
        }
    }
}
