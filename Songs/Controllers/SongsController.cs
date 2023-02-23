﻿using System;
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
    public class SongsController : ControllerBase
    {
        private readonly SongContext _context;

        public SongsController(SongContext context)
        {
            _context = context;
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            if (_context.Songs == null)
            {
                return NotFound();
            }

            //Hämtar alla och inkluderar kategori
            return await _context.Songs.Include(e => e.Category).ToListAsync();

        }

        // GET: api/Songs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(int id)
        {
            if (_context.Songs == null)
            {
                return NotFound();
            }

            //Inkluderar kategori och id
            var song = await _context.Songs
           .Include(s => s.Category)
           .AsNoTracking()
           .FirstOrDefaultAsync(m => m.Id == id);

            if (song == null)
            {
                //Skriver ut felmeddelande om låten inte hittas
                return NotFound("Låten kunde inte hittas");
            }


            return song;
        }

        // PUT: api/Songs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, Song song)
        {
            if (id != song.Id)
            {
                return BadRequest();
            }

            _context.Entry(song).State = EntityState.Modified;

            try
            {

                //Hämtar kategori-id
                var category = await _context.Categories.FindAsync(song.CategoryId);

                //Om kategorin inte finns returneras felmeddelande
                if (category == null)
                {
                    return BadRequest("Kategorin som har uppgetts finns inte");
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
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

        // POST: api/Songs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
            //Hämtar kategori-id
            var category = await _context.Categories.FindAsync(song.CategoryId);

            //Om kategorin inte finns returneras felmeddelande
            if (category == null)
            {
                return BadRequest("Kategorin som har uppgetts finns inte");
            }

            song.Category = category;

            //Sparar låten
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            //Returnerar det som skapats
            return CreatedAtAction(nameof(GetSong), new { id = song.Id }, song);
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            if (_context.Songs == null)
            {
                return NotFound();
            }
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SongExists(int id)
        {
            return (_context.Songs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
