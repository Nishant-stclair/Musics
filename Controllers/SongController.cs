using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcSongs.Models;

namespace MvcSongs.Controllers
{
    public class SongController : Controller
    {
        private readonly MvcSongsContext _context;

        public SongController(MvcSongsContext context)
        {
            _context = context;
        }

        // GET: Song
        public async Task<IActionResult> Index()
        {
            var genreQuery = _context.Songs.Select(s => s.Genre).Distinct();
            var viewModel = new SongsGenreViewModel
            {
                Genres = new SelectList(await genreQuery.ToListAsync()),
                music = await _context.Songs.Where(s => !s.IsHidden).ToListAsync()
            };

            return View(viewModel);
        }

        // GET: Song/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var songs = await _context.Songs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (songs == null)
            {
                return View("NotFound", id);
            }

            return View(songs);
        }

        // GET: Song/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Song/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,SingerName,Production,Rating")] Songs songs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(songs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(songs);
        }

        // GET: Song/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songs = await _context.Songs.FindAsync(id);
            if (songs == null)
            {
                return View("NotFound", id);

            }
            return View(songs);
        }

        // POST: Song/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,SingerName,Production,Rating")] Songs songs)
        {
            if (id != songs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(songs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongsExists(songs.Id))
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
            return View(songs);
        }

        // GET: Song/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songs = await _context.Songs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (songs == null)
            {
                return View("NotFound", id);

            }

            return View(songs);
        }

        // POST: Song/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var songs = await _context.Songs.FindAsync(id);
            _context.Songs.Remove(songs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongsExists(int id)
        {
            return _context.Songs.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Search(string searchMusic, string searchDate, string searchPrice, string SongsGenre)
        {

            var music = from m in _context.Songs
                        select m;

            if (!string.IsNullOrEmpty(searchMusic))
            {
                music = music.Where(s => s.Title!.ToLower().Contains(searchMusic.ToLower()));
            }
            ViewData["SearchTitle"] = searchMusic;

            if (!string.IsNullOrEmpty(searchDate) && DateTime.TryParse(searchDate, out DateTime dateFilter))
            {
                music = music.Where(s => s.ReleaseDate.Date == dateFilter.Date);
            }

            if (!string.IsNullOrEmpty(searchPrice) && decimal.TryParse(searchPrice, out decimal priceFilter))
            {
                music = music.Where(s => s.Price == priceFilter);
            }

            if (!string.IsNullOrEmpty(SongsGenre))
            {
                music = music.Where(x => x.Genre == SongsGenre);
            }

            ViewData["SearchGenre"] = SongsGenre;



            var SongGenreVM = new SongsGenreViewModel
            {
               
                music = await music.ToListAsync()
            };

            return View("Index", SongGenreVM);



        }

        public async Task<IActionResult> DeleteAll()
        {
            return View();
        }
        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAll()
        {
            var allSongs = _context.Songs.ToList();
            if (allSongs.Count > 0)
            {
                _context.Songs.RemoveRange(allSongs);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> DeleteHidden()
        {
            var hiddenSongs = await _context.Songs.Where(s => s.IsHidden).ToListAsync();

            foreach (var song in hiddenSongs)
            {
                song.IsHidden = false;
            }

            await _context.SaveChangesAsync();


            return RedirectToAction("HiddenRecords");
        }

        public async Task<IActionResult> Hidden(int[] selectedSongs)
        {
            foreach (var songId in selectedSongs)
            {
                var song = await _context.Songs.FindAsync(songId);
                if (song != null)
                {
                    song.IsHidden = !song.IsHidden;
                    _context.Update(song);
                }
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> HiddenRecords()
        {
            var viewModel = new SongsGenreViewModel
            {
                music = await _context.Songs.Where(s => s.IsHidden).ToListAsync()
            };

            return View(viewModel);
        }



        

    }
}
