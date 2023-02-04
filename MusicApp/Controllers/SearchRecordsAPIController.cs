using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApp.Data;
using MusicApp.Models;

namespace MusicApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchRecordsAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SearchRecordsAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET : api/SearchRecordsAPI
        [HttpGet("search")]

        public async Task<ActionResult<IEnumerable<MusicRecord>>> Search([FromQuery(Name = "term")] string searchText, string? orderBy, string? orderDirection)
        {
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
                case nameof(MusicRecord.Genre):
                    query = desc ? query.OrderByDescending(x => x.Genre) : query.OrderBy(x => x.Genre);
                    break;
                default:
                    query = query.OrderByDescending(x => x.Year);
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

            return await query.ToListAsync();

        }


        //// GET: api/SearchRecordsAPI
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<MusicRecord>>> GetMusicRecord()
        //{
        //    if (_context.MusicRecord == null)
        //    {
        //        return NotFound();
        //    }
        //    return await _context.MusicRecord.ToListAsync();
        //}



        //// GET: api/SearchRecordsAPI/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<MusicRecord>> GetMusicRecord(int id)
        //{
        //  if (_context.MusicRecord == null)
        //  {
        //      return NotFound();
        //  }
        //    var musicRecord = await _context.MusicRecord.FindAsync(id);

        //    if (musicRecord == null)
        //    {
        //        return NotFound();
        //    }

        //    return musicRecord;
        //}

        //// PUT: api/SearchRecordsAPI/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMusicRecord(int id, MusicRecord musicRecord)
        //{
        //    if (id != musicRecord.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(musicRecord).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MusicRecordExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/SearchRecordsAPI
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<MusicRecord>> PostMusicRecord(MusicRecord musicRecord)
        //{
        //  if (_context.MusicRecord == null)
        //  {
        //      return Problem("Entity set 'ApplicationDbContext.MusicRecord'  is null.");
        //  }
        //    _context.MusicRecord.Add(musicRecord);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetMusicRecord", new { id = musicRecord.Id }, musicRecord);
        //}

        //// DELETE: api/SearchRecordsAPI/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMusicRecord(int id)
        //{
        //    if (_context.MusicRecord == null)
        //    {
        //        return NotFound();
        //    }
        //    var musicRecord = await _context.MusicRecord.FindAsync(id);
        //    if (musicRecord == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.MusicRecord.Remove(musicRecord);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool MusicRecordExists(int id)
        //{
        //    return (_context.MusicRecord?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
