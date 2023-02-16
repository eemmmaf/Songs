using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Songs.Data;
using Songs.Models;

namespace Songs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongModelsController : ControllerBase
    {
        private readonly SongContext _context;

        public SongModelsController(SongContext context)
        {
            _context = context;
        }

        // GET: api/SongModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongModel>>> GetSongs()
        {
          if (_context.Songs == null)
          {
              return NotFound();
          }
            return await _context.Songs.ToListAsync();
        }

        // GET: api/SongModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SongModel>> GetSongModel(int id)
        {
          if (_context.Songs == null)
          {
              return NotFound();
          }
            var songModel = await _context.Songs.FindAsync(id);

            if (songModel == null)
            {
                return NotFound();
            }

            return songModel;
        }

        // PUT: api/SongModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSongModel(int id, SongModel songModel)
        {
            if (id != songModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(songModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SongModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SongModel>> PostSongModel(SongModel songModel)
        {
          if (_context.Songs == null)
          {
              return Problem("Entity set 'SongContext.Songs'  is null.");
          }
            _context.Songs.Add(songModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSongModel", new { id = songModel.Id }, songModel);
        }

        // DELETE: api/SongModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSongModel(int id)
        {
            if (_context.Songs == null)
            {
                return NotFound();
            }
            var songModel = await _context.Songs.FindAsync(id);
            if (songModel == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(songModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SongModelExists(int id)
        {
            return (_context.Songs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
