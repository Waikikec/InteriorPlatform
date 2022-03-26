namespace InteriorPlatform.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Common.Repositories;
    using InteriorPlatform.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class PositionsController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Position> dataRepository;

        public PositionsController(IDeletableEntityRepository<Position> dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        // GET: Administration/Positions
        public async Task<IActionResult> Index()
        {
            return this.View(await this.dataRepository.AllWithDeleted().ToListAsync());
        }

        // GET: Administration/Positions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var position = await this.dataRepository
                .All()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (position == null)
            {
                return this.NotFound();
            }

            return this.View(position);
        }

        // GET: Administration/Positions/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Positions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Position position)
        {
            if (this.ModelState.IsValid)
            {
                await this.dataRepository.AddAsync(position);
                await this.dataRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(position);
        }

        // GET: Administration/Positions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var position = await this.dataRepository.All().FirstOrDefaultAsync(m => m.Id == id);
            if (position == null)
            {
                return this.NotFound();
            }

            return this.View(position);
        }

        // POST: Administration/Positions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Position position)
        {
            if (id != position.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.dataRepository.Update(position);
                    await this.dataRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.PositionExists(position.Id))
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

            return this.View(position);
        }

        // GET: Administration/Positions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var position = await this.dataRepository
                .All()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (position == null)
            {
                return this.NotFound();
            }

            return this.View(position);
        }

        // POST: Administration/Positions/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var position = await this.dataRepository.All().FirstOrDefaultAsync(m => m.Id == id);
            this.dataRepository.Delete(position);
            await this.dataRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool PositionExists(int id)
        {
            return this.dataRepository.All().Any(e => e.Id == id);
        }
    }
}
