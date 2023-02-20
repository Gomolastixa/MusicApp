using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicApp.Data;
using MusicApp.Models;
using MusicApp.Services;

namespace MusicApp.Controllers
{
    public class MusicRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMusicRecordInterface _musicRecordInterface;
        private readonly IMusicianInterface _musicianInterface;
        private readonly IMapper _mapper;

        public MusicRecordsController(ApplicationDbContext context, IMusicRecordInterface musicRecordInterface, IMusicianInterface musicianInterface, IMapper mapper)
        {
            _context = context;
            _musicRecordInterface = musicRecordInterface;
            _musicianInterface = musicianInterface;
            _mapper = mapper;
        }

        // GET: MusicRecords
        public async Task<IActionResult> Index([FromQuery(Name = "q")] string searchText, string orderBy, string orderDirection)
        {
            ViewBag.SearchText = searchText;
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


        // GET: MusicRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicRecord = await _musicRecordInterface.GetByIdAsync(id);

            if (musicRecord == null)
            {
                return NotFound();
            }

            return View(musicRecord);
        }

        // GET: MusicRecords/RecordAdd
        public IActionResult RecordAdd(int id)
        {

            return View();
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
        public async Task<IActionResult> Create([Bind("Name,Artist,Year,Genre")] CreateMusicRecordDto musicRecordDto)
        {

            if (ModelState.IsValid)
            {
                var musicRecord = _mapper.Map<MusicRecord>(musicRecordDto);
                await _musicRecordInterface.AddAsync(musicRecord);

                TempData["AlertMessage"] = "Record created successfully!";

                return RedirectToAction(nameof(Index));
            }

            return View(musicRecordDto);
        }

        //POST: MusicRecords/AddRecordMember
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecordMember(int id, CreateMusicianDto musicianDto)
        {
            if (await _musicianInterface.GetByNameAsync(musicianDto.FullName) == null)
            {
                var newMusician = _mapper.Map<Musician>(musicianDto);
                await _musicianInterface.AddAsync(newMusician);
            }

            var musician = await _musicianInterface.GetByNameAsync(musicianDto.FullName);


            if (await _context.RecordMembers.FirstOrDefaultAsync(rm => rm.MusicRecordId == id && rm.MusicianId == musician.Id) == null)
            {
                var recordMember = new RecordMember { MusicRecordId = id, MusicianId = musician.Id };

                _context.Add(recordMember);
                await _context.SaveChangesAsync();

            }

            var musicRecord = await _musicRecordInterface.GetByIdAsync(id);

            return RedirectToAction("Details", musicRecord);
        }

        public async Task<IActionResult> DeleteMusician(int id)
        {

            var recordMemberToDelete = await _context.RecordMembers.FirstOrDefaultAsync(rm => rm.Id == id);


            if (recordMemberToDelete != null)
            {
                _context.RecordMembers.Remove(recordMemberToDelete);
                await _context.SaveChangesAsync();
            }

            var musicRecord = await _musicRecordInterface.GetByIdAsync(recordMemberToDelete.MusicRecordId);

            TempData["AlertMessage"] = "Member deleted successfully!";

            return RedirectToAction("Details", musicRecord);

        }

        // GET: MusicRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicRecord = await _musicRecordInterface.GetByIdAsync(id);


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
                    await _musicRecordInterface.UpdateAsync(musicRecord);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await MusicRecordExists(musicRecordDto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["AlertMessage"] = "Record updated successfully!";

                return RedirectToAction(nameof(Index));
            }
            return View(musicRecordDto);
        }

        // GET: MusicRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicRecord = await _musicRecordInterface.GetByIdAsync(id);

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
            //if (_context.MusicRecord == null)
            //{
            //    return Problem("Entity set 'ApplicationDbContext.MusicRecord'  is null.");
            //}
           
            await _musicRecordInterface.DeleteAsync(id);

            TempData["AlertMessage"] = "Record deleted successfully!";

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MusicRecordExists(int id)
        {
           return await _musicRecordInterface.Exists(id);
        }
    }
}
