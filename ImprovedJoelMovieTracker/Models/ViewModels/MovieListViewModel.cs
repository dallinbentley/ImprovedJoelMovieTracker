using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprovedJoelMovieTracker.Models
{
    public class MovieListViewModel
    {
        public IEnumerable<Movie> movies { get; set; }

        public int MovieId { get; set; }
    }
}
