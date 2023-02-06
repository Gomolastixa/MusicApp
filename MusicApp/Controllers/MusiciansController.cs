using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MusicApp.Data;
using MusicApp.Models;

namespace MusicApp.Controllers
{
    public class MusiciansController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MusiciansController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Musicians
        public async Task<IActionResult> Index()
        {
            return _context.Musicians != null ?
                        View(await _context.Musicians.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Musicians'  is null.");
        }

        // GET: Musicians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Musicians == null)
            {
                return NotFound();
            }

            var musician = await _context.Musicians.Include(rm => rm.RecordMembers)
                                                   .ThenInclude(mr => mr.MusicRecord)
                                                   .FirstOrDefaultAsync(mr => mr.Id == id);
            if (musician == null)
            {
                return NotFound();
            }

            return View(musician);
        }

        // GET: Musicians/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Musicians/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Instrument")] CreateMusicianDto musicianDto)
        {
            if (ModelState.IsValid)
            {
                var musician = _mapper.Map<Musician>(musicianDto);
                _context.Musicians.Add(musician);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(musicianDto);
        }

        // GET: Musicians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Musicians == null)
            {
                return NotFound();
            }

            var musician = await _context.Musicians.FindAsync(id);
            if (musician == null)
            {
                return NotFound();
            }
            return View(musician);
        }

        // POST: Musicians/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Instrument")] EditMusicianDto musicianDto)
        {
            if (id != musicianDto.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var musician = _mapper.Map<Musician>(musicianDto);
                    _context.Update(musician);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicianExists(musicianDto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(musicianDto);
        }

        // GET: Musicians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Musicians == null)
            {
                return NotFound();
            }

            var musician = await _context.Musicians
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musician == null)
            {
                return NotFound();
            }

            return View(musician);
        }

        // POST: Musicians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Musicians == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Musicians'  is null.");
            }
            var musician = await _context.Musicians.FindAsync(id);
            if (musician != null)
            {
                _context.Musicians.Remove(musician);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicianExists(int id)
        {
            return (_context.Musicians?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
