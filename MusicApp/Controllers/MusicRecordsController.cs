﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicApp.Data;
using MusicApp.Models;

namespace MusicApp.Controllers
{
    public class MusicRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MusicRecordsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: MusicRecords
        public async Task<IActionResult> Index([FromQuery(Name = "q")] string searchText, string orderBy, string orderDirection)
        {

            ViewBag.OrderDir = String.IsNullOrEmpty(orderDirection) ? "desc" : "";

            var query = _context.MusicRecord.AsQueryable();
            bool desc = "desc".Equals(orderDirection, StringComparison.OrdinalIgnoreCase);

            

            switch (orderBy)
            {
                case nameof(MusicRecord.Name):
                    query = desc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                    break;
                case nameof(MusicRecord.Artist):
                    query = desc ? query.OrderByDescending(x => x.Artist) : query.OrderBy(x => x.Artist);
                    break;
                case nameof(MusicRecord.Year):
                    query = desc ? query.OrderByDescending(x => x.Year) : query.OrderBy(x => x.Year);
                    break;
                case nameof(MusicRecord.Genre):
                    query = desc ? query.OrderByDescending(x => x.Genre) : query.OrderBy(x => x.Genre);
                    break;
                default:
                    query = query.OrderBy(x => x.Name);
                    break;

            }
          
            if (!string.IsNullOrEmpty(searchText))
            {
                bool isyear = int.TryParse(searchText, out int year);
                string filter = searchText.Trim().ToLower();
                query = query.Where(x => (isyear && x.Year == year) ||
                                                                    (x.Name.Contains(filter) ||
                                                                    x.Artist.Contains(filter) ||
                                                                    x.Genre.Contains(filter))
                                                                    );

            }

            return View(await query.ToListAsync());

        }

        // GET: MusicRecords/SearchForm
        public IActionResult SearchForm()
        {
            return View();
        }

        // POST: MusicRecords/SearchForm
        public async Task<IActionResult> SearchResults(string SearchText)
        {
            return View("Index", await _context.MusicRecord.Where(j => j.Name.Contains(SearchText) || j.Artist.Contains(SearchText)
                                                                        || j.Genre.Contains(SearchText)).ToListAsync());

        }

        // GET: MusicRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MusicRecord == null)
            {
                return NotFound();
            }

            var musicRecord = await _context.MusicRecord.Include(rm => rm.RecordMembers)
                                                        .ThenInclude(m => m.Musician)
                                                        .FirstOrDefaultAsync(m => m.Id == id);
            if (musicRecord == null)
            {
                return NotFound();
            }

            return View(musicRecord);
        }

        // GET: MusicRecords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MusicRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Artist,Year,Genre")] CreateMusicRecordDto musicRecordDto)
        {
         
            if (ModelState.IsValid)
            {
                var musicRecord = _mapper.Map<MusicRecord>(musicRecordDto);
                _context.Add(musicRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(musicRecordDto);
        }

        // GET: MusicRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MusicRecord == null)
            {
                return NotFound();
            }

            var musicRecord = await _context.MusicRecord.FindAsync(id);
            if (musicRecord == null)
            {
                return NotFound();
            }
            return View(musicRecord);
        }

        // POST: MusicRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Artist,Year,Genre")] EditMusicRecordDto musicRecordDto)
        {
            if (id != musicRecordDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var musicRecord = _mapper.Map<MusicRecord>(musicRecordDto);
                    _context.Update(musicRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicRecordExists(musicRecordDto.Id))
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
            return View(musicRecordDto);
        }

        // GET: MusicRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MusicRecord == null)
            {
                return NotFound();
            }

            var musicRecord = await _context.MusicRecord
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musicRecord == null)
            {
                return NotFound();
            }

            return View(musicRecord);
        }

        // POST: MusicRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MusicRecord == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MusicRecord'  is null.");
            }
            var musicRecord = await _context.MusicRecord.FindAsync(id);
            if (musicRecord != null)
            {
                _context.MusicRecord.Remove(musicRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicRecordExists(int id)
        {
            return (_context.MusicRecord?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
