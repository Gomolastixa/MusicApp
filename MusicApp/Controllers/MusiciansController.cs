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
using MusicApp.Interfaces;
using MusicApp.Models;
using MusicApp.Services;

namespace MusicApp.Controllers
{
    public class MusiciansController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMusicRecordInterface _musicRecordInterface;
        private readonly IMusicianInterface _musicianInterface;
        private readonly IMapper _mapper;

        public MusiciansController(ApplicationDbContext context, IMusicRecordInterface musicRecordInterface, IMusicianInterface musicianInterface, IMapper mapper)
        {
            _context = context;
            _musicRecordInterface = musicRecordInterface;
            _musicianInterface = musicianInterface;
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
            if (id == null)
            {
                return NotFound();
            }

            var musician = await _musicianInterface.GetByIdAsync(id);

            if (musician == null)
            {
                return NotFound();
            }

            return View(musician);
        }

        // GET: Musicians/MemberAdd
        public IActionResult MemberAdd(int id)
        {

            return View();
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
        public async Task<IActionResult> Create([Bind("FullName,Instrument")] CreateMusicianDto musicianDto)
        {
            if (ModelState.IsValid)
            {
                var musician = _mapper.Map<Musician>(musicianDto);
                await _musicianInterface.AddAsync(musician);

                TempData["AlertMessage"] = "Musician created successfully!";

                return RedirectToAction(nameof(Index));
            }
            return View(musicianDto);
        }

        //POST: Musicians/AddRecordPlayed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecordPlayed(int id, CreateMusicRecordDto musicRecordDto)
        {
            if (await _musicRecordInterface.GetByNameAsync(musicRecordDto.Name) == null)
            {
                var newMusicRecord = _mapper.Map<MusicRecord>(musicRecordDto);
                await _musicRecordInterface.AddAsync(newMusicRecord);
            }

            var musicRecord = await _musicRecordInterface.GetByNameAsync(musicRecordDto.Name);

            if (await _context.RecordMembers.FirstOrDefaultAsync(rm => rm.MusicRecordId == musicRecord.Id && rm.MusicianId == id) == null)
            {
                var recordMember = new RecordMember { MusicRecordId = musicRecord.Id, MusicianId = id };

                _context.Add(recordMember);
                await _context.SaveChangesAsync();
            }


            var musician = await _musicianInterface.GetByIdAsync(id);

            return RedirectToAction("Details", musician);
        }

        // GET: Musicians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musician = await _musicianInterface.GetByIdAsync(id);

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
                    await _musicianInterface.UpdateAsync(musician);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await MusicianExists(musicianDto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["AlertMessage"] = "Musician updated successfully!";


                return RedirectToAction(nameof(Index));
            }
            return View(musicianDto);
        }


        public async Task<IActionResult> DeleteRecord(int id)
        {
            var recordMemberToDelete = await _context.RecordMembers.FirstOrDefaultAsync(rm => rm.Id == id);


            if (recordMemberToDelete != null)
            {
                _context.RecordMembers.Remove(recordMemberToDelete);
                await _context.SaveChangesAsync();
            }

            var musician = await _musicianInterface.GetByIdAsync(recordMemberToDelete.MusicianId);

            TempData["AlertMessage"] = "Record deleted successfully!";

            return RedirectToAction("Details", musician);

        }

        // GET: Musicians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musician = await _musicianInterface.GetByIdAsync(id);

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
            //if (_context.Musicians == null)
            //{
            //    return Problem("Entity set 'ApplicationDbContext.Musicians'  is null.");
            //}

            await _musicianInterface.DeleteAsync(id);

            TempData["AlertMessage"] = "Musician deleted successfully!";


            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MusicianExists(int id)
        {
            return await _musicianInterface.Exists(id);
        }
    }
}
