using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcSongs.Models
{
    public class SongsGenreViewModel
    {
        public List<Songs>? music { get; set; }
        public SelectList? Genres { get; set; }
        public string? SongsGenre { get; set; }
        public string? SearchString { get; set; }
        public string? Title { get; set; }

    }
}